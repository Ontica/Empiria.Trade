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
        string fechaUltimaCompra = row["FECHA_ULTIMA_COMPRA"].ToString();
        DateTime fechaUltimaCompraDate = new DateTime(2077, 01, 01);
        
        if (fechaUltimaCompra != "") {
          fechaUltimaCompraDate = Convert.ToDateTime(fechaUltimaCompra);
        }

        objs.Add(row);
        ProductosAdapter prod = new ProductosAdapter();
        prod.ALMACEN_ID = connectionId;
        prod.DET = "1";
        prod.PRODUCTO = row["PRODUCTO"].ToString() ?? "";
        prod.CLAVEPRODSERV = row["CLAVEPRODSERV"].ToString() ?? "";
        prod.DESCRIPCION = row["DESCRIPCION"].ToString() ?? "";
        prod.DESC_LARGA = row["DESC_LARGA"].ToString() ?? "";
        prod.ALTA = (DateTime)row["ALTA"] != null ? (DateTime)row["ALTA"] : prod.ALTA;
        prod.GIRO = row["GIRO"].ToString() ?? "";
        prod.MARCA = row["MARCA"].ToString() ?? "";
        prod.MODELO = row["MODELO"].ToString() ?? "";
        prod.SECCION = row["SECCION"].ToString() ?? "";
        prod.LINEA = row["LINEA"].ToString() ?? "";
        prod.NLINEA = row["NLINEA"].ToString() ?? "";
        prod.GRUPO = row["GRUPO"].ToString() ?? "";
        prod.NGRUPO = row["NGRUPO"].ToString() ?? "";
        prod.SUBGRUPO = row["SUBGRUPO"].ToString() ?? "";
        prod.COSTO_BASE = row["COSTO_BASE"].ToString() == "" ? 0 : (decimal) row["COSTO_BASE"];
        prod.FECHA_ULTIMA_COMPRA = fechaUltimaCompraDate;
        prod.COSTO_ULTIMA_COMPRA = row["COSTO_ULTIMA_COMPRA"].ToString() == "" ? 0 : (decimal) row["COSTO_ULTIMA_COMPRA"];
        prod.EXISTENCIA = row["EXISTENCIA"].ToString() ?? "";
        prod.MONEDA = row["MONEDA"].ToString() ?? "";
        prod.PRECIOLISTA1 = row["PRECIO1"].ToString() == "" ? 0 : (decimal) row["PRECIO1"];
        prod.PRECIOLISTA2 = row["PRECIO2"].ToString() == "" ? 0 : (decimal) row["PRECIO2"];
        prod.PRECIOLISTA3 = row["PRECIO3"].ToString() == "" ? 0 : (decimal) row["PRECIO3"];
        prod.PRECIOLISTA4 = row["PRECIO4"].ToString() == "" ? 0 : (decimal) row["PRECIO4"];
        prod.EMPAQUE = row["EMPAQUE"].ToString() ?? "";
        prod.MULTIPLO_RESURTIDO = row["MULTIPLO_RESURTIDO"].ToString() ?? "";
        prod.PROVEEDOR = row["PROVEEDOR"].ToString() ?? "";
        prod.NPROVEEDOR = row["NPROVEEDOR"].ToString() ?? "";
        prod.TIPO = row["TIPO"].ToString() ?? "";
        prod.BAJA = row["BAJA"].ToString() ?? "";
        prod.CATEGORIA = row["CATEGORIA"].ToString() ?? "";
        prod.UNIDAD_COMPRA = row["UNIDAD_COMPRA"].ToString() ?? "";
        prod.UNIDAD_VENTA = row["UNIDAD_VENTA"].ToString() ?? "";

        listaProductos.Add(prod);
      }
      return listaProductos;
    }


    private List<ProductosAdapter> GetNkProductsList(DataTable dt, int connectionId) {
      List<ProductosAdapter> listaProductos = new List<ProductosAdapter>();

      int index = 0;
      try {
        foreach (DataRow row in dt.Rows) {
          
          ProductosAdapter prod = new ProductosAdapter();
          prod.ALMACEN_ID = connectionId;
          prod.DET = "1";
          prod.PRODUCTO = row["PRODUCTO"].ToString() ?? "";
          prod.CLAVEPRODSERV = row["CLAVEPRODSERV"].ToString() ?? "";
          prod.DESCRIPCION = row["DESCRIPCION"].ToString() ?? "";
          prod.ALTA = (DateTime) row["ALTA"];
          prod.LINEA = row["LINEA"].ToString() ?? "";
          prod.NLINEA = row["NLINEA"].ToString() ?? "";
          prod.GRUPO = row["GRUPO"].ToString() ?? "";
          prod.NGRUPO = row["NGRUPO"].ToString() ?? "";
          prod.SUBGRUPO = row["SUBGRUPO"].ToString() ?? "";
          prod.NSUBGRUPO = row["NSUBGRUPO"].ToString() ?? "";
          prod.LARGO = row["LARGO"].ToString();
          prod.SECCION = row["SECCION"].ToString();
          
          prod.PASOS = row["PASOS"].ToString();
          prod.NPASOS = row["NPASOS"].ToString();

          prod.CABEZAS = row["CABEZAS"].ToString();
          prod.NCABEZAS = row["NCABEZAS"].ToString();

          prod.ACABADOS = row["ACABADOS"].ToString();
          prod.NACABADOS = row["NACABADOS"].ToString();

          prod.PMINIMO = row["PMINIMO"].ToString() == "" ? 0 : (decimal) row["PMINIMO"];

          
          prod.EXISTENCIA = row["EXISTENCIA"].ToString() ?? "";
          prod.UNIDAD_VENTA = row["UNIDAD_VENTA"].ToString() ?? "";
          prod.MONEDA = row["MONEDA"].ToString() ?? "";
          prod.COSTO_BASE = row["COSTO_BASE"].ToString() == "" ? 0 : (decimal) row["COSTO_BASE"];

          prod.PRECIOLISTA1 = row["PLISTA_1"].ToString() == "" ? 0 : (decimal) row["PLISTA_1"];
          prod.PRECIOLISTA2 = row["PLISTA_2"].ToString() == "" ? 0 : (decimal) row["PLISTA_2"];
          prod.PRECIOLISTA3 = row["PLISTA_3"].ToString() == "" ? 0 : (decimal) row["PLISTA_3"];
          prod.PRECIOLISTA4 = row["PLISTA_4"].ToString() == "" ? 0 : (decimal) row["PLISTA_4"];
          prod.EMPAQUE = row["EMPAQUE"].ToString() ?? "";
          prod.MULTIPLO_RESURTIDO = row["MULTIPLO_RESURTIDO"].ToString() ?? "";
          prod.COSTO_ULTIMA_COMPRA = row["COSTO_ULTIMA_COMPRA"].ToString() == "" ? 0 : (decimal) row["COSTO_ULTIMA_COMPRA"];
          prod.PROVEEDOR = row["PROVEEDOR"].ToString() ?? "";
          prod.NPROVEEDOR = row["NPROVEEDOR"].ToString() ?? "";
          prod.TIPO = row["TIPO"].ToString() ?? "";
          prod.BAJA = row["BAJA"].ToString() ?? "";
          prod.PESO = row["PESO"].ToString() == "" ? 0 : (decimal) row["PESO"];
          prod.CATEGORIA = row["CATEGORIA"].ToString() ?? "";
          prod.MINIMO_RESURTIDO = row["MINIMO_RESURTIDO"].ToString() ?? "";

          prod.DIAMETRO = row["DIAMETRO"].ToString() ?? "";
          prod.LARGO = row["LARGO"].ToString() ?? "";
          prod.GRADO = row["GRADO"].ToString() ?? "";
          prod.HILOS = row["HILOS"].ToString();
          prod.NHILOS = row["NHILOS"].ToString();

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
