Imports System.IO
Imports System.Text
Imports System.Data

Namespace LogFiles
    Public Class CreateLogFiles
        Private sLogFormat As String
        Private sErrorTime As String

        Public Sub New()
            'sLogFormat used to create log files format :
            ' dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() & " " & DateTime.Now.ToLongTimeString().ToString() & " "
            'this variable used to create log filename format "
            'for example filename : ErrorLogYYYYMMDD
            Dim sYear As String = DateTime.Now.Year.ToString()
            Dim sMonth As String = DateTime.Now.Month.ToString()
            Dim sDay As String = DateTime.Now.Day.ToString()
            sErrorTime = sMonth + "_" + sDay + "_" + sYear
        End Sub

        Public Sub LoginConflictLog(ByVal sPathName As String, ByVal userName As String, ByVal license As String, ByVal modName As String)
            Dim sw As New StreamWriter(sPathName, True)
            ' sw.WriteLine("***************************************************************************************************")
            sw.WriteLine("***************************************************************************************************")
            sw.WriteLine("Login conflict on " + sLogFormat + " for:")
            sw.WriteLine("    User: " + userName)
            sw.WriteLine("    License: " + license)
            sw.WriteLine("    Module: " + modName)
            sw.Flush()
            sw.Close()
        End Sub

        Public Sub WriteData(ByVal sPathName As String, ByVal sErrMsg As String, ByVal ds As DataSet)
            Dim sw As New StreamWriter(sPathName, True)
            Dim i As Integer
            sw.WriteLine(sErrMsg)
            sw.WriteLine("User Name ,Date")
            For i = 0 To ds.Tables(0).Rows.Count - 1
                sw.WriteLine(ds.Tables(0).Rows(i).Item("PERSDES").ToString() + ", " + ds.Tables(0).Rows(i).Item("ARCHSALARY").ToString())
            Next
            'sw.WriteLine("*****************************************************************************")
            sw.Flush()
            sw.Close()
        End Sub
    End Class
End Namespace

