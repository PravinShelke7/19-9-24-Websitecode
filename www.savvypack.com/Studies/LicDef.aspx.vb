Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Studies_LicDef
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("MenuItem") = "ACNT"
        'Dim objmenu As Menu
        'objmenu = Page.Master.FindControl("AlliedMenu")
        'objmenu.Visible = False

        'objmenu = Page.Master.FindControl("AlliedMenu2")
        'objmenu.Visible = False

    End Sub

End Class
