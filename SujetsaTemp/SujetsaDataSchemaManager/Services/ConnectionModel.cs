using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionsToFirebirdSujetsa.Services {


  internal enum ConnectionName {
    ProductosNkConn,
    ProductosNKHidroplomex,
    ArtículosMicrosip
  }


  internal class ConnectionModel {

    public List<ConnectionSettings> ConnectionSettings = new List<ConnectionSettings>();

  }


  internal class ConnectionSettings {

    public int ConnectionId {
      get; set;
    }

    public string ConnectionName {
      get; set;
    } = string.Empty;

    public string ConnectionString {
      get; set;
    } = string.Empty;

    public string Query {
      get; set;
    } = string.Empty;

  }


  public class FbQueryStrings {

    public string productosNkConn = "SELECT * FROM PRODUCTO";

    public string productosNKHidroplomexConn = 
      "SELECT PRODUCTO.PRODUCTO, CLAVEPRODSERV, PRODUCTO.DESCRIPCION, DESC_LARGA, ALTA, LINEA, PRODUCTO.GRUPO, SUBGRUPO, LARGO, " +
      "COSTO_BASE, UNIDAD, MONEDA, PRECIO1, PRECIO2, PRECIO3, PRECIO4, EMPAQUE, MULTIPLO_RESURTIDO, PROVEEDOR, PRODUCTO.TIPO, BAJA, " +
      "CATEGORIA, UNIDAD_COMPRA, UNIDAD_VENTA_MENUDEO, GRUPO.DESCRIPCION NGRUPO, INVENTARIO.EXISTENCIA " +
      "FROM PRODUCTO " +
      "LEFT JOIN GRUPO ON PRODUCTO.GRUPO = GRUPO.GRUPO " +
      "LEFT JOIN (SELECT PRODUCTO, SUM(CANTIDAD) EXISTENCIA FROM CAPA GROUP BY CAPA.PRODUCTO) INVENTARIO " +
      "ON PRODUCTO.PRODUCTO = INVENTARIO.PRODUCTO";

    public string articulosMicrosipConn = "";

  }

}
