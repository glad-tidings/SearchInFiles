Imports System.IO

Public Class Form1

    Dim intNum As Integer = 0
    Dim s1, s2 As String

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        FolderBrowserDialog1.ShowDialog()
        TextBox1.Text = FolderBrowserDialog1.SelectedPath
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        intNum = 0
        lvDir.Items.Clear()
        Button2.Enabled = False
        DisplayFile(TextBox1.Text)
        Button2.Enabled = True
        ToolStripStatusLabel1.Text = lvDir.Items.Count & " File Found"
    End Sub

    Private Sub DisplayFile(ByVal strDir As String)
        Dim reader As System.IO.StreamReader
        Dim df As DirectoryInfo = New DirectoryInfo(strDir)
        Dim fi As FileInfo() = df.GetFiles(ComboBox1.Text)
        Dim lvi As ListViewItem
        For i As Integer = 0 To (fi.GetLength(0) - 1)
            s1 = "" : s2 = ""
            s1 = fi(i).FullName
            ToolStripStatusLabel1.Text = s1
            reader = New System.IO.StreamReader(s1)
            s2 = reader.ReadToEnd
            If InStr(s2, TextBox2.Text) <> 0 Then
                intNum += 1
                lvi = lvDir.Items.Add(intNum.ToString())
                lvi.SubItems.Add(s1)
                lvi.SubItems.Add((fi(i).Length).ToString())
            End If
            reader.Close()
            Application.DoEvents()
        Next

        Dim di() As DirectoryInfo = df.GetDirectories
        For j As Integer = 0 To (di.GetLength(0) - 1)
            DisplayFile(di(j).FullName)
        Next

    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Form1_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        FileName.Width = Me.Width - 168
    End Sub

    Private Sub lvDir_DoubleClick(sender As Object, e As System.EventArgs) Handles lvDir.DoubleClick
        If lvDir.SelectedItems.Count <> 0 Then
            Shell("notepad.exe " & lvDir.SelectedItems(0).SubItems(1).Text, AppWinStyle.NormalFocus)
        End If
    End Sub
End Class
