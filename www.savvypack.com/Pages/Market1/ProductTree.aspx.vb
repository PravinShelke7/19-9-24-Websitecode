Imports System
Imports System.Data
Imports M1GetData



Partial Class Pages_Market1_ProductTree
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            BindTree()
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
            Dts = objGetData.GetProducts(node.value.tostring())
            For Each dr As DataRow In Dts.Tables(0).Rows
                Dim nNode As New TreeNode
                nNode.Text = dr("CATEGORYNAME").ToString()
                nNode.Value = dr("ID").ToString()
                AddNodes(nNode)
                node.ChildNodes.Add(nNode)
                ' prdTree.Nodes.Add(nNode)
            Next
        Catch ex As Exception

        End Try

    End Sub
End Class
