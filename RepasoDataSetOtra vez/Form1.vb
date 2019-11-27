Public Class Form1
    Private Sub NuevoPedidoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoPedidoToolStripMenuItem.Click
        Dim resp As MsgBoxResult = MsgBox("Crear nuevo pedido???", vbYesNo)
        If resp = vbYes Then



            Dim f As New Form2
            f.Show()

        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InicializarDataSet()
    End Sub
End Class
