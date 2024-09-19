Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class SavvyProGetData
    Dim odbUtil As New DBUtil()
    Dim SavvyProConnection As String = System.Configuration.ConfigurationManager.AppSettings("SavvyPackProConnectionString")
    Dim EmoConnection As String = System.Configuration.ConfigurationManager.AppSettings("EmonitorConnectionString")

#Region "Proposal Manager"

    Public Function GetProposalDetails(ByVal keyword As String, ByVal GroupID As String) As DataSet
        Dim Dts As New DataSet()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT  "
            StrSql = StrSql + "PID,GROUPID,SKUID,SPECID,PRODUCTTYPE,PACAKAGETYPE,WIDTH, "
            StrSql = StrSql + "LENGTH,HEIGHT,WEIGHT,STRUCTURE,THICKNESS,VOLUME,PRICE,SETUP,GROUPDES,SKUDES "
            StrSql = StrSql + "FROM PROPOSALMANAGER "
            StrSql = StrSql + "ORDER BY PID "
            Dts = odbUtil.FillDataSet(StrSql, SavvyProConnection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("SavvyProGetData:GetProposalDetails:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

#End Region

#Region "Spec Manager"

    Public Function GetSpecDetails(ByVal keyword As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT "
            StrSql = StrSql + "(CASE WHEN GROUPID=0 THEN 0 ELSE GROUPID END)GROUPID,"
            StrSql = StrSql + "(CASE WHEN GROUPID='0' THEN 'Nothing Selected' ELSE GROUPDES END)GROUPDES,"
            StrSql = StrSql + "SPECID,SKUID,SKUDES,"
            StrSql = StrSql + "PRODUCTTYPE,PACAKAGETYPE, "
            StrSql = StrSql + "WIDTH,LENGTH,HEIGHT,WEIGHT, "
            StrSql = StrSql + "STRUCTURE,THICKNESS,VOLUME,PRICE,SETUP "
            StrSql = StrSql + "FROM SPECSDETAILS "

            Dts = odbUtil.FillDataSet(StrSql, SavvyProConnection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("E1GetData:GetSpecDetails:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

#End Region

#Region "Invest Manager"
    Public Function GetInvestmentManagerDet(ByVal Userid As String) As DataSet
        Dim ds As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim strSql As String = ""
        Try

            strSql = "Select imDescription,amounts,comments from investmentmanager where userid='" + Userid.ToString() + "'"

            ds = odbUtil.FillDataSet(strSql, SavvyProConnection)
            Return ds
        Catch ex As Exception
            Throw New Exception("SavvyProGetData:GetInvestmentManagerDet:" + ex.Message.ToString())
            Return ds
        End Try
    End Function
    Public Function GetCountInvestmentManagerDet(ByVal Userid As String) As Integer
        Dim ds As Integer
        Dim odbUtil As New DBUtil()
        Dim strSql As String = ""
        Try

            strSql = "Select count(*) from investmentmanager where userid='" + Userid.ToString() + "'"

            ds = odbUtil.FillData(strSql, SavvyProConnection)
            Return ds
        Catch ex As Exception
            Throw New Exception("SavvyProGetData:GetCountInvestmentManagerDet:" + ex.Message.ToString())
            Return ds
        End Try
    End Function
#End Region

#Region "Price&Cost"

    Public Function GetPriceCost() As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
            StrSql = "SELECT SKUID,SKUDESC,'Material' AS PDES3,'Labor' AS PDES4,'Packaging' AS PDES5,'Distribution' AS PDES6,'Other Variable'  AS PDES7,'Variable Margin' AS PDES8,"
            StrSql = StrSql + " 'Labor Fixed' AS PDES9,'Depreciation' AS PDES10,'Other Overhead' AS PDES11,'Plant Margin' AS PDES12,'Sales and Marketing' AS PDES13,'Research and Development' AS PDES14,"
            StrSql = StrSql + "'Other Corporate' AS PDES15,'Operating Margin' AS PDES16,UNIT,MATERIAL AS PL3,LABOR AS PL4,PACKAGING AS PL5,DISTRIBUTION AS PL6,OTHERVARIABLE AS PL7,VARIABLEMARGIN AS PL8,"
            StrSql = StrSql + "LABORFIXED AS PL9,DEPRECIATION AS PL10,OTHEROVERHEAD AS PL11,PLANTMARGIN AS PL12,SALESMKT AS PL13,RESEARCHDEV AS PL14,OTHERCORP AS PL15,OTHERCORP AS PL16,UNITTYPE,UNITPP,UNITPS,PUN,"
            StrSql = StrSql + "TITLE2,TITLE8,PRICE,SETUP,'PRICE' AS PS1,'SETUP' AS PS2  FROM PRICECOSTMANAGER WHERE SKUID=1"

            Dts = odbUtil.FillDataSet(StrSql, SavvyProConnection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("SavvyProGetData:GetPriceCost:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

#End Region

#Region "Terms Manager"

    Public Function GetTermsManagerDet(ByVal specid As String) As DataSet
        Dim ds As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim strSql As String = ""
        Try

            'strSql = "Select  buyer,rfpnumber,rfpdes,rfpduedate,tpayment,INVENTORYCOMMITMENT from termsmanager where specid='" + specid.ToString() + "'"
            strSql = "Select * from termsmanager where specid='" + specid.ToString() + "'"

            ds = odbUtil.FillDataSet(strSql, SavvyProConnection)
            Return ds
        Catch ex As Exception
            Throw New Exception("SavvyProGetData:GetInvestmentManagerDet:" + ex.Message.ToString())
            Return ds
        End Try
    End Function

#End Region

End Class
