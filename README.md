# Semana2-Lab
FechaPedido,FechaEntrega
from Clientes inner join Pedidos
on Clientes.idCliente=Pedidos.idcliente
inner join Empleados
on Pedidos.idempleado=Empleados.idempleado
where year(Fechapedido)=@anio

Usp_Lista_Pedidos_Anios 1996

ALTER PROCEDURE Usp_Detalle_Pedido
@idpedido int
as
select detallesdepedidos.IdProducto,NombreProducto,detallesdepedidos.PrecioUnidad,Cantidad,detallesdepedidos.PrecioUnidad*Cantidad as Monto
from detallesdepedidos inner join Productos
on detallesdepedidos.IdProducto=Productos.IdProducto
where idPedido=@idpedido

Usp_Detalle_Pedido '10251'
