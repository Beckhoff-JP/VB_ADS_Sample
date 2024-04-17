Imports System.Buffers.Binary
Imports TwinCAT.Ads
Imports TwinCAT.TypeSystem


Public Class Display
    Private client As TwinCAT.Ads.AdsClient
    Private num_result As UInteger
    Private in1_result As UInteger
    Private in2_result As UInteger
    Private in3_result As UInteger
    Private in4_result As UInteger
    Private str1_handle As UInteger
    Private str_result As UInteger
    Private button2_status As Boolean
    Private binReader As System.IO.BinaryReader
    Private converter As PrimitiveTypeMarshaler ' this type is defined by TwincAT.TypeSystem


    ''' <summary>
    ''' Form起動時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Diplay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' ADS通信クライアントオブジェクト作成
        client = New AdsClient()

        ' Notificationイベントハンドラの追加
        AddHandler client.AdsNotification, AddressOf Client_AdsNotification2

        ' 文字型変数のバイト列を.NET環境のString型に変換するオブジェクト生成
        converter = New PrimitiveTypeMarshaler(StringMarshaler.DefaultEncoding)

        ' ターゲットPLCへの接続
        client.Connect("127.0.0.1.1.1", 851)

        ' Notification用設定オブジェクトの作成
        ' Check for change every 100 ms
        Dim notification_setting = New NotificationSettings(AdsTransMode.OnChange, 100, 0)

        Try
            ''' 変数の値変更イベント登録
            ''' see https://infosys.beckhoff.com/content/1033/tc3_ads.net/9408448267.html?id=6017818919540761304
            in1_result = client.AddDeviceNotification("MAIN.test_input[0]", 2, notification_setting, Nothing)
            in2_result = client.AddDeviceNotification("MAIN.test_input[1]", 2, notification_setting, Nothing)
            num_result = client.AddDeviceNotification("MAIN.test_value", 4, notification_setting, Nothing)

            ''' 文字列処理に関するインスタンス設定
            ''' 入力BOXとテキスト表示を連動させるため、次の3点の処理が必要。
            ''' 1. 入力BOXの初期値を設定するためADSのReadValueによりテキストを読み出してString型へ変換
            ''' 2. 入力BOXから入力された文字列をADSのWriteValueにより書込み
            ''' 3. Notificationにより変数の文字列が変化したらテキスト表示部にその文字を反映
            ''' 以上を使うことにより、Formからの文字入力、PLC側の制御で得た文字情報のどちらからもフォームへ反映させることができる。
            ''' see https://infosys.beckhoff.com/content/1033/tc3_ads.net/9407532811.html?id=4153935062066595398
            ' 変数の値変更イベント登録
            str_result = client.AddDeviceNotification("MAIN.test_string", 255, notification_setting, Nothing)
            ' 変数へ値書込み用のハンドル定義
            str1_handle = client.CreateVariableHandle("MAIN.test_string")
            ' PLC文字型変数の現在値をForm上の入力BOX初期値として設定
            TextBox1.Text = GetCurrentString(str1_handle, 64)


        Catch ex As TwinCAT.Ads.AdsErrorException
            MessageBox.Show(ex.Message & Environment.NewLine & ex.HelpLink)
            Application.Exit()
        End Try


    End Sub

    ' ADS Notificationによるイベント処理スレッドからFormコントロールスレッドへ代理実行してもらうためのデリゲートを定義
    Delegate Sub UpdateDisplayDelegate(e As AdsNotificationEventArgs)

    ''' <summary>
    ''' ADS Notificationによる イベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e">イベントハンドラ引数</param>
    Private Sub Client_AdsNotification2(sender As Object, e As AdsNotificationEventArgs)
        Try
            Select Case e.Handle
                ' 登録したイベント種別ごとに定義した処理をFormスレッドに実行を依頼する
                Case num_result
                    Invoke(New UpdateDisplayDelegate(AddressOf SetCounter), e)
                Case in1_result
                    Invoke(New UpdateDisplayDelegate(AddressOf SetIn1), e)
                Case in2_result
                    Invoke(New UpdateDisplayDelegate(AddressOf SetIn2), e)
                Case str_result
                    Invoke(New UpdateDisplayDelegate(AddressOf SetString), e)
            End Select
        Catch ex As System.ObjectDisposedException
            ' フォームを閉じた時、Delegate objectがDisposeされたためinvokeに失敗する例外を握り潰す
        End Try

    End Sub

    ''' <summary>
    ''' "MAIN.test_value" : DINT の値変更イベントハンドラ
    ''' </summary>
    ''' <param name="e">イベントハンドラ引数</param>
    Sub SetCounter(e As AdsNotificationEventArgs)
        ' ネイティブバイトオーダへ変換
        Dim nCounter As UInteger = BinaryPrimitives.ReadUInt32LittleEndian(e.Data.Span)
        indicator_in_n1.Text = nCounter.ToString()
    End Sub

    ''' <summary>
    ''' "MAIN.test_input[0]" : BOOL の値変更イベントハンドラ
    ''' Formのindicator_in_0のテキストに表示反映する
    ''' </summary>
    ''' <param name="e">イベントハンドラ引数</param>
    Sub SetIn1(e As AdsNotificationEventArgs)
        ' ネイティブバイトオーダへ変換
        Dim input As UShort = BinaryPrimitives.ReadUInt16LittleEndian(e.Data.Span)
        ' Byteサイズに切り詰める
        input = input And &HF
        If input > 0 Then
            indicator_in_0.Text = "ON"
            indicator_in_0.BackColor = Color.Yellow
        Else
            indicator_in_0.Text = "OFF"
            indicator_in_0.BackColor = Nothing
        End If
    End Sub

    ''' <summary>
    ''' "MAIN.test_input[1]" : BOOL の値変更イベントハンドラ
    ''' Formのindicator_in_1のテキストに表示反映する
    ''' </summary>
    ''' <param name="e">イベントハンドラ引数</param>
    Sub SetIn2(e As AdsNotificationEventArgs)
        ' ネイティブバイトオーダへ変換
        Dim input As UShort = BinaryPrimitives.ReadUInt16LittleEndian(e.Data.Span)
        ' Byteサイズに切り詰める
        input = input And &HF
        If input > 0 Then
            indicator_in_1.Text = "ON"
            indicator_in_1.BackColor = Color.Yellow
        Else
            indicator_in_1.Text = "OFF"
            indicator_in_1.BackColor = Nothing
        End If
    End Sub

    ''' <summary>
    ''' MAIN.test_string" : STRING(64) の値変更イベントハンドラ
    ''' Formのindicator_in_stringへテキストを反映する
    ''' </summary>
    ''' <param name="e">イベントハンドラ引数</param>
    Sub SetString(e As AdsNotificationEventArgs)
        Dim read_text As String
        'バイト配列を文字型へ変換して
        converter.Unmarshal(e.Data.Span, read_text)
        indicator_in_string.Text = read_text
        TextBox1.Text = read_text
    End Sub

    ''' <summary>
    ''' PLCの文字列型変数のテキストを読み取る
    ''' </summary>
    ''' <param name="TcHandle">文字列変数のハンドラ</param>
    ''' <param name="TextSize">TwinCATの文字列変数のサイズn STRING(n) </param>
    ''' <returns>読みだしたテキスト</returns>
    Private Function GetCurrentString(TcHandle As UInteger, TextSize As UInteger) As String
        Dim Buffer As Byte() = New Byte(TextSize) {}
        Dim readBytes As Integer = client.Read(TcHandle, Buffer.AsMemory())
        If readBytes > 0 Then
            converter.Unmarshal(Buffer.AsSpan(), GetCurrentString)
        End If
    End Function

    ''' <summary>
    ''' テキスト入力BOXのテキストが変更されたイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim TextSize As UInteger = 64
        If Len(sender.Text) <= TextSize Then
            Dim writeBuffer As Byte() = New Byte(TextSize) {}
            converter.Marshal(sender.Text, writeBuffer.AsSpan())
            client.Write(str1_handle, writeBuffer.AsMemory())
        End If

    End Sub

    ''' <summary>
    ''' フォームを閉じる際のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Display_Closing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closing
        client.DeleteDeviceNotification(num_result)
        client.DeleteDeviceNotification(in1_result)
        client.DeleteDeviceNotification(in2_result)

        If client IsNot Nothing Then
            RemoveHandler client.AdsNotification, AddressOf Client_AdsNotification2
        End If
    End Sub

    ' Outputs

    ''' <summary>
    ''' モメンタリボタンの押した時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button1_MouseDown(sender As Object, e As EventArgs) Handles Button1.MouseDown
        client.WriteValue("MAIN.test_output[0]", True)
        Button1.BackColor = Color.Yellow
    End Sub

    ''' <summary>
    ''' モメンタリボタンの離した時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button1_MouseUp(sender As Object, e As EventArgs) Handles Button1.MouseUp
        client.WriteValue("MAIN.test_output[0]", False)
        Button1.BackColor = Nothing
    End Sub

    ''' <summary>
    ''' オルタネートボタンのクリック時のイベントハンドラ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If button2_status Then
            button2_status = False
            Button2.BackColor = Nothing
        Else
            button2_status = True
            Button2.BackColor = Color.Yellow
        End If
        client.WriteValue("MAIN.test_output[1]", button2_status)
    End Sub

End Class
