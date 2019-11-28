Public Class Form2
    Dim linea As DataRow
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        txtIdPedido.Text = BuscarPedido()
        txtFecha.Text = Date.Today

        For Each elem As DataRow In ds.Tables("Productos").Rows
            cbProductos.Items.Add(elem("IdProducto"))
        Next

        linea = ds.Tables("Pedidos").NewRow()
        linea.BeginEdit()
        linea("IdPedido") = CInt(txtIdPedido.Text)
        linea("Fecha") = txtFecha.Text
        linea.EndEdit()
        ds.Tables("Pedidos").Rows().Add(linea)
        daPedidos.Update(ds, "Pedidos")
    End Sub

    Private Function BuscarPedido() As Integer
        Dim Id As Object
        con.Open()
        Dim consulta As New SqlCommand("Select Max(IdPedido) From RepasoDataSet.Pedidos", con)
        Id = consulta.ExecuteScalar()
        con.Close()
        If IsDBNull(Id) Then
            Return 1
        Else
            Return CInt(Id) + 1
        End If

    End Function

    Private Function BuscarDetalle() As Integer

        Dim Id As Object
        con.Open()
        Dim consulta As New SqlCommand("Select Max(IdDetalle) From RepasoDataSet.LineasDetalle", con)
        Id = consulta.ExecuteScalar()
        con.Close()
        If IsDBNull(Id) Then
            Return 1
        Else
            Return CInt(Id) + 1
        End If

    End Function

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dim linia As DataRow = ds.Tables("Pedidos").NewRow
        linia.BeginEdit()
        linia("Cerrado") = True
        linia("TotalNeto") = txtPrecioTotal.Text
        linia("") =
        linia("") =
        linia.EndEdit()


    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        Dim line As DataRow = ds.Tables("LineasDetalle").NewRow

        line.BeginEdit()
        line("IdDetalle") = BuscarDetalle()
        line("IdPedido") = txtIdPedido.Text
        line("IdProducto") = cbProductos.Text
        line("unidades") = txtUnidades.Text
        line("TotalLinea") = txtTotalDet.Text
        line.EndEdit()

        ds.Tables("LineasDetalle").Rows.Add(line)
        daLineasDetalle.Update(ds, "LineasDetalle")
        Dim it As ListViewItem = lvDetalles.Items.Add(line("IdDetalle"))

        For i As Integer = 1 To ds.Tables("LineasDetalle").Columns.Count - 1
            it.SubItems.Add(line(i))
        Next

    End Sub

    Private Sub CbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProductos.SelectedIndexChanged
        Dim lineaElegida As DataRow = ds.Tables("Productos").Rows.Find(CInt(cbProductos.Text))
        txtPrecio.Text = lineaElegida("PVP")
    End Sub

    Private Sub TxtUnidades_TextChanged(sender As Object, e As EventArgs) Handles txtUnidades.TextChanged
        Try
            txtTotalDet.Text = CDbl(txtPrecio.Text) * CInt(txtUnidades.Text)
        Catch ex As Exception
            MsgBox("No metas letras en los campos, ni los dejes vacios.")
        End Try
    End Sub
End Class