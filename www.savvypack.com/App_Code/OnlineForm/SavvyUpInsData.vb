Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class SavvyUpInsData
    Public Class UpdateInsert
        Dim SavvyConnection As String = System.Configuration.ConfigurationManager.AppSettings("SavvyPackConnectionString")
Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")


#Region "Email Store"
        Public Sub InsertEmailStore(ByVal To1 As String, ByVal logid As String, ByVal Code As String, ByVal strbody As String, ByVal userid As String, ByVal Priority As String, ByVal PageId As String, ByVal adetails As String, ByVal projectid As String)
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim i As Integer = 0
            Try
               


                'Inserting Into Groups Table
                strsql = String.Empty
                strsql = "INSERT INTO EMAILSTORE  "
                strsql = strsql + "( "
                strsql = strsql + " EMAILSTOREID,LOGID,TOADD,CODE,USERID,STRBODY,UPDATEDATE,PRIORITY,PAGEID,ACTIVITYDETAILS,PROJECTID) "
                strsql = strsql + "VALUES(SEQEMAILSTOREID.NEXTVAL,'" + logid.ToString() + "','" + To1.ToString() + "','" + Code.ToString() + "','" + userid.ToString() + "','" + strbody.ToString().Replace("'", "''") + "',SYSDATE,'" + Priority.ToString() + "','" + PageId.ToString() + "','" + adetails.ToString() + "','" + projectid.ToString() + "') "


                odButil.UpIns(strsql, EconConnection)


            Catch ex As Exception
                Throw New Exception("E1UpdateData:InsertEmailStore:" + ex.Message.ToString())
            End Try
        End Sub
#End Region


#Region "QuickPrice"
        Public Function InsertQuickPrice(ByVal PROJECTID As String, ByVal OrderQ As String, ByVal OrderSize As String, ByVal FDWidth As String, ByVal FDHeight As String, ByVal ddlFlat_BD As String, ByVal CDWidth As String, _
                                             ByVal CDHeight As String, ByVal CDLength As String, ByVal ddl_COD As String, ByVal Weight_EmptyCase As String, ByVal Weight_ProdPacked As String, ByVal Printed_DDL As String, ByVal ECT_DDL As String, ByVal mULLENS_DDL As String, _
                                             ByVal PQ_DDL As String, ByVal PrintC As String, ByVal Bcom_DDL As String, ByVal Overall_BoardWeight As String, ByVal FS_DDL As String, ByVal BStyle_DDL As String, _
                                             ByVal SFormat_DDL As String, ByVal Unit_WEmptyCase As String, ByVal Unit_WPackage As String, ByVal Bw_BD As String) As Integer
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ProjId As Integer = 0
            Dim RESULTPRICE As Integer = 0
            Try
                strSql = "SELECT SEQPROJQUICKPRICEID.NEXTVAL PROJQUICKPRICEID FROM DUAL "
                ProjId = dbUtil.FillData(strSql, SavvyConnection)

                strSql = String.Empty
                strSql = "INSERT INTO PROJQUICKPRICE (PROJQUICKPRICEID,PROJECTID,ANNUALORDQUANT,ORDSIZE, FLATBLANKDIM_w,FLATBLANKDIM_l,CARTOUTDIM_l,CARTOUTDIM_w,CARTOUTDIM_h,WGHTOFEMPTCASE,WGHTOFPRODPACK,ECT,MullensRating,PRINTED,"
                strSql = strSql + "PRINTEDQUAL,PRINT,BOARDCOMB,OVALLBOARDWGHT,FLUTESIZE,CONTSTYL,SHIPFORMT,RESULTPRICE,ANNUALORDQ_UNIT,ORDSIZE_UNIT,FLATBLANKDIM_UNIT,CARTOUTDIM_UNIT,WGHTOFEMPTCASE_UNIT,WGHTOFPRODPACK_UNIT,ECT_UNIT,MULLENSRATING_UNIT,PRINT_UNIT,OVALLBOARDWGHT_UNIT,UPDATEDDATE)"
                strSql = strSql + "SELECT " + ProjId.ToString() + "," + PROJECTID + ",'" + OrderQ + "','" + OrderSize + "','" + FDWidth + "','" + FDHeight + "','" + CDLength + "','" + CDWidth + "','" + CDHeight + "','" + Weight_EmptyCase + "','" + Weight_ProdPacked + "','" + ECT_DDL + "','" + mULLENS_DDL + "','" + Printed_DDL + "','"
                strSql = strSql + PQ_DDL + "','" + PrintC + "','" + Bcom_DDL + "','" + Overall_BoardWeight + "','" + FS_DDL + "','" + BStyle_DDL + "','" + SFormat_DDL + "','" + RESULTPRICE.ToString() + "',(SELECT ID FROM QPRICE_UNIT WHERE ID='1'),(SELECT ID FROM QPRICE_UNIT WHERE ID='1'),'" + ddlFlat_BD.ToString() + "','" + ddl_COD.ToString() + "','" + Unit_WEmptyCase + "','" + Unit_WPackage + "',"
                strSql = strSql + "(SELECT ID FROM QPRICE_UNIT WHERE ID='2'), "
                strSql = strSql + "(SELECT ID FROM QPRICE_UNIT WHERE ID='3'), "
                strSql = strSql + "(SELECT ID FROM QPRICE_UNIT WHERE ID='1'),'"
                strSql = strSql + Bw_BD + "'"
                strSql = strSql + ",SYSDATE FROM DUAL "
                dbUtil.UpIns(strSql, SavvyConnection)
                Return ProjId
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjTitle:" + ex.Message.ToString())
                Return False

            End Try
        End Function
        Public Sub AddUserAnalysis(ByVal UserId As Integer, ByVal AnalysisId As Integer, ByVal LogInUserId As Integer)
            Dim Ds As New DataSet()
            Dim odbutil As New DBUtil()
            Try
                Dim StrSql As String = String.Empty

                'Inserting The AddUserAnalysis if not Exits
                StrSql = "INSERT INTO USERANALYSIS "
                StrSql = StrSql + "(USERANALYSISID,USERID, ANALYSISTYPEID,LOGINUSERID,ACTIVITYTIME) "
                StrSql = StrSql + "SELECT SEQUSERANALYSISID.NEXTVAL," + UserId.ToString() + "," + AnalysisId.ToString() + "," + LogInUserId.ToString() + ",SYSDATE" + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM USERANALYSIS "
                StrSql = StrSql + "WHERE USERANALYSIS.USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "AND USERANALYSIS.ANALYSISTYPEID=" + AnalysisId.ToString() + " "
                StrSql = StrSql + ") "
                odbutil.UpIns(StrSql, SavvyConnection)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
#End Region

#Region "Report Details"

        Public Function InsertProjDetails(ByVal UserId As String, ByVal Title As String, ByVal KeyWord As String, ByVal Desc As String, ByVal Active As String, ByVal AnalysisId As String) As Integer
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ProjId As Integer = 0
            Try
                strSql = "SELECT SEQPROJECTID.NEXTVAL PROJECTID FROM DUAL "
                ProjId = dbUtil.FillData(strSql, SavvyConnection)

                strSql = String.Empty
                strSql = "INSERT INTO PROJECTDETAILS (PROJECTID,USERID,TITLE,KEYWORD,DESCRIPTION,STATUSID,ANALYSISID,CREATEDON) "
                strSql = strSql + "SELECT " + ProjId.ToString() + "," + UserId + ",'" + Title + "','" + KeyWord + "','" + Desc + "','" + Active + "'," + AnalysisId + ",SYSDATE FROM DUAL"

                dbUtil.UpIns(strSql, SavvyConnection)
                Return ProjId
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjDetails:" + ex.Message.ToString())
                Return False
            End Try
        End Function

        Public Function EditProjDetails(ByVal ProjId As String, ByVal UserId As String, ByVal Title As String, ByVal KeyWord As String, ByVal Desc As String, ByVal AnalysisId As String) As Boolean
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE PROJECTDETAILS SET TITLE='" + Title + "',DESCRIPTION='" + Desc + "', "
                strSql = strSql + "KEYWORD='" + KeyWord + "',ANALYSISID=" + AnalysisId + ",LASTUPDATEDON=SYSDATE "
                strSql = strSql + "WHERE USERID=" + UserId + " AND PROJECTID=" + ProjId + " "

                dbUtil.UpIns(strSql, SavvyConnection)
                Return True
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:EditProjDetails:" + ex.Message.ToString())
                Return False
            End Try
        End Function

        Public Function EditProjDetailsbyAnalyst(ByVal ProjId As String, ByVal Title As String, ByVal KeyWord As String, ByVal Desc As String, ByVal StatusId As String, ByVal AnalysisId As String) As Boolean
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE PROJECTDETAILS SET TITLE='" + Title + "',DESCRIPTION='" + Desc + "', "
                strSql = strSql + "KEYWORD='" + KeyWord + "',STATUSID=" + StatusId + ",ANALYSISID=" + AnalysisId + ",LASTUPDATEDON=SYSDATE "
                strSql = strSql + "WHERE PROJECTID=" + ProjId + " "

                dbUtil.UpIns(strSql, SavvyConnection)
                Return True
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:EditProjDetails:" + ex.Message.ToString())
                Return False
            End Try
        End Function

        Public Sub InsertViewProject(ByVal LicenseId As String, ByVal Type As String)
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "INSERT INTO VIEWPROJECT (LICENSEID,TYPE) SELECT " + LicenseId + ",'" + Type + "' FROM DUAL "
                strSql = strSql + "WHERE NOT EXISTS (SELECT 1 FROM VIEWPROJECT WHERE LICENSEID=" + LicenseId + ")"
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception

            End Try
        End Sub

        Public Sub EditViewProject(ByVal LicenseId As String, ByVal Type As String)
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "UPDATE VIEWPROJECT SET TYPE='" + Type + "' WHERE LICENSEID=" + LicenseId + " "
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception

            End Try
        End Sub

        Public Sub UpdateProjectStatus(ByVal StatusId As String, ByVal ProjectId As String, ByVal UserID As String)
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "UPDATE PROJECTDETAILS SET STATUSID=" + StatusId + ",SUBMITTEDUSERID=" + UserID + " WHERE PROJECTID=" + ProjectId + " "
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception

            End Try
        End Sub
		
		Public Sub UpdateSubmitDate(ByVal TypeId As String, ByVal ProjectId As String)
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "INSERT INTO PROJECTDATE(PROJECTID,DATETYPEID,VALUE) "
                strSql = strSql + "SELECT " + ProjectId + "," + TypeId + ",SYSDATE FROM DUAL "
                strSql = strSql + "WHERE NOT EXISTS(SELECT 1 FROM PROJECTDATE WHERE PROJECTID=" + ProjectId + " AND DATETYPEID=" + TypeId + ")"
                dbUtil.UpIns(strSql, SavvyConnection)

                strSql = "UPDATE PROJECTDATE SET VALUE=SYSDATE WHERE PROJECTID=" + ProjectId + " AND DATETYPEID=" + TypeId + " "
                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception

            End Try
        End Sub

