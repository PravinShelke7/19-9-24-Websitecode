Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Microsoft.VisualBasic.FileIO
Imports SpecGetData
Imports SpecUpdateData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports Spec
Partial Class Pages_Spec_Uploads_MassSpecUpload
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iSpecId As Integer
    'Dim _btnUpdate As ImageButton
    Dim _btnClose As ImageButton
    Dim _strFormMode As String


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property SpecId() As Integer
        Get
            Return _iSpecId
        End Get
        Set(ByVal Value As Integer)
            _iSpecId = Value
        End Set
    End Property

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnClose
        End Get
        Set(ByVal value As ImageButton)
            _btnClose = value
        End Set
    End Property

    Public Property FormMode() As String
        Get
            Return _strFormMode
        End Get
        Set(ByVal Value As String)
            _strFormMode = Value
        End Set
    End Property


#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub



#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetErrorLable()
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Try
            Dim InsertUpdata As New SpecUpdateData.UpdateInsert()
            Dim cs As New Import()
            GetImportSettings(cs)
            'Delete By UserName
            InsertUpdata.FireInsUpdateDelete("DELETE FROM " + cs.DestinationTable.ToString() + " WHERE UPPER(USERNAME) ='" + Session("UserName").ToString().ToUpper() + "' ")

            ImportCSVFile()

        Catch ex As Exception
            ErrorLable.Text = "Error:btnUpload_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub ImportCSVFile()
        Try
            Dim filepath As String
            Dim ImportFileId As Integer
            Dim I As Integer
            Dim Filecheck As String = ""
            Dim _iRowIndex As Integer
            Dim cs As New Import()
            Dim Status As String = ""
            Dim ImportType As String = ""
            Dim LineNumber As New Long

            Call GetImportSettings(cs)

            filepath = fileUpload()

            Using reader As New TextFieldParser(filepath)
                reader.TextFieldType = FieldType.Delimited
                reader.Delimiters = New String() {cs.ColumnDelimiter}

                Dim currentRow As String()
                _iRowIndex = 1

                Dim len As Long
                len = reader.LineNumber
                While Not reader.EndOfData
                    Try
                        If cs.SkipRows > 0 Then
                            If _iRowIndex > cs.SkipRows Then
                                LineNumber = reader.LineNumber
                                currentRow = reader.ReadFields()
                                If reader.LineNumber = 1 Then

                                Else
                                    'Include code here to handle the row.
                                    Call InsertIntoTable(currentRow, cs, ImportFileId, LineNumber)
                                End If
                            Else
                                currentRow = reader.ReadFields()
                                Session("ColumnName") = currentRow
                            End If
                        Else
                            LineNumber = reader.LineNumber
                            currentRow = reader.ReadFields()

                            If reader.LineNumber = 1 Then
                            Else
                                ' Include code here to handle the row.
                                Call InsertIntoTable(currentRow, cs, ImportFileId, LineNumber)
                            End If

                        End If
                    Catch ex As MalformedLineException
                        'MsgBox("Line " & ex.Message & " is invalid.  Skipping")
                    End Try
                    _iRowIndex = _iRowIndex + 1

                End While
            End Using

            'StatusFeild.Text = "Uploded the data  in " + cs.DestinationTable + " sucessfully"
            'StatusFeild.Visible = True
            InserUpdateMain()
            lblStatus.Visible = True

        Catch ex As Exception
            ErrorLable.Text = "Error:ImportCSVFile:" + ex.Message.ToString()
        End Try



    End Sub

    Private Function fileUpload() As String
        Dim Path As String = ""
        Dim FileName As String
        Try
            If CommanUpload.HasFile Then
                FileName = CommanUpload.FileName.Replace(".csv", "") + "_" + Session("Username").ToString() + "_" + Now.ToString("MM_dd_yyyy") + "_" + Now.Hour.ToString() + "_" + Now.Minute.ToString() + "_" + Now.Second.ToString() + ".csv"

                CommanUpload.SaveAs(Server.MapPath("CSV/") + FileName)
                Path = Server.MapPath("CSV/") + FileName
            End If
        Catch ex As Exception
            ErrorLable.Text = "fileUpload.Error:" + ex.Message.ToString()
        End Try
        Return Path

    End Function

    Private Sub GetImportSettings(ByVal cs As Import)
        Try
            cs.ColumnDelimiter = ","
            cs.DestinationTable = "TEMPSPECUPLOAD"
            cs.SkipRows = "1"
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub InsertIntoTable(ByVal values As String(), ByVal cs As Import, ByVal ImportFileId As Integer, ByVal Linenumber As String)

        Dim dataType As String()
        Dim Data As String = ""
        Dim Conn As New OleDbConnection
        Dim strsql As String
        Dim _iStartCount As Integer = 0
        Dim J As Integer
        Dim ColumnCount As String = ""
        Dim Datacount As String = ""
        Dim Text As String = ""
        Dim Dt As New DataTable()
        Dim ColumnMatchCount As String = ""
        Dim ColumnName() As String
        ColumnName = Session("ColumnName")
        Dim GetData As New SpecGetData.Selectdata()
        Dim InsertUpdata As New SpecUpdateData.UpdateInsert()



        Dim i As Integer

        Try
            Dim Dts As New DataTable()

            Dts = GetData.GetColoumnType(cs.DestinationTable)
            ColumnMatchCount = Dt.Rows.Count - 1

            'Getting the Column type For tables
            ReDim dataType(Dts.Columns.Count)
            For i = 0 To Dts.Columns.Count - 1
                dataType(i) = Dts.Columns(i).DataType.Name
            Next
            ColumnCount = Dts.Columns.Count
            Datacount = values.Length

            For J = 0 To values.Length - 1
                Text = Text + values(J) + ","
            Next

            If ColumnCount = Datacount Then

                             '---------------------------------------------------------------------------------------------------------------------

                'INSERT QUERY
                strsql = "INSERT INTO " & cs.DestinationTable
                strsql = strsql & " SELECT "
                For i = _iStartCount To values.Length - 1
                    If (dataType(i) = "Decimal") Then
                        If values(i) = "" Then
                            values(i) = "0"
                        End If
                        If i = values.Length - 1 Then
                            strsql = strsql & values(i) & " "
                        Else
                            strsql = strsql & values(i) & ", "
                        End If
                    Else
                        If i = values.Length - 1 Then
                            strsql = strsql & "'" & values(i) & "'" & " "
                        Else
                            strsql = strsql & "'" & values(i) & "'" & ", "
                        End If

                    End If

                Next
                strsql = strsql + ", '" + Session("UserName").ToString() + "' "
                strsql = strsql + ", " + Linenumber.ToString() + " "
                'CHECKING FOR THE DUPLICATE RECORD
                strsql = strsql & " FROM DUAL"

                'Calling the Insert function 
                InsertUpdata.FireInsUpdateDelete(strsql)



            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:InsertIntoTable:" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub InserUpdateMain()
        Dim objUpIns As New SpecUpdateData.UpdateInsert()
        Try
            'Update Specification Details
            objUpIns.UpdateSpecDetailsCsv(Session("UserName").ToString())
            'Insert Specification Details
            objUpIns.InsertSpecDetailsCsv(Session("UserName").ToString())
            'Delete Specification Layer Details
            objUpIns.DeleteSpecLayerCsv(Session("UserName").ToString())
            'Insert Specification Layer Details
            objUpIns.InsertSpecLayerCsv(Session("UserName").ToString())
        Catch ex As Exception
            ErrorLable.Text = "Error:InserUpdateMain:" + ex.Message.ToString()
        End Try
    End Sub
End Class
