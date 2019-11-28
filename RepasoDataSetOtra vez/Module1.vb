Module Module1
    Public ds As New DataSet()
    Public PedidoActual As DataRow
    Public cadena As String = "Data Source=SEGUNDO150;Initial Catalog=DAM_Andermaestre_DEV;Integrated Security=True"
    Public con As New SqlConnection(cadena)
    Public daPedidos As New SqlDataAdapter("Select IdPedido, Fecha, Cerrado, TotalNeto, TotalIva, TotalPagar From RepasoDataSet.Pedidos", cadena)
    Public daProductos As New SqlDataAdapter("Select IdProducto, Descripcion, PVP From RepasoDataSet.Productos", cadena)
    Public daLineasDetalle As New SqlDataAdapter("Select IdDetalle, IdPedido, IdProducto, unidades, TotalLinea From RepasoDataSet.LineasDetalle", cadena)


    Public Sub InicializarDataSet()
        InicializarDA(daPedidos)
        InicializarDA(daProductos)
        InicializarDA(daLineasDetalle)

        daPedidos.Fill(ds, "Pedidos")
        daProductos.Fill(ds, "Productos")
        daLineasDetalle.Fill(ds, "LineasDetalle")

        Dim t As DataTable
        Dim key(0) As DataColumn
        t = ds.Tables("Pedidos")
        key(0) = t.Columns("IdPedido")
        t.PrimaryKey = key

        t = ds.Tables("Productos")
        key(0) = t.Columns("IdProducto")
        t.PrimaryKey = key

        t = ds.Tables("LineasDetalle")
        key(0) = t.Columns("IdDetalle")
        t.PrimaryKey = key

        Dim col1 As DataColumn
        Dim col2 As DataColumn

        col1 = ds.Tables("Pedidos").Columns("IdPedido")
        col2 = ds.Tables("LineasDetalle").Columns("IdPedido")
        Dim dr As New DataRelation("Ped-LinDet", col1, col2)
        ds.Relations.Add(dr)

        col1 = ds.Tables("LineasDetalle").Columns("IdProducto")
        col2 = ds.Tables("Productos").Columns("IdProducto")
        Dim lailolai As New DataRelation("Prod-LinDet", col2, col1)
        ds.Relations.Add(lailolai)



    End Sub

    Public Sub InicializarDA(da As SqlDataAdapter)
        Dim cb As New SqlCommandBuilder(da)
        da.UpdateCommand = cb.GetUpdateCommand
        da.DeleteCommand = cb.GetDeleteCommand
        da.InsertCommand = cb.GetInsertCommand
    End Sub
End Module
