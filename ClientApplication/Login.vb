Public Class Login
    Private Sub Login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillCombo()
    End Sub

    Public Sub fillCombo()

        'Fill usernames in combobox
        'Username : Admin
        'Password : admin
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("Admin")
        ComboBox1.Focus()
    End Sub

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        Dim er As Integer = 0
        If ComboBox1.Text = Nothing Then
            er = 1
            MsgBox("Please select username.")
        ElseIf TextBox1.Text = Nothing Then
            er = 1
            MsgBox("Please enter password.")
        End If

        If er = 0 Then
            Try
                Dim ctr As Integer
                If ComboBox1.Text = "Admin" And TextBox1.Text = "admin" Then
                    ctr = 1
                Else
                    ctr = 0
                End If

                If ctr > 0 Then
                    Form1.Show()
                    Me.Hide()
                Else
                    MsgBox("Password was incorrect.", MsgBoxStyle.Critical)
                End If
            Catch ex As Exception
                MsgBox("Username or Password was incorrect.", MsgBoxStyle.Critical)
            End Try
        End If

        er = 0
    End Sub

End Class
