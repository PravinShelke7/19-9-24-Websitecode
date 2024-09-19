Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Imaging
Imports System.Net.Mail
Imports System.Net

Partial Class InteractiveServices_SAModule
    Inherits System.Web.UI.Page
    

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        Session("MenuItem") = "MCB"        
    End Sub
    
#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_STUDIES_MULTICLIENTSTUDY")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ObjCrypto As New CryptoHelper()
        Try
            Session("MenuItem") = "SAISERVICE"
            GetInventoryDetails()
            If Session("UserId") <> Nothing Then
                hdnUserId.Value = Session("UserId")
            End If
            'Set Report Id
            hdnRepId.Value = ObjCrypto.Encrypt("SA")
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetInventoryDetails()
        Dim objGetData As New InteractiveGetData.Selectdata
        Dim dsInventory As New DataSet
        Dim i As Integer = 0
        Dim var1, var2, var3, var4, var5, var6 As String
        Try
            dsInventory = objGetData.GetInventoryDetails("SA")
            If dsInventory.Tables(0).Rows.Count > 0 Then
                var1 = Convert.ToDateTime(dsInventory.Tables(0).Rows(0).Item("PUBdate").ToString()).Date()

                'Getting Study Number
                var2 = dsInventory.Tables(0).Rows(0).Item("PartID").ToString()

                'Getting CopyRight
                var3 = dsInventory.Tables(0).Rows(0).Item("copyright").ToString()

                'lblPDate.Text = var1

                lblSNo.Text = var2

                lblCopyR.Text = var3
                For i = 0 To dsInventory.Tables(0).Rows.Count - 1
                    If dsInventory.Tables(0).Rows(i).Item("delFORMAT").ToString().ToUpper().Trim() = "ANNUAL LICENSE - SINGLE USER" Then
                        var4 = "US$ " + FormatNumber(dsInventory.Tables(0).Rows(i).Item("Price").ToString(), 0)
                        lblSA3.Text = var4
                    ElseIf dsInventory.Tables(0).Rows(i).Item("delFORMAT").ToString().ToUpper().Trim() = "ANNUAL LICENSE - 10 USERS" Then
                        var5 = "US$ " + FormatNumber(dsInventory.Tables(0).Rows(i).Item("PRICE").ToString(), 0)
                        lblSA1.Text = var5
                    ElseIf dsInventory.Tables(0).Rows(i).Item("delFORMAT").ToString().ToUpper().Trim() = "ANNUAL LICENSE - 3 USERS" Then
                        var6 = "US$ " + FormatNumber(dsInventory.Tables(0).Rows(i).Item("PRICE").ToString(), 0)
                        lblSA2.Text = var6

                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub imgbtnOrder_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnOrder.Click
        Try
            Dim ObjCrypto As New CryptoHelper()
            '
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "_New", " window.open('../ShoppingCart/Order.aspx?Id=" + ObjCrypto.Encrypt("SA") + "');", True)
        Catch ex As Exception
            lblError.Text = "Error:imgbtnOrder_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim dsUserData As New DataSet
            Dim objUpdataData As New UsersUpdateData.UpdateInsert
            Dim strBody As String = ""
            Dim strBodyUser As String = ""

            strBody = GetEmailBodyData(txtFName.Text, txtLName.Text, txtEmail.Text)
            Try
                objUpdataData.InsertSAUserDetails(txtEmail.Text.Trim(), txtFName.Text.Trim(), txtLName.Text.Trim())

                SendEmail(strBody, txtEmail.Text, txtFName.Text)
                txtFName.Text = ""
                txtLName.Text = ""
                txtEmail.Text = ""
            Catch ex As Exception

            End Try

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Thank you for contacting us. Your request has been recieved.');", True)

        Catch ex As Exception
            lblError.Text = "Error:submitQuestion:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetEmailBodyData(ByVal fName As String, ByVal lName As String, ByVal EMAILADDRESS As String) As String
        Dim StrSqlBody As String = ""
        Try

            Dim dsEmail As New DataSet
            Dim objgetData As New UsersGetData.Selectdata
            dsEmail = objgetData.GetEmailConfigDetails("Y")
            StrSqlBody = "<div style='font-family:Verdana;'>  "
            StrSqlBody = StrSqlBody + "<table cellpadding='0' cellspacing='0' style='width:650px'> "
            StrSqlBody = StrSqlBody + "<tr> "
            StrSqlBody = StrSqlBody + "<td><img src='" + dsEmail.Tables(0).Rows(0).Item("URL").ToString() + "/Images/SavvyPackLogo3.gif' /> "
            StrSqlBody = StrSqlBody + "<br /> "
            StrSqlBody = StrSqlBody + "</td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:35px;'> "
            StrSqlBody = StrSqlBody + "<td> "
            StrSqlBody = StrSqlBody + "<div style='color:white;font-size:18px;font-family:verdana;font-weight:bold;margin-left:5px;'><b>Contact Request Confirmation:</b> </div> "
            StrSqlBody = StrSqlBody + "</td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "</table> "
            StrSqlBody = StrSqlBody + "<div style='margin-top:10px;margin-right:10px;font-family:Verdana;font-size:12px;'> "
            StrSqlBody = StrSqlBody + "<p>Dear " + fName + " " + lName + ",<br/><br/>   Thank you for contacting us regarding your interest in SavvyPack Corporation’s Structure Assistant ™. We have received your request and will respond within 24 hours. </p>"
            StrSqlBody = StrSqlBody + "<p> "
            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:12px;'> "
            StrSqlBody = StrSqlBody + "Regards,<br/> "
            StrSqlBody = StrSqlBody + "SavvyPack Corporation Sales Team<br/><br/>SavvyPack Corporation<br/>1850 East 121st Street<br/>Suite 106B<br/>Burnsville, MN USA 55337<br/><a style='font-family:verdana;' href='http://www.savvypack.com'>www.savvypack.com</a>"
            StrSqlBody = StrSqlBody + "<br/>Phone: [1] 952-405-7500</div>"
            StrSqlBody = StrSqlBody + "</p> "
            StrSqlBody = StrSqlBody + "</div> "

            Return StrSqlBody
        Catch ex As Exception
            Return StrSqlBody
        End Try
    End Function

    Public Sub SendEmail(ByVal strBody As String, ByVal EmailAdd As String, ByVal FirstName As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet

            ds = objGetData.GetAlliedMemberMail("CONREQ")



            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                Dim _To As New MailAddressCollection()
                Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _CC As New MailAddressCollection()
                Dim _BCC As New MailAddressCollection()
                Dim Item As MailAddress
                Dim Email As New EmailConfig()
                Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()

                'To's
                Item = New MailAddress(EmailAdd, FirstName)
                _To.Add(Item)

                Item = New MailAddress("sales@savvypack.com", "Sales")
                _CC.Add(Item)

                For i = 1 To 10
                    ' BCC() 's
                    If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC.Add(Item)
                    End If
                    If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        ' Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        '_CC.Add(Item)
                    End If

                Next

                Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
            End If
        Catch ex As Exception
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub
	
	Protected Sub btnOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrder.Click
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "UManager", "OpenOrderPage();", True)
        Catch ex As Exception

        End Try
    End Sub

End Class
