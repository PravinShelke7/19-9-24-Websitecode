#Region "Imports Classes"
Imports System
Imports System.Data
Imports Allied.GetData
Imports Allied.UpdateInsert
Imports AjaxControlToolkit
Imports System.Math
#End Region
Partial Class Masters_E1S1E2S2
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ScriptManager1.RegisterAsyncPostBackControl(imgUpdate)
            ScriptManager1.RegisterAsyncPostBackControl(lblError)

            If (Session("SessionCheck")) = Nothing Then
                'ContentPage.Visible = False
                lblError.Text = "Session is Expired"
            Else
                ContentPage.Visible = True
            End If


        Catch ex As Exception

        End Try
    End Sub
End Class

