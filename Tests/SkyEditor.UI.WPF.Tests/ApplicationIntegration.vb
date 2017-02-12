Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class ApplicationIntegration

    <TestMethod()> Public Sub TestOpenClose()
        'Ensure no exceptions are thrown when opening then closing
        Dim window = StartupHelpers.RunWPFStartupSequence().Result
        window.Close()
    End Sub

End Class