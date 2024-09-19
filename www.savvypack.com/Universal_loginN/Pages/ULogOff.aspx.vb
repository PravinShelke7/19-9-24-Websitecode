Imports LoginUpdateData.Selectdata
Partial Class Universal_loginN_Pages_ULogOff
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objUpdate As New LoginUpdateData.Selectdata
        If (Request.QueryString("Type") <> "") Then
            If Session("TID") <> Nothing Then
                If Request.QueryString("Type") <> "Contract" And Request.QueryString("Type") <> "PackProd" Then
                    If Session("Service") = "COMP" Or Session("Service") = "COMPS1" Then
                        objUpdate.UpdateLogOffDetails(Session("USERID"), "", Session("Service").ToString(), Session.SessionID)
						Session("Service") = ""
                    Else
                        objUpdate.UpdateLogOffDetails(Session("USERID"), "", Request.QueryString("Type"), Session.SessionID)
			 If Session("Service") = "SBASSIST" Then
                            Dim objUpIns As New StandUpInsData.UpdateInsert
                            objUpIns.DeleteFlagTemp(Session.SessionID)
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Logged off from Structure Assistant Module", "", Session("LogInCount"), Session.SessionID, "", "", "", "", "")
                        End If
                    End If

                Else
                    objUpdate.UpdateLogOffDetails(Session("USERID"), Session("Password"), Request.QueryString("Type"), Session.SessionID)
                End If
            End If
            'Response.Redirect("UniversalM.aspx")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Login_SavvyPack", "javascript:window.close();", True)
        End If
    End Sub
End Class
