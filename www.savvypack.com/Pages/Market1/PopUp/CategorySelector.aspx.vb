Imports System
Imports System.Data
Imports M1GetData
Partial Class Pages_Market1_PopUp_CategorySelector
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BindTree()

            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindTree()
        Dim Dts As New DataSet
        Dim objGetData As New Selectdata
        Try
            Dts = objGetData.GetProducts("0")
            For Each dr As DataRow In Dts.Tables(0).Rows
                Dim node As New TreeNode
                node.Text = dr("CATEGORYNAME").ToString()
                node.Value = dr("ID").ToString()
                AddNodes(node)
                prdTree.Nodes.Add(node)

            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AddNodes(ByRef node As TreeNode)
        Dim Dts As New DataSet
        Dim objGetData As New Selectdata
        Try
            Dts = objGetData.GetProducts(node.Value.ToString())

            For Each dr As DataRow In Dts.Tables(0).Rows
                Dim nNode As New TreeNode
                nNode.Text = dr("CATEGORYNAME").ToString()
                nNode.Value = dr("ID").ToString()
                If Dts.Tables(0).Rows(0)("FACTID").ToString = "" Then
                    AddNodes(nNode)
                End If
                node.ChildNodes.Add(nNode)
                ' prdTree.Nodes.Add(nNode)
            Next

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub prdTree_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles prdTree.SelectedNodeChanged
        Try
            Dim str As String
            str = prdTree.SelectedNode.Value
            ' str = prdTree.SelectedNode.Parent.Value

            hidCatID.Value = Request.QueryString("Id").ToString()
            hidCatDes.Value = Request.QueryString("Des").ToString()
            hidCatDes1.Value = Request.QueryString("CatDes").ToString()
            ClientScript.RegisterStartupScript(Me.GetType(), "RowSel", "CategorySelection('" + prdTree.SelectedNode.Text.ToString() + "','" + prdTree.SelectedNode.Value.ToString() + "','" + Request.QueryString("Id").ToString() + "');", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
