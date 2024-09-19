Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class OnlineForm_Popup_QuickPricePopup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            'If Session("SBack") = Nothing Then
            '    Dim obj As New CryptoHelper
            '    Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            'End If

            If Not IsPostBack Then
                Session("Savvy") = "N"
                Session("ProjId") = Nothing
                hiPrevUserId.Value = ""

                'ResultDetails()
            Else
                If Session("Savvy") = "Y" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "window.open('http://localhost:51788/www.savvypack.com/OnlineForm/ProjectManager.aspx','_blank');", True)
                End If

                
            End If
            BtnLoginQuickP.Attributes.Add("onclick", "return OpenLoginPopup();")
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_QP", "OpenLoginPopup();", True)
            If Request.QueryString("SavvyLink") = "y" Then
                imgFoot.Enabled = True
                imgFoot.Attributes.Add("onclick", "return window.open('https://www.savvypack.com/Index.aspx')")
            End If
            If Session("UserId") <> Nothing Then
                If Request.QueryString("ProjectId").ToString() <> "Nothing" Then

                    Session("ProjectId") = Request.QueryString("ProjectId").ToString()
                    hidProjectId.Value = Request.QueryString("ProjectId").ToString()

                    BtnLoginQuickP.Visible = False
                    BtnSavvy.Visible = True
                    BtnSavvy.ToolTip = "Project Manager"
                    BtnLogoutQuickP.Visible = True
                    BtnLogoutQuickP.ToolTip = "Logout"
                    BtnSavvy.Attributes.Add("onclick", "return OpenSavvyPack();")


                    'New changes started
                    Dim objGetData As New Selectdata()
                    Dim dsData As New DataSet
                    Dim dvData As New DataView
                    Dim dtData As New DataTable
                    'dsData1 = objGetData1.GetMsgDetails(Session("Message"))
                    dsData = objGetData.ExistingProjQuickPriceDetails(hidProjectId.Value)
                    Session("QPData") = dsData
                    dvData = dsData.Tables(0).DefaultView
                    dvData.RowFilter = "PROJECTID=" + hidProjectId.Value
                    dtData = dvData.ToTable()
                    If CInt(dtData.Rows(0).Item("USERID").ToString()) <> CInt(Session("UserId").ToString()) Then
                        Dim flg As Boolean = objGetData.GetUSRLICDetails(dtData.Rows(0).Item("USERID").ToString(), Session("UserId").ToString())
                        If flg = False Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "ShowPopWindow('ConfirmationPopup.aspx');", True)
                            Response.Redirect("https://www.savvypack.com/")
                        Else
                            ResultDetails()
                        End If
                      

                    Else
                        ResultDetails()
                    End If
                    'New changes ended               
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_QP", "OpenLoginPopup();", True)
                BtnLoginQuickP.Visible = True
                BtnSavvy.Visible = True
                lblRPrice1.Text = ""
                BtnSavvy.Attributes.Add("onclick", "return OpenLoginPopupSavvy();")
                'ResultDetails()
            End If

            'Started Activity Log Changes
            Try
                Dim objUpIns As New SavvyUpInsData.UpdateInsert()
                objUpIns.InsertLog1(Session("UserId").ToString(), "QuickPricePopup.aspx", "Opened QuickPrice Results Popup for ProjectId:" + hidProjectId.Value + "", hidProjectId.Value, Session("SPROJLogInCount").ToString())
            Catch ex As Exception

            End Try
           
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
        Try
            If Request.QueryString("PType") = "E" Then
                BtnLoginQuickP.Visible = False
                BtnSavvy.Visible = False
                BtnLogoutQuickP.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If Request.QueryString("ProjectId").ToString() Is Nothing Then
            Session("ProjectId") = Nothing

        End If

    End Sub
    Protected Sub BtnLogoutQuickP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLogoutQuickP.Click
        Try
            Dim objUpdate As New LoginUpdateData.Selectdata
            If Session("TID") <> Nothing Then
                If Session("SPROJLogInCount") <> Nothing Then
                    objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), Session("SPROJLogInCount").ToString())
                Else
                    objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), "")

                End If
            End If
            Session("Savvy") = "N"
            Session("SavvyMail") = "N"
            Session("Message") = Nothing
            Session.Abandon()
            Session.RemoveAll()
            Response.Redirect("~/Index.aspx", True)


        Catch ex As Exception

        End Try
    End Sub

    Public Sub ResultDetails()
        Dim dsProjId As New DataSet
        Dim dsUser As New DataSet
        Dim dsProj As New DataSet
        Dim objGetData As New Selectdata()
        Try
            If Session("UserId") = Nothing Then
                lblRPrice1.Text = ""
            Else

                hidProjectId.Value = Request.QueryString("ProjectId").ToString()
                dsProj = Session("QPData")


                lblRPrice1.Text = dsProj.Tables(0).Rows(0).Item("RESULTPRICE").ToString()
                lblOQ.Text = dsProj.Tables(0).Rows(0).Item("ANNUALORDQUANT").ToString()

                lblOS1.Text = dsProj.Tables(0).Rows(0).Item("ORDSIZE").ToString()

                lblddlFlat_BD.Text = dsProj.Tables(0).Rows(0).Item("DIMENSION_UNIT_NAME_FLT").ToString()

                lblWidth.Text = dsProj.Tables(0).Rows(0).Item("FLATBLANKDIM_w").ToString()
                lblHeight.Text = dsProj.Tables(0).Rows(0).Item("FLATBLANKDIM_l").ToString()

                lblddl_COD.Text = dsProj.Tables(0).Rows(0).Item("DIMENSION_UNIT_CARTOON").ToString()
                lblWidth_COD.Text = dsProj.Tables(0).Rows(0).Item("CARTOUTDIM_w").ToString()
                lblHeight_COD.Text = dsProj.Tables(0).Rows(0).Item("CARTOUTDIM_h").ToString()
                lblLength_COD.Text = dsProj.Tables(0).Rows(0).Item("CARTOUTDIM_l").ToString()

                lblWEC.Text = dsProj.Tables(0).Rows(0).Item("WGHTOFEMPTCASE").ToString()
                lblwc_BD.Text = dsProj.Tables(0).Rows(0).Item("WGHTOFEMPTCASE_UNIT_NAME").ToString()
                lblWPP.Text = dsProj.Tables(0).Rows(0).Item("WGHTOFPRODPACK").ToString()
                lblwpp_BD.Text = dsProj.Tables(0).Rows(0).Item("WGHTOFPRODPACK_UNIT_NAME").ToString()
                lblPrinted_DDL.Text = dsProj.Tables(0).Rows(0).Item("PRINTED_VALUE").ToString()
                lblBw_BD.Text = dsProj.Tables(0).Rows(0).Item("WEIGHT_UNIT").ToString()

                LblECT.Text = dsProj.Tables(0).Rows(0).Item("ECT_UNIT_NAME").ToString()
                lblECT_DDL.Text = dsProj.Tables(0).Rows(0).Item("ECT_VALUE").ToString()
                lblmULLENS_DDL.Text = dsProj.Tables(0).Rows(0).Item("Mullen_VALUE").ToString()
                LblmULLENS.Text = dsProj.Tables(0).Rows(0).Item("MULLENSRATING_UNIT_NAME").ToString()

                lblPQ_DDL.Text = dsProj.Tables(0).Rows(0).Item("PQ_VALUE").ToString()
                lblPC.Text = dsProj.Tables(0).Rows(0).Item("PRINT").ToString()

                lblPC1.Text = dsProj.Tables(0).Rows(0).Item("PRINT_UNIT_NAME").ToString()
                lblBCom1.Text = dsProj.Tables(0).Rows(0).Item("BCOM_VALUE").ToString()

                lblBW.Text = dsProj.Tables(0).Rows(0).Item("OVALLBOARDWGHT").ToString()

                lblFS_DDL.Text = dsProj.Tables(0).Rows(0).Item("FS_VALUE").ToString()

                lblBStyle_DDL.Text = dsProj.Tables(0).Rows(0).Item("CONTSTYLE_VALUE").ToString()

                lblSFormat_DDL.Text = dsProj.Tables(0).Rows(0).Item("SFORMAT_VALUE").ToString()

                LblAQ.Text = dsProj.Tables(0).Rows(0).Item("UNIT_NAME").ToString()
                LblOS.Text = dsProj.Tables(0).Rows(0).Item("ORDERSIZE_NAME").ToString()



            End If




        Catch ex As Exception
            lblError.Text = "Error:ProjectDetails:" + ex.Message.ToString() + ""
        End Try

    End Sub
End Class
