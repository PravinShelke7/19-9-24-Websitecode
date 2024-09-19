#Region "Imports Classes"
Imports System
Imports System.Data
Imports Allied.GetData
Imports Allied.UpdateInsert
Imports AjaxControlToolkit
Imports System.Math
#End Region
Partial Class Pages_Messages
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _iUserId As Integer
    Dim _lErrorLble As Label
    Dim _bUpdateBtn As ImageButton

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
        End Set
    End Property

    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UpdateBtn() As ImageButton
        Get
            Return _bUpdateBtn
        End Get
        Set(ByVal Value As ImageButton)
            _bUpdateBtn = Value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GerUpdateButton()
            GerErrorLable()
            If Not IsPostBack Then
                BindMessages()
                lnkShowAll.Text = "Show All Announcements"
                hvUserGrd.Value = "0"

            End If
        Catch ex As Exception
            ErrorLable.Text = "Messages:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub BindMessages()
        Try
            Dim objGetData As New GetData()
            Dim dts As New DataSet
            UserId = Session("UserId")
            dts = objGetData.GetMessages(UserId)
            grdMessages.DataSource = dts
            grdMessages.DataBind()
            Session("Message") = dts

            If dts.Tables(0).Rows.Count - 1 < grdMessages.PageSize Then
                lnkShowAll.Enabled = False
            End If


        Catch ex As Exception
            ErrorLable.Text = "Messages:BindMessages:" + ex.Message.ToString() + ""
        End Try
    End Sub





#Region "MastePage Content Variables"
    Protected Sub GerErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GerUpdateButton()
        UpdateBtn = Page.Master.FindControl("imgUpdate")
        UpdateBtn.Visible = False
    End Sub
#End Region


    
   
  
    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Try
            Dim objUpIns As New UpInsData()
            Dim Flag As String = String.Empty
            lblMsg.Visible = True
            For Each gvr As GridViewRow In grdMessages.Rows
                Dim chek As New CheckBox
                Dim MessageId As New Integer

                chek = CType(gvr.FindControl("delete"), CheckBox)
                MessageId = grdMessages.DataKeys(gvr.RowIndex).Value
                UserId = Session("UserId")
                If (chek.Checked = True) Then
                    objUpIns.DeleteUserMessages(UserId, MessageId)
                    lblMsg.Text = "Selected Announcement deleted successfully."
                    Flag = "Y"
                End If

            Next
            If Flag = "Y" Then
                BindMessages()
            Else
                lblMsg.Text = " Please select at least one check box for deleting the Announcement."
            End If
        Catch ex As Exception
            ErrorLable.Text = "Messages:lnkDelete_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub lnkShowAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowAll.Click
        Try
            If grdMessages.AllowPaging = True Then
                grdMessages.AllowPaging = False
                lnkShowAll.Text = "Show Announcements By Page"
            Else
                grdMessages.AllowPaging = True
                lnkShowAll.Text = "Show All Announcements"
            End If

            BindMessages()
            lblMsg.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMessages_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMessages.PageIndexChanging
        Try
            grdMessages.PageIndex = e.NewPageIndex
            BindMessages()

        Catch ex As Exception
            ErrorLable.Text = "Messages:grdMessages_PageIndexChanging:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub grdMessages_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMessages.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("Message")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvUserGrd.Value = numberDiv.ToString()
            grdMessages.DataSource = dv
            grdMessages.DataBind()
            lblMsg.Visible = False
        Catch ex As Exception
            ErrorLable.Text = "Messages:grdMessages_Sorting:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
