Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Feedback
    Inherits System.Web.UI.Page
    Dim objUpIns As New UpdateInsert()

#Region "Get Set Variables"
    Dim _strAssumptionCode As String
    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _strUserRole As String

    Public Property AssumptionCode() As String
        Get
            Return _strAssumptionCode
        End Get
        Set(ByVal Value As String)
            _strAssumptionCode = Value
        End Set
    End Property

    Public Property CaseId() As Integer
        Get
            Return _iCaseId
        End Get
        Set(ByVal Value As Integer)
            _iCaseId = Value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
        End Set
    End Property

    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
        End Set
    End Property


#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            
            If Not IsPostBack Then
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Feedback.aspx", "Opened FeedbackPopup for ProjectId:" + Request.QueryString("ProjectId").ToString() + "", "", Session("SPROJLogInCount").ToString())
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
                GetPageDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim dsProj As New DataSet
        Dim dsFeedback As New DataSet
        Dim objGetData As New Selectdata
        Try
            dsProj = objGetData.GetEditProjectDetails(Request.QueryString("ProjectId").ToString())
            dsFeedback = objGetData.GetExistingBenefitsDetails(Request.QueryString("ProjectId").ToString())
            lblId.Text = dsProj.Tables(0).Rows(0).Item("PROJECTID").ToString()
            lblTitle.Text = dsProj.Tables(0).Rows(0).Item("TITLE").ToString().Replace("&#", "'")
            Try
                txtQuan.Text = dsFeedback.Tables(0).Rows(0).Item("QUANTBNF").ToString().Replace("&#", "'")
                txtQual.Text = dsFeedback.Tables(0).Rows(0).Item("QUALBNF").ToString().Replace("&#", "'")

            Catch ex As Exception
                'txtNotes.Text = ""
            End Try
            If Session("SavvyAnalyst") <> "Y" Then
                If Session("UserId") <> Request.QueryString("UserId").ToString() Then
                    txtQuan.Enabled = False
                    txtQual.Enabled = False
                    btnUpdate.Enabled = False
                End If
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objUpIn As New UpdateInsert()
        Dim QuanBn As String = String.Empty
        Dim QualBn As String = String.Empty
        Dim flag As Boolean
        Try
            QuanBn = txtQuan.Text.Trim.ToString().Replace("'", "''")
            QualBn = txtQual.Text.Trim.ToString().Replace("'", "''")
            flag = objUpIn.BenefitsUpdate(Request.QueryString("ProjectId").ToString(), Session("UserId").ToString(), QuanBn, QualBn)
            'GetPageDetails()
            If flag = True Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Benefits updated successfully');", True)
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "Feedback.aspx", "Edited Feedback for ProjectId:" + Request.QueryString("ProjectId").ToString() + " QuanBn:" + QuanBn + " and QualBn:" + QualBn + "", Request.QueryString("ProjectId").ToString(), Session("SPROJLogInCount").ToString())
                    Dim ht As Hashtable = New Hashtable()
                    Dim str() As String
		    If Session("ht") IsNot Nothing Then
                        ht = DirectCast(Session("ht"), Hashtable)
                    End If
                    For i = 1 To ht.Count
                        For Each item As Object In ht
                            str = item.Key.ToString().Split("-")
                            If str(1) = i Then
                                objUpIns.EditInsertLog(Session("UserId").ToString(), "15", str(0), "1", item.Value.ToString().Replace("'", "''"), Request.QueryString("ProjectId").ToString(), Session("SPROJLogInCount").ToString())
                                Exit For
                            End If
                        Next
                    Next
                    Session("ht") = Nothing
                    Session("SeqCnt") = "1"
                Catch ex As Exception

                End Try
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "ClosePage", "ClosePage()", True)
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnUpdate_Click:" + ex.Message.ToString()
        End Try
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function UpdateCase(ByVal Header As String, ByVal text As String) As String
        Try
            Dim ht As Hashtable = New Hashtable()
            Dim str() As String
            Dim seq As Integer = Convert.ToInt32(HttpContext.Current.Session("SeqCnt"))
            Dim flag As Boolean = False

            If HttpContext.Current.Session("ht") IsNot Nothing Then
                ht = DirectCast(HttpContext.Current.Session("ht"), Hashtable)
            End If

            For Each item As Object In ht
                str = item.Key.ToString().Split("-")
                If str(0) = Header Then
                    ht.Remove(item.Key)
                    flag = True
                    Exit For
                End If
            Next

            If flag Then
                ht.Add(Header + "-" + str(1).ToString(), text)
            Else
                ht.Add(Header + "-" + seq.ToString(), text)
                seq += 1
                HttpContext.Current.Session("SeqCnt") = seq
            End If
            HttpContext.Current.Session("ht") = ht
            Dim str1 As String = HttpContext.Current.Session("UserId").ToString()
            str1 = str1 + "Bhavesh"
            Return str1

        Catch ex As Exception

        End Try
    End Function
End Class
