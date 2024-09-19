<%@ Application Language="VB" %>
<%@ Import namespace="LoginGetData" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="System.Data.SqlClient" %>
<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
     Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
		Dim exc As Exception = Server.GetLastError
        If (exc.GetType Is GetType(HttpException)) Then
            ' The Complete Error Handling Example generates
            ' some errors using URLs with "NoCatch" in them;
            ' ignore these here to simulate what would happen
            ' if a global.asax handler were not implemented.
            If exc.Message.Contains("NoCatch") Or exc.Message.Contains("maxUrlLength") Then
                Return
            End If

            'Redirect HTTP errors to HttpError page
            'Server.Transfer("HttpErrorPage.aspx")
        End If
        If (exc.GetType Is GetType(HttpUnhandledException)) Then
            ' The Complete Error Handling Example generates
            ' some errors using URLs with "NoCatch" in them;
            ' ignore these here to simulate what would happen
            ' if a global.asax handler were not implemented.
			' Server.ClearError()
             Server.ClearError()
             Response.Redirect("http://www.allied-dev.com/")

          
        End If
      
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Dim objUpdate As New LoginUpdateData.Selectdata
        If Session("TID") <> Nothing Then
            If Session("LogInCount") <> Nothing Then
                objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), Session("LogInCount").ToString())
            Else
                objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), "")
            End If
                
             End If
        
    End Sub
   
       
</script>