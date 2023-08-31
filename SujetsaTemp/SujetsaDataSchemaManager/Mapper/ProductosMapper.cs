using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionsToFirebirdSujetsa.Adapters;
using ConnectionsToFirebirdSujetsa.Services;

namespace ConnectionsToFirebirdSujetsa.Mapper {
  public class ProductosMapper {


    public List<ProductosAdapter> Map(DataTable dt, int connectionId) {
      
      List<ProductosAdapter> list = new List<ProductosAdapter>();

      if (connectionId == 1) {
        list = GetNkProductsList(dt, connectionId);
      }
      if (connectionId == 2) {
        list = GetHpNkProductsList(dt, connectionId);
      }
      if (connectionId == 3) {

      }

      return list;
    }


    private List<ProductosAdapter> GetHpNkProductsList(DataTable dt, int connectionId) {
      List<ProductosAdapter> listaProductos = new List<ProductosAdapter>();

      List<object> objs = new List<object>();
      foreach (DataRow row in dt.Rows) {

        objs.Add(row);
        ProductosAdapter prod = new ProductosAdapter();
        prod.ALMACEN_ID = connectionId;
        prod.DET = "1";
        prod.PRODUCTO = row["PRODUCTO"].ToString() ?? "";
        prod.CLAVEPRODSERV = row["CLAVEPRODSERV"].ToString() ?? "";
        prod.DESCRIPCION = row["DESCRIPCION"].ToString() ?? "";
        prod.DESC_LARGA = row["DESC_LARGA"].ToString() ?? "";
        prod.ALTA = (DateTime)row["ALTA"];
        prod.LINEA = row["LINEA"].ToString() ?? "";
        prod.GRUPO = row["GRUPO"].ToString() ?? "";
        prod.NGRUPO = row["NGRUPO"].ToString() ?? "";
        prod.SUBGRUPO = row["SUBGRUPO"].ToString() ?? "";
        prod.COSTO_BASE = row["COSTO_BASE"].ToString() == "" ? 0 : (decimal) row["COSTO_BASE"];
        prod.EXISTENCIA = row["EXISTENCIA"].ToString() ?? "";
        prod.UNIDAD = row["UNIDAD"].ToString() ?? "";
        prod.MONEDA = row["MONEDA"].ToString() ?? "";
        prod.PRECIO1 = row["PRECIO1"].ToString() == "" ? 0 : (decimal) row["PRECIO1"];
        prod.PRECIO2 = row["PRECIO2"].ToString() == "" ? 0 : (decimal) row["PRECIO2"];
        prod.PRECIO3 = row["PRECIO3"].ToString() == "" ? 0 : (decimal) row["PRECIO3"];
        prod.PRECIO4 = row["PRECIO4"].ToString() == "" ? 0 : (decimal) row["PRECIO4"];
        prod.EMPAQUE = row["EMPAQUE"].ToString() ?? "";
        prod.MULTIPLO_RESURTIDO = row["MULTIPLO_RESURTIDO"].ToString() ?? "";
        prod.PROVEEDOR = row["PROVEEDOR"].ToString() ?? "";
        prod.TIPO = row["TIPO"].ToString() ?? "";
        prod.BAJA = row["BAJA"].ToString() ?? "";
        prod.CATEGORIA = row["CATEGORIA"].ToString() ?? "";
        prod.UNIDAD_COMPRA = row["UNIDAD_COMPRA"].ToString() ?? "";
        prod.UNIDAD_VENTA = row["UNIDAD_VENTA_MENUDEO"].ToString() ?? "";

        listaProductos.Add(prod);
      }
      return listaProductos;
    }


    private List<ProductosAdapter> GetNkProductsList(DataTable dt, int connectionId) {
      List<ProductosAdapter> listaProductos = new List<ProductosAdapter>();

      List<object> objs = new List<object>();
      int index = 0;
      try {
        foreach (DataRow row in dt.Rows) {
          
          objs.Add(row);
          ProductosAdapter prod = new ProductosAdapter();
          prod.ALMACEN_ID = connectionId;
          prod.DET = "1";
          prod.PRODUCTO = row["PRODUCTO"].ToString() ?? "";
          prod.CLAVEPRODSERV = row["CLAVEPRODSERV"].ToString() ?? "";
          prod.DESCRIPCION = row["DESCRIPCION"].ToString() ?? "";
          prod.DESC_LARGA = row["DESC_LARGA"].ToString() ?? "";
          prod.ALTA = (DateTime) row["ALTA"];
          prod.LINEA = row["LINEA"].ToString() ?? "";
          prod.GRUPO = row["GRUPO"].ToString() ?? "";
          prod.SUBGRUPO = row["SUBGRUPO"].ToString() ?? "";
          prod.LARGO = row["LARGO"].ToString();
          prod.COSTO_BASE = row["COSTO_BASE"].ToString() == "" ? 0 : (decimal) row["COSTO_BASE"];
          prod.EXISTENCIA = row["EXISTENCIA"].ToString() ?? "";
          prod.UNIDAD = row["UNIDAD"].ToString() ?? "";
          prod.MONEDA = row["MONEDA"].ToString() ?? "";
          prod.PRECIO1 = row["PRECIO1"].ToString() == "" ? 0 : (decimal) row["PRECIO1"];
          prod.PRECIO2 = row["PRECIO2"].ToString() == "" ? 0 : (decimal) row["PRECIO2"];
          prod.PRECIO3 = row["PRECIO3"].ToString() == "" ? 0 : (decimal) row["PRECIO3"];
          prod.PRECIO4 = row["PRECIO4"].ToString() == "" ? 0 : (decimal) row["PRECIO4"];
          prod.EMPAQUE = row["EMPAQUE"].ToString() ?? "";
          prod.MULTIPLO_RESURTIDO = row["MULTIPLO_RESURTIDO"].ToString() ?? "";
          prod.PROVEEDOR = row["PROVEEDOR"].ToString() ?? "";
          prod.TIPO = row["TIPO"].ToString() ?? "";
          prod.BAJA = row["BAJA"].ToString() ?? "";
          prod.CLAVEPRODSERV = row["CLAVEPRODSERV"].ToString() ?? "";
          prod.PESO = row["PESO"].ToString() == "" ? 0 : (decimal) row["PESO"];
          prod.CATEGORIA = row["CATEGORIA"].ToString() ?? "";
          prod.UNIDAD_COMPRA = row["UNIDAD_COMPRA"].ToString() ?? "";
          prod.UNIDAD_VENTA = row["UNIDAD_VENTA_MENUDEO"].ToString() ?? "";

          listaProductos.Add(prod);
          index++;
        }
      } catch (Exception ex) {

        throw new Exception($"Error in method GetNkProductsList, index: {index}. {ex.Message}");
      }
      
      return listaProductos;
    }

   
    public IEnumerable<dynamic> GetTable(DataTable dt) {

      IEnumerable<dynamic> list = DynamicListObject.AsDynamicEnumerable(dt);

      return list;
    }



  }

  public static class DynamicListObject {

    public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table) {


      IEnumerable<dynamic> list = table.AsEnumerable().Select(row => new DynamicRow(row));
      return list;
    }
  }


  sealed class DynamicRow : DynamicObject {

    private readonly DataRow _row;

    internal DynamicRow(DataRow row) {
      _row = row;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result) {
      var retVal = _row.Table.Columns.Contains(binder.Name);
      result = retVal ? _row[binder.Name] : null;
      return retVal;
    }

  }


}
