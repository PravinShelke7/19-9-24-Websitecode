﻿
Partial Class Masters_S2Chart
    Inherits System.Web.UI.MasterPage

  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
        Catch ex As Exception
        End Try
    End Sub
End Class

