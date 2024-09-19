Imports System.Data
Imports System.Data.OleDb
Imports System
Imports RetlGetData
Imports RetlUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Retail_Notes_Notes
    Inherits System.Web.UI.Page

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
            AssumptionCode = Request.QueryString("ACODE")
            GetSessionDetails()
            If Not IsPostBack Then
                GetPageDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("RetlCaseId")
            UserRole = Session("RetlUserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim dsNotes As New DataSet
        Dim dsAssumotion As New DataSet
        Dim objGetData As New RetlGetData.Selectdata
        Try
            dsNotes = objGetData.GetPageNoteDetails(AssumptionCode, CaseId)
            dsAssumotion = objGetData.GetAssumptionPageDetails(AssumptionCode)
            lblNotesFor.Text = dsAssumotion.Tables(0).Rows(0).Item("ASSUMPTIONTYPEDE1").ToString()
            Try
                txtNotes.Text = dsNotes.Tables(0).Rows(0).Item("NOTE").ToString()
            Catch ex As Exception
                txtNotes.Text = ""
            End Try
            If Session("RetlServiceRole") = "ReadOnly" Then
                btnsubmit.Enabled = False
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnsubmitt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Dim objUpIn As New RetlUpInsData.UpdateInsert()
        Dim Notes As String = String.Empty
        Try
            Notes = txtNotes.Text.Trim.ToString()
            objUpIn.NotesUpdate(CaseId, AssumptionCode, Notes)
            GetPageDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Notes updated successfully');", True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSubmitt_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
