Imports System
Imports System.Data
Imports M1GetData
Partial Class Pages_Market1_PopUp_Default
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
            Dts = objGetData.GetGroups("0")
            For Each dr As DataRow In Dts.Tables(0).Rows
                Dim node As New TreeNode
                node.Text = dr("NAME").ToString()
                node.Value = dr("PRODGROUPID").ToString()
                AddNodes(node)
                grpTree.Nodes.Add(node)

            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AddNodes(ByRef node As TreeNode)
        Dim Dts As New DataSet
        Dim objGetData As New Selectdata
        Try
            Dts = objGetData.GetGroups(node.Value.ToString())

            For Each dr As DataRow In Dts.Tables(0).Rows
                Dim nNode As New TreeNode
                nNode.Text = dr("NAME").ToString()
                nNode.Value = dr("PRODGROUPID").ToString()               
                node.ChildNodes.Add(nNode)
                ' grpTree.Nodes.Add(nNode)
            Next

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grpTree_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grpTree.SelectedNodeChanged
        Try
            Dim str As String
            str = grpTree.SelectedNode.Value
            ' str = grpTree.SelectedNode.Parent.Value

            hidCatID.Value = Request.QueryString("Id").ToString()
            hidCatDes.Value = Request.QueryString("Des").ToString()
            hidCatDes1.Value = Request.QueryString("CatDes").ToString()
            ClientScript.RegisterStartupScript(Me.GetType(), "RowSel", "CategorySelection('" + grpTree.SelectedNode.Text.ToString() + "','" + grpTree.SelectedNode.Value.ToString() + "','" + Request.QueryString("Id").ToString() + "');", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
