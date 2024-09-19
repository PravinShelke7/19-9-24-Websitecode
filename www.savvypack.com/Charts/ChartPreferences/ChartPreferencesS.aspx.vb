Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1UpInsData
Imports E1GetData
Imports System.Collections
Imports System.Math
Partial Class Charts_ChartPreferences_ChartPreferencesS
    Inherits System.Web.UI.Page
    Dim GetData As New Selectdata()
    Dim UpdateData As New UpdateInsert

    Dim strUserName As String
    Dim strUserId As String

    Public Property UserName() As String
        Get
            Return strUserName
        End Get
        Set(ByVal value As String)
            strUserName = value
        End Set
    End Property

    Public Property UserId() As String
        Get
            Return strUserId
        End Get
        Set(ByVal value As String)
            strUserId = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim Dts As New DataSet()
            Dim DtsPref As New DataSet()
            Try
                UserName = Session("S1UserName").ToString()
            Catch ex As Exception
                Try
                    UserName = Session("S2UserName").ToString()
                Catch exm As Exception
                    'UserName = Session("SDistUserName").ToString()
                    Try
                        UserName = Session("SDistUserName").ToString()
                    Catch exE3 As Exception
                        UserName = Session("UserName").ToString()
                    End Try
                End Try

            End Try

		UserId = Session("UserId").ToString()

            If IsPostBack <> True Then
                lblUser.Text = UserName.ToString()
                'Dts = GetData.GetCountry()
                'Getting the Prefrences
                DtsPref = GetData.GetChartPrefrences(UserId)

                If DtsPref.Tables(0).Rows.Count > 0 Then
                    Dim Units As String
                    Units = DtsPref.Tables(0).Rows(0).Item("UNITS").ToString()
                    If Units = "0" Then
                        rdEngUni.Checked = True
                    Else
                        rdMetUni.Checked = True
                    End If


                End If
            End If

        Catch ex As Exception
            Response.Write("Chart preferences.Page_Load.Error:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim Unit As String = "'"
            If rdEngUni.Checked = True Then
                Unit = "0"
            Else
                Unit = "1"
            End If
            UpdateData.UpdateCSPreferences(UserId, Unit)

        Catch ex As Exception

        End Try
    End Sub
End Class
