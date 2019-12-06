Imports System.ComponentModel
Imports MyIrwan.Encryption

Public NotInheritable Class Loading
    Dim bw As BackgroundWorker = New BackgroundWorker
    Dim secretKey As String = "CV"
    Dim LicensePath As String = Application.StartupPath & "\LicenseKey.dat"

    Sub New()
        InitializeComponent()
        ' This call is required by the designer.
        Application.EnableVisualStyles()

        bw.WorkerSupportsCancellation = True
        bw.WorkerReportsProgress = True
        AddHandler bw.DoWork, AddressOf bw_DoWork
        AddHandler bw.ProgressChanged, AddressOf bw_ProgressChanged
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        For i = 1 To 1
            If bw.CancellationPending = True Then
                e.Cancel = True
                Exit For
            Else
                Try

                Catch ex As Exception

                End Try
                ' Perform a time consuming operation and report progress.
                System.Threading.Thread.Sleep(3000)
                bw.ReportProgress(i * 100)
            End If
        Next
    End Sub

    Private Sub bw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        'Me.ProgressBar1.Value = e.ProgressPercentage.ToString()
    End Sub

    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If e.Cancelled = True Then
            Me.Label1.Text = "Canceled!"
        ElseIf e.Error IsNot Nothing Then
            Me.Label1.Text = "Error: " & e.Error.Message
        Else
            Me.Label1.Text = "Done..!"

            'Check For license string.
            If System.IO.File.Exists(LicensePath) Then
                Dim ke As String
                ke = ClientEncryption.keys(LicensePath)

                Dim i As System.Management.ManagementObject
                Dim searchInfo_Processor As New System.Management.ManagementObjectSearcher("Select * from Win32_Processor")
                For Each i In searchInfo_Processor.Get()
                    txtHardwareID.Text = i("ProcessorID").ToString
                Next
                Dim searchInfo_Board As New System.Management.ManagementObjectSearcher("Select * from Win32_BaseBoard")
                For Each i In searchInfo_Board.Get()
                    txtSerialNo.Text = i("SerialNumber").ToString
                Next

                Dim st As String = (txtHardwareID.Text) + secretKey
                TextBox1.Text = ClientEncryption.MakePassword(st, 659)

                'Decrypt the license key and check for correctness if stored key and genrated key matches then software will proceed to login.
                txtActivationID.Text = ClientEncryption.Decrypt(ke)

                If txtActivationID.Text = TextBox1.Text Then
                    Login.Show()
                    Me.Hide()
                Else
                    'If there is no license string.
                    frmActivation.Show()
                    Me.Hide()
                End If

            Else

                'If there is no license string.
                frmActivation.Show()
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub Loading_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
handler:
        If Not bw.IsBusy = True Then
            bw.RunWorkerAsync()
        End If
    End Sub

End Class
