Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class SavvyGetData
    Public Class Selectdata

        Dim SavvyConnection As String = System.Configuration.ConfigurationManager.AppSettings("SavvyPackConnectionString")
        Dim ConfigurationConnection As String = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")
Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

#Region "GetPageDetail"
        Public Function GetLogid() As Integer
            Dim strSql As String = String.Empty
            Dim seqlogid As New Integer
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT SEQLOGID.NEXTVAL FROM DUAL "
                seqlogid = odbUtil.FillData(strSql, EconConnection)
                Return seqlogid
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:GetLogid:" + ex.Message.ToString())
            End Try
        End Function

        Public Function GetPageId(ByVal Name As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT DISTINCT PAGEID FROM PAGEDETAILS WHERE PAGENAME='" + Name.ToString() + "' "

                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetPageId:" + ex.Message.ToString())

            End Try
        End Function

#End Region


#Region "QuickPrice"

Public Function GetUSRLICDetails(ByVal UId1 As String, ByVal UId2 As String) As Boolean
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim flg As Boolean
            Dim ds As New DataSet
            Try
                strSql = "SELECT DISTINCT LICENSEID FROM ECON.USERS WHERE USERID IN(" + UId1.ToString() + "," + UId2.ToString() + ")"

                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                If ds.Tables(0).Rows.Count > 1 Then
                    flg = False
                Else
                    flg = True
                End If
                Return flg
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetUSRLICDetails:" + ex.Message.ToString())
                Return flg
            End Try
        End Function

        Public Function GetUserDelFileDetails(ByVal ProjectId As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                StrSql = "SELECT PROJECTID, PROJECTFILEID, USR.USERID, USR.USERNAME,TO_CHAR(UPLOADDATE,'MM/DD/YYYY')UPLOADDATE,FILETYPE.TYPEID,  "
                StrSql = StrSql + "FILENAME, TYPENAME, FILEPATH, FO.OWNERID, FO.OWNERNAME "
                StrSql = StrSql + "FROM PROJECTFILES "
                StrSql = StrSql + "INNER JOIN FILETYPE on FILETYPE.TYPEID=PROJECTFILES.TYPEID "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR on USR.USERID=PROJECTFILES.UPLOADBY "
                StrSql = StrSql + "INNER JOIN FILEOWNER FO ON FO.OWNERID=PROJECTFILES.OWNERID "
                StrSql = StrSql + "WHERE PROJECTID=CASE WHEN " + ProjectId + "=-1 THEN PROJECTID ELSE " + ProjectId + " END  "
                StrSql = StrSql + "AND FILETYPE.TYPEID IN (SELECT TYPEID FROM FILETYPE "
                StrSql = StrSql + "WHERE UPPER(TYPENAME) IN ('DELIVERABLE') )  "
                StrSql = StrSql + "ORDER BY PROJECTFILEID DESC  "
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetUserDelFileDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetQuickPriceDetails_ECT() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT ECTID,ECT_VALUE "
                strSql = strSql + "FROM QECT_VAR "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_ECT:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_UNIT() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT ID,UNIT_NAME "
                strSql = strSql + "FROM QPRICE_UNIT "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_UNIT:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_MULLENS() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT MULLENID,MULLEN_VALUE "
                strSql = strSql + "FROM QMULLENS_VAR "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_MULLENS:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_PQ() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT PQID,PQ_VALUE "
                strSql = strSql + "FROM QPRINT_QUALITY_VAR "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_PQ:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_BCOM() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT BCOMID,BCOM_VALUE "
                strSql = strSql + "FROM QBCOMB_VAR "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_BCOM:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_FS() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT FSID,FS_VALUE "
                strSql = strSql + "FROM QFS_VAR "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_FS:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_CONSTSTYLE() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT CONTSTYLEID,CONTSTYLE_VALUE "
                strSql = strSql + "FROM QCONTSTYLE_VAR "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_CONSTSTYLE:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_SFORMAT() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT SFORMATID,SFORMAT_VALUE "
                strSql = strSql + "FROM QSFORMAT_VAR "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_SFORMAT:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_Dimension_Unit() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT ID,DIMENSION_UNIT_NAME "
                strSql = strSql + "FROM QDIMENSION_UNIT "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_Dimension_Unit:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function GetQuickPriceDetails_Weight_Unit() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT ID,WEIGHT_UNIT_NAME "
                strSql = strSql + "FROM QWEIGHT_UNIT "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_Weight_Unit:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_OverallBoardW_Unit() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT ID,WEIGHT_UNIT "
                strSql = strSql + "FROM QOVERALL_BORADWEIGHT_UNIT "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_OverallBoardW_Unit:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetQuickPriceDetails_Printed() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT PRINTEDID,PRINTED_VALUE "
                strSql = strSql + "FROM QPRINTED_VAR "
                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetQuickPriceDetails_Printed:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
        Public Function GetAnalysis() As DataSet
            Dim Ds As New DataSet()
            Dim odbutil As New DBUtil()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT  ANALYSISID,TYPE,ISDEFAULT, "
                StrSql = StrSql + "(CASE WHEN ANALYSIS.ISDEFAULT='N' THEN 0 ELSE 1 END)CHK "
                StrSql = StrSql + "FROM  ANALYSIS "
                StrSql = StrSql + "WHERE ISDEFAULT='Y' "
                StrSql = StrSql + "ORDER BY ANALYSISID ASC  "

                Ds = odbutil.FillDataSet(StrSql, SavvyConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetAnalysis:" + ex.Message.ToString())
            End Try
        End Function

        Public Function GetUserAnalysisDetails(ByVal UserId As Integer) As DataSet
            Dim Ds As New DataSet()
            Dim odbutil As New DBUtil()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT USERANALYSISID,USERID,ANALYSISTYPEID FROM USERANALYSIS "
                StrSql = StrSql + "WHERE USERID = " + UserId.ToString() + " "
                StrSql = StrSql + "ORDER BY USERANALYSISID ASC  "

                Ds = odbutil.FillDataSet(StrSql, SavvyConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetUserAnalysisDetails:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetUserAnalysis(ByVal UserId As Integer) As DataSet
            Dim Ds As New DataSet()
            Dim odbUtil As New DBUtil()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT ANALYSISID,USERID,ANALYSIS.TYPE, "
                StrSql = StrSql + "(CASE WHEN USERANALYSIS.ANALYSISTYPEID IS NULL THEN 0 ELSE 1 END)CHK  "
                StrSql = StrSql + "FROM  ANALYSIS "
                StrSql = StrSql + "LEFT OUTER JOIN  USERANALYSIS "
                StrSql = StrSql + "ON ANALYSIS.ANALYSISID = USERANALYSIS.ANALYSISTYPEID  "
                StrSql = StrSql + "WHERE USERID = " + UserId.ToString() + " "
                StrSql = StrSql + "ORDER BY ANALYSIS.ANALYSISID ASC  "

                Ds = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function ExistingProjQuickPriceDetails(ByVal ProjectId As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT PROJQUICKPRICEID,PROJECTID,(SELECT USERID FROM PROJECTDETAILS WHERE PROJECTID=" + ProjectId + ")USERID,ANNUALORDQUANT,PRQ.ANNUALORDQ_UNIT,QU.UNIT_NAME,ORDSIZE,ORDSIZE_UNIT,QU1.UNIT_NAME ORDERSIZE_NAME,FLATBLANKDIM_W,FLATBLANKDIM_L,FLATBLANKDIM_UNIT,QD.DIMENSION_UNIT_NAME DIMENSION_UNIT_NAME_FLT,CARTOUTDIM_L,CARTOUTDIM_W, "
                strSql = strSql + " CARTOUTDIM_H,CARTOUTDIM_UNIT,QD1.DIMENSION_UNIT_NAME DIMENSION_UNIT_CARTOON,WGHTOFEMPTCASE,WGHTOFEMPTCASE_UNIT,QW.WEIGHT_UNIT_NAME WGHTOFEMPTCASE_UNIT_NAME ,WGHTOFPRODPACK,WGHTOFPRODPACK_UNIT,QW1.WEIGHT_UNIT_NAME WGHTOFPRODPACK_UNIT_NAME,"
                strSql = strSql + " ECT,QECT.ECT_VALUE,ECT_UNIT,QU2.UNIT_NAME ECT_UNIT_NAME,MULLENSRATING,QMUL.MULLEN_VALUE,MULLENSRATING_UNIT,QU3.UNIT_NAME MULLENSRATING_UNIT_NAME,PRINTED,QP.PRINTED_VALUE,PRINTEDQUAL,QQP.PQ_VALUE,PRINT,PRINT_UNIT,QU4.UNIT_NAME PRINT_UNIT_NAME,BOARDCOMB,QBC.BCOM_VALUE,OVALLBOARDWGHT,OVALLBOARDWGHT_UNIT,"
                strSql = strSql + " WEIGHT_UNIT,FLUTESIZE,QFS.FS_VALUE,CONTSTYL,QCS.CONTSTYLE_VALUE,SHIPFORMT,QSF.SFORMAT_VALUE,RESULTPRICE,UPDATEDDATE"
                strSql = strSql + " FROM PROJQUICKPRICE PRQ"
                strSql = strSql + " INNER JOIN QPRICE_UNIT QU ON QU.ID=PRQ.ANNUALORDQ_UNIT "
                strSql = strSql + " INNER JOIN QPRICE_UNIT QU1 ON QU1.ID=PRQ.ORDSIZE_UNIT "
                strSql = strSql + " INNER JOIN QPRICE_UNIT QU2 ON QU2.ID=PRQ.ECT_UNIT "
                strSql = strSql + " INNER JOIN QPRICE_UNIT QU3 ON QU3.ID=PRQ.MULLENSRATING_UNIT "
                strSql = strSql + " INNER JOIN QPRICE_UNIT QU4 ON QU4.ID=PRQ.PRINT_UNIT "
                strSql = strSql + " INNER JOIN QDIMENSION_UNIT QD ON QD.ID=PRQ.FLATBLANKDIM_UNIT "
                strSql = strSql + " INNER JOIN QDIMENSION_UNIT QD1 ON QD1.ID=PRQ.CARTOUTDIM_UNIT "
                strSql = strSql + " INNER JOIN QWEIGHT_UNIT QW ON QW.ID=PRQ.WGHTOFEMPTCASE_UNIT "
                strSql = strSql + " INNER JOIN QWEIGHT_UNIT QW1 ON QW1.ID=PRQ.WGHTOFPRODPACK_UNIT "
                strSql = strSql + " INNER JOIN QOVERALL_BORADWEIGHT_UNIT QW ON QW.ID=PRQ.OVALLBOARDWGHT_UNIT "
                strSql = strSql + " INNER JOIN QPRINTED_VAR QP ON QP.PRINTEDID=PRQ.PRINTED "
                strSql = strSql + " INNER JOIN QPRINT_QUALITY_VAR QQP ON QQP.PQID=PRQ.PRINTEDQUAL "
                strSql = strSql + " INNER JOIN QBCOMB_VAR QBC ON QBC.BCOMID=PRQ.BOARDCOMB "
                strSql = strSql + " INNER JOIN QFS_VAR QFS ON QFS.FSID=PRQ.FLUTESIZE "
                strSql = strSql + " INNER JOIN QCONTSTYLE_VAR QCS ON QCS.CONTSTYLEID=PRQ.CONTSTYL "
                strSql = strSql + " INNER JOIN QSFORMAT_VAR QSF ON QSF.SFORMATID=PRQ.SHIPFORMT "
                strSql = strSql + " INNER JOIN QECT_VAR QECT ON QECT.ECTID=PRQ.ECT "
                strSql = strSql + " INNER JOIN QMULLENS_VAR QMUL ON QMUL.MULLENID=PRQ.MULLENSRATING "
                strSql = strSql + " WHERE ProjectId = " + ProjectId + ""

                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:ExistingProjQuickPriceDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
#End Region

#Region "Project Details"
         Public Function GetProjectDetails(ByVal UserId As String, ByVal Text As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT PRD.PROJECTID,TITLE,KEYWORD,PRD.USERID,USR.USERNAME OWNER,REPLACE(DESCRIPTION,'''','&#')DESCRIPTION,QUANTBNF,QUALBNF,(USRC.FIRSTNAME||' '||USRC.LASTNAME)OWNER1,USRC.PHONENUMBER, "
            strSql = strSql + "CASE WHEN LENGTH(DESCRIPTION)>15 THEN (SUBSTR(NVL(DESCRIPTION,'DESCRIPTION'),1,15) || ' '||'...') ELSE DESCRIPTION  END DESCRIPTION1 ,PRD.STATUSID,STATUS,TYPE,"
            strSql = strSql + "CASE WHEN LENGTH(QUANTBNF)>15 THEN (SUBSTR(NVL(QUANTBNF, 'Benefits'),1,10) || ' '||'...') ELSE NVL(QUANTBNF, 'Benefits')  END QUANTBNF1,"
            strSql = strSql + "CASE WHEN LENGTH(QUALBNF)>15 THEN (SUBSTR(NVL(QUALBNF, 'Benefits'),1,10) || ' '||'...') ELSE NVL(QUALBNF, 'Benefits')  END QUALBNF1, "
            strSql = strSql + "NVL(TO_CHAR(PROJECTDATE.VALUE, 'MM/DD/YYYY'),'Dates')VALUE FROM PROJECTDETAILS PRD "
            strSql = strSql + "INNER JOIN STATUS ON STATUS.STATUSID=PRD.STATUSID "
            strSql = strSql + "INNER JOIN ANALYSIS ON ANALYSIS.ANALYSISID=PRD.ANALYSISID "
            strSql = strSql + "INNER JOIN ECON.USERS USR ON USR.USERID=PRD.USERID "
            strSql = strSql + "INNER JOIN ECON.USERCONTACTS USRC ON USRC.USERID=PRD.USERID  "
            strSql = strSql + "LEFT JOIN PROJECTBENEFITS PB ON PB.PROJECTID=PRD.PROJECTID "
            strSql = strSql + " LEFT OUTER JOIN PROJECTDATE ON PROJECTDATE.PROJECTID=PRD.PROJECTID "
            strSql = strSql + "AND PROJECTDATE.DATETYPEID=1 "
            strSql = strSql + "WHERE USR.USERID=" + UserId + " "
            strSql = strSql + "AND "
            strSql = strSql + "( UPPER(PRD.PROJECTID) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(TITLE) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(KEYWORD) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(USERNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(STATUS) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(TYPE) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(DESCRIPTION) LIKE '%" + Text.ToUpper().Trim() + "%'  "
            strSql = strSql + "OR UPPER(QUANTBNF) LIKE '%" + Text.ToUpper().Trim() + "%'  "
            strSql = strSql + "OR UPPER(QUALBNF) LIKE '%" + Text.ToUpper().Trim() + "%' ) "
            strSql = strSql + "ORDER BY PROJECTID ASC "
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetAllProjectDetailsOLD1(ByVal UserId As String, ByVal Text As String, ByVal Desc As String, ByVal DateTypeId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT PRD.PROJECTID,TITLE,KEYWORD,PRD.USERID,USR.USERNAME OWNER,REPLACE(DESCRIPTION,'''','&#')DESCRIPTION,QUANTBNF,QUALBNF,(USRC.FIRSTNAME||' '||USRC.LASTNAME)OWNER1,USRC.PHONENUMBER,PRD.ISANALYST,(USR1.FIRSTNAME||' '||USR1.LASTNAME)ANALYST,(USR1.EMAILADDRESS) ANALYSTEMAILID, USR1.PHONENUMBER ANALYST1, "
            If Desc = "Min" Then
                strSql = strSql + "CASE WHEN LENGTH(DESCRIPTION)>15 THEN (SUBSTR(NVL(DESCRIPTION,'DESCRIPTION'),1,15) || ' '||'...') ELSE DESCRIPTION  END DESCRIPTION1 ,PRD.STATUSID,STATUS,TYPE,"
                strSql = strSql + "CASE WHEN LENGTH(QUANTBNF)>15 THEN (SUBSTR(NVL(QUANTBNF, 'Benefits'),1,10) || ' '||'...') ELSE NVL(QUANTBNF, 'Benefits')  END QUANTBNF1,"
                strSql = strSql + "CASE WHEN LENGTH(QUALBNF)>15 THEN (SUBSTR(NVL(QUALBNF, 'Benefits'),1,10) || ' '||'...') ELSE NVL(QUALBNF, 'Benefits')  END QUALBNF1, "
                strSql = strSql + "CASE WHEN LENGTH(TITLE)>15 THEN (SUBSTR(NVL(TITLE, ' '),1,10) || ' '||'...') ELSE NVL(TITLE, ' ')  END TITLE1, "
                strSql = strSql + "CASE WHEN LENGTH(KEYWORD)>15 THEN (SUBSTR(NVL(KEYWORD, ' '),1,10) || ' '||'...') ELSE NVL(KEYWORD, '  ')  END KEYWORD1, "
            Else
                strSql = strSql + "NVL(DESCRIPTION,'DESCRIPTION')DESCRIPTION1,PRD.STATUSID,STATUS,TYPE,"
                strSql = strSql + "NVL(QUANTBNF, 'Benefits')QUANTBNF1,"
                strSql = strSql + "NVL(QUALBNF, 'Benefits')QUALBNF1,"
                strSql = strSql + "NVL(TITLE, ' ')TITLE1,"

                strSql = strSql + "NVL(KEYWORD, ' ')KEYWORD1,"
            End If

            strSql = strSql + "NVL(TO_CHAR(PROJECTDATE.VALUE, 'MM/DD/YYYY'),'Dates')VALUE FROM PROJECTDETAILS PRD "
            strSql = strSql + "INNER JOIN STATUS ON STATUS.STATUSID=PRD.STATUSID  "
            strSql = strSql + "INNER JOIN ANALYSIS ON ANALYSIS.ANALYSISID=PRD.ANALYSISID "
            strSql = strSql + "INNER JOIN ECON.USERS USR ON USR.USERID=PRD.USERID "
            strSql = strSql + "INNER JOIN ECON.USERCONTACTS USRC ON USRC.USERID=PRD.USERID  "
            strSql = strSql + "LEFT JOIN ECON.USERCONTACTS USR1 ON  USR1.USERID=PRD.ISANALYST "
            strSql = strSql + "LEFT JOIN PROJECTBENEFITS PB ON PB.PROJECTID=PRD.PROJECTID "
            strSql = strSql + " LEFT OUTER JOIN PROJECTDATE ON PROJECTDATE.PROJECTID=PRD.PROJECTID "
            strSql = strSql + "AND PROJECTDATE.DATETYPEID=" + DateTypeId + " "
            strSql = strSql + "WHERE USR.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + ") "
            strSql = strSql + "AND"
            strSql = strSql + "( UPPER(PRD.PROJECTID) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(TITLE) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(KEYWORD) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(USERNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(STATUS) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(TYPE) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(USRC.FIRSTNAME||' '||USRC.LASTNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(USR1.FIRSTNAME||' '||USR1.LASTNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(DESCRIPTION) LIKE '%" + Text.ToUpper().Trim() + "%'  "
            strSql = strSql + "OR UPPER(QUANTBNF) LIKE '%" + Text.ToUpper().Trim() + "%'  "
            strSql = strSql + "OR UPPER(QUALBNF) LIKE '%" + Text.ToUpper().Trim() + "%' ) "
            strSql = strSql + "ORDER BY PROJECTID DESC "
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function
		
		Public Function GetAllProjectDetails(ByVal UserId As String, ByVal Text As String, ByVal Desc As String, ByVal DateTypeId As String, ByVal DisplayStatusId As String, ByVal SortStatusId As String, ByVal DisplayMilestoneId As String, ByVal SortMilesDate As String, ByVal SORTMILESID As String, ByVal SortCond As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim strSqlVal As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim dsSt As New DataSet

            'Getting Sort Status
            strSql = strSql + "SELECT STATUSID,SEQ FROM SORTSTATDET WHERE SORTSTATUSID=" + SortStatusId + " ORDER BY SEQ ASC "
            dsSt = odbUtil.FillDataSet(strSql, SavvyConnection)
            strSql = String.Empty
            'strSql = strSql + "Select case when '" + DisplayStatusId + "'='0' then '1,2,3,4,5' else '" + DisplayStatusId + "' end"

            'Get Datetype
            Dim dsDate As New DataSet
            dsDate = GetDateDetails()

            'Getting Display Status for Milestone
            Dim arrMileS() As String
            arrMileS = DisplayMilestoneId.Split(",")


            strSql = "SELECT PRD.PROJECTID,TITLE,KEYWORD,PRD.USERID,USR.USERNAME OWNER,REPLACE(DESCRIPTION,'''','&#')DESCRIPTION,QUANTBNF,QUALBNF,(USRC.FIRSTNAME||' '||USRC.LASTNAME)OWNER1,USRC.PHONENUMBER,PRD.ISANALYST,(USR1.FIRSTNAME||' '||USR1.LASTNAME)ANALYST,(USR1.EMAILADDRESS) ANALYSTEMAILID, USR1.PHONENUMBER ANALYST1,PRD.ANALYSISID ANALYSISID, "
            If dsDate.Tables(0).Rows.Count > 0 Then
                For j = 0 To dsDate.Tables(0).Rows.Count - 1
                    'strSql = strSql + "NVL(TO_CHAR(PD" + (j + 1).ToString() + ".VALUE, 'MM/DD/YYYY'),'Dates') VALUE" + (j + 1).ToString() + ",  "
                   strSql = strSql + "PD" + (j + 1).ToString() + ".VALUE VALUE" + (j + 1).ToString() + ",  "
               
				Next
            End If
            If Desc = "Min" Then
                strSql = strSql + "CASE WHEN LENGTH(DESCRIPTION)>15 THEN (SUBSTR(NVL(DESCRIPTION,'DESCRIPTION'),1,15) || ' '||'...') ELSE DESCRIPTION  END DESCRIPTION1 ,PRD.STATUSID,STATUS,TYPE,"
                strSql = strSql + "CASE WHEN LENGTH(QUANTBNF)>15 THEN (SUBSTR(NVL(QUANTBNF, 'Results'),1,10) || ' '||'...') ELSE NVL(QUANTBNF, 'Results')  END QUANTBNF1,"
                strSql = strSql + "CASE WHEN LENGTH(QUALBNF)>15 THEN (SUBSTR(NVL(QUALBNF, 'Results'),1,10) || ' '||'...') ELSE NVL(QUALBNF, 'Results')  END QUALBNF1, "
                strSql = strSql + "CASE WHEN LENGTH(TITLE)>15 THEN (SUBSTR(NVL(TITLE, ' '),1,10) || ' '||'...') ELSE NVL(TITLE, ' ')  END TITLE1, "
                strSql = strSql + "CASE WHEN LENGTH(KEYWORD)>15 THEN (SUBSTR(NVL(KEYWORD, ' '),1,10) || ' '||'...') ELSE NVL(KEYWORD, '  ')  END KEYWORD1, "
                'strSql = strSql + "(CASE WHEN LENGTH(PD1.VALUE)>5 THEN (SUBSTR(NVL(TO_CHAR(PD1.VALUE, 'MM/DD/YYYY'), ' '),1,10) || '...') ELSE NVL(TO_CHAR(PD1.VALUE,'MM/DD/YYYY'), ' Dates')  END) VALUEDES,"
                If arrMileS.Length > 0 Then
                    For j = 0 To arrMileS.Length - 1
                        'strSql = strSql + "NVL(TO_CHAR(PD" + (j + 1).ToString() + ".VALUE, 'MM/DD/YYYY'),'Dates') VALUE" + (j + 1).ToString() + ",  "
                        If j = 0 Then
                            If arrMileS(j) = "1" Then
                                strSqlVal = strSqlVal + " 'Sub: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || '... ' "
                            ElseIf arrMileS(j) = "2" Then
                                strSqlVal = strSqlVal + "  ' Des: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || '...' "
                            ElseIf arrMileS(j) = "3" Then
                                strSqlVal = strSqlVal + " ' Agr: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || '... ' "
                            ElseIf arrMileS(j) = "4" Then
                                strSqlVal = strSqlVal + "  ' Com: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || '...' "
                            ElseIf arrMileS(j) = "5" Then
                                strSqlVal = strSqlVal + "  ' Hold: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || '...' "
                            End If
                        End If

                    Next
                    strSql = strSql + strSqlVal + " VALUE "
                End If
            Else
                strSql = strSql + "NVL(DESCRIPTION,'DESCRIPTION')DESCRIPTION1,PRD.STATUSID,STATUS,TYPE,"
                strSql = strSql + "NVL(QUANTBNF, 'Results')QUANTBNF1,"
                strSql = strSql + "NVL(QUALBNF, 'Results')QUALBNF1,"
                strSql = strSql + "NVL(TITLE, ' ')TITLE1,"
                strSql = strSql + "NVL(KEYWORD, ' ')KEYWORD, "
                If arrMileS.Length > 0 Then
                    For j = 0 To arrMileS.Length - 1
                        'strSql = strSql + "NVL(TO_CHAR(PD" + (j + 1).ToString() + ".VALUE, 'MM/DD/YYYY'),'Dates') VALUE" + (j + 1).ToString() + ",  "
                        If j = 0 Then
                            If arrMileS(j) = "1" Then
                                strSqlVal = strSqlVal + " 'Sub: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || ' </br> ' "
                            ElseIf arrMileS(j) = "2" Then
                                strSqlVal = strSqlVal + "  ' Des: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || ' </br> '  "
                            ElseIf arrMileS(j) = "3" Then
                                strSqlVal = strSqlVal + " ' Agr: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || ' </br> '  "
                            ElseIf arrMileS(j) = "4" Then
                                strSqlVal = strSqlVal + "  ' Com: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || ' </br> '  "
                            ElseIf arrMileS(j) = "5" Then
                                strSqlVal = strSqlVal + "  ' Hold: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || '  '  "
                            End If
                        Else
                            If arrMileS(j) = "1" Then
                                strSqlVal = strSqlVal + " || 'Sub: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || ' </br> ' "

                            ElseIf arrMileS(j) = "2" Then
                                strSqlVal = strSqlVal + " || ' Des: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || ' </br> '  "
                            ElseIf arrMileS(j) = "3" Then
                                strSqlVal = strSqlVal + " || ' Agr: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || ' </br> '  "
                            ElseIf arrMileS(j) = "4" Then
                                strSqlVal = strSqlVal + " || ' Com: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || ' </br> '  "
                            ElseIf arrMileS(j) = "5" Then
                                strSqlVal = strSqlVal + " || ' Hold: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ') || '  '  "
                            End If
                        End If

                    Next
                    strSql = strSql + strSqlVal + " VALUE "
                End If
            End If

            strSql = strSql + "FROM PROJECTDETAILS PRD "
            strSql = strSql + "INNER JOIN STATUS ON STATUS.STATUSID=PRD.STATUSID AND PRD.STATUSID IN (" + DisplayStatusId + ") "
            strSql = strSql + "INNER JOIN ANALYSIS ON ANALYSIS.ANALYSISID=PRD.ANALYSISID "
            strSql = strSql + "INNER JOIN ECON.USERS USR ON USR.USERID=PRD.USERID "
            strSql = strSql + "INNER JOIN ECON.USERCONTACTS USRC ON USRC.USERID=PRD.USERID  "
            strSql = strSql + "LEFT JOIN ECON.USERCONTACTS USR1 ON  USR1.USERID=PRD.ISANALYST "
            strSql = strSql + "LEFT JOIN PROJECTBENEFITS PB ON PB.PROJECTID=PRD.PROJECTID "

            If dsDate.Tables(0).Rows.Count > 0 Then
                For j = 0 To dsDate.Tables(0).Rows.Count - 1
                    strSql = strSql + " LEFT OUTER JOIN PROJECTDATE PD" + (j + 1).ToString() + " ON PD" + (j + 1).ToString() + ".PROJECTID=PRD.PROJECTID "
                    strSql = strSql + "AND PD" + (j + 1).ToString() + ".DATETYPEID=" + dsDate.Tables(0).Rows(j).Item("DATETYPEID").ToString() + " "
                Next
            End If

            strSql = strSql + "WHERE PRD.ISVISIBLE='Y'AND USR.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + ") "
            strSql = strSql + "AND"
            strSql = strSql + "( UPPER(PRD.PROJECTID) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(TITLE) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(KEYWORD) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(USERNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(STATUS) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(TYPE) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(USRC.FIRSTNAME||' '||USRC.LASTNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(USR1.FIRSTNAME||' '||USR1.LASTNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
            strSql = strSql + "OR UPPER(DESCRIPTION) LIKE '%" + Text.ToUpper().Trim() + "%'  "
            strSql = strSql + "OR UPPER(QUANTBNF) LIKE '%" + Text.ToUpper().Trim() + "%'  "
            strSql = strSql + "OR UPPER(QUALBNF) LIKE '%" + Text.ToUpper().Trim() + "%'  "


            If arrMileS.Length > 0 Then
                For j = 0 To arrMileS.Length - 1
                    If j = 0 Then
                        If arrMileS(j) = "1" Then
                            strSql = strSql + "OR UPPER('SUB: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        ElseIf arrMileS(j) = "2" Then
                            strSql = strSql + "OR UPPER('DES: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        ElseIf arrMileS(j) = "3" Then
                            strSql = strSql + "OR UPPER('AGR: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        ElseIf arrMileS(j) = "4" Then
                            strSql = strSql + "OR UPPER('COM: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        ElseIf arrMileS(j) = "5" Then
                            strSql = strSql + "OR UPPER('Hold: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        End If
                    Else
                        If arrMileS(j) = "1" Then
                            strSql = strSql + "OR UPPER('SUB: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        ElseIf arrMileS(j) = "2" Then
                            strSql = strSql + "OR UPPER('DES: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        ElseIf arrMileS(j) = "3" Then
                            strSql = strSql + "OR UPPER('AGR: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        ElseIf arrMileS(j) = "4" Then
                            strSql = strSql + "OR UPPER('COM: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        ElseIf arrMileS(j) = "5" Then
                            strSql = strSql + "OR UPPER('Hold: ' || NVL(TO_CHAR(PD" + arrMileS(j).ToString() + ".VALUE, 'MM/DD/YYYY'),'NA ')) LIKE '%" + Text.ToUpper().Trim() + "%' "
                        End If
                    End If
                Next
				strSql = strSql + " ) "
            End If


            strSql = strSql + "ORDER BY "
            If SortCond = "DATE" Then
                strSql = strSql + "VALUE" + SortMilesDate.ToString() + " "

                If SORTMILESID = "1" Then
                    strSql = strSql + "ASC "
                Else
                    strSql = strSql + "DESC "
                End If
            Else
                strSql = strSql + "STATUSID "

                If SortStatusId = "1" Then
                    strSql = strSql + "ASC "
                Else
                    strSql = strSql + "DESC "
                End If


            End If


            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function


        Public Function ExistingProjDetails(ByVal UserId As String, ByVal Title As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT 1 FROM PROJECTDETAILS WHERE USERID=" + UserId + " AND TITLE='" + Title + "' "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function ExistingProjDetails(ByVal UserId As String, ByVal Title As String, ByVal ProjId As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT 1 FROM PROJECTDETAILS WHERE USERID=" + UserId + " AND TITLE='" + Title + "' AND PROJECTID NOT IN (" + ProjId + ") "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function GetExistProjectDetails(ByVal ProjId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT PROJECTID,TITLE,USERID "
            strSql = strSql + "FROM PROJECTDETAILS  "

            strSql = strSql + "WHERE PROJECTID=" + ProjId + " "
            
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetUserDetails(ByVal UserId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT USERID,USERNAME,LICENSEID,ISINTERNALUSR,ISIADMINLICUSR,ISANALYST "
            strSql = strSql + "FROM ECON.USERS WHERE USERID=" + UserId + ""
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetCategoryDetails(ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            'strSql = strSql + "SELECT PROJECTID,CATEGORYID,VALUE,ISACTIVE FROM PROJECTCATEGORY  "
            'strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND ISACTIVE='Y'"
            strSql = "SELECT DISTINCT MDT.PROJECTID,CATEGORYID,VALUE,PC.ISACTIVE FROM PROJECTCATEGORY PC INNER JOIN MODELDETAILSTEMP MDT ON MDT.PRODPACK=PC.VALUE "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " AND PC.ISACTIVE='Y' AND CATEGORYID=4 "
            strSql = strSql + "UNION "
            strSql = strSql + "SELECT DISTINCT MDT.PROJECTID,CATEGORYID,VALUE,PC.ISACTIVE FROM PROJECTCATEGORY PC INNER JOIN MODELDETAILSTEMP MDT ON MDT.PACKTYPE=PC.VALUE "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " AND PC.ISACTIVE='Y' AND CATEGORYID=1 "
            strSql = strSql + "UNION "
            strSql = strSql + "SELECT DISTINCT MDT.PROJECTID,CATEGORYID,VALUE,PC.ISACTIVE FROM PROJECTCATEGORY PC INNER JOIN MODELDETAILSTEMP MDT ON MDT.VCHAIN=PC.VALUE "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " AND PC.ISACTIVE='Y' AND CATEGORYID=2 "
            strSql = strSql + "UNION "
            strSql = strSql + "SELECT DISTINCT MDT.PROJECTID,CATEGORYID,VALUE,PC.ISACTIVE FROM PROJECTCATEGORY PC INNER JOIN MODELDETAILSTEMP MDT ON MDT.PACKSIZE=PC.VALUE "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " AND PC.ISACTIVE='Y' AND CATEGORYID=3 "
            strSql = strSql + "UNION "
            strSql = strSql + "SELECT DISTINCT MDT.PROJECTID,CATEGORYID,VALUE,PC.ISACTIVE FROM PROJECTCATEGORY PC INNER JOIN MODELDETAILSTEMP MDT ON MDT.GEOGRAPHY=PC.VALUE "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " AND PC.ISACTIVE='Y' AND CATEGORYID=5 "
            strSql = strSql + "UNION "
            strSql = strSql + "SELECT DISTINCT MDT.PROJECTID,CATEGORYID,VALUE,PC.ISACTIVE FROM PROJECTCATEGORY PC INNER JOIN MODELDETAILSTEMP MDT ON MDT.SPFET1=PC.VALUE "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " AND PC.ISACTIVE='Y' AND CATEGORYID=6 "
            strSql = strSql + "UNION "
            strSql = strSql + "SELECT DISTINCT MDT.PROJECTID,CATEGORYID,VALUE,PC.ISACTIVE FROM PROJECTCATEGORY PC INNER JOIN MODELDETAILSTEMP MDT ON MDT.SPFET2=PC.VALUE "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " AND PC.ISACTIVE='Y' AND CATEGORYID=7 "
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetCountDetails(ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = strSql + "SELECT MODELID,ISACTIVE FROM MODELDETAILSTEMP  "
            strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND ISACTIVE='Y'"
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetEditProjectDetails(ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT PROJECTID,USR.USERNAME OWNER,TITLE,DESCRIPTION,KEYWORD,PRD.STATUSID,STATUS,ANALYSISID "
            strSql = strSql + "FROM PROJECTDETAILS PRD INNER JOIN ECON.USERS USR ON USR.USERID=PRD.USERID "
            strSql = strSql + "INNER JOIN STATUS ON STATUS.STATUSID=PRD.STATUSID "
            strSql = strSql + "WHERE PROJECTID=" + ProjectId + " "

            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetStatusDetails_old() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT STATUSID,STATUS FROM STATUS ORDER BY STATUSID ASC"


            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetStatusDetails() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT STATUSID,STATUS FROM STATUS WHERE ISVISIBLE='Y' ORDER BY STATUSID ASC"
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetAnalysisDetails() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT ANALYSISID,TYPE FROM ANALYSIS ORDER BY ANALYSISID ASC"


            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetModelValueDetails(ByVal ProjectId As String, ByVal Type As String, ByVal Value As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT 1 FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND UPPER(VALUE)='" + Value.ToUpper() + "' AND CATEGORYID=" + Type + ""
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetExistingModelDetails(ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT DISTINCT PRODPACK,PACKSIZE,PACKTYPE,GEOGRAPHY,VCHAIN,SPFET1,SPFET2,PRODUCER,PACKER,GRAPHICS, "
            strSql = strSql + "STRUCTURES,FORMULA,MANUFACTORY,PACKAGING,SKU,PROJECTID,MODELID,ISACTIVE FROM MODELDETAILSTEMP "
            strSql = strSql + "WHERE PROJECTID=" + ProjectId + " "


            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetProductDetails(ByVal ProjectId As String, ByVal Type As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = " "
            If Type = "PROD" Then
                strSql = strSql + "SELECT DISTINCT PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,SEQUENCE FROM PROJECTCATEGORY  "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=4 ORDER BY SEQUENCE ASC"
            ElseIf Type = "SIZE" Then
                strSql = strSql + "SELECT DISTINCT PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,SEQUENCE FROM PROJECTCATEGORY  "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=3 ORDER BY SEQUENCE ASC"
            ElseIf Type = "TYPE" Then
                strSql = strSql + "SELECT DISTINCT PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,SEQUENCE FROM PROJECTCATEGORY  "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=1 ORDER BY SEQUENCE ASC"
            ElseIf Type = "GEOG" Then
                strSql = strSql + "SELECT DISTINCT PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,SEQUENCE FROM PROJECTCATEGORY  "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=5 ORDER BY SEQUENCE ASC"
            ElseIf Type = "VCHAIN" Then
                strSql = strSql + "SELECT DISTINCT PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,SEQUENCE FROM PROJECTCATEGORY  "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=2 ORDER BY SEQUENCE ASC"
            ElseIf Type = "SPFET1" Then
                strSql = strSql + "SELECT DISTINCT PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,SEQUENCE FROM PROJECTCATEGORY  "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=6 ORDER BY SEQUENCE ASC"
            ElseIf Type = "SPFET2" Then
                strSql = strSql + "SELECT DISTINCT PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,SEQUENCE FROM PROJECTCATEGORY  "
                strSql = strSql + "WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=7 ORDER BY SEQUENCE ASC"
            ElseIf Type = "PRODUCER" Then
                strSql = strSql + "SELECT DISTINCT PC.PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,PRODUCERID,SEQUENCE FROM PROJECTCATEGORY PC "
                strSql = strSql + "INNER JOIN PRODUCERDETAILS PD ON PD.NAME=PC.VALUE AND PD.PROJECTID=PC.PROJECTID "
                strSql = strSql + "WHERE PC.PROJECTID=" + ProjectId + " AND CATEGORYID=8 ORDER BY SEQUENCE ASC"
            ElseIf Type = "PACKER" Then
                strSql = strSql + "SELECT DISTINCT PC.PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,PACKERID,SEQUENCE FROM PROJECTCATEGORY PC "
                strSql = strSql + "INNER JOIN PACKERDETAILS PD ON PD.NAME=PC.VALUE AND PD.PROJECTID=PC.PROJECTID "
                strSql = strSql + "WHERE PC.PROJECTID=" + ProjectId + " AND CATEGORYID=9 ORDER BY SEQUENCE ASC"
            ElseIf Type = "GRAPHIC" Then
                strSql = strSql + "SELECT DISTINCT PC.PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,GRAPHICSID,SEQUENCE FROM PROJECTCATEGORY PC "
                strSql = strSql + "INNER JOIN GRAPHICSDETAILS GD ON GD.NAME=PC.VALUE AND GD.PROJECTID=PC.PROJECTID "
                strSql = strSql + "WHERE PC.PROJECTID=" + ProjectId + " AND CATEGORYID=10 ORDER BY SEQUENCE ASC"
            ElseIf Type = "STRUCT" Then
                strSql = strSql + "SELECT DISTINCT PC.PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,STRUCTUREID,SEQUENCE FROM PROJECTCATEGORY PC "
                strSql = strSql + "INNER JOIN STRUCTUREDETAILS SD ON SD.NAME=PC.VALUE AND SD.PROJECTID=PC.PROJECTID "
                strSql = strSql + "WHERE PC.PROJECTID=" + ProjectId + " AND CATEGORYID=11 ORDER BY SEQUENCE ASC"
            ElseIf Type = "FORMULA" Then
                strSql = strSql + "SELECT DISTINCT PC.PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,FORMULAID,SEQUENCE FROM PROJECTCATEGORY PC "
                strSql = strSql + "INNER JOIN FORMULADETAILS FD ON FD.NAME=PC.VALUE AND FD.PROJECTID=PC.PROJECTID "
                strSql = strSql + "WHERE PC.PROJECTID=" + ProjectId + " AND CATEGORYID=12 ORDER BY SEQUENCE ASC"
            ElseIf Type = "MANUF" Then
                strSql = strSql + "SELECT DISTINCT PC.PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,MANUFACTORYID,SEQUENCE FROM PROJECTCATEGORY PC "
                strSql = strSql + "INNER JOIN MANUFACTORYDETAILS MD ON MD.NAME=PC.VALUE AND MD.PROJECTID=PC.PROJECTID "
                strSql = strSql + "WHERE PC.PROJECTID=" + ProjectId + " AND CATEGORYID=13 ORDER BY SEQUENCE ASC"
            ElseIf Type = "PACKG" Then
                strSql = strSql + "SELECT DISTINCT PC.PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,PACKAGINGID,SEQUENCE FROM PROJECTCATEGORY PC "
                strSql = strSql + "INNER JOIN PACKAGINGDETAILS PGD ON PGD.NAME=PC.VALUE AND PGD.PROJECTID=PC.PROJECTID "
                strSql = strSql + "WHERE PC.PROJECTID=" + ProjectId + " AND CATEGORYID=14 ORDER BY SEQUENCE ASC"
            ElseIf Type = "SKU" Then
                strSql = strSql + "SELECT DISTINCT PC.PROJECTID,CATEGORYID,VALUE,ISACTIVE,PROJCATEGORYID,SKUID,SEQUENCE FROM PROJECTCATEGORY PC "
                strSql = strSql + "INNER JOIN SKUDETAILS SKD ON SKD.NAME=PC.VALUE AND SKD.PROJECTID=PC.PROJECTID "
                strSql = strSql + "WHERE PC.PROJECTID=" + ProjectId + " AND CATEGORYID=15 ORDER BY SEQUENCE ASC"
            End If

            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetModelDetails_ORG(ByVal ProjectId As String, ByVal PFlag As Boolean, ByVal SFlag As Boolean, ByVal TFlag As Boolean, ByVal GFlag As Boolean, ByVal CFlag As Boolean, ByVal F1Flag As Boolean, ByVal F2Flag As Boolean, _
                                        ByVal PdFlag As Boolean, ByVal MatFlag As Boolean, ByVal GrFlag As Boolean, ByVal StFlag As Boolean, ByVal FrFlag As Boolean, ByVal MfFlag As Boolean, ByVal PgFlag As Boolean, ByVal SkuFlag As Boolean) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT DISTINCT MODELID,PRODPACK,MDT.PACKSIZE,PACKTYPE,GEOGRAPHY,VCHAIN,SPFET1,SPFET2, "
            strSql = strSql + "PRODUCER,PACKER,GRAPHICS,STRUCTURES,FORMULA,MANUFACTORY,PACKAGING,MDT.SKU,MDT.PROJECTID,PRODUCERID,PACKERID,GRAPHICSID,  "
            strSql = strSql + "STRUCTUREID,FORMULAID,MANUFACTORYID,PACKAGINGID,SKUID,ISACTIVE FROM MODELDETAILSTEMP MDT "
            strSql = strSql + "LEFT OUTER JOIN PRODUCERDETAILS PD ON PD.NAME=MDT.PRODUCER AND PD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN PACKERDETAILS MD ON MD.NAME=MDT.PACKER AND MD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN GRAPHICSDETAILS GD ON GD.NAME=MDT.GRAPHICS AND GD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN STRUCTUREDETAILS SD ON SD.NAME=MDT.STRUCTURES AND SD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN FORMULADETAILS FD ON FD.NAME=MDT.FORMULA AND FD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN MANUFACTORYDETAILS MFD ON MFD.NAME=MDT.MANUFACTORY AND MFD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN PACKAGINGDETAILS PGD ON PGD.NAME=MDT.PACKAGING AND PGD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN SKUDETAILS SKD ON SKD.NAME=MDT.SKU AND SKD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " "

            If PFlag = True Then
                strSql = strSql + "AND PRODPACK IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=4 AND ISACTIVE='Y') "
            End If

            If SFlag = True Then
                strSql = strSql + "AND MDT.PACKSIZE IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=3 AND ISACTIVE='Y') "
            End If

            If TFlag = True Then
                strSql = strSql + "AND PACKTYPE IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=1 AND ISACTIVE='Y') "
            End If

            If GFlag = True Then
                strSql = strSql + "AND GEOGRAPHY IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=5 AND ISACTIVE='Y') "
            End If

            If CFlag = True Then
                strSql = strSql + "AND VCHAIN IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=2 AND ISACTIVE='Y') "
            End If

            If F1Flag = True Then
                strSql = strSql + "AND SPFET1 IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=6 AND ISACTIVE='Y') "
            End If

            If F2Flag = True Then
                strSql = strSql + "AND SPFET2 IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=7 AND ISACTIVE='Y') "
            End If

            If PdFlag = True Then
                strSql = strSql + "AND PRODUCER IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=8 AND ISACTIVE='Y') "
            End If

            If MatFlag = True Then
                strSql = strSql + "AND PACKER IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=9 AND ISACTIVE='Y') "
            End If

            If GrFlag = True Then
                strSql = strSql + "AND GRAPHICS IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=10 AND ISACTIVE='Y') "
            End If

            If StFlag = True Then
                strSql = strSql + "AND STRUCTURES IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=11 AND ISACTIVE='Y') "
            End If

            If FrFlag = True Then
                strSql = strSql + "AND FORMULA IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=12 AND ISACTIVE='Y') "
            End If

            If MfFlag = True Then
                strSql = strSql + "AND MANUFACTORY IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=13 AND ISACTIVE='Y') "
            End If

            If PgFlag = True Then
                strSql = strSql + "AND PACKAGING IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=14 AND ISACTIVE='Y') "
            End If

            If SkuFlag = True Then
                strSql = strSql + "AND MDT.SKU IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=15 AND ISACTIVE='Y') "
            End If

            strSql = strSql + "ORDER BY MODELID ASC "


            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetModelDetails(ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()

            strSql = "SELECT DISTINCT MODELID,PRODPACK,MDT.PACKSIZE,PACKTYPE,GEOGRAPHY,VCHAIN,SPFET1,SPFET2, "
            strSql = strSql + "PRODUCER,PACKER,GRAPHICS,STRUCTURES,FORMULA,MANUFACTORY,PACKAGING,MDT.SKU,MDT.PROJECTID,PRODUCERID,PACKERID,GRAPHICSID,  "
            strSql = strSql + "STRUCTUREID,FORMULAID,MANUFACTORYID,PACKAGINGID,SKUID,ISACTIVE FROM MODELDETAILSTEMP MDT "
            strSql = strSql + "LEFT OUTER JOIN PRODUCERDETAILS PD ON PD.NAME=MDT.PRODUCER AND PD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN PACKERDETAILS MD ON MD.NAME=MDT.PACKER AND MD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN GRAPHICSDETAILS GD ON GD.NAME=MDT.GRAPHICS AND GD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN STRUCTUREDETAILS SD ON SD.NAME=MDT.STRUCTURES AND SD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN FORMULADETAILS FD ON FD.NAME=MDT.FORMULA AND FD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN MANUFACTORYDETAILS MFD ON MFD.NAME=MDT.MANUFACTORY AND MFD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN PACKAGINGDETAILS PGD ON PGD.NAME=MDT.PACKAGING AND PGD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "LEFT OUTER JOIN SKUDETAILS SKD ON SKD.NAME=MDT.SKU AND SKD.PROJECTID=MDT.PROJECTID "
            strSql = strSql + "WHERE MDT.PROJECTID=" + ProjectId + " "
            strSql = strSql + "AND ( PRODPACK IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=4 AND ISACTIVE='Y') "
            strSql = strSql + "OR MDT.PACKSIZE IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=3 AND ISACTIVE='Y') "
            strSql = strSql + "OR PACKTYPE IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=1 AND ISACTIVE='Y') "
            strSql = strSql + "OR GEOGRAPHY IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=5 AND ISACTIVE='Y') "
            strSql = strSql + "OR VCHAIN IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=2 AND ISACTIVE='Y') "
            strSql = strSql + "OR SPFET1 IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=6 AND ISACTIVE='Y') "
            strSql = strSql + "OR SPFET2 IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=7 AND ISACTIVE='Y') "
            strSql = strSql + "OR PRODUCER IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=8 AND ISACTIVE='Y') "
            strSql = strSql + "OR PACKER IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=9 AND ISACTIVE='Y') "
            strSql = strSql + "OR GRAPHICS IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=10 AND ISACTIVE='Y') "
            strSql = strSql + "OR STRUCTURES IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=11 AND ISACTIVE='Y') "
            strSql = strSql + "OR FORMULA IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=12 AND ISACTIVE='Y') "
            strSql = strSql + "OR MANUFACTORY IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=13 AND ISACTIVE='Y') "
            strSql = strSql + "OR PACKAGING IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=14 AND ISACTIVE='Y') "
            strSql = strSql + "OR MDT.SKU IN (SELECT VALUE FROM PROJECTCATEGORY WHERE PROJECTID=" + ProjectId + " AND CATEGORYID=15 AND ISACTIVE='Y') )"
            strSql = strSql + "ORDER BY MODELID ASC "
            ds = odbUtil.FillDataSet(strSql, SavvyConnection)

            Return ds
        End Function

        Public Function GetViewProjectDetails(ByVal LicenseId As String) As DataSet
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT TYPE FROM VIEWPROJECT WHERE LICENSEID=" + LicenseId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception

            End Try
        End Function

        Public Function GetAnalystUserDetails(ByVal LicenseId As String) As DataSet
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT USR.USERID,USERNAME,FIRSTNAME,LASTNAME FROM ECON.USERS USR "
                strSql = strSql + "INNER JOIN  ECON.USERCONTACTS USC ON USC.USERID=USR.USERID  WHERE USR.LICENSEID=" + LicenseId + " AND ISANALYST='Y'"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception

            End Try
        End Function

        Public Function GetMailUserDetails(ByVal UserId As String) As DataSet
            Dim strSql As String = String.Empty
            Dim ds As New DataSet
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT USR.USERID,USERNAME,FIRSTNAME,LASTNAME,EMAILADDRESS FROM ECON.USERS USR "
                strSql = strSql + "INNER JOIN  ECON.USERCONTACTS USC ON USC.USERID=USR.USERID  WHERE USR.USERID=" + UserId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception

            End Try
        End Function

        Public Function GetAlliedMemberMail(ByVal code As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM SAVVYPADMINMAIL  "
                StrSql = StrSql + "WHERE CODE='" + code.Trim() + "' "
                Dts = odbUtil.FillDataSet(StrSql, ConfigurationConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetUserId:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        'BACKBUTTON
        Public Function ChkProjectNm(ByVal ProjID As String, ByVal ProjNm As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT 1 FROM PROJECTDETAILS WHERE UPPER(TITLE)='" + ProjNm.ToUpper() + "' AND PROJECTID=" + ProjID
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:ChkProjectNm:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function ChkProjectDate(ByVal ProjID As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT 1 FROM PROJECTDATE WHERE PROJECTID=" + ProjID + " AND DATETYPEID=2"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:ChkProjectDate:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function ChkQuickPriceData(ByVal ProjID As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT 1 FROM PROJQUICKPRICE WHERE PROJECTID=" + ProjID
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:ChkQuickPriceData:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
#End Region

#Region "Producer Details"
        Public Function GetProducerDetails(ByVal ProducerId As String, ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT PRODUCERID,NAME,LOCATION,CAPACITY,LINES,INFORMATION "
                strSql = strSql + "FROM PRODUCERDETAILS WHERE PROJECTID=" + ProjectId + " AND PRODUCERID=" + ProducerId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetExistProducerDetails(ByVal Name As String, ByVal ProjectId As String, ByVal ProducerId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM PRODUCERDETAILS WHERE PROJECTID=" + ProjectId + " AND NAME='" + Name + "' AND PRODUCERID NOT IN (" + ProducerId + ")"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "PACKER Details"
        Public Function GetPackerDetails(ByVal PackerId As String, ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT PACKERID,NAME,LOCATION,CAPACITY,LINES,INFORMATION "
                strSql = strSql + "FROM PACKERDETAILS WHERE PROJECTID=" + ProjectId + " AND PACKERID=" + PackerId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetExistPackerDetails(ByVal Name As String, ByVal ProjectId As String, ByVal PackerId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM PACKERDETAILS WHERE PROJECTID=" + ProjectId + " AND NAME='" + Name + "' AND PACKERID NOT IN (" + PackerId + ")"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "Graphics Details"
        Public Function GetGraphicsDetails(ByVal GraphicsId As String, ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT GRAPHICSID,NAME,DESIGN,COLORS,VOLDESIGN,INFORMATION "
                strSql = strSql + "FROM GRAPHICSDETAILS WHERE PROJECTID=" + ProjectId + " AND GRAPHICSID=" + GraphicsId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetExistGraphicDetails(ByVal Name As String, ByVal ProjectId As String, ByVal GraphicId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM GRAPHICSDETAILS WHERE PROJECTID=" + ProjectId + " AND NAME='" + Name + "' AND GRAPHICSID NOT IN (" + GraphicId + ")"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "Structure Details"
        Public Function GetStructureDetails(ByVal StructureId As String, ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT STRUCTUREID,NAME,PACKSIZE,MAT1,MAT2,MAT3,MAT4,MAT5,MAT6,MAT7,MAT8,MAT9,MAT10,"
                strSql = strSql + "DIM1,DIM2,DIM3,DIM4,DIM5,DIM6,DIM7,DIM8,DIM9,DIM10,DEN1,DEN2,DEN3,DEN4,DEN5,DEN6,DEN7,DEN8,DEN9,DEN10,"
                strSql = strSql + "PRICE1,PRICE2,PRICE3,PRICE4,PRICE5,PRICE6,PRICE7,PRICE8,PRICE9,PRICE10,INFO1,INFO2,INFO3,INFO4,INFO5,INFO6,INFO7,INFO8,INFO9,INFO10 "
                strSql = strSql + "FROM STRUCTUREDETAILS WHERE PROJECTID=" + ProjectId + " AND STRUCTUREID=" + StructureId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetExistStructureDetails(ByVal Name As String, ByVal ProjectId As String, ByVal StructureId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM STRUCTUREDETAILS WHERE PROJECTID=" + ProjectId + " AND NAME='" + Name + "' AND STRUCTUREID NOT IN (" + StructureId + ")"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "Formula Details"
        Public Function GetFormulaDetails(ByVal FormulaId As String, ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT FORMULAID,NAME,PACKSIZE,MAT1,MAT2,MAT3,MAT4,MAT5,MAT6,MAT7,MAT8,MAT9,MAT10,VOL1,VOL2,VOL3,VOL4,VOL5,"
                strSql = strSql + "VOL6,VOL7,VOL8,VOL9,VOL10,PRICE1,PRICE2,PRICE3,PRICE4,PRICE5,PRICE6,PRICE7,PRICE8,PRICE9,PRICE10,INFO1,INFO2,INFO3,INFO4,INFO5, "
                strSql = strSql + "INFO6,INFO7,INFO8,INFO9,INFO10 FROM FORMULADETAILS WHERE PROJECTID=" + ProjectId + " AND FORMULAID=" + FormulaId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetExistFormulaDetails(ByVal Name As String, ByVal ProjectId As String, ByVal FormulaId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM FORMULADETAILS WHERE PROJECTID=" + ProjectId + " AND NAME='" + Name + "' AND FORMULAID NOT IN (" + FormulaId + ")"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "Manufactory Details"
        Public Function GetManufactoryDetails(ByVal ManufactoryId As String, ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT MANUFACTORYID,NAME,ORDERSIZE,RUNSIZE,PP1,PP2,PP3,PP4,PP5,PP6,PP7,PP8,PP9,PP10,MAC1,MAC2,MAC3,MAC4,MAC5,MAC6,MAC7,MAC8,MAC9,MAC10,"
                strSql = strSql + "RATE1,RATE2,RATE3,RATE4,RATE5,RATE6,RATE7,RATE8,RATE9,RATE10,TPUT1,TPUT2,TPUT3,TPUT4,TPUT5,TPUT6,TPUT7,TPUT8,TPUT9,TPUT10,"
                strSql = strSql + "DTIME1,DTIME2,DTIME3,DTIME4,DTIME5,DTIME6,DTIME7,DTIME8,DTIME9,DTIME10,WASTE1,WASTE2,WASTE3,WASTE4,WASTE5,WASTE6,WASTE7,WASTE8,WASTE9,WASTE10,"
                strSql = strSql + "CC1,CC2,CC3,CC4,CC5,CC6,CC7,CC8,CC9,CC10,INFO1,INFO2,INFO3,INFO4,INFO5,INFO6,INFO7,INFO8,INFO9,INFO10 "
                strSql = strSql + "FROM MANUFACTORYDETAILS WHERE PROJECTID=" + ProjectId + " AND MANUFACTORYID=" + ManufactoryId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetExistManufactoryDetails(ByVal Name As String, ByVal ProjectId As String, ByVal ManufactoryId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM MANUFACTORYDETAILS WHERE PROJECTID=" + ProjectId + " AND NAME='" + Name + "' AND MANUFACTORYID NOT IN (" + ManufactoryId + ")"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "Packaging Details"
        Public Function GetPackagingDetails(ByVal PackagingId As String, ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT PACKAGINGID,NAME,ORDERSIZE,RUNSIZE,PP1,PP2,PP3,PP4,PP5,PP6,PP7,PP8,PP9,PP10,MAC1,MAC2,MAC3,MAC4,MAC5,MAC6,MAC7,MAC8,MAC9,MAC10,"
                strSql = strSql + "RATE1,RATE2,RATE3,RATE4,RATE5,RATE6,RATE7,RATE8,RATE9,RATE10,TPUT1,TPUT2,TPUT3,TPUT4,TPUT5,TPUT6,TPUT7,TPUT8,TPUT9,TPUT10,"
                strSql = strSql + "DTIME1,DTIME2,DTIME3,DTIME4,DTIME5,DTIME6,DTIME7,DTIME8,DTIME9,DTIME10,WASTE1,WASTE2,WASTE3,WASTE4,WASTE5,WASTE6,WASTE7,WASTE8,WASTE9,WASTE10,"
                strSql = strSql + "CC1,CC2,CC3,CC4,CC5,CC6,CC7,CC8,CC9,CC10,INFO1,INFO2,INFO3,INFO4,INFO5,INFO6,INFO7,INFO8,INFO9,INFO10 "
                strSql = strSql + "FROM PACKAGINGDETAILS WHERE PROJECTID=" + ProjectId + " AND PACKAGINGID=" + PackagingId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetExistPackagingDetails(ByVal Name As String, ByVal ProjectId As String, ByVal PackagingId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM PACKAGINGDETAILS WHERE PROJECTID=" + ProjectId + " AND NAME='" + Name + "' AND PACKAGINGID NOT IN (" + PackagingId + ")"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "SKU Details"
        Public Function GetSKUDetails(ByVal SKUId As String, ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT SKUID,NAME,SKU,VOLSKU,INFORMATION "
                strSql = strSql + "FROM SKUDETAILS WHERE PROJECTID=" + ProjectId + " AND SKUID=" + SKUId + ""
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetExistSKUDetails(ByVal Name As String, ByVal ProjectId As String, ByVal SKUId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM SKUDETAILS WHERE PROJECTID=" + ProjectId + " AND NAME='" + Name + "' AND SKUID NOT IN (" + SKUId + ")"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "Save the Grid"
        Public Function GetSequeceDetails(ByVal ProjectId As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT MAX(SEQUENCEID) SEQUENCEID FROM SAVEPROJECTDETAILS WHERE PROJECTID=" + ProjectId + " "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function
#End Region

#Region "Upload File"
        Public Function GetExistFileDetails(ByVal FileName As String, ByVal ProjectId As String, ByVal Type As String) As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Try
                strSql = "SELECT 1 FROM SPECSHEETDETAILS WHERE  FILENAME='" + FileName + "' AND TYPE= '" + Type + "'" ' PROJECTID=" + ProjectId + " '"
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds

            Catch ex As Exception

            End Try

        End Function

        Public Function GetDwnldFileDetails(ByVal ProjectId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "PROJECTID, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "FILENAME, "
                StrSql = StrSql + "TYPE "
                StrSql = StrSql + "FROM SPECSHEETDETAILS "
                StrSql = StrSql + "WHERE PROJECTID=CASE WHEN " + ProjectId + "=-1 THEN PROJECTID ELSE " + ProjectId + " END "
                StrSql = StrSql + " ORDER BY PROJECTID"

                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("DownloadGetDat:GetDwnldDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

		#Region "Benefits"

        Public Function GetExistingBenefitsDetails(ByVal ProjectId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROJECTID,REPLACE(QUANTBNF,'''','&#')QUANTBNF,REPLACE(QUALBNF,'''','&#')QUALBNF,USERID FROM PROJECTBENEFITS "
                StrSql = StrSql + "WHERE PROJECTID=" + ProjectId + " "
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetExistingBenefitsDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region
        Public Function GetDateDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DATETYPEID,DATETYPENAME,ISCLIENT,TOOLTIPDS FROM DATETYPE  "
                StrSql = StrSql + "ORDER BY DATETYPEID ASC "
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetDateDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetExistingDateDetails(ByVal ProjectId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROJECTID,DATETYPEID,TO_CHAR(VALUE,'MM/DD/YYYY') VALUE FROM PROJECTDATE "
                StrSql = StrSql + "WHERE PROJECTID=" + ProjectId + " "
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetDateDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
		
		Public Function MemorizedDetailsOLD1(ByVal UserId As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT USERID,PAGESIZEID,DATETYPEID,DESCTYPE FROM MEMORIZEDDETAILS WHERE USERID=" + UserId + "  "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:InsertProjDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

		Public Function MemorizedDetails(ByVal UserId As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT USERID,PAGESIZEID,DATETYPEID,DESCTYPE,SORTSTATUSID,DISPLAYSTATUSID,DISPLAYMILESTONEID,SORTMILESDATE,SORTMILESID FROM MEMORIZEDDETAILS WHERE USERID=" + UserId + "  "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:MemorizedDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
		
        Public Function GetEmailADetails(ByVal UserId As String, ByVal Text As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MESSAGEID,EMAILANALYST.PROJECTID,SENDFROM,TO_CHAR(RECEIVEDTIME,'mm/dd/yyyy hh:mi:ss AM')RECEIVEDTIME,RECEIVEDTIME RCVTIME,VIEWTIME,ISSENT,CONTENT,SUBJECT,SENDTO,UC.EMAILADDRESS FROMUSER, "
                StrSql = StrSql + " UC1.EMAILADDRESS TOUSER,ISVIEWED,(UC.FIRSTNAME||' '||UC.LASTNAME)FROMUSERNAME, "
                StrSql = StrSql + "(UC1.FIRSTNAME||' '||UC1.LASTNAME)TOUSERNAME,PD.TITLE,ISREPLY,ISFORWARD  "
                StrSql = StrSql + " FROM EMAILANALYST INNER JOIN ECON.USERCONTACTS UC ON UC.USERID=EMAILANALYST.SENDFROM "
                StrSql = StrSql + "INNER JOIN PROJECTDETAILS PD ON PD.PROJECTID=EMAILANALYST.PROJECTID "
                StrSql = StrSql + "INNER JOIN ECON.USERCONTACTS UC1 ON UC1.USERID=EMAILANALYST.SENDTO  WHERE SENDTO=" + UserId + "  "
                StrSql = StrSql + "AND"
                StrSql = StrSql + "( UPPER(CONTENT) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(SUBJECT) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(UC.EMAILADDRESS) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(UC1.EMAILADDRESS) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(UC.FIRSTNAME||' '||UC.LASTNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(UC1.FIRSTNAME||' '||UC1.LASTNAME) LIKE '%" + Text.ToUpper().Trim() + "%' ) "
                StrSql = StrSql + "ORDER BY RCVTIME DESC"
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetEmailADetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMsgDetails(ByVal MsgId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MESSAGEID,SENDFROM,RECEIVEDTIME,VIEWTIME,ISSENT,CONTENT,SUBJECT,SENDTO,UC.EMAILADDRESS FROMUSER,"
                StrSql = StrSql + "UC1.EMAILADDRESS TOUSER,ISVIEWED,PD.TITLE,"
                StrSql = StrSql + "(UC.FIRSTNAME||' '||UC.LASTNAME)USERNAME1,(UC1.FIRSTNAME||' '||UC1.LASTNAME)USERNAME2,ISREPLY,ISFORWARD "
                StrSql = StrSql + " FROM EMAILANALYST INNER JOIN ECON.USERCONTACTS UC ON UC.USERID=EMAILANALYST.SENDFROM "
                StrSql = StrSql + "INNER JOIN ECON.USERCONTACTS UC1 ON UC1.USERID=EMAILANALYST.SENDTO  "
                StrSql = StrSql + "INNER JOIN PROJECTDETAILS PD ON PD.PROJECTID=EMAILANALYST.PROJECTID  WHERE MESSAGEID=" + MsgId + "  "
                StrSql = StrSql + "ORDER BY RECEIVEDTIME DESC"
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetMsgDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserDetail(ByVal UserId As String, ByVal Username As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT UC.USERID,FIRSTNAME,LASTNAME,USERNAME EMAILADDRESS FROM ECON.USERCONTACTS UC "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR ON USR.USERID=UC.USERID "
                StrSql = StrSql + "WHERE USR.LICENSEID IN (SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + ") "
                StrSql = StrSql + "AND "
                StrSql = StrSql + "(UPPER(USERNAME) LIKE '%" + Username.ToUpper().Trim() + "%' )"
                StrSql = StrSql + "ORDER BY USERID "
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetProjTitle(ByVal UserId As String, ByVal Title As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PD.USERID,PROJECTID,TITLE,REPLACE(TITLE,'''','&#')TITLE1 FROM PROJECTDETAILS PD "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR ON USR.USERID=PD.USERID "
                StrSql = StrSql + "WHERE PD.ISVISIBLE='Y' AND USR.LICENSEID IN (SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + ")"
                StrSql = StrSql + "AND "
                StrSql = StrSql + "(UPPER(TITLE) LIKE '%" + Title.ToUpper().Trim() + "%' ) ORDER BY PROJECTID DESC"
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetMessageDetails(ByVal MessageId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MESSAGEID,PD.PROJECTID,TITLE,SENDFROM,SENDTO,CONTENT,SUBJECT ,UC1.EMAILADDRESS FROMUSER, "
                StrSql = StrSql + "UC2.EMAILADDRESS TOUSER,(UC1.FIRSTNAME||' '||UC1.LASTNAME)FROMUSERNAME,TO_CHAR(RECEIVEDTIME,'mm/dd/yyyy hh:mi:ss')RECEIVEDTIME FROM PROJECTDETAILS PD "
                StrSql = StrSql + "INNER JOIN EMAILANALYST EA ON EA.PROJECTID=PD.PROJECTID "
                StrSql = StrSql + "INNER JOIN ECON.USERCONTACTS UC1 ON UC1.USERID=EA.SENDFROM "
                StrSql = StrSql + "INNER JOIN ECON.USERCONTACTS UC2 ON UC2.USERID=EA.SENDTO "
                StrSql = StrSql + "WHERE MESSAGEID=" + MessageId + " "
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function
		
		Public Function GetSortStatusDetails() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT SORTSTATUSID,DETAILS FROM SORTSTATUS ORDER BY SORTSTATUSID ASC"

                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetSortStatusDetails:" + ex.Message.ToString())
            End Try
        End Function
		
		Public Function GetSortMilestoneDetails() As DataSet
            Dim ds As New DataSet
            Dim strSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT SORTMILESID,SORTMILESNAME,SORTMILESORDER FROM SORTMILESTONE ORDER BY SORTMILESID ASC"

                ds = odbUtil.FillDataSet(strSql, SavvyConnection)

                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetSortMilestoneDetails:" + ex.Message.ToString())
            End Try
        End Function
		
		Public Function GetEmailADetailsSend(ByVal UserId As String, ByVal Text As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MESSAGEID,EMAILANALYST.PROJECTID,SENDFROM,TO_CHAR(RECEIVEDTIME,'mm/dd/yyyy hh:mi:ss')RECEIVEDTIME,TO_CHAR(SENDTIME,'mm/dd/yyyy hh:mi:ss AM')SENDTIME,SENDTIME SENTIME,VIEWTIME,ISSENT,CONTENT,SUBJECT,SENDTO,UC.EMAILADDRESS FROMUSER, "
                StrSql = StrSql + " UC1.EMAILADDRESS TOUSER,ISVIEWED,(UC.FIRSTNAME||' '||UC.LASTNAME)FROMUSERNAME, "
                StrSql = StrSql + "(UC1.FIRSTNAME||' '||UC1.LASTNAME)TOUSERNAME,PD.TITLE,ISREPLY,ISFORWARD  "
                StrSql = StrSql + " FROM EMAILANALYST INNER JOIN ECON.USERCONTACTS UC ON UC.USERID=EMAILANALYST.SENDFROM "
                StrSql = StrSql + "INNER JOIN PROJECTDETAILS PD ON PD.PROJECTID=EMAILANALYST.PROJECTID "
                StrSql = StrSql + "INNER JOIN ECON.USERCONTACTS UC1 ON UC1.USERID=EMAILANALYST.SENDTO WHERE ISSENT='Y' AND "
                StrSql = StrSql + "SENDFROM = " + UserId + " "
                StrSql = StrSql + "AND"
                StrSql = StrSql + "( UPPER(CONTENT) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(SUBJECT) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(UC.EMAILADDRESS) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(UC1.EMAILADDRESS) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(UC.FIRSTNAME||' '||UC.LASTNAME) LIKE '%" + Text.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(UC1.FIRSTNAME||' '||UC1.LASTNAME) LIKE '%" + Text.ToUpper().Trim() + "%' ) "
                StrSql = StrSql + "ORDER BY SENTIME DESC"
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetEmailADetailsSend:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
		
#Region "UniversalFileUpload"
        Public Function GetUserFileDetails(ByVal ProjectId As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                StrSql = "SELECT PROJECTID, PROJECTFILEID, USR.USERID, USR.USERNAME,TO_CHAR(UPLOADDATE,'MM/DD/YYYY')UPLOADDATE,FILETYPE.TYPEID,  "
                StrSql = StrSql + "FILENAME, TYPENAME, FILEPATH, FO.OWNERID, FO.OWNERNAME "
                StrSql = StrSql + "FROM PROJECTFILES "
                StrSql = StrSql + "INNER JOIN FILETYPE on FILETYPE.TYPEID=PROJECTFILES.TYPEID "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR on USR.USERID=PROJECTFILES.UPLOADBY "
                StrSql = StrSql + "INNER JOIN FILEOWNER FO ON FO.OWNERID=PROJECTFILES.OWNERID "
                StrSql = StrSql + "WHERE PROJECTID=CASE WHEN " + ProjectId + "=-1 THEN PROJECTID ELSE " + ProjectId + " END  "
                StrSql = StrSql + "AND FILETYPE.TYPEID IN (SELECT TYPEID FROM FILETYPE WHERE UPPER(TYPENAME) IN ('UNIVERSAL','STRUCTURE','FORMULA','GENERAL','DELIVERABLE','UNIVERSAL1') )  "
                StrSql = StrSql + "ORDER BY PROJECTFILEID DESC  "
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("DownloadGetDat:GetUserFileDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetFileDetails(ByVal ProjectId As String, ByVal FileName As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                StrSql = "SELECT 1 FROM PROJECTFILES "
                StrSql = StrSql + "WHERE PROJECTID=" + ProjectId + " AND UPPER(FILENAME)='" + FileName.ToUpper() + "' "

                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("DownloadGetDat:GetFileDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function AddFileDetails(ByVal ProjId As String, ByVal FileName As String, ByVal FilePath As String, ByVal Type As String, ByVal UserId As String, ByVal OwnerId As String) As Integer
            Dim strSql As String = String.Empty
            Dim ProjFileId As New Integer
            Dim odbUtil As New DBUtil()
            Try
                strSql = "SELECT SEQPFILEID.NEXTVAL FROM DUAL "
                ProjFileId = odbUtil.FillData(strSql, SavvyConnection)

                'Insert Data into STRUCTUREDETAILS table
                strSql = String.Empty
                strSql = "INSERT INTO PROJECTFILES (PROJECTFILEID,PROJECTID,FILENAME,FILEPATH,UPLOADDATE,UPLOADBY,TYPEID,OWNERID) "
                strSql = strSql + "SELECT " + ProjFileId.ToString() + "," + ProjId + ",'" + FileName + "','" + FilePath + "',SYSDATE, "
                strSql = strSql + " " + UserId + ", (SELECT TYPEID FROM FILETYPE WHERE TYPENAME='" + Type + "')," + OwnerId + " FROM DUAL "
                odbUtil.UpIns(strSql, SavvyConnection)

                Return ProjFileId
            Catch ex As Exception
                Throw New Exception("SavvyUpInsData:AddFileDetails:" + ex.Message.ToString())
            End Try
        End Function
		
		  Public Function GetFileName(ByVal UserId As String, ByVal PROJECTID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT FILENAME,UPLOADBY FROM PROJECTFILES WHERE PROJECTID='" + PROJECTID + "' AND UPLOADBY='" + UserId + "' "
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetFileName:" + ex.Message.ToString())
                Throw ex
            End Try
        End Function

        Public Function GetProjDetails(ByVal UserId As String, ByVal Title As String) As DataSet
            Dim strSql As String = String.Empty
            Dim dbUtil As New DBUtil()
            Dim ds As New DataSet
            Try
                strSql = "SELECT PRD.PROJECTID,TITLE,KEYWORD,PRD.USERID,USR.USERNAME OWNER,REPLACE(DESCRIPTION,'''','&#')DESCRIPTION FROM PROJECTDETAILS PRD "
                strSql = strSql + "INNER JOIN ECON.USERS USR ON USR.USERID=PRD.USERID WHERE USR.USERID=" + UserId + " AND TITLE='" + Title + "' "
                ds = dbUtil.FillDataSet(strSql, SavvyConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetProjDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function GetFileDetails_new(ByVal ProjectId As String, ByVal FileName As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                StrSql = "SELECT PROJECTFILEID FROM PROJECTFILES "
                StrSql = StrSql + "WHERE PROJECTID=" + ProjectId + " AND UPPER(FILENAME)='" + FileName.ToUpper().Replace("'", "''") + "' "

                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("DownloadGetDat:GetFileDetails_new:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAllFileNameByProjID(ByVal PROJECTID As String) As String
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim StrFileNm As String = String.Empty
            Try
                StrSql = "SELECT FILENAME,UPLOADBY FROM PROJECTFILES WHERE PROJECTID='" + PROJECTID + "'"
                Dts = odbUtil.FillDataSet(StrSql, SavvyConnection)

                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        If i = 0 Then
                            StrFileNm = Dts.Tables(0).Rows(i).Item("FILENAME").ToString()
                        Else
                            StrFileNm = StrFileNm + "," + Dts.Tables(0).Rows(i).Item("FILENAME").ToString()
                        End If
                    Next
                End If

                Return StrFileNm
            Catch ex As Exception
                Throw New Exception("SavvyGetData:GetAllFileNameByProjID:" + ex.Message.ToString())
                Throw ex
            End Try
        End Function

#End Region

#Region "Subscription Data"

        Public Function GetSubscrpDetails(ByVal Keyword As String, ByVal LicenseID As String) As DataSet
            Dim Dts As New DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT SUBSCDETAILID,PRICE,TO_CHAR(STARTDATE,'FMMonth DD, YYYY')STARTDATE,TO_CHAR(ENDDDATE,'FMMonth DD, YYYY')ENDDDATE,"
                StrSql = StrSql + "STARTDATE SDATE,ENDDDATE EDATE, SUBSCNAME,SUBSUBSCNAME,LM.LICENSEID,LICENSENAME  "
                StrSql = StrSql + "FROM SUBSCRIPTIONDETAILS SD  "
                StrSql = StrSql + "INNER JOIN SUBSCRIPTIONTYPE ST ON ST.SUBSCID =SD.SUBSCTYPEID AND ST.SUBSCID=1 "
                StrSql = StrSql + "INNER JOIN ECON.LICENSEMASTER LM ON LM.LICENSEID =SD.LICENSEID "
                StrSql = StrSql + "INNER JOIN SUBSUBSCRIPTIONTYPE SST ON SST.SUBSUBSCID=SD.SUBSUBSCID "
                StrSql = StrSql + "WHERE SD.LICENSEID=" + LicenseID + " "
                'StrSql = StrSql + "AND (NVL(UPPER(SUBSCNAME),'#') LIKE '%" + Keyword.ToUpper().Replace("'", "''") + "%'"
                'StrSql = StrSql + "OR NVL(UPPER(LICENSENAME),'#') LIKE '%" + Keyword.ToUpper().Replace("'", "''") + "%' "
                'StrSql = StrSql + "OR NVL(UPPER(STARTDATE),'#') LIKE '%" + Keyword.ToUpper().Replace("'", "''") + "%' "
                'StrSql = StrSql + "OR NVL(UPPER(ENDDDATE),'#') LIKE '%" + Keyword.ToUpper().Replace("'", "''") + "%' "
                'StrSql = StrSql + "OR NVL(UPPER(PRICE),'#') LIKE '%" + Keyword.ToString().Replace("'", "''") + "%') "
                StrSql = StrSql + "ORDER BY EDATE DESC"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetProjDetailBySubScription(ByVal keyword As String, ByVal LicenseID As String, ByVal SubSDate As String, ByVal SubEDate As String) As DataSet
            Dim Ds As New DataSet()
            Dim DsProjDates As New DataSet()
            Dim dtOutPut As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim StrSqlDate As String = String.Empty
            Dim StrSql1 As String = String.Empty
            Dim strSqlOutPut As String = String.Empty
            Dim AccCheckDays As Integer = -1
            Try
                StrSql = String.Empty
                StrSql = "SELECT U.USERID,U.USERNAME,U.LICENSEID,P.PROJECTID,P.TITLE,NVL(PROJCNT.COMPPROJ,0)ModelCompleted  "
                StrSql = StrSql + "FROM PROJECTDETAILS P "
                StrSql = StrSql + "INNER JOIN ECON.USERS U ON U.USERID=P.USERID AND U.LICENSEID=" + LicenseID.ToString() + " "
                'TESTING_PK
                'For Submit date
                StrSql = StrSql + "LEFT OUTER JOIN  "
                StrSql = StrSql + "(SELECT PDA.VALUE,DT.DATETYPENAME,PDA.PROJECTID FROM PROJECTDATE PDA INNER JOIN DATETYPE DT ON DT.DATETYPEID=PDA.DATETYPEID AND DT.DATETYPEID=1) DP1 ON DP1.PROJECTID=P.PROJECTID "
                'For CompletedModel COunt
                StrSql = StrSql + "LEFT JOIN  "
                StrSql = StrSql + "(SELECT COUNT(PROJECTDETAILCASESID)COMPPROJ,PDSS.PROJECTID FROM PROJECTDETAILCASES PDC INNER JOIN PROJECTDETAILS PDSS ON PDC.PROJECTID=PDSS.PROJECTID "
                StrSql = StrSql + "GROUP BY PDSS.PROJECTID) PROJCNT ON PROJCNT.PROJECTID=P.PROJECTID "
                'END
                StrSql = StrSql + "WHERE DP1.VALUE BETWEEN TO_DATE('" + SubSDate + "','MM/DD/YYYY') "
                StrSql = StrSql + "AND TO_DATE('" + SubEDate + "','MM/DD/YYYY')"
                Ds = odbUtil.FillDataSet(StrSql, SavvyConnection)

                If Ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Ds.Tables(0).Rows.Count - 1
                        AccCheckDays = -1
                        StrSqlDate = String.Empty
                        StrSqlDate = "SELECT TO_CHAR(PDA.VALUE,'FMMonth DD, YYYY')VALUE,DT.DATETYPENAME,PDA.PROJECTID,PDA.DATETYPEID  "
                        StrSqlDate = StrSqlDate + "FROM PROJECTDATE PDA "
                        StrSqlDate = StrSqlDate + "INNER JOIN DATETYPE DT ON DT.DATETYPEID =PDA.DATETYPEID AND PDA.PROJECTID=" + Ds.Tables(0).Rows(i).Item("PROJECTID").ToString() + " "
                        StrSqlDate = StrSqlDate + "AND  DT.DATETYPEID IN (1,4) "
                        StrSqlDate = StrSqlDate + "ORDER BY DT.DATETYPEID "
                        DsProjDates = odbUtil.FillDataSet(StrSqlDate, SavvyConnection)

                        If DsProjDates.Tables(0).Rows.Count = 2 Then
                            AccCheckDays = DateDiff(DateInterval.Day, CDate(DsProjDates.Tables(0).Rows(0).Item("VALUE")), DsProjDates.Tables(0).Rows(1).Item("VALUE"))
                        End If
                        If i = 0 Then
                            StrSql1 = "SELECT " + Ds.Tables(0).Rows(i).Item("PROJECTID").ToString() + " PROJECTID, "
                            StrSql1 = StrSql1 + "'" + Ds.Tables(0).Rows(i).Item("TITLE").ToString().Replace("'", "''") + "' PROJTITLE, "
                            StrSql1 = StrSql1 + "'" + Ds.Tables(0).Rows(i).Item("USERNAME").ToString().Replace("'", "''") + "' POWNER, "
                            If AccCheckDays = -1 Then
                                If DsProjDates.Tables(0).Rows.Count <> 0 Then
                                    If DsProjDates.Tables(0).Rows(0).Item("DATETYPEID").ToString() = 1 Then
                                        StrSql1 = StrSql1 + "'" + DsProjDates.Tables(0).Rows(0).Item("VALUE").ToString() + "' SUBMITDATE, "
                                        StrSql1 = StrSql1 + "'' COMPLETEDATE, "
                                    Else
                                        StrSql1 = StrSql1 + "'' SUBMITDATE, "
                                        StrSql1 = StrSql1 + "'" + DsProjDates.Tables(0).Rows(0).Item("VALUE").ToString() + "' COMPLETEDATE, "
                                    End If
                                Else
                                    StrSql1 = StrSql1 + "'' SUBMITDATE, "
                                    StrSql1 = StrSql1 + "'' COMPLETEDATE, "
                                End If
                                StrSql1 = StrSql1 + "0 PROJDAYS, "
                            Else
                                StrSql1 = StrSql1 + "'" + DsProjDates.Tables(0).Rows(0).Item("VALUE").ToString() + "' SUBMITDATE, "
                                StrSql1 = StrSql1 + "'" + DsProjDates.Tables(0).Rows(1).Item("VALUE").ToString() + "' COMPLETEDATE, "
                                StrSql1 = StrSql1 + "" + AccCheckDays.ToString() + " PROJDAYS, "
                            End If
                            StrSql1 = StrSql1 + "" + Ds.Tables(0).Rows(i).Item("ModelCompleted").ToString() + " ModelCompleted "
                            StrSql1 = StrSql1 + "FROM DUAL "
                        Else
                            StrSql1 = StrSql1 + "UNION ALL "
                            StrSql1 = StrSql1 + "SELECT " + Ds.Tables(0).Rows(i).Item("PROJECTID").ToString() + " PROJECTID, "
                            StrSql1 = StrSql1 + "'" + Ds.Tables(0).Rows(i).Item("TITLE").ToString().Replace("'", "''") + "' PROJTITLE, "
                            StrSql1 = StrSql1 + "'" + Ds.Tables(0).Rows(i).Item("USERNAME").ToString().Replace("'", "''") + "' POWNER, "
                            If AccCheckDays = -1 Then
                                If DsProjDates.Tables(0).Rows.Count <> 0 Then
                                    If DsProjDates.Tables(0).Rows(0).Item("DATETYPEID").ToString() = 1 Then
                                        StrSql1 = StrSql1 + "'" + DsProjDates.Tables(0).Rows(0).Item("VALUE").ToString() + "' SUBMITDATE, "
                                        StrSql1 = StrSql1 + "'' COMPLETEDATE, "
                                    Else
                                        StrSql1 = StrSql1 + "'' SUBMITDATE, "
                                        StrSql1 = StrSql1 + "'" + DsProjDates.Tables(0).Rows(0).Item("VALUE").ToString() + "' COMPLETEDATE, "
                                    End If
                                Else
                                    StrSql1 = StrSql1 + "'' SUBMITDATE, "
                                    StrSql1 = StrSql1 + "'' COMPLETEDATE, "
                                End If
                                StrSql1 = StrSql1 + "0 PROJDAYS, "
                            Else
                                StrSql1 = StrSql1 + "'" + DsProjDates.Tables(0).Rows(0).Item("VALUE").ToString() + "' SUBMITDATE, "
                                StrSql1 = StrSql1 + "'" + DsProjDates.Tables(0).Rows(1).Item("VALUE").ToString() + "' COMPLETEDATE, "
                                StrSql1 = StrSql1 + "" + AccCheckDays.ToString() + " PROJDAYS, "
                            End If
                            StrSql1 = StrSql1 + "" + Ds.Tables(0).Rows(i).Item("ModelCompleted").ToString() + " ModelCompleted "
                            StrSql1 = StrSql1 + "FROM DUAL "
                        End If
                    Next
                End If

                If StrSql1 <> "" Then
                    strSqlOutPut = "SELECT * FROM ( " + StrSql1 + " ) DUAL "
                    strSqlOutPut = strSqlOutPut + "WHERE NVL(UPPER(PROJTITLE),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(POWNER),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(SUBMITDATE),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(COMPLETEDATE),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "ORDER BY PROJECTID"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SavvyConnection)
                Else
                    StrSql1 = "SELECT '' PROJECTID,'' PROJTITLE,  '' POWNER, '' SUBMITDATE, '' COMPLETEDATE,'' PROJDAYS,''ModelCompleted FROM PROJECTDETAILS WHERE PROJECTID=0"
                    dtOutPut = odbUtil.FillDataSet(StrSql1, SavvyConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetProjDetailBySubScription_Optmz(ByVal keyword As String, ByVal LicenseID As String, ByVal SubSDate As String, ByVal SubEDate As String) As DataSet
            Dim Ds As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT P.PROJECTID,P.TITLE PROJTITLE,U.USERNAME POWNER,TO_CHAR(DP1.VALUE,'FMMonth DD, YYYY')SUBMITDATE,"
                StrSql = StrSql + "TO_CHAR(DP2.VALUE,'FMMonth DD, YYYY')COMPLETEDATE,ROUND((DP2.VALUE-DP1.VALUE))PROJDAYS,NVL(PROJCNT.COMPPROJ,0)MODELCOMPLETED,"
                StrSql = StrSql + "DP1.VALUE SDATE,DP2.VALUE CDATE,NVL((NVL(E1S1CNT.E1S1MDLCNT,0)+NVL(E2S2CNT.E2S2MDLCNT,0)),0)GRPMODELCOMPLETED "
                StrSql = StrSql + "FROM PROJECTDETAILS P "
                StrSql = StrSql + "INNER JOIN ECON.USERS U ON U.USERID=P.USERID AND U.LICENSEID=" + LicenseID.ToString() + " "
                StrSql = StrSql + "LEFT OUTER JOIN "
                StrSql = StrSql + "(SELECT COUNT(PROJECTDETAILCASESID)COMPPROJ,PDSS.PROJECTID FROM PROJECTDETAILCASES PDC INNER JOIN PROJECTDETAILS PDSS ON PDC.PROJECTID=PDSS.PROJECTID "
                StrSql = StrSql + "GROUP BY PDSS.PROJECTID) PROJCNT ON PROJCNT.PROJECTID=P.PROJECTID "

                'Groups model count
                StrSql = StrSql + "LEFT OUTER JOIN "
                StrSql = StrSql + "(SELECT COUNT(GC.CASEID)E1S1MDLCNT,PG.PROJECTID FROM PROJECTGRP PG INNER JOIN ECON.GROUPS G ON G.GROUPID=PG.GROUPID AND PG.MODEL IN (1,2) "
                StrSql = StrSql + "INNER JOIN ECON.GROUPCASES GC ON GC.GROUPID=G.GROUPID GROUP BY PG.PROJECTID) E1S1CNT ON E1S1CNT.PROJECTID=P.PROJECTID "
                StrSql = StrSql + "LEFT OUTER JOIN "
                StrSql = StrSql + "(SELECT COUNT(GC.CASEID)E2S2MDLCNT,PG.PROJECTID FROM PROJECTGRP PG INNER JOIN ECON2.GROUPS G ON G.GROUPID=PG.GROUPID AND PG.MODEL IN (3,4) "
                StrSql = StrSql + "INNER JOIN ECON2.GROUPCASES GC ON GC.GROUPID=G.GROUPID GROUP BY PG.PROJECTID) E2S2CNT ON E2S2CNT.PROJECTID=P.PROJECTID "
                'Groups model count

                StrSql = StrSql + "LEFT OUTER JOIN "
                StrSql = StrSql + "(SELECT PDA.VALUE,DT.DATETYPENAME,PDA.PROJECTID FROM PROJECTDATE PDA INNER JOIN DATETYPE DT ON DT.DATETYPEID =PDA.DATETYPEID  AND DT.DATETYPEID=1) DP1 ON DP1.PROJECTID=P.PROJECTID "
                StrSql = StrSql + "LEFT OUTER JOIN "
                StrSql = StrSql + "(SELECT PDA.VALUE,DT.DATETYPENAME,PDA.PROJECTID FROM PROJECTDATE PDA INNER JOIN DATETYPE DT ON DT.DATETYPEID =PDA.DATETYPEID  AND DT.DATETYPEID=4)DP2 ON DP2.PROJECTID=P.PROJECTID "
                StrSql = StrSql + "WHERE DP1.VALUE BETWEEN TO_DATE('" + SubSDate + "','MM/DD/YYYY') "
                StrSql = StrSql + "AND TO_DATE('" + SubEDate + "','MM/DD/YYYY') "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(PROJTITLE),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(POWNER),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(PROJDAYS),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MODELCOMPLETED),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(GRPMODELCOMPLETED),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(SUBMITDATE),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(COMPLETEDATE),'#') LIKE '%" + keyword.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY CDATE DESC "
                Ds = odbUtil.FillDataSet(StrSql, SavvyConnection)

                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSubScrInfo(ByVal SubscriptionID As String) As DataSet
            Dim Dts As New DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SUBSCDETAILID,LICENSEID,TO_CHAR(STARTDATE,'FMMonth DD, YYYY')STARTDATE,TO_CHAR(ENDDDATE,'FMMonth DD, YYYY')ENDDDATE "
                StrSql = StrSql + "FROM SUBSCRIPTIONDETAILS "
                StrSql = StrSql + "WHERE SUBSCDETAILID=" + SubscriptionID.ToString()
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSubDatesBySubID(ByVal SubscriptionIDs As String) As DataSet
            Dim Dts As New DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "TO_CHAR(MIN(STARTDATE),'MM/DD/YYYY')SDATE, "
                StrSql = StrSql + "TO_CHAR(MAX(ENDDDATE),'MM/DD/YYYY')EDATE, "
                StrSql = StrSql + "TO_CHAR(MIN(STARTDATE),'FMMonth DD, YYYY')INNERSDATE, "
                StrSql = StrSql + "TO_CHAR(MAX(ENDDDATE),'FMMonth DD, YYYY')INNEREDATE "
                StrSql = StrSql + "FROM SUBSCRIPTIONDETAILS WHERE SUBSCDETAILID IN (" + SubscriptionIDs + ") "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSubLatestSub(ByVal LicenseID As String) As DataSet
            Dim Dts As New DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT SUBSCDETAILID,TO_CHAR(STARTDATE,'FMMonth DD, YYYY')STARTDATE,TO_CHAR(ENDDDATE,'FMMonth DD, YYYY')ENDDDATE,  "
                StrSql = StrSql + "TO_CHAR(STARTDATE,'MM/DD/YYYY')SDATE,TO_CHAR(ENDDDATE,'MM/DD/YYYY')EDATE "
                StrSql = StrSql + "FROM SUBSCRIPTIONDETAILS SD "
                StrSql = StrSql + "INNER JOIN SUBSCRIPTIONTYPE ST ON ST.SUBSCID =SD.SUBSCTYPEID AND ST.SUBSCID=1 "
                StrSql = StrSql + "INNER JOIN ECON.LICENSEMASTER LM ON LM.LICENSEID =SD.LICENSEID "
                StrSql = StrSql + "WHERE SD.LICENSEID=" + LicenseID.ToString() + " "
                StrSql = StrSql + "AND TO_CHAR(ENDDDATE,'MM/DD/YYYY')=(SELECT TO_CHAR(MAX(SDI.ENDDDATE),'MM/DD/YYYY')ENDDATE "
                StrSql = StrSql + "FROM SUBSCRIPTIONDETAILS SDI INNER JOIN SUBSCRIPTIONTYPE STT ON STT.SUBSCID=SDI.SUBSCTYPEID AND STT.SUBSCID=1 WHERE SDI.LICENSEID=" + LicenseID.ToString() + ")"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
		
    End Class
	
End Class
