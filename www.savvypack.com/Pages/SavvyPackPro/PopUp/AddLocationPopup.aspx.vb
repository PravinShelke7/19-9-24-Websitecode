Imports System.Data
Imports SavvyProGetData
Partial Class AddLocationPopup
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            BindCountry()
            BindCountryDetails("0")

        End If

    End Sub
    Protected Sub BindCountry()
        Try
            Dim Dts, DtsD As New DataSet
            Dts = objGetdata.GetCountryName()
            With ddlCountryN
                .DataSource = Dts
                .DataTextField = "NAME"
                .DataValueField = "COUNTRYID"
                .DataBind()
            End With
            bindStateDropdwon(ddlCountryN.SelectedValue)
        Catch ex As Exception
            Response.Write("BindCountry" + ex.Message)
        End Try
    End Sub
    Protected Sub BindCountryDetails(ByVal countryId As String)
        Try
            Dim Dts, DtsD As New DataSet
            Dts = objGetdata.GetCountryName()
            With ddlCountryN
                .DataSource = Dts
                .DataTextField = "NAME"
                .DataValueField = "COUNTRYID"
                .DataBind()
            End With

        Catch ex As Exception
            Response.Write("BindCountryDetails" + ex.Message)

        End Try
    End Sub
    Protected Sub GetStateDetails(ByVal countryId As String)
        Try
            Dim Dts, DtsD As New DataSet
            Dts = objGetdata.GetState(countryId)
            With ddlStateD
                .DataSource = Dts
                .DataTextField = "NAME"
                .DataValueField = "STATEID"
                .DataBind()
            End With

        Catch ex As Exception
            Response.Write("BindStateDetails" + ex.Message)

        End Try
    End Sub

    Protected Sub bindStateDropdwon(ByVal countryId As String)
        Try
            Dim Dts, DtsD As New DataSet
            Dts = objGetdata.GetState(countryId)
            With ddlStateD
                .DataSource = Dts
                .DataTextField = "NAME"
                .DataValueField = "STATEID"
                .DataBind()
            End With
        Catch ex As Exception
            Response.Write("bindStateDropdwon" + ex.Message)
        End Try
    End Sub

    'Protected Sub ddlStateD_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStateD.SelectedIndexChanged
    '    Try
    '        bindStateDropdwon(ddlCountry.SelectedValue)
    '    Catch ex As Exception
    '        Response.Write("ddlCountry_SelectedIndexChanged" + ex.Message)
    '    End Try
    'End Sub


    Protected Sub ddlCountryN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCountryN.SelectedIndexChanged
        Try
            bindStateDropdwon(ddlCountryN.SelectedValue)
        Catch ex As Exception
            Response.Write("ddlCountryN_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub
End Class
