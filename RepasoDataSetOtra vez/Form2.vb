Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtIdPedido.Text = BuscarPedido()
        txtFecha.Text = Date.Today

        For Each elem As DataRow In ds.Tables("Productos").Rows
            cbProductos.Items.Add(elem("IdProducto"))
        Next

        Dim linea As DataRow = ds.Tables("Pedidos").NewRow()
        linea.BeginEdit()
        linea("IdPedido") = CInt(txtIdPedido.Text)
        linea.EndEdit()
        ds.Tables("Pedidos").Rows().Add(linea)


        linea.RejectChanges()







        daPedidos.Update(ds, "Pedidos")



    End Sub

    Private Function BuscarPedido() As Integer
        Dim Id As Object
        con.Open()
        Dim consulta As New SqlCommand("Select Max(IdPedido) From RepasoDataSet.Pedidos", con)
        Id = consulta.ExecuteScalar()
        If IsDBNull(Id) Then
            Return 1
        Else
            Return CInt(Id) + 1
        End If
        con.Close()

    End Function

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Me.Dispose()
    End Sub
End Class