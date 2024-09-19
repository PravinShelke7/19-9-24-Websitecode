Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls


Public Class Settings
    Public Sub New()
        '
        ' TODO: Add constructor logic here
        ''
    End Sub

    Friend Shared ReadOnly Key As Byte() = New Byte() {113, 49, 213, 243, 213, 97, _
     186, 164}
    Friend Shared ReadOnly IV As Byte() = New Byte() {190, 48, 13, 193, 137, 134, _
     106, 173}
    Friend Const LDAP_EXCEPTION_TIMEOUT_ERROR_CODE As Integer = 85
End Class
