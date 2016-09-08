Imports System.Xml
Imports System.Text

Public Class Form1

    Public Function Base64Decode(ByVal Texte As String) As String
        Try
            If Texte.Length = 0 Then
                Return ""
            Else
                Return Encoding.ASCII.GetString(Convert.FromBase64String(Texte))
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim FileZillaPath As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\FileZilla\"

        If Not IO.Directory.Exists(FileZillaPath) Then
            MessageBox.Show("No file !", "Error... !", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim RecentServers As String = FileZillaPath & "recentservers.xml"

        If IO.File.Exists(RecentServers) Then
            Dim xmlRecentServers As New XmlDocument
            xmlRecentServers.Load(RecentServers)

            Dim RecentServersList As XmlNodeList = xmlRecentServers.GetElementsByTagName("RecentServers")
            Dim Servers As XmlNodeList = DirectCast(RecentServersList(0), XmlElement).GetElementsByTagName("Server")

            For Each Server As XmlElement In Servers

                Dim Host As String = Nothing
                Dim User As String = Nothing
                Dim Pass As String = Nothing
                Dim Port As String = Nothing

                Host = Server.GetElementsByTagName("Host")(0).InnerText
                User = Server.GetElementsByTagName("User")(0).InnerText
                Pass = Base64Decode(Server.GetElementsByTagName("Pass")(0).InnerText)
                Port = Server.GetElementsByTagName("Port")(0).InnerText

                Dim item As New ListViewItem
                item.Text = Host
                item.SubItems.Add(User)
                item.SubItems.Add(Pass)
                item.SubItems.Add(Port)

                ListView1.Items.Add(item)
            Next
        End If
        ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub

End Class
