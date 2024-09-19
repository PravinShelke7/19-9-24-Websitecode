Imports System
Imports System.Data
Imports M1SubGetData
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
        Dim ds As New DataSet
        Dim StrSql As String = String.Empty
        Dim ProdId As String = String.Empty
        Dim FactId As String = String.Empty
        Dim RptID As String = Request.QueryString("RptID").ToString()
        Try
            If Request.QueryString("isTemp").ToString() = "Y" Then
                ds = objGetData.GetUserReportProdFilter(RptID)
            Else
                ds = objGetData.GetUserReportProdFilterR(RptID)
            End If

            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Product" Then
                        If ProdId = String.Empty Then
                            ProdId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            ProdId = ProdId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If
                    End If
                Next
            End If

            If ProdId <> "" And ProdId <> "0" Then
                Dts = objGetData.GetProductCatGroupsNew(ProdId)
            Else
                Dts = objGetData.GetProductGroupsNew(Session("M1SubGroupId"))
            End If

            If Dts.Tables(0).Rows.Count > 0 Then
                For Each dr As DataRow In Dts.Tables(0).Rows
                    Dim node As New TreeNode
                    node.Text = dr("NAME").ToString()
                    node.Value = dr("PRODGROUPID").ToString()
                    node.SelectAction = TreeNodeSelectAction.None
                    AddNodes(node)
                    grpTree.Nodes.Add(node)
                Next
            Else
                lblGrp.Visible = True
            End If
            
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
