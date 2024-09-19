Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med1GetData
Imports Med1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon1_PopUp_GetPersonnelINPopup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hypPosDes.Value = Request.QueryString("Des").ToString()
            hidPosid.Value = Request.QueryString("ID").ToString()
            hdnCOUNTRY.Value = Request.QueryString("COUNTRY").ToString()
            hidPosid.Value.Replace("/""", "#")
            If Not IsPostBack Then
                GetPositionDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetPositionDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetPositionDetails()
        Dim Ds As New DataSet
        Dim ObjGetdata As New Med1GetData.Selectdata()
        Try

            Ds = ObjGetdata.GetPersonnelInfo(-1, txtPosDe1.Text, "", hdnCOUNTRY.Value.ToString())
            grdPos.DataSource = Ds
            grdPos.DataBind()
            'Ds = ObjGetdata.GetMaterials(-1, "", "")
            'grdMaterials.DataSource = Ds
            'grdMaterials.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
