Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Collections.Specialized


Public Class CryptoHelper
    Public Sub New()
        '
        ' TODO: Add constructor logic here
        ''
    End Sub


    Friend Const DEFAULT_ALGORITHM As String = "DES"

    Private Function GetProvider(ByVal algorithm As String) As SymmetricAlgorithm
        Return SymmetricAlgorithm.Create(algorithm)
    End Function

    Public Function Encrypt(ByVal str As String) As String
        Return Encrypt(str, DEFAULT_ALGORITHM)
    End Function


    Public Function Encrypt(ByVal str As String, ByVal algorithm As String) As String
        Dim des As SymmetricAlgorithm = GetProvider(algorithm)
        Dim ms As New MemoryStream()
        Dim EncryptStr As String = String.Empty
        Try
            Dim desTr As ICryptoTransform = des.CreateEncryptor(Settings.Key, Settings.IV)
            Dim enc As Encoding = Encoding.UTF8
            Dim bytes As Byte() = enc.GetBytes(str.Trim())
            Dim cs As New CryptoStream(ms, desTr, CryptoStreamMode.Write)
            cs.Write(bytes, 0, bytes.Length)
            cs.FlushFinalBlock()
            EncryptStr = Convert.ToBase64String(ms.ToArray()).Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!")
            Return EncryptStr
        Finally
            ms.Close()
        End Try
    End Function

    Public Function Decrypt(ByVal str As String) As String
        Return Decrypt(str.Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&"), DEFAULT_ALGORITHM)
    End Function


    Public Function Decrypt(ByVal str As String, ByVal algorithm As String) As String

        Dim des As SymmetricAlgorithm = GetProvider(algorithm)
        Dim enc As Encoding = Encoding.UTF8
        Dim bytes As Byte() = enc.GetBytes(str)
        Dim b64 As New FromBase64Transform()
        bytes = b64.TransformFinalBlock(bytes, 0, bytes.Length)
        Dim ms As New MemoryStream(bytes)
        Dim DecryptStr As String = String.Empty
        Try
            Dim desTr As ICryptoTransform = des.CreateDecryptor(Settings.Key, Settings.IV)
            Dim cs As New CryptoStream(ms, desTr, CryptoStreamMode.Read)
            Dim resBytes As Byte() = New Byte(ms.Length - 1) {}
            cs.Read(resBytes, 0, resBytes.Length)
            DecryptStr = enc.GetString(resBytes).TrimEnd(ControlChars.NullChar)
            Return DecryptStr
        Finally
            ms.Close()
        End Try
    End Function
End Class

