using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperExample.Models;
using Newtonsoft.Json;

namespace DapperExample
{
    class Program
    {
        static void Main(string[] args)
        {

            //EJEMPLO DE UNA CONSULTA ANIDADA CON DAPPER

            string cnn = "{your-connection-string}";
            string query = "select RutaId RouteId, getdate() DeliveryDate from Pedido where RutaId = 'IND' group by RutaId";

            using (var cn = new SqlConnection(cnn))
            {
                //MAPEAMOS LA PRIMERA CONSULTA SQL A UNA CLASE DE C# CON DAPPER
                RouteDTO route = cn.Query<RouteDTO>(query).FirstOrDefault();

                //SACAMOS LA SEGUNDA CONSULTA USANDO UN CAMPO DE LA PRIMER CONSULTA MAPEADA, COMO FILTRO EN EL WHERE
                query = $"select t0.Fecha DocDate, t0.RefDoc DocNum, t0.SAPDoc DocEntry, T0.ClienteId CardCode, T0.Comentarios Comments , t0.PedidoId IdWms from Pedido t0 where RutaId = '{route.RouteId}'";
                List<SalesOrderDTO> orderHeaders = cn.Query<SalesOrderDTO>(query).ToList();

                //AGREGAMOS EL RESULTADO DE AL SEGUNDA CONSULTA AL OBJETO PADRE
                route.SalesOrders = orderHeaders;

                //SACAREMOS EL DETALLE DE CADA ORDEN DE VENTA
                foreach (var order in orderHeaders)
                {
                    query = $"select t0.ItemId ItemCode, t0.Cantidad Quantity,t0.Cantidad InvQty, 1 UomEntry, t0.BodegaId WhsCode, t0.ReturnReason  from PedidoDetalle t0  where t0.PedidoId = {order.IdWms}";
                    //OBTENEMOS EL DETALLE DE LA ORDEN Y LA MAPEAMOS CON LA CLASE
                    List<SalesOrderDetailDTO> details = cn.Query<SalesOrderDetailDTO>(query).ToList();
                    //AGREGAMOS EL RESULTADO DEL DETALLE AL OBJETO PADRE
                    order.Details = details;
                }

                //CREAMOS UN NUEVO OBJETO QUE ENMASCARE TODA LA PETICION A UN OBJETO GENERICO
                MessageDTO message = new MessageDTO
                {
                    ObjectType = ObjectTypes.Route,
                    Data = route
                };

                //CONVERTIMOS A JSON EL OBJETO FINAL
                string json = JsonConvert.SerializeObject(message);

                Console.WriteLine(json);
                Console.Read();
            }
        }
    }
}
