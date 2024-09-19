
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'E1:30        Caseid:15540
            'E2:7         CaseId: 2459
            'Sustain: 174 Caseid:7020
            'Sustain2:1   CaseId:1066

            Dim SessionId As String = Request.QueryString("AspSessionId")
            Dim Modules As String = Request.QueryString("Module")
            Dim WizardType As String = Request.QueryString("Type")
            Dim UserId As String = Request.QueryString("UserId")
            Session("SessionId") = SessionId
            Session("Modules") = Modules
            Session("SessionCheck") = "e467bkg5"
            Session("UserId") = UserId
			
			If WizardType = "GHG" Then
                Response.Redirect("Pages/Co2Wizard.aspx")
            ElseIf WizardType = "ERGY" Then
                Response.Redirect("Pages/ErgyWizard.aspx")
            ElseIf WizardType = "WATER" Then
                Response.Redirect("Pages/WaterWizard.aspx")
            ElseIf WizardType = "BLDBX" Then
                Response.Redirect("Pages/BuildABoxWizard.aspx")
            ElseIf WizardType = "DSGNWASTE" Then
                Response.Redirect("Pages/DesignWaste.aspx")
            ElseIf WizardType = "MSG" Then
                Response.Redirect("Pages/Messages.aspx")
             ElseIf WizardType = "ALL" Then
                Response.Redirect("Pages/SustainWizard.aspx")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class