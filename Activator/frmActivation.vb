Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports MyIrwan.Encryption

Public Class frmActivation
    Dim secretKey As String = "CV"
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If txtHardwareID.Text = "" Then
                MessageBox.Show("Please enter hardware id", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtHardwareID.Focus()
                Exit Sub
            End If
            Dim st As String = (txtHardwareID.Text) + (secretKey)
            'Genrate encripted activation key
            txtActivationID.Text = ActivatorEncryption.MakePassword(st, 659)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmActivation_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub

    Private Sub frmActivation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        If txtActivationID.Text <> "" Then
            Clipboard.Clear()    'Clear if any old value is there in Clipboard        
            Clipboard.SetText(txtActivationID.Text) 'Copy text to Clipboard
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        txtHardwareID.Text = Clipboard.GetText() 'Get text from Clipboard
    End Sub

    Private Sub Label1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label1.Click

    End Sub
End Class
