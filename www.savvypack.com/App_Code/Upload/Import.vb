Imports Microsoft.VisualBasic

Public Class Import
    Dim _sColumndelimiter As String
    Dim _sDefaultFileName As String
    Dim _iskiprows As Integer
    Dim _sdestinationTable As String
    Dim _sdestination2Table As String
    Dim _sDefaultFileFolder As String
    Dim _sColumnName As String
    Dim _sbeforeimportproc As String
    Dim _safterimportproc As String
    Dim _sfiletype As String

    Public Property ColumnDelimiter() As String
        Get
            Return _sColumndelimiter
        End Get
        Set(ByVal Value As String)
            _sColumndelimiter = Value
        End Set
    End Property

    Public Property SkipRows() As Integer
        Get
            Return _iskiprows
        End Get
        Set(ByVal Value As Integer)
            _iskiprows = Value
        End Set
    End Property

    Public Property DestinationTable() As String
        Get
            Return _sdestinationTable
        End Get
        Set(ByVal Value As String)
            _sdestinationTable = Value
        End Set
    End Property

    Public Property Destination2Table() As String
        Get
            Return _sdestination2Table
        End Get
        Set(ByVal Value As String)
            _sdestination2Table = Value
        End Set
    End Property

    Public Property DefaultFileName() As String
        Get
            Return _sDefaultFileName
        End Get
        Set(ByVal Value As String)
            _sDefaultFileName = Value
        End Set
    End Property

    Public Property DefaultFiletype() As String
        Get
            Return _sfiletype
        End Get
        Set(ByVal Value As String)
            _sfiletype = Value
        End Set
    End Property

    Public Property DefaultFileFolder() As String
        Get
            Return _sDefaultFileFolder
        End Get
        Set(ByVal Value As String)
            _sDefaultFileFolder = Value
        End Set
    End Property

    Public Property ColumnName() As String
        Get
            Return _sColumnName
        End Get
        Set(ByVal Value As String)
            _sColumnName = Value
        End Set
    End Property

    Public Property BeforeImportProc() As String
        Get
            Return _sbeforeimportproc
        End Get
        Set(ByVal Value As String)
            _sbeforeimportproc = Value
        End Set
    End Property
    Public Property AfterImportProc() As String
        Get
            Return _safterimportproc
        End Get
        Set(ByVal Value As String)
            _safterimportproc = Value
        End Set
    End Property




End Class