#End Region

#Region "Model Details"
        Public Sub InsertModelDetails(ByVal ProjId As String, ByVal ProdPack As String, ByVal PackSize As String, ByVal PackType As String, ByVal Geography As String, ByVal VChain As String, ByVal SpFet1 As String, ByVal SpFet2 As String, ByVal Producer As String, _
                                      ByVal Packer As String, ByVal Graphics As String, ByVal Structures As String, ByVal Formula As String, ByVal Manufactory As String, ByVal Packaging As String, ByVal Sku As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Dim ModelId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(MODELID) MODELID FROM MODELDETAILSTEMP WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("MODELID").ToString() = "" Then
                    ModelId = 1
                Else
                    ModelId = ds.Tables(0).Rows(0).Item("MODELID") + 1
                End If

                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO MODELDETAILSTEMP (MODELID,PROJECTID,PRODPACK,PACKSIZE,PACKTYPE,GEOGRAPHY,VCHAIN,SPFET1, "
                strSql = strSql + "SPFET2,PRODUCER,PACKER,GRAPHICS,STRUCTURES,FORMULA,MANUFACTORY,PACKAGING,SKU) "
                strSql = strSql + "SELECT " + ModelId.ToString() + "," + ProjId + ",'" + ProdPack + "','" + PackSize + "','" + PackType + "','" + Geography + "', "
                strSql = strSql + "'" + VChain + "','" + SpFet1 + "','" + SpFet2 + "','" + Producer + "','" + Packer + "','" + Graphics + "', "
                strSql = strSql + "'" + Structures + "','" + Formula + "','" + Manufactory + "','" + Packaging + "','" + Sku + "' FROM DUAL "
                strSql = strSql + "WHERE NOT EXISTS (SELECT 1 FROM MODELDETAILSTEMP WHERE "
                If ProdPack = "" Then
                    strSql = strSql + "PRODPACK IS NULL "
                Else
                    strSql = strSql + "PRODPACK='" + ProdPack + "' "
                End If
                If PackSize = "" Then
                    strSql = strSql + "AND PACKSIZE IS NULL "
                Else
                    strSql = strSql + "AND PACKSIZE='" + PackSize + "' "
                End If
                If PackType = "" Then
                    strSql = strSql + "AND PACKTYPE IS NULL "
                Else
                    strSql = strSql + "AND PACKTYPE='" + PackType + "' "
                End If
                If Geography = "" Then
                    strSql = strSql + "AND GEOGRAPHY IS NULL "
                Else
                    strSql = strSql + "AND GEOGRAPHY='" + Geography + "' "
                End If
                If VChain = "" Then
                    strSql = strSql + "AND VCHAIN IS NULL "
                Else
                    strSql = strSql + "AND VCHAIN='" + VChain + "' "
                End If
                If SpFet1 = "" Then
                    strSql = strSql + "AND SPFET1 IS NULL "
                Else
                    strSql = strSql + "AND SPFET1='" + SpFet1 + "' "
                End If
                If SpFet2 = "" Then
                    strSql = strSql + "AND SPFET2 IS NULL "
                Else
                    strSql = strSql + "AND SPFET2='" + SpFet2 + "' "
                End If
                If Producer = "" Then
                    strSql = strSql + "AND PRODUCER IS NULL "
                Else
                    strSql = strSql + "AND PRODUCER='" + Producer + "' "
                End If
                If Packer = "" Then
                    strSql = strSql + "AND PACKER IS NULL "
                Else
                    strSql = strSql + "AND PACKER='" + Packer + "' "
                End If
                If Graphics = "" Then
                    strSql = strSql + "AND GRAPHICS IS NULL "
                Else
                    strSql = strSql + "AND GRAPHICS='" + Graphics + "' "
                End If
                If Structures = "" Then
                    strSql = strSql + "AND STRUCTURES IS NULL "
                Else
                    strSql = strSql + "AND STRUCTURES='" + Structures + "' "
                End If
                If Formula = "" Then
                    strSql = strSql + "AND FORMULA IS NULL "
                Else
                    strSql = strSql + "AND FORMULA='" + Formula + "' "
                End If
                If Manufactory = "" Then
                    strSql = strSql + "AND MANUFACTORY IS NULL "
                Else
                    strSql = strSql + "AND MANUFACTORY='" + Manufactory + "' "
                End If
                If Packaging = "" Then
                    strSql = strSql + "AND PACKAGING IS NULL "
                Else
                    strSql = strSql + "AND PACKAGING='" + Packaging + "' "
                End If
                If Sku = "" Then
                    strSql = strSql + "AND SKU IS NULL "
                Else
                    strSql = strSql + "AND SKU='" + Sku + "' "
                End If
                strSql = strSql + "AND PROJECTID=" + ProjId + " )"
                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjDetails:" + ex.Message.ToString())

            End Try
        End Sub

        Public Function InsertProjCategoryDetails(ByVal ProjId As String, ByVal CatId As String, ByVal Value As String) As Integer
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Dim Sequence As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(SEQUENCE) SEQUENCE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjId + " AND CATEGORYID=" + CatId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("SEQUENCE").ToString() = "" Then
                    Sequence = 1
                Else
                    Sequence = ds.Tables(0).Rows(0).Item("SEQUENCE") + 1
                End If

                strSql = "INSERT INTO PROJECTCATEGORY (PROJCATEGORYID,PROJECTID,CATEGORYID,VALUE,SEQUENCE) "
                strSql = strSql + "SELECT SEQPROJCATEGORYID.NEXTVAL," + ProjId + "," + CatId + ",'" + Value + "'," + Sequence.ToString() + " FROM DUAL "
                strSql = strSql + "WHERE NOT EXISTS (SELECT 1 FROM PROJECTCATEGORY WHERE CATEGORYID=" + CatId + " "
                strSql = strSql + "AND UPPER(VALUE)='" + Value.ToUpper() + "' AND PROJECTID=" + ProjId + " )"

                dbUtil.UpIns(strSql, SavvyConnection)
                Return Sequence

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjDetails:" + ex.Message.ToString())

            End Try
        End Function
        Public Sub UpdateProjCategoryDetails(ByVal ProjId As String, ByVal CatId As String, ByVal Value As String, ByVal ProjCatId As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE PROJECTCATEGORY SET VALUE='" + Value + "' "
                strSql = strSql + "WHERE PROJCATEGORYID=" + ProjCatId + " AND PROJECTID=" + ProjId + " AND  CATEGORYID=" + CatId + ""

                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateProjCategoryDetails:" + ex.Message.ToString())

            End Try
        End Sub

        Public Sub DeleteProjCategoryDetails(ByVal ProjId As String, ByVal CatId As String, ByVal Value As String, ByVal ProjCatId As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM PROJECTCATEGORY WHERE PROJCATEGORYID=" + ProjCatId + " AND PROJECTID=" + ProjId + " AND  CATEGORYID=" + CatId + " AND VALUE='" + Value + "'"

                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:DeleteProjCategoryDetails:" + ex.Message.ToString())

            End Try
        End Sub

        Public Sub EditProjCategoryDetails(ByVal ProjId As String, ByVal CatId As String, ByVal Value As String, ByVal isActive As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE PROJECTCATEGORY SET ISACTIVE='" + isActive + "' "
                strSql = strSql + "WHERE CATEGORYID=" + CatId + " AND VALUE='" + Value + "' AND PROJECTID=" + ProjId + " "

                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjDetails:" + ex.Message.ToString())

            End Try
        End Sub

        Public Sub EditModelDetails(ByVal ProjId As String, ByVal ModelId As String, ByVal ProdPack As String, ByVal PackSize As String, ByVal PackType As String, ByVal Geography As String, ByVal VChain As String, ByVal SpFet1 As String, ByVal SpFet2 As String, _
                                    ByVal Producer As String, ByVal Packer As String, ByVal Graphics As String, ByVal Structures As String, ByVal Formula As String, ByVal Manufactory As String, ByVal Packaging As String, ByVal Sku As String, ByVal Check As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try

                If Check = "Y" Then
                    strSql = "SELECT 1 FROM MODELDETAILSTEMP WHERE PROJECTID=" + ProjId + " "
                    If ProdPack = "" Then
                        strSql = strSql + "AND PRODPACK IS NULL "
                    Else
                        strSql = strSql + "AND PRODPACK='" + ProdPack + "' "
                    End If
                    If PackSize = "" Then
                        strSql = strSql + "AND PACKSIZE IS NULL "
                    Else
                        strSql = strSql + "AND PACKSIZE='" + PackSize + "' "
                    End If
                    If PackType = "" Then
                        strSql = strSql + "AND PACKTYPE IS NULL "
                    Else
                        strSql = strSql + "AND PACKTYPE='" + PackType + "' "
                    End If
                    If Geography = "" Then
                        strSql = strSql + "AND GEOGRAPHY IS NULL "
                    Else
                        strSql = strSql + "AND GEOGRAPHY='" + Geography + "' "
                    End If
                    If VChain = "" Then
                        strSql = strSql + "AND VCHAIN IS NULL "
                    Else
                        strSql = strSql + "AND VCHAIN='" + VChain + "' "
                    End If
                    If SpFet1 = "" Then
                        strSql = strSql + "AND SPFET1 IS NULL "
                    Else
                        strSql = strSql + "AND SPFET1='" + SpFet1 + "' "
                    End If
                    If SpFet2 = "" Then
                        strSql = strSql + "AND SPFET2 IS NULL "
                    Else
                        strSql = strSql + "AND SPFET2='" + SpFet2 + "' "
                    End If
                    If Producer = "" Then
                        strSql = strSql + "AND PRODUCER IS NULL "
                    Else
                        strSql = strSql + "AND PRODUCER='" + Producer + "' "
                    End If
                    If Packer = "" Then
                        strSql = strSql + "AND PACKER IS NULL "
                    Else
                        strSql = strSql + "AND PACKER='" + Packer + "' "
                    End If
                    If Graphics = "" Then
                        strSql = strSql + "AND GRAPHICS IS NULL "
                    Else
                        strSql = strSql + "AND GRAPHICS='" + Graphics + "' "
                    End If
                    If Structures = "" Then
                        strSql = strSql + "AND STRUCTURES IS NULL "
                    Else
                        strSql = strSql + "AND STRUCTURES='" + Structures + "' "
                    End If
                    If Formula = "" Then
                        strSql = strSql + "AND FORMULA IS NULL "
                    Else
                        strSql = strSql + "AND FORMULA='" + Formula + "' "
                    End If
                    If Manufactory = "" Then
                        strSql = strSql + "AND MANUFACTORY IS NULL "
                    Else
                        strSql = strSql + "AND MANUFACTORY='" + Manufactory + "' "
                    End If
                    If Packaging = "" Then
                        strSql = strSql + "AND PACKAGING IS NULL "
                    Else
                        strSql = strSql + "AND PACKAGING='" + Packaging + "' "
                    End If
                    If Sku = "" Then
                        strSql = strSql + "AND SKU IS NULL "
                    Else
                        strSql = strSql + "AND SKU='" + Sku + "' "
                    End If

                    ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                    If ds.Tables(0).Rows.Count < 1 Then
                        strSql = String.Empty
                        strSql = "UPDATE  MODELDETAILSTEMP SET PRODPACK='" + ProdPack + "',PACKSIZE='" + PackSize + "',PACKTYPE='" + PackType + "',GEOGRAPHY='" + Geography + "', "
                        strSql = strSql + "VCHAIN='" + VChain + "',SPFET1='" + SpFet1 + "',SPFET2='" + SpFet2 + "',PRODUCER='" + Producer + "',PACKER='" + Packer + "',GRAPHICS='" + Graphics + "', "
                        strSql = strSql + "STRUCTURES='" + Structures + "',FORMULA='" + Formula + "',MANUFACTORY='" + Manufactory + "',PACKAGING='" + Packaging + "',SKU='" + Sku + "' WHERE MODELID=" + ModelId + " AND PROJECTID=" + ProjId + " "
                    End If

                Else
                    strSql = "SELECT 1 FROM MODELDETAILSTEMP WHERE PROJECTID=" + ProjId + " "
                    If ProdPack = "" Then
                        strSql = strSql + "AND PRODPACK IS NULL "
                    Else
                        strSql = strSql + "AND PRODPACK='" + ProdPack + "' "
                    End If
                    If PackSize = "" Then
                        strSql = strSql + "AND PACKSIZE IS NULL "
                    Else
                        strSql = strSql + "AND PACKSIZE='" + PackSize + "' "
                    End If
                    If PackType = "" Then
                        strSql = strSql + "AND PACKTYPE IS NULL "
                    Else
                        strSql = strSql + "AND PACKTYPE='" + PackType + "' "
                    End If
                    If Geography = "" Then
                        strSql = strSql + "AND GEOGRAPHY IS NULL "
                    Else
                        strSql = strSql + "AND GEOGRAPHY='" + Geography + "' "
                    End If
                    If VChain = "" Then
                        strSql = strSql + "AND VCHAIN IS NULL "
                    Else
                        strSql = strSql + "AND VCHAIN='" + VChain + "' "
                    End If
                    If SpFet1 = "" Then
                        strSql = strSql + "AND SPFET1 IS NULL "
                    Else
                        strSql = strSql + "AND SPFET1='" + SpFet1 + "' "
                    End If
                    If SpFet2 = "" Then
                        strSql = strSql + "AND SPFET2 IS NULL "
                    Else
                        strSql = strSql + "AND SPFET2='" + SpFet2 + "' "
                    End If
                    If Producer = "" Then
                        strSql = strSql + "AND PRODUCER IS NULL "
                    Else
                        strSql = strSql + "AND PRODUCER='" + Producer + "' "
                    End If
                    If Packer = "" Then
                        strSql = strSql + "AND PACKER IS NULL "
                    Else
                        strSql = strSql + "AND PACKER='" + Packer + "' "
                    End If
                    If Graphics = "" Then
                        strSql = strSql + "AND GRAPHICS IS NULL "
                    Else
                        strSql = strSql + "AND GRAPHICS='" + Graphics + "' "
                    End If
                    If Structures = "" Then
                        strSql = strSql + "AND STRUCTURES IS NULL "
                    Else
                        strSql = strSql + "AND STRUCTURES='" + Structures + "' "
                    End If
                    If Formula = "" Then
                        strSql = strSql + "AND FORMULA IS NULL "
                    Else
                        strSql = strSql + "AND FORMULA='" + Formula + "' "
                    End If
                    If Manufactory = "" Then
                        strSql = strSql + "AND MANUFACTORY IS NULL "
                    Else
                        strSql = strSql + "AND MANUFACTORY='" + Manufactory + "' "
                    End If
                    If Packaging = "" Then
                        strSql = strSql + "AND PACKAGING IS NULL "
                    Else
                        strSql = strSql + "AND PACKAGING='" + Packaging + "' "
                    End If
                    If Sku = "" Then
                        strSql = strSql + "AND SKU IS NULL "
                    Else
                        strSql = strSql + "AND SKU='" + Sku + "' "
                    End If

                    ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                    If ds.Tables(0).Rows.Count < 1 Then
                        strSql = String.Empty
                        strSql = "UPDATE  MODELDETAILSTEMP SET PRODPACK='" + ProdPack + "',PACKSIZE='" + PackSize + "',PACKTYPE='" + PackType + "',GEOGRAPHY='" + Geography + "', "
                        strSql = strSql + "VCHAIN='" + VChain + "',SPFET1='" + SpFet1 + "',SPFET2='" + SpFet2 + "',PRODUCER='" + Producer + "',PACKER='" + Packer + "',GRAPHICS='" + Graphics + "', "
                        strSql = strSql + "STRUCTURES='" + Structures + "',FORMULA='" + Formula + "',MANUFACTORY='" + Manufactory + "',PACKAGING='" + Packaging + "',SKU='" + Sku + "' WHERE MODELID=" + ModelId + " AND PROJECTID=" + ProjId + " "
                    Else
                        strSql = String.Empty
                        strSql = "DELETE FROM MODELDETAILSTEMP WHERE MODELID=" + ModelId + " AND PROJECTID=" + ProjId + ""
                    End If

                    'strSql = "UPDATE  MODELDETAILSTEMP SET PRODPACK='" + ProdPack + "',PACKSIZE='" + PackSize + "',PACKTYPE='" + PackType + "',GEOGRAPHY='" + Geography + "', "
                    'strSql = strSql + "VCHAIN='" + VChain + "',SPFET1='" + SpFet1 + "',SPFET2='" + SpFet2 + "' "
                    'strSql = strSql + "WHERE MODELID=" + ModelId + " AND PROJECTID=" + ProjId + " "
                End If


                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:EditProjDetails:" + ex.Message.ToString())

            End Try
        End Sub

        Public Sub DeleteModelByType(ByVal ProjId As String, ByVal ModelId As String, ByVal Type As String, ByVal Value As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM MODELDETAILSTEMP WHERE PROJECTID=" + ProjId + " AND MODELID=" + ModelId + " "
                If Type = "PROD" Then
                    strSql = strSql + "AND PRODPACK='" + Value + "'"
                ElseIf Type = "SIZE" Then
                    strSql = strSql + "AND PACKSIZE='" + Value + "'"
                ElseIf Type = "TYPE" Then
                    strSql = strSql + "AND PACKTYPE='" + Value + "'"
                ElseIf Type = "GEOG" Then
                    strSql = strSql + "AND GEOGRAPHY='" + Value + "'"
                ElseIf Type = "VCHAIN" Then
                    strSql = strSql + "AND VCHAIN='" + Value + "'"
                ElseIf Type = "SPFET1" Then
                    strSql = strSql + "AND SPFET1='" + Value + "'"
                ElseIf Type = "SPFET2" Then
                    strSql = strSql + "AND SPFET2='" + Value + "'"
                ElseIf Type = "PRODU" Then
                    strSql = strSql + "AND PRODUCER='" + Value + "'"
                ElseIf Type = "PACKER" Then
                    strSql = strSql + "AND PACKER='" + Value + "'"
                ElseIf Type = "GRAPH" Then
                    strSql = strSql + "AND GRAPHICS='" + Value + "'"
                ElseIf Type = "STRUCT" Then
                    strSql = strSql + "AND STRUCTURES='" + Value + "'"
                ElseIf Type = "FORMULA" Then
                    strSql = strSql + "AND FORMULA='" + Value + "'"
                ElseIf Type = "MANUF" Then
                    strSql = strSql + "AND MANUFACTORY='" + Value + "'"
                ElseIf Type = "PACKG" Then
                    strSql = strSql + "AND PACKAGING='" + Value + "'"
                ElseIf Type = "SKU" Then
                    strSql = strSql + "AND SKU='" + Value + "'"
                End If

                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:EditProjDetails:" + ex.Message.ToString())

            End Try
        End Sub

        Public Function DeleteModelDetails(ByVal ProjId As String, ByVal UserId As String) As Boolean
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM MODELDETAILSTEMP WHERE PROJECTID=" + ProjId + ""

                dbUtil.UpIns(strSql, SavvyConnection)
                Return True
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:EditProjDetails:" + ex.Message.ToString())
                Return False
            End Try
        End Function

        Public Sub ActivateModel(ByVal ModelId As String, ByVal ProjectId As String, ByVal IsActive As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE MODELDETAILSTEMP SET ISACTIVE='" + IsActive + "' "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND MODELID=" + ModelId + ""
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception

            End Try
        End Sub

        Public Sub EditModelDetail(ByVal ModelId As String, ByVal ProjectId As String, ByVal Value As String, ByVal Type As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE MODELDETAILSTEMP SET "
                If Type = "PRODUCER" Then
                    strSql = strSql + "PRODUCER='" + Value + "' "
                ElseIf Type = "PACKER" Then
                    strSql = strSql + "PACKER='" + Value + "' "
                ElseIf Type = "GRAPHICS" Then
                    strSql = strSql + "GRAPHICS='" + Value + "' "
                ElseIf Type = "STRUCT" Then
                    strSql = strSql + "STRUCTURES='" + Value + "' "
                ElseIf Type = "FORMULA" Then
                    strSql = strSql + "FORMULA='" + Value + "' "
                ElseIf Type = "MANUF" Then
                    strSql = strSql + "MANUFACTORY='" + Value + "' "
                ElseIf Type = "PACKG" Then
                    strSql = strSql + "PACKAGING='" + Value + "' "
                ElseIf Type = "SKU" Then
                    strSql = strSql + "SKU='" + Value + "' "
                End If
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND MODELID=" + ModelId + ""
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception

            End Try
        End Sub

        Public Sub UpdateModelDetail(ByVal ProjectId As String, ByVal NewValue As String, ByVal OldValue As String, ByVal Type As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE MODELDETAILSTEMP SET "
                If Type = "PROD" Then
                    strSql = strSql + "PRODPACK='" + NewValue + "' WHERE PRODPACK='" + OldValue + "'"
                ElseIf Type = "SIZE" Then
                    strSql = strSql + "PACKSIZE='" + NewValue + "' WHERE PACKSIZE='" + OldValue + "'"
                ElseIf Type = "TYPE" Then
                    strSql = strSql + "PACKTYPE='" + NewValue + "' WHERE PACKTYPE='" + OldValue + "'"
                ElseIf Type = "GEOG" Then
                    strSql = strSql + "GEOGRAPHY='" + NewValue + "' WHERE GEOGRAPHY='" + OldValue + "'"
                ElseIf Type = "VCHAIN" Then
                    strSql = strSql + "VCHAIN='" + NewValue + "' WHERE VCHAIN='" + OldValue + "'"
                ElseIf Type = "SPFET1" Then
                    strSql = strSql + "SPFET1='" + NewValue + "' WHERE SPFET1='" + OldValue + "'"
                ElseIf Type = "SPFET2" Then
                    strSql = strSql + "SPFET2='" + NewValue + "' WHERE SPFET2='" + OldValue + "'"
                End If
                strSql = strSql + "AND PROJECTID=" + ProjectId + " "
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Producer Details"

        Public Sub AddProducerDetails(ByVal ProjId As String, ByVal Name As String, ByVal Location As String, ByVal Capacity As String, ByVal Lines As String, ByVal Information As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Dim ProducerId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(PRODUCERID) PRODUCERID FROM PRODUCERDETAILS WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("PRODUCERID").ToString() = "" Then
                    ProducerId = 1
                Else
                    ProducerId = ds.Tables(0).Rows(0).Item("PRODUCERID") + 1
                End If

                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO PRODUCERDETAILS (PRODUCERID,PROJECTID,NAME,LOCATION,CAPACITY,LINES,INFORMATION) "
                strSql = strSql + "SELECT " + ProducerId.ToString() + "," + ProjId + ",'" + Name + "','" + Location + "','" + Capacity + "','" + Lines + "','" + Information + "' FROM DUAL "
                strSql = strSql + "WHERE NOT EXISTS (SELECT 1 FROM PRODUCERDETAILS WHERE "
                If Name = "" Then
                    strSql = strSql + "NAME IS NULL "
                Else
                    strSql = strSql + "NAME='" + Name + "' "
                End If
                If Location = "" Then
                    strSql = strSql + "AND LOCATION IS NULL "
                Else
                    strSql = strSql + "AND LOCATION='" + Location + "' "
                End If
                If Capacity = "" Then
                    strSql = strSql + "AND CAPACITY IS NULL "
                Else
                    strSql = strSql + "AND CAPACITY='" + Capacity + "' "
                End If
                If Lines = "" Then
                    strSql = strSql + "AND LINES IS NULL "
                Else
                    strSql = strSql + "AND LINES='" + Lines + "' "
                End If
                If Information = "" Then
                    strSql = strSql + "AND INFORMATION IS NULL "
                Else
                    strSql = strSql + "AND INFORMATION='" + Information + "' "
                End If

                strSql = strSql + "AND PROJECTID=" + ProjId + " )"
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateProducerDetails(ByVal Producerid As String, ByVal ProjId As String, ByVal Name As String, ByVal Location As String, ByVal Capacity As String, ByVal Lines As String, ByVal Information As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE PRODUCERDETAILS SET NAME='" + Name + "',LOCATION='" + Location + "',CAPACITY='" + Capacity + "', "
                strSql = strSql + "LINES='" + Lines + "',INFORMATION='" + Information + "' WHERE PROJECTID=" + ProjId + " AND PRODUCERID=" + Producerid + ""

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteProducerDetails(ByVal Producerid As String, ByVal ProjId As String, ByVal Name As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM PRODUCERDETAILS WHERE PROJECTID=" + ProjId + " AND PRODUCERID=" + Producerid + " AND NAME='" + Name + "'"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:DeleteProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "Packer Details"

        Public Sub AddPackerDetails(ByVal ProjId As String, ByVal Name As String, ByVal Location As String, ByVal Capacity As String, ByVal Lines As String, ByVal Information As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Dim PackerId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(PACKERID) PACKERID FROM PACKERDETAILS WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("PACKERID").ToString() = "" Then
                    PackerId = 1
                Else
                    PackerId = ds.Tables(0).Rows(0).Item("PACKERID") + 1
                End If

                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO PACKERDETAILS (PACKERID,PROJECTID,NAME,LOCATION,CAPACITY,LINES,INFORMATION) "
                strSql = strSql + "SELECT " + PackerId.ToString() + "," + ProjId + ",'" + Name + "','" + Location + "','" + Capacity + "','" + Lines + "','" + Information + "' FROM DUAL "
                strSql = strSql + "WHERE NOT EXISTS (SELECT 1 FROM PACKERDETAILS WHERE "
                If Name = "" Then
                    strSql = strSql + "NAME IS NULL "
                Else
                    strSql = strSql + "NAME='" + Name + "' "
                End If
                If Location = "" Then
                    strSql = strSql + "AND LOCATION IS NULL "
                Else
                    strSql = strSql + "AND LOCATION='" + Location + "' "
                End If
                If Capacity = "" Then
                    strSql = strSql + "AND CAPACITY IS NULL "
                Else
                    strSql = strSql + "AND CAPACITY='" + Capacity + "' "
                End If
                If Lines = "" Then
                    strSql = strSql + "AND LINES IS NULL "
                Else
                    strSql = strSql + "AND LINES='" + Lines + "' "
                End If
                If Information = "" Then
                    strSql = strSql + "AND INFORMATION IS NULL "
                Else
                    strSql = strSql + "AND INFORMATION='" + Information + "' "
                End If
                strSql = strSql + "AND PROJECTID=" + ProjId + " )"
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdatePackerDetails(ByVal PackerId As String, ByVal ProjId As String, ByVal Name As String, ByVal Location As String, ByVal Capacity As String, ByVal Lines As String, ByVal Information As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()

            Try
                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "UPDATE PACKERDETAILS SET NAME='" + Name + "',LOCATION='" + Location + "',CAPACITY='" + Capacity + "', "
                strSql = strSql + "LINES='" + Lines + "',INFORMATION='" + Information + "' WHERE PROJECTID=" + ProjId + " AND PACKERID=" + PackerId + ""

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdatePackerDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeletePackerDetails(ByVal PackerId As String, ByVal ProjId As String, ByVal Name As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()

            Try
                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "DELETE FROM PACKERDETAILS WHERE PROJECTID=" + ProjId + " AND PACKERID=" + PackerId + " AND NAME='" + Name + "'"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdatePackerDetails:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Graphics Details"

        Public Sub AddGraphicsDetails(ByVal ProjId As String, ByVal Name As String, ByVal Design As String, ByVal Colors As String, ByVal VolDesg As String, ByVal Information As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Dim GraphicsId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(GRAPHICSID) GRAPHICSID FROM GRAPHICSDETAILS WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("GRAPHICSID").ToString() = "" Then
                    GraphicsId = 1
                Else
                    GraphicsId = ds.Tables(0).Rows(0).Item("GRAPHICSID") + 1
                End If

                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO GRAPHICSDETAILS (GRAPHICSID,PROJECTID,NAME,DESIGN,COLORS,VOLDESIGN,INFORMATION) "
                strSql = strSql + "SELECT " + GraphicsId.ToString() + "," + ProjId + ",'" + Name + "','" + Design + "','" + Colors + "','" + VolDesg + "','" + Information + "' FROM DUAL "
                strSql = strSql + "WHERE NOT EXISTS (SELECT 1 FROM GRAPHICSDETAILS WHERE "
                If Name = "" Then
                    strSql = strSql + "NAME IS NULL "
                Else
                    strSql = strSql + "NAME='" + Name + "' "
                End If
                If Design = "" Then
                    strSql = strSql + "AND DESIGN IS NULL "
                Else
                    strSql = strSql + "AND DESIGN='" + Design + "' "
                End If
                If Colors = "" Then
                    strSql = strSql + "AND COLORS IS NULL "
                Else
                    strSql = strSql + "AND COLORS='" + Colors + "' "
                End If
                If VolDesg = "" Then
                    strSql = strSql + "AND VOLDESIGN IS NULL "
                Else
                    strSql = strSql + "AND VOLDESIGN='" + VolDesg + "' "
                End If
                If Information = "" Then
                    strSql = strSql + "AND INFORMATION IS NULL "
                Else
                    strSql = strSql + "AND INFORMATION='" + Information + "' "
                End If

                strSql = strSql + "AND PROJECTID=" + ProjId + " )"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateGraphicDetails(ByVal GraphicId As String, ByVal ProjId As String, ByVal Name As String, ByVal Design As String, ByVal Color As String, ByVal VolDes As String, ByVal Information As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()

            Try
                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "UPDATE GRAPHICSDETAILS SET NAME='" + Name + "',DESIGN='" + Design + "',COLORS='" + Color + "', "
                strSql = strSql + "VOLDESIGN='" + VolDes + "',INFORMATION='" + Information + "' WHERE PROJECTID=" + ProjId + " AND GRAPHICSID=" + GraphicId + ""

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateGraphicDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteGraphicDetails(ByVal GraphicId As String, ByVal ProjId As String, ByVal Name As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM GRAPHICSDETAILS WHERE PROJECTID=" + ProjId + " AND GRAPHICSID=" + GraphicId + " AND NAME='" + Name + "'"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:DeleteGraphicDetails:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Structure Details"

        Public Sub AddStructureDetails(ByVal ProjId As String, ByVal Name As String, ByVal Size As String, ByVal Mat() As String, ByVal Dims() As String, ByVal Den() As String, ByVal Price() As String, ByVal Info() As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Dim StructureId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(STRUCTUREID) STRUCTUREID FROM STRUCTUREDETAILS WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("STRUCTUREID").ToString() = "" Then
                    StructureId = 1
                Else
                    StructureId = ds.Tables(0).Rows(0).Item("STRUCTUREID") + 1
                End If

                'Insert Data into STRUCTUREDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO STRUCTUREDETAILS (STRUCTUREID,PROJECTID,NAME,PACKSIZE, "
                For i = 1 To 10
                    strSql = strSql + "MAT" + i.ToString() + ",DIM" + i.ToString() + ",DEN" + i.ToString() + ",PRICE" + i.ToString() + ",INFO" + i.ToString() + ","
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + ") "

                strSql = strSql + "SELECT " + StructureId.ToString() + "," + ProjId + ",'" + Name + "','" + Size + "',"
                For i = 0 To 9
                    strSql = strSql + "'" + Mat(i).Replace("'", "''") + "','" + Dims(i).Replace("'", "''") + "','" + Den(i).Replace("'", "''") + "','" + Price(i).Replace("'", "''") + "','" + Info(i).Replace("'", "''") + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " FROM DUAL WHERE NOT EXISTS (SELECT 1 FROM STRUCTUREDETAILS WHERE "
                If Name = "" Then
                    strSql = strSql + "NAME IS NULL "
                Else
                    strSql = strSql + "NAME='" + Name + "' "
                End If
                strSql = strSql + "AND PROJECTID=" + ProjId + " )"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateStructureDetails(ByVal StructureId As String, ByVal ProjId As String, ByVal Name As String, ByVal Size As String, ByVal Mat() As String, ByVal Dims() As String, ByVal Den() As String, ByVal Price() As String, ByVal Info() As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE STRUCTUREDETAILS SET NAME='" + Name + "',PACKSIZE='" + Size + "',"
                For i = 1 To 10
                    strSql = strSql + "MAT" + i.ToString() + "='" + Mat(i - 1).Replace("'", "''") + "',DIM" + i.ToString() + "='" + Dims(i - 1).Replace("'", "''") + "',DEN" + i.ToString() + "='" + Den(i - 1).Replace("'", "''") + "',"
                    strSql = strSql + "PRICE" + i.ToString() + "='" + Price(i - 1).Replace("'", "''") + "',INFO" + i.ToString() + "='" + Info(i - 1).Replace("'", "''") + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " WHERE PROJECTID=" + ProjId + " AND STRUCTUREID=" + StructureId + ""

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteStructureDetails(ByVal StuctureId As String, ByVal ProjId As String, ByVal Name As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM STRUCTUREDETAILS WHERE PROJECTID=" + ProjId + " AND STRUCTUREID=" + StuctureId + " AND NAME='" + Name + "'"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:DeleteProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "Formula Details"

        Public Sub AddFormulaDetails(ByVal ProjId As String, ByVal Name As String, ByVal Size As String, ByVal Mat() As String, ByVal Vol() As String, ByVal Price() As String, ByVal Info() As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Dim FormulaId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(FORMULAID) FORMULAID FROM FORMULADETAILS WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("FORMULAID").ToString() = "" Then
                    FormulaId = 1
                Else
                    FormulaId = ds.Tables(0).Rows(0).Item("FORMULAID") + 1
                End If

                'Insert Data into STRUCTUREDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO FORMULADETAILS (FORMULAID,PROJECTID,NAME,PACKSIZE, "
                For i = 1 To 10
                    strSql = strSql + "MAT" + i.ToString() + ",VOL" + i.ToString() + ",PRICE" + i.ToString() + ",INFO" + i.ToString() + ","
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + ") "

                strSql = strSql + "SELECT " + FormulaId.ToString() + "," + ProjId + ",'" + Name + "','" + Size + "',"
                For i = 0 To 9
                    strSql = strSql + "'" + Mat(i).Replace("'", "''") + "','" + Vol(i).Replace("'", "''") + "','" + Price(i).Replace("'", "''") + "','" + Info(i).Replace("'", "''") + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " FROM DUAL WHERE NOT EXISTS (SELECT 1 FROM FORMULADETAILS WHERE "
                If Name = "" Then
                    strSql = strSql + "NAME IS NULL "
                Else
                    strSql = strSql + "NAME='" + Name + "' "
                End If
                strSql = strSql + "AND PROJECTID=" + ProjId + " )"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddFormulaDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateFormulaDetails(ByVal FormulaId As String, ByVal ProjId As String, ByVal Name As String, ByVal Size As String, ByVal Mat() As String, ByVal Vol() As String, ByVal Price() As String, ByVal Info() As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE FORMULADETAILS SET NAME='" + Name + "',PACKSIZE='" + Size + "',"
                For i = 1 To 10
                    strSql = strSql + "MAT" + i.ToString() + "='" + Mat(i - 1).Replace("'", "''") + "',VOL" + i.ToString() + "='" + Vol(i - 1).Replace("'", "''") + "',"
                    strSql = strSql + "PRICE" + i.ToString() + "='" + Price(i - 1).Replace("'", "''") + "',INFO" + i.ToString() + "='" + Info(i - 1).Replace("'", "''") + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " WHERE PROJECTID=" + ProjId + " AND FORMULAID=" + FormulaId + ""

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateFormulaDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteFormulaDetails(ByVal FormulaId As String, ByVal ProjId As String, ByVal Name As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM FORMULADETAILS WHERE PROJECTID=" + ProjId + " AND FORMULAID=" + FormulaId + " AND NAME='" + Name + "'"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:DeleteProducerDetails:" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "Manufactory Details"

        Public Sub AddManufactoryDetails(ByVal ProjId As String, ByVal Name As String, ByVal OSize As String, ByVal RSize As String, ByVal PP() As String, ByVal Mac() As String, ByVal Rate() As String, ByVal TPut() As String, ByVal DTime() As String, ByVal Waste() As String, ByVal CC() As String, ByVal Info() As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Dim ManufactoryId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(MANUFACTORYID) MANUFACTORYID FROM MANUFACTORYDETAILS WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("MANUFACTORYID").ToString() = "" Then
                    ManufactoryId = 1
                Else
                    ManufactoryId = ds.Tables(0).Rows(0).Item("MANUFACTORYID") + 1
                End If

                'Insert Data into STRUCTUREDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO MANUFACTORYDETAILS (MANUFACTORYID,PROJECTID,NAME,ORDERSIZE,RUNSIZE, "
                For i = 1 To 10
                    strSql = strSql + "PP" + i.ToString() + ",MAC" + i.ToString() + ",RATE" + i.ToString() + ",TPUT" + i.ToString() + ","
                    strSql = strSql + "DTIME" + i.ToString() + ",WASTE" + i.ToString() + ",CC" + i.ToString() + ",INFO" + i.ToString() + ","
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + ") "

                strSql = strSql + "SELECT " + ManufactoryId.ToString() + "," + ProjId + ",'" + Name + "','" + OSize + "','" + RSize + "',"
                For i = 0 To 9
                    strSql = strSql + "'" + PP(i).Replace("'", "''") + "','" + Mac(i).Replace("'", "''") + "','" + Rate(i).Replace("'", "''") + "','" + TPut(i).Replace("'", "''") + "',"
                    strSql = strSql + "'" + DTime(i).Replace("'", "''") + "','" + Waste(i).Replace("'", "''")+ "','" + CC(i).Replace("'", "''") + "','" + Info(i).Replace("'", "''") + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " FROM DUAL WHERE NOT EXISTS (SELECT 1 FROM MANUFACTORYDETAILS WHERE "
                If Name = "" Then
                    strSql = strSql + "NAME IS NULL "
                Else
                    strSql = strSql + "NAME='" + Name + "' "
                End If
                strSql = strSql + "AND PROJECTID=" + ProjId + " )"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddManufactoryDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateManufactoryDetails(ByVal ManufactoryId As String, ByVal ProjId As String, ByVal Name As String, ByVal OSize As String, ByVal RSize As String, ByVal PP() As String, ByVal Mac() As String, ByVal Rate() As String, ByVal TPut() As String, ByVal DTime() As String, ByVal Waste() As String, ByVal CC() As String, ByVal Info() As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE MANUFACTORYDETAILS SET NAME='" + Name + "',ORDERSIZE='" + OSize + "',RUNSIZE='" + RSize + "',"
                For i = 1 To 10
                    strSql = strSql + "PP" + i.ToString() + "='" + PP(i - 1).Replace("'", "''") + "',MAC" + i.ToString() + "='" + Mac(i - 1).Replace("'", "''") + "',"
                    strSql = strSql + "RATE" + i.ToString() + "='" + Rate(i - 1).Replace("'", "''") + "',TPUT" + i.ToString() + "='" + TPut(i - 1).Replace("'", "''") + "',"
                    strSql = strSql + "DTIME" + i.ToString() + "='" + DTime(i - 1).Replace("'", "''") + "',WASTE" + i.ToString() + "='" + Waste(i - 1) .Replace("'", "''")+ "',"
                    strSql = strSql + "CC" + i.ToString() + "='" + CC(i - 1).Replace("'", "''") + "',INFO" + i.ToString() + "='" + Info(i - 1).Replace("'", "''") + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " WHERE PROJECTID=" + ProjId + " AND MANUFACTORYID=" + ManufactoryId + ""

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateManufactoryDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteManufactoryDetails(ByVal ManufactoryId As String, ByVal ProjId As String, ByVal Name As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM MANUFACTORYDETAILS WHERE PROJECTID=" + ProjId + " AND MANUFACTORYID=" + ManufactoryId + " AND NAME='" + Name + "'"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:DeleteManufactoryDetails:" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "Packaging Details"

        Public Sub AddPackagingDetails(ByVal ProjId As String, ByVal Name As String, ByVal OSize As String, ByVal RSize As String, ByVal PP() As String, ByVal Mac() As String, ByVal Rate() As String, ByVal TPut() As String, ByVal DTime() As String, ByVal Waste() As String, ByVal CC() As String, ByVal Info() As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Dim PackagingId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(PACKAGINGID) PACKAGINGID FROM PACKAGINGDETAILS WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("PACKAGINGID").ToString() = "" Then
                    PackagingId = 1
                Else
                    PackagingId = ds.Tables(0).Rows(0).Item("PACKAGINGID") + 1
                End If

                'Insert Data into STRUCTUREDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO PACKAGINGDETAILS (PACKAGINGID,PROJECTID,NAME,ORDERSIZE,RUNSIZE, "
                For i = 1 To 10
                    strSql = strSql + "PP" + i.ToString() + ",MAC" + i.ToString() + ",RATE" + i.ToString() + ",TPUT" + i.ToString() + ","
                    strSql = strSql + "DTIME" + i.ToString() + ",WASTE" + i.ToString() + ",CC" + i.ToString() + ",INFO" + i.ToString() + ","
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + ") "

                strSql = strSql + "SELECT " + PackagingId.ToString() + "," + ProjId + ",'" + Name + "','" + OSize + "','" + RSize + "',"
                For i = 0 To 9
                    strSql = strSql + "'" + PP(i).Replace("'", "''") + "','" + Mac(i).Replace("'", "''") + "','" + Rate(i).Replace("'", "''") + "','" + TPut(i).Replace("'", "''") + "',"
                    strSql = strSql + "'" + DTime(i).Replace("'", "''") + "','" + Waste(i).Replace("'", "''") + "','" + CC(i).Replace("'", "''") + "','" + Info(i).Replace("'", "''") + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " FROM DUAL WHERE NOT EXISTS (SELECT 1 FROM PACKAGINGDETAILS WHERE "
                If Name = "" Then
                    strSql = strSql + "NAME IS NULL "
                Else
                    strSql = strSql + "NAME='" + Name + "' "
                End If
                strSql = strSql + "AND PROJECTID=" + ProjId + " )"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddPackagingDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdatePackagingDetails(ByVal PackagingId As String, ByVal ProjId As String, ByVal Name As String, ByVal OSize As String, ByVal RSize As String, ByVal PP() As String, ByVal Mac() As String, ByVal Rate() As String, ByVal TPut() As String, ByVal DTime() As String, ByVal Waste() As String, ByVal CC() As String, ByVal Info() As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE PACKAGINGDETAILS SET NAME='" + Name + "',ORDERSIZE='" + OSize + "',RUNSIZE='" + RSize + "',"
                For i = 1 To 10
                    strSql = strSql + "PP" + i.ToString() + "='" + PP(i - 1).Replace("'", "''") + "',MAC" + i.ToString() + "='" + Mac(i - 1).Replace("'", "''") + "',"
                    strSql = strSql + "RATE" + i.ToString() + "='" + Rate(i - 1).Replace("'", "''") + "',TPUT" + i.ToString() + "='" + TPut(i - 1).Replace("'", "''") + "',"
                    strSql = strSql + "DTIME" + i.ToString() + "='" + DTime(i - 1).Replace("'", "''") + "',WASTE" + i.ToString() + "='" + Waste(i - 1).Replace("'", "''") + "',"
                    strSql = strSql + "CC" + i.ToString() + "='" + CC(i - 1).Replace("'", "''") + "',INFO" + i.ToString() + "='" + Info(i - 1).Replace("'", "''") + "',"
                Next
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " WHERE PROJECTID=" + ProjId + " AND PACKAGINGID=" + PackagingId + ""

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdatePackagingDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeletePackagingDetails(ByVal PackagingId As String, ByVal ProjId As String, ByVal Name As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM PACKAGINGDETAILS WHERE PROJECTID=" + ProjId + " AND PACKAGINGID=" + PackagingId + " AND NAME='" + Name + "'"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:DeletePackagingDetails:" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "SKU Details"

        Public Sub AddSKUDetails(ByVal ProjId As String, ByVal Name As String, ByVal SKU As String, ByVal VolSKU As String, ByVal Information As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Dim SKUId As New Integer
            Try
                'Get ModelId Sequence
                strSql = "SELECT MAX(SKUID) SKUID FROM SKUDETAILS WHERE PROJECTID=" + ProjId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows(0).Item("SKUID").ToString() = "" Then
                    SKUId = 1
                Else
                    SKUId = ds.Tables(0).Rows(0).Item("SKUID") + 1
                End If

                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO SKUDETAILS (SKUID,PROJECTID,NAME,SKU,VOLSKU,INFORMATION) "
                strSql = strSql + "SELECT " + SKUId.ToString() + "," + ProjId + ",'" + Name + "','" + SKU + "','" + VolSKU + "','" + Information + "' FROM DUAL "
                strSql = strSql + "WHERE NOT EXISTS (SELECT 1 FROM SKUDETAILS WHERE "
                If Name = "" Then
                    strSql = strSql + "NAME IS NULL "
                Else
                    strSql = strSql + "NAME='" + Name + "' "
                End If
                If SKU = "" Then
                    strSql = strSql + "AND SKU IS NULL "
                Else
                    strSql = strSql + "AND SKU='" + SKU + "' "
                End If
                
                If VolSKU = "" Then
                    strSql = strSql + "AND VOLSKU IS NULL "
                Else
                    strSql = strSql + "AND VOLSKU='" + VolSKU + "' "
                End If
                If Information = "" Then
                    strSql = strSql + "AND INFORMATION IS NULL "
                Else
                    strSql = strSql + "AND INFORMATION='" + Information + "' "
                End If

                strSql = strSql + "AND PROJECTID=" + ProjId + " )"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddSKUDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateSKUDetails(ByVal SKUId As String, ByVal ProjId As String, ByVal Name As String, ByVal SKU As String, ByVal VolSKU As String, ByVal Information As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()

            Try
                'Insert Data into MODELDETAILS table
                strSql = String.Empty
                strSql = "UPDATE SKUDETAILS SET NAME='" + Name + "',SKU='" + SKU + "', "
                strSql = strSql + "VOLSKU='" + VolSKU + "',INFORMATION='" + Information + "' WHERE PROJECTID=" + ProjId + " AND SKUID=" + SKUId + ""

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateSKUDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteSKUDetails(ByVal SKUId As String, ByVal ProjId As String, ByVal Name As String)
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "DELETE FROM SKUDETAILS WHERE PROJECTID=" + ProjId + " AND SKUID=" + SKUId + " AND NAME='" + Name + "'"

                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:DeleteSKUDetails:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Activity Log"
        Public Function InsertLog(ByVal UserId As String, ByVal PageName As String, ByVal ActivityType As String) As Integer

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim count As Integer

            Try
                StrSql = "SELECT SEQLOGINID.NEXTVAL FROM DUAL"
                count = odbUtil.FillData(StrSql, SavvyConnection)

                'Report Details
                StrSql = String.Empty
                StrSql = "INSERT INTO ACTIVITYLOG (LOGID,USERID,LOGINCOUNT,USERSEQUENCE,PAGENAME,ACTIVITYDETAILS,ACTIVITYTIME)  "
                StrSql = StrSql + "VALUES( SEQACTIVITYLOGID.NEXTVAL," + UserId + "," + count.ToString() + ",1,'" + PageName + "','" + ActivityType + "',SYSDATE)"
                odbUtil.UpIns(StrSql, SavvyConnection)

                Return count
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertLog:" + ex.Message.ToString())
            End Try

        End Function
        Public Sub InsertLog1(ByVal UserId As String, ByVal PageName As String, ByVal ActivityType As String, ByVal ProjId As String, ByVal logCount As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim userseq As Integer

            Try
                StrSql = "SELECT NVL(MAX(USERSEQUENCE),1) USERSEQUENCE FROM ACTIVITYLOG WHERE LOGINCOUNT=" + logCount
                userseq = odbUtil.FillData(StrSql, SavvyConnection) + 1

                'log Details
                StrSql = String.Empty
                StrSql = "INSERT INTO ACTIVITYLOG (LOGID,  "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "LOGINCOUNT, "
                StrSql = StrSql + "USERSEQUENCE, "
                StrSql = StrSql + "PAGENAME, "
                StrSql = StrSql + "ACTIVITYDETAILS, "
                StrSql = StrSql + "ACTIVITYTIME, "
                StrSql = StrSql + "PROJECTID) "

                StrSql = StrSql + "VALUES( SEQACTIVITYLOGID.NEXTVAL,  "
                StrSql = StrSql + UserId + ", "
                StrSql = StrSql + logCount + ", "
                StrSql = StrSql + userseq.ToString() + " , "
                StrSql = StrSql + "'" + PageName + "', "
                StrSql = StrSql + "'" + ActivityType + "', "
                StrSql = StrSql + "sysdate, "
                StrSql = StrSql + "'" + ProjId + " ') "

                odbUtil.UpIns(StrSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertLog1:" + ex.Message.ToString())
            End Try

        End Sub

	Public Sub EditInsertLog(ByVal UserId As String, ByVal PageId As String, ByVal Title As String, ByVal Seq As String, ByVal Details As String, ByVal ProjId As String, ByVal logCount As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim userseq As Integer

            Try
                StrSql = "SELECT NVL(MAX(USERSEQUENCE),0) USERSEQUENCE FROM EDITACTIVITYLOG WHERE LOGINCOUNT=" + logCount
                userseq = odbUtil.FillData(StrSql, SavvyConnection) + 1

                'Log Details
                StrSql = String.Empty
                StrSql = "INSERT INTO EDITACTIVITYLOG (LOGID,  "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "PROJECTID, "
                StrSql = StrSql + "LOGINCOUNT, "
                StrSql = StrSql + "PAGEID, "
                StrSql = StrSql + "USERSEQUENCE, "
                StrSql = StrSql + "VALSEQUENCE,"
                StrSql = StrSql + "ACTIVITYTITLE, "
                StrSql = StrSql + "ACTIVITYDETAILS, "
                StrSql = StrSql + "ACTIVITYTIME) "

                StrSql = StrSql + "VALUES(SEQEACTIVITYLOGID.NEXTVAL,  "
                StrSql = StrSql + UserId + ", "
                StrSql = StrSql + "'" + ProjId + " ', "
                StrSql = StrSql + logCount + ", "
                StrSql = StrSql + PageId + ", "
                StrSql = StrSql + userseq.ToString() + " , "
                StrSql = StrSql + Seq.ToString() + " , "
                StrSql = StrSql + "'" + Title + "', "
                StrSql = StrSql + "'" + Details + "', "
                StrSql = StrSql + "sysdate) "

                odbUtil.UpIns(StrSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:EditInsertLog:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Save the Grid"
        Public Sub InsertSaveProjectDetails(ByVal SequenceId As String, ByVal ProjectId As String, ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "INSERT INTO SAVEPROJECTDETAILS (SPDETAILSID,SEQUENCEID,PROJECTID,USERID,LASTUPDATEDON) "
                StrSql = StrSql + "SELECT SEQSPDETAILSID.NEXTVAL," + SequenceId + "," + ProjectId + "," + UserId + ",SYSDATE FROM DUAL "

                odbUtil.UpIns(StrSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertSaveProjectDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub InsertSaveModelDetails(ByVal SequenceId As String, ByVal ProjectId As String, ByVal ModelId As String, ByVal ProdPack As String, ByVal PackSize As String, ByVal PackType As String, ByVal Geography As String, ByVal VChain As String, ByVal SpFet1 As String, _
                                          ByVal Spfet2 As String, ByVal Producer As String, ByVal Packer As String, ByVal Graphics As String, ByVal Structures As String, ByVal Formula As String, ByVal Manufactory As String, ByVal Packaging As String, ByVal SKU As String, ByVal IsActive As String)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "INSERT INTO SAVEMODELDETAILS (SMDETAILSID,SEQUENCEID,PROJECTID,MODELID,PRODPACK,PACKSIZE, "
                StrSql = StrSql + "PACKTYPE,GEOGRAPHY,VCHAIN,SPFET1,SPFET2,PRODUCER,PACKER,GRAPHICS,STRUCTURES,FORMULA,MANUFACTORY,PACKAGING,SKU,ISACTIVE) "
                StrSql = StrSql + "SELECT SEQSMDETAILSID.NEXTVAL," + SequenceId + "," + ProjectId + "," + ModelId + ",'" + ProdPack + "','" + PackSize + "','" + PackType + "', "
                StrSql = StrSql + "'" + Geography + "','" + VChain + "','" + SpFet1 + "','" + Spfet2 + "','" + Producer + "','" + Packer + "','" + Graphics + "',"
                StrSql = StrSql + "'" + Structures + "','" + Formula + "','" + Manufactory + "','" + Packaging + "','" + SKU + "','" + IsActive + "' FROM DUAL"
                odbUtil.UpIns(StrSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertSaveModelDetails:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Edit"
        Public Sub UpdateProductName(ByVal Name() As String, ByVal CatId() As String, ByVal ProjCatId() As String, ByVal count As Integer, ByVal ProjectId As String)
            Dim DtsCount As New DataSet()
            Dim objGetData As New E1GetData.Selectdata()
            Dim seqCount As Integer = 0
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Dim i As Integer = 0
            Try
                For i = 0 To count - 1
                    If Name(i) <> "" Then
                        StrSqlUpadate = "UPDATE PROJECTCATEGORY SET "
                        StrSqlUpadate = StrSqlUpadate + " VALUE='" + Name(i).ToString() + "' "
                        StrSqlUpadate = StrSqlUpadate + " WHERE CATEGORYID= " + CatId(i).ToString() + ""
                        StrSqlUpadate = StrSqlUpadate + " AND PROJCATEGORYID= " + ProjCatId(i).ToString() + ""
                        StrSqlUpadate = StrSqlUpadate + " AND PROJECTID= " + ProjectId + ""
                        odButil.UpIns(StrSqlUpadate, SavvyConnection)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateProductName:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteProducts(ByVal ProjCatId As String)
            Dim odButil As New DBUtil()
            Dim StrSqlUpadate As String = ""

            Try
                StrSqlUpadate = " DELETE FROM PROJECTCATEGORY WHERE PROJCATEGORYID=" + ProjCatId
                odButil.UpIns(StrSqlUpadate, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("E1Update:DeleteGroup:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Upload File"
        Public Sub AddFileDetails(ByVal ProjId As String, ByVal FileName As String, ByVal FilePath As String, ByVal Type As String, ByVal UserId As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                'Insert Data into STRUCTUREDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO SPECSHEETDETAILS (PROJECTID,USERID,FILENAME,FILEPATH,TYPE) "
                strSql = strSql + "SELECT " + ProjId + "," + UserId + ",'" + FileName + "','" + FilePath + "','" + Type + "' FROM DUAL"
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddFileDetails:" + ex.Message.ToString())
            End Try
        End Sub
#End Region


#Region "Benefits"

        Public Function BenefitsUpdate(ByVal ProjectId As String, ByVal UserId As String, ByVal QuanBn As String, ByVal QualBn As String) As Boolean
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""

                StrSql = "INSERT INTO PROJECTBENEFITS  "
                StrSql = StrSql + "(PROJECTID,USERID,QUANTBNF,QUALBNF,LASTUPDATEDON) "
                StrSql = StrSql + "SELECT " + ProjectId + " ," + UserId + ",'" + QuanBn + "','" + QualBn + "',SYSDATE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM PROJECTBENEFITS "
                StrSql = StrSql + "WHERE PROJECTID = " + ProjectId + " "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, SavvyConnection)

                StrSql = "UPDATE PROJECTBENEFITS  "
                StrSql = StrSql + "SET QUANTBNF = '" + QuanBn + "',QUALBNF = '" + QualBn + "',"
                StrSql = StrSql + "USERID=" + UserId + ",LASTUPDATEDON=SYSDATE WHERE PROJECTID=  " + ProjectId + ""
                odbUtil.UpIns(StrSql, SavvyConnection)

                Return True
            Catch ex As Exception
                Return False
                Throw New Exception("SavvyGetData:BenefitsUpdate:" + ex.Message.ToString())
            End Try

        End Function
#End Region
        Public Function UpdateProjectDate(ByVal Value As String, ByVal DateId As String, ByVal ProjectId As String) As Boolean
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "UPDATE PROJECTDATE SET VALUE=TO_DATE('" + Value + "','MM/DD/YYYY')  "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND DATETYPEID=" + DateId + ""
                dbUtil.UpIns(strSql, SavvyConnection)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

         Public Function InsertProjectDate(ByVal Value As String, ByVal DateId As String, ByVal ProjectId As String) As Boolean
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "INSERT INTO PROJECTDATE (PROJECTID,DATETYPEID,VALUE) "
                strSql = strSql + "SELECT " + ProjectId + "," + DateId + ",TO_DATE('" + Value + "','MM/DD/YYYY') FROM DUAL"
                dbUtil.UpIns(strSql, SavvyConnection)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
		
		Public Sub InsertMemorizedDetails(ByVal UserId As String)
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "INSERT INTO MEMORIZEDDETAILS (USERID) "
                strSql = strSql + "SELECT " + UserId + " FROM DUAL"
                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception

            End Try
        End Sub

        Public Function UpdateMemorizedDetailsOLD1(ByVal UserId As String, ByVal PageSizeId As String, ByVal DateTypeId As String, ByVal Desc As String) As Boolean
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "UPDATE MEMORIZEDDETAILS SET "
                If PageSizeId <> "" Then
                    strSql = strSql + "PAGESIZEID=" + PageSizeId + ","
                End If
                If DateTypeId <> "" Then
                    strSql = strSql + "DATETYPEID=" + DateTypeId + ","
                End If
                If Desc <> "" Then
                    strSql = strSql + "DESCTYPE='" + Desc + "',"
                End If
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " WHERE USERID=" + UserId + ""
                dbUtil.UpIns(strSql, SavvyConnection)

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

		Public Function UpdateMemorizedDetails(ByVal UserId As String, ByVal PageSizeId As String, ByVal DateTypeId As String, ByVal Desc As String, ByVal SortId As String, ByVal DisplayId As String, ByVal DisplayMilestoneId As String, ByVal SortMilesDate As String, ByVal SortMilesId As String) As Boolean
            Dim dbUtil As New DBUtil()
            Dim strSql As String = String.Empty
            Try
                strSql = "UPDATE MEMORIZEDDETAILS SET "
                If PageSizeId <> "" Then
                    strSql = strSql + "PAGESIZEID=" + PageSizeId + ","
                End If
                If DateTypeId <> "" Then
                    strSql = strSql + "DATETYPEID=" + DateTypeId + ","
                End If
                If Desc <> "" Then
                    strSql = strSql + "DESCTYPE='" + Desc + "',"
                End If
                If SortId <> "" Then
                    strSql = strSql + "SORTSTATUSID='" + SortId + "',"
                End If
                If DisplayId <> "" Then
                    strSql = strSql + "DISPLAYSTATUSID='" + DisplayId + "',"
                End If
                If DisplayMilestoneId <> "" Then
                    strSql = strSql + "DISPLAYMILESTONEID='" + DisplayMilestoneId + "',"
                End If
                If SortMilesDate <> "" Then
                    strSql = strSql + "SORTMILESDATE='" + SortMilesDate + "',"
                End If
                If SortMilesId <> "" Then
                    strSql = strSql + "SORTMILESID='" + SortMilesId + "',"
                End If
                strSql = strSql.Remove(strSql.Length - 1)
                strSql = strSql + " WHERE USERID=" + UserId + ""
                dbUtil.UpIns(strSql, SavvyConnection)

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
		
        Public Function InsertProjTitle(ByVal UserId As String, ByVal Title As String, ByVal Active As String, ByVal AnalysisId As String) As Integer
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ProjId As Integer = 0
            Try
                strSql = "SELECT SEQPROJECTID.NEXTVAL PROJECTID FROM DUAL "
                ProjId = dbUtil.FillData(strSql, SavvyConnection)

                strSql = String.Empty
                strSql = "INSERT INTO PROJECTDETAILS (PROJECTID,USERID,TITLE,STATUSID,ANALYSISID,CREATEDON) "
                strSql = strSql + "SELECT " + ProjId.ToString() + "," + UserId + ",'" + Title + "','" + Active + "'," + AnalysisId + ",SYSDATE FROM DUAL"

                dbUtil.UpIns(strSql, SavvyConnection)
                Return ProjId
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjTitle:" + ex.Message.ToString())
                Return False
            End Try
        End Function

        Public Function UpdateProjDetails(ByVal UserId As String, ByVal Desc As String, ByVal ProjId As String) As Integer
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "UPDATE PROJECTDETAILS SET KEYWORD='" + "',DESCRIPTION='" + Desc + "' "
                strSql = strSql + "WHERE PROJECTID=" + ProjId.ToString() + " "

                dbUtil.UpIns(strSql, SavvyConnection)
                Return ProjId
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateProjDetails:" + ex.Message.ToString())
                Return False
            End Try
        End Function

        Public Sub MessageViewedInfo(ByVal MsgId As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = String.Empty
                strSql = "UPDATE EMAILANALYST SET VIEWTIME=SYSDATE,ISVIEWED='Y' WHERE MESSAGEID=" + MsgId + " "
                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:MessageViewedInfo:" + ex.Message.ToString())
            End Try
        End Sub

        Public Function InsertMessageInfo(ByVal SendFr As String, ByVal SendTo As String, ByVal ProjectId As String, ByVal Content As String, ByVal Subject As String, ByVal IsReply As String, ByVal IsForward As String) As Integer
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim MsgId As Integer = 0
            Try
                strSql = "SELECT SEQMESSAGEID.NEXTVAL FROM DUAL "
                MsgId = dbUtil.FillData(strSql, SavvyConnection)

                strSql = String.Empty
                strSql = "INSERT INTO EMAILANALYST (MESSAGEID,SENDFROM,SENDTO,SENDTIME,PROJECTID,ISSENT,CONTENT,SUBJECT,ISREPLY,ISFORWARD) "
                strSql = strSql + "SELECT " + MsgId.ToString() + "," + SendFr + "," + SendTo + ",SYSDATE," + ProjectId + ",'Y', "
                strSql = strSql + "'" + Content + "','" + Subject + "','" + IsReply + "','" + IsForward + "' FROM DUAL "
                dbUtil.UpIns(strSql, SavvyConnection)

                Return MsgId

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertMessageInfo:" + ex.Message.ToString())
                Return MsgId
            End Try
        End Function

        Public Sub UpdateReceiveTime(ByVal MsgId As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = String.Empty
                strSql = "UPDATE EMAILANALYST SET RECEIVEDTIME=SYSDATE WHERE MESSAGEID=" + MsgId + " "
                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateReceiveTime:" + ex.Message.ToString())
            End Try
        End Sub

#Region "BackButton"

        Public Sub UpdateProjectNmType(ByVal ProjID As String, ByVal Nm As String, ByVal TypeID As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = String.Empty
                strSql = "UPDATE PROJECTDETAILS SET TITLE='" + Nm + "',ANALYSISID=" + TypeID + " WHERE PROJECTID=" + ProjID
                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateProjectNmType:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateProjDate_Global(ByVal ProjID As String, ByVal DateTypeID As String, ByVal Value As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = String.Empty
                strSql = "UPDATE PROJECTDATE SET VALUE=TO_DATE('" + Value + "','MM/DD/YYYY') WHERE PROJECTID=" + ProjID + " AND DATETYPEID=" + DateTypeID + " "
                dbUtil.UpIns(strSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateProjDate_Global:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateQPData(ByVal PROJECTID As String, ByVal OrderQ As String, ByVal OrderSize As String, ByVal FDWidth As String, ByVal FDHeight As String, _
                                ByVal ddlFlat_BD As String, ByVal CDWidth As String, ByVal CDHeight As String, ByVal CDLength As String, ByVal ddl_COD As String, _
                                ByVal Weight_EmptyCase As String, ByVal Weight_ProdPacked As String, ByVal Printed_DDL As String, ByVal ECT_DDL As String, _
                                ByVal mULLENS_DDL As String, ByVal PQ_DDL As String, ByVal PrintC As String, ByVal Bcom_DDL As String, ByVal Overall_BoardWeight As String, _
                                ByVal FS_DDL As String, ByVal BStyle_DDL As String, ByVal SFormat_DDL As String, ByVal Unit_WEmptyCase As String, ByVal Unit_WPackage As String, ByVal Bw_BD As String)
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim RESULTPRICE As Integer = 0
            Try
                strSql = String.Empty
                strSql = " UPDATE PROJQUICKPRICE SET "
                strSql = strSql + "ANNUALORDQUANT='" + OrderQ + "', "
                strSql = strSql + "ORDSIZE='" + OrderSize + "',FLATBLANKDIM_w='" + FDWidth + "',FLATBLANKDIM_l='" + FDHeight + "',CARTOUTDIM_l='" + CDLength + "',"
                strSql = strSql + "CARTOUTDIM_w='" + CDWidth + "',CARTOUTDIM_h='" + CDHeight + "',WGHTOFEMPTCASE='" + Weight_EmptyCase + "',WGHTOFPRODPACK='" + Weight_ProdPacked + "',"
                strSql = strSql + "ECT='" + ECT_DDL + "',MullensRating='" + mULLENS_DDL + "',PRINTED='" + Printed_DDL + "',PRINTEDQUAL='" + PQ_DDL + "',PRINT='" + PrintC + "',"
                strSql = strSql + "BOARDCOMB='" + Bcom_DDL + "',OVALLBOARDWGHT='" + Overall_BoardWeight + "',FLUTESIZE='" + FS_DDL + "',CONTSTYL='" + BStyle_DDL + "',"
                strSql = strSql + "SHIPFORMT='" + SFormat_DDL + "',RESULTPRICE='" + RESULTPRICE.ToString() + "',"
                strSql = strSql + "ANNUALORDQ_UNIT=(SELECT ID FROM QPRICE_UNIT WHERE ID='1'),"
                strSql = strSql + "ORDSIZE_UNIT=(SELECT ID FROM QPRICE_UNIT WHERE ID='1'),"
                strSql = strSql + "FLATBLANKDIM_UNIT='" + ddlFlat_BD.ToString() + "',CARTOUTDIM_UNIT='" + ddl_COD.ToString() + "',"
                strSql = strSql + "WGHTOFEMPTCASE_UNIT='" + Unit_WEmptyCase + "',WGHTOFPRODPACK_UNIT='" + Unit_WPackage + "',"
                strSql = strSql + "ECT_UNIT=(SELECT ID FROM QPRICE_UNIT WHERE ID='2'),"
                strSql = strSql + "MULLENSRATING_UNIT=(SELECT ID FROM QPRICE_UNIT WHERE ID='3'),"
                strSql = strSql + "PRINT_UNIT=(SELECT ID FROM QPRICE_UNIT WHERE ID='1'),"
                strSql = strSql + "OVALLBOARDWGHT_UNIT='" + Bw_BD + "',"
                strSql = strSql + "UPDATEDDATE=SYSDATE "
                strSql = strSql + "WHERE PROJECTID=" + PROJECTID
                dbUtil.UpIns(strSql, SavvyConnection)
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:UpdateQPData:" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "UniversalFileUpload"
        Public Sub InsertLog2_Old(ByVal UserId As String, ByVal PageName As String, ByVal ActivityType As String, ByVal ProjId As String, ByVal logCount As String, ByVal ActionT As String, ByVal InfoType As String, ByVal PFileId As String, ByVal OwnerId As String)
            Dim StrSql As String = String.Empty
            Dim userseq As Integer
            Dim dbUtil As New DBUtil()
            Try
                StrSql = "SELECT NVL(MAX(USERSEQUENCE),1) USERSEQUENCE FROM ACTIVITYLOG WHERE LOGINCOUNT=" + logCount
                userseq = dbUtil.FillData(StrSql, SavvyConnection) + 1

                'log Details
                StrSql = String.Empty
                StrSql = "INSERT INTO ACTIVITYLOG (LOGID,  "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "LOGINCOUNT, "
                StrSql = StrSql + "USERSEQUENCE, "
                StrSql = StrSql + "PAGENAME, "
                StrSql = StrSql + "ACTIVITYDETAILS, "
                StrSql = StrSql + "ACTIVITYTIME, "
                StrSql = StrSql + "PROJECTID,ACTIONID,PROJECTFILEID,FILETYPEID,OWNERID) "

                StrSql = StrSql + "SELECT SEQACTIVITYLOGID.NEXTVAL,  "
                StrSql = StrSql + UserId + ", "
                StrSql = StrSql + logCount + ", "
                StrSql = StrSql + userseq.ToString() + " , "
                StrSql = StrSql + "'" + PageName + "', "
                StrSql = StrSql + "'" + ActivityType + "', "
                StrSql = StrSql + "sysdate, "
                StrSql = StrSql + "'" + ProjId + " ', "
                StrSql = StrSql + "(SELECT ACTIONID FROM FILEACTION WHERE UPPER(ACTIONNAME)='" + ActionT.ToUpper() + "'), "
                StrSql = StrSql + "'" + PFileId + "' , "
                StrSql = StrSql + "(SELECT TYPEID FROM FILETYPE WHERE UPPER(TYPENAME)='" + InfoType.ToUpper() + "'), "
                StrSql = StrSql + "" + OwnerId + " FROM DUAL"
                dbUtil.UpIns(StrSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertLog2:" + ex.Message.ToString())
            End Try

        End Sub
		
		Public Sub InsertLog2(ByVal UserId As String, ByVal PageName As String, ByVal ActivityType As String, ByVal ProjId As String, ByVal logCount As String, ByVal ActionT As String, ByVal InfoType As String, ByVal PFileId As String)
            Dim StrSql As String = String.Empty
            Dim userseq As Integer
            Dim dbUtil As New DBUtil()
            Try
                StrSql = "SELECT NVL(MAX(USERSEQUENCE),1) USERSEQUENCE FROM ACTIVITYLOG WHERE LOGINCOUNT=" + logCount
                userseq = dbUtil.FillData(StrSql, SavvyConnection) + 1

                'log Details
                StrSql = String.Empty
                StrSql = "INSERT INTO ACTIVITYLOG (LOGID,  "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "LOGINCOUNT, "
                StrSql = StrSql + "USERSEQUENCE, "
                StrSql = StrSql + "PAGENAME, "
                StrSql = StrSql + "ACTIVITYDETAILS, "
                StrSql = StrSql + "ACTIVITYTIME, "
                StrSql = StrSql + "PROJECTID,ACTIONID,PROJECTFILEID,FILETYPEID) "

                StrSql = StrSql + "SELECT SEQACTIVITYLOGID.NEXTVAL,  "
                StrSql = StrSql + UserId + ", "
                StrSql = StrSql + logCount + ", "
                StrSql = StrSql + userseq.ToString() + " , "
                StrSql = StrSql + "'" + PageName.Replace("'", "''") + "', "
                StrSql = StrSql + "'" + ActivityType.Replace("'", "''") + "', "
                StrSql = StrSql + "sysdate, "
                StrSql = StrSql + "'" + ProjId + " ', "
                StrSql = StrSql + "(SELECT ACTIONID FROM FILEACTION WHERE UPPER(ACTIONNAME)='" + ActionT.ToUpper() + "'), "
                StrSql = StrSql + "'" + PFileId + "' , "
                StrSql = StrSql + "(SELECT TYPEID FROM FILETYPE WHERE UPPER(TYPENAME)='" + InfoType.ToUpper() + "') "
                StrSql = StrSql + " FROM DUAL"
                dbUtil.UpIns(StrSql, SavvyConnection)

            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertLog2:" + ex.Message.ToString())
            End Try

        End Sub
		
        Public Function AddFileDetails(ByVal ProjId As String, ByVal FileName As String, ByVal FilePath As String, ByVal Type As String, ByVal UserId As String, ByVal OwnerId As String) As Integer
            Dim strSql As String = String.Empty
            Dim ProjFileId As New Integer
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT SEQPFILEID.NEXTVAL FROM DUAL "
                ProjFileId = dbUtil.FillData(strSql, SavvyConnection)

                'Insert Data into STRUCTUREDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO PROJECTFILES (PROJECTFILEID,PROJECTID,FILENAME,FILEPATH,UPLOADDATE,UPLOADBY,TYPEID,OWNERID) "
                strSql = strSql + "SELECT " + ProjFileId.ToString() + "," + ProjId + ",'" + FileName + "','" + FilePath + "',SYSDATE, "
                strSql = strSql + " " + UserId + ", (SELECT TYPEID FROM FILETYPE WHERE TYPENAME='" + Type + "')," + OwnerId + " FROM DUAL "
                dbUtil.UpIns(strSql, SavvyConnection)

                Return ProjFileId
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddFileDetails:" + ex.Message.ToString())
            End Try
        End Function

        Public Function AddFileDetails_new(ByVal ProjId As String, ByVal FileName As String, ByVal FilePath As String, ByVal Type As String, ByVal UserId As String, _
                                     ByVal OwnerId As String, ByVal FileAction As String) As Integer
            Dim strSql As String = String.Empty
            Dim ProjFileId As New Integer
            Dim dbUtil As New DBUtil()
            Try
                If FileAction = "Insert" Then
                    strSql = "SELECT SEQPFILEID.NEXTVAL FROM DUAL "
                    ProjFileId = dbUtil.FillData(strSql, SavvyConnection)

                    'Insert Data into STRUCTUREDETAILS table
                    strSql = String.Empty
                    strSql = "INSERT INTO PROJECTFILES (PROJECTFILEID,PROJECTID,FILENAME,FILEPATH,UPLOADDATE,UPLOADBY,TYPEID,OWNERID) "
                    strSql = strSql + "SELECT " + ProjFileId.ToString() + "," + ProjId + ",'" + FileName.Replace("'", "''") + "','" + FilePath.Replace("'", "''") + "',SYSDATE, "
                    strSql = strSql + " " + UserId + ", (SELECT TYPEID FROM FILETYPE WHERE TYPENAME='" + Type + "')," + OwnerId + " FROM DUAL "
                ElseIf FileAction = "Update" Then
                    Dim objGetData As New SavvyGetData.Selectdata()
                    Dim DsProjFileID As New DataSet()
                    DsProjFileID = objGetData.GetFileDetails_new(ProjId, FileName)
                    ProjFileId = DsProjFileID.Tables(0).Rows(0).Item("PROJECTFILEID").ToString()

                    If ProjFileId <> 0 Then
                        strSql = "UPDATE PROJECTFILES SET "
                        strSql = strSql + "FILEPATH='" + FilePath.Replace("'", "''") + "', "
                        strSql = strSql + "FILENAME='" + FileName.Replace("'", "''") + "', "
                        strSql = strSql + "UPLOADDATE=SYSDATE, "
			strSql = strSql + "TYPEID=(SELECT TYPEID FROM FILETYPE WHERE TYPENAME='" + Type + "'), "
                        strSql = strSql + "UPLOADBY=" + UserId.ToString() + " "
                        strSql = strSql + "WHERE PROJECTFILEID=" + ProjFileId.ToString() + ""
                    End If
                End If
                dbUtil.UpIns(strSql, SavvyConnection)

                Return ProjFileId
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddFileDetails_new:" + ex.Message.ToString())
            End Try
        End Function

#End Region
		
    End Class
	
End Class
