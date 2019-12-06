Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports MyIrwan.Encryption

Public Class frmActivation
    Dim secretKey As String = "CV"
    Private Sub frmActivation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Get processor id.
        Try
            Dim i As System.Management.ManagementObject
            Dim searchInfo_Processor As New System.Management.ManagementObjectSearcher("Select * from Win32_Processor")
            For Each i In searchInfo_Processor.Get()
                txtHardwareID.Text = i("ProcessorID").ToString
            Next
            Dim searchInfo_Board As New System.Management.ManagementObjectSearcher("Select * from Win32_BaseBoard")
            For Each i In searchInfo_Board.Get()
                txtSerialNo.Text = i("SerialNumber").ToString
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error!")
            End
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Genrate processor id on client system again for cross verifying the key.
        'secrete key value must be same on both system.
        Try
            Cursor = Cursors.WaitCursor
            Timer1.Enabled = True

            If txtActivationID.Text = "" Then
                MessageBox.Show("Please enter activation id", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtActivationID.Focus()
                Exit Sub
            End If

            Dim st As String = (txtHardwareID.Text) + secretKey
            TextBox1.Text = ClientEncryption.MakePassword(st, 659)
            'Check the sysetm genrated key and provided key
            If txtActivationID.Text = TextBox1.Text Then

                If MsgBox("License file will be created with given Activation Key., Do you want to proceed?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
                    'Create licenced file in software nstalltion directory.
                    'to stored the license file to another location use bellow commented code.                    

                    Using sw As StreamWriter = New StreamWriter(Application.StartupPath & "\LicenseKey.dat")
                        sw.WriteLine(ClientEncryption.Encrypt(txtActivationID.Text.Trim()))
                        sw.Close()
                    End Using
                    MessageBox.Show("Software Activated successfully..." & vbCrLf & "", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Login.Show()
                    Me.Hide()
                Else
                    End
                End If

            Else
                Cursor = Cursors.Default
                Timer1.Enabled = False

                MessageBox.Show("Invalid activation id...Please contact software provider for buying full licence" & vbCrLf & "Contact us at :" & vbCrLf & "Coding Visions Infotech Pvt. Ltd." & vbCrLf & "Email-Contact@codingvisions.com" & vbCrLf & "Mobile No. +91 8308075524", "Activation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            Cursor = Cursors.Default
            Timer1.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        End
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Clipboard.Clear()    'Clear if any old value is there in Clipboard        
        Clipboard.SetText(txtHardwareID.Text) 'Copy text to Clipboard
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        txtActivationID.Text = Clipboard.GetText() 'Get text from Clipboard
    End Sub
End Class
