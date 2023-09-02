using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionsToFirebirdSujetsa.Adapters;
using FirebirdSql.Data.FirebirdClient;

namespace ConnectionsToFirebirdSujetsa.Data {

  internal class DataService {


    public FbConnection GetFbConnection(string bdConection) {

      FbConnection conn = new FbConnection(bdConection);
      conn.Open();
      return conn;

    }


    public DataTable GetDataAdapter(string toConnect, string query) {

      using (var fbConn = GetFbConnection(toConnect)) {

        FbDataAdapter result = new FbDataAdapter(query, fbConn);

        DataTable dt = new DataTable();
        result.Fill(dt);
        fbConn.Close();

        return dt;

      }

    }


    public SqlConnection GetSQLConnect() {

      SqlConnection conn = new SqlConnection(@"Data Source=MSI\MSSQLSERVER01;Initial Catalog=SujetsaDBLocal; User Id=empiria_trade; Password=admin123;");
      conn.Open();

      return conn;

    }


    public string InsertProductToSql(List<ProductosAdapter> productsList) {

      string message = "";
      int cont = 0;

      try {

        using (var sqlCon = GetSQLConnect()) {

          //productsList = productsList.Take(50).ToList();

          int pagesize = 10;
          int countitens = productsList.Count;
          int pagecount = countitens % pagesize <= 0 ? countitens / pagesize : (countitens / pagesize) + 1;

          for (int page = 0; page < pagecount; page++) {

            List<ProductosAdapter> list = productsList.Skip(page * pagesize).Take(pagesize).ToList();

            foreach (var producto in list) {

              SqlCommand cmd = new SqlCommand("spInsertProductosTemp", sqlCon);
              cmd.CommandType = CommandType.StoredProcedure;

              cmd.Parameters.Add("@Det", SqlDbType.VarChar).Value = producto.DET;
              cmd.Parameters.Add("@Producto", SqlDbType.VarChar).Value = producto.PRODUCTO;
              cmd.Parameters.Add("@ClaveDeServ", SqlDbType.VarChar).Value = producto.CLAVEPRODSERV;
              cmd.Parameters.Add("@Clave", SqlDbType.VarChar).Value = producto.CLAVE;
              cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = producto.DESCRIPCION;
              cmd.Parameters.Add("@Alta", SqlDbType.SmallDateTime).Value = producto.ALTA;

              cmd.Parameters.Add("@Giro", SqlDbType.VarChar).Value = producto.GIRO;
              cmd.Parameters.Add("@Marca", SqlDbType.VarChar).Value = producto.MARCA;
              cmd.Parameters.Add("@Modelo", SqlDbType.VarChar).Value = producto.MODELO;
              cmd.Parameters.Add("@Seccion", SqlDbType.VarChar).Value = producto.SECCION;
              cmd.Parameters.Add("@Linea", SqlDbType.VarChar).Value = producto.LINEA;
              cmd.Parameters.Add("@NLinea", SqlDbType.VarChar).Value = producto.NLINEA;
              cmd.Parameters.Add("@Grupo", SqlDbType.VarChar).Value = producto.GRUPO;
              cmd.Parameters.Add("@NGrupo", SqlDbType.VarChar).Value = producto.NGRUPO;
              cmd.Parameters.Add("@SubGrupo", SqlDbType.VarChar).Value = producto.SUBGRUPO;
              cmd.Parameters.Add("@NSubGrupo", SqlDbType.VarChar).Value = producto.NSUBGRUPO;
              cmd.Parameters.Add("@Diametro", SqlDbType.VarChar).Value = producto.DIAMETRO;
              cmd.Parameters.Add("@Largo", SqlDbType.VarChar).Value = producto.LARGO;

              cmd.Parameters.Add("@CostoBase", SqlDbType.Decimal).Value = producto.COSTO_BASE;

              cmd.Parameters.Add("@Grado", SqlDbType.VarChar).Value = producto.GRADO;
              cmd.Parameters.Add("@Hilos", SqlDbType.VarChar).Value = producto.HILOS;
              cmd.Parameters.Add("@NHilos", SqlDbType.VarChar).Value = producto.NHILOS;
              cmd.Parameters.Add("@Pasos", SqlDbType.VarChar).Value = producto.PASOS;
              cmd.Parameters.Add("@NPasos", SqlDbType.VarChar).Value = producto.NPASOS;

              cmd.Parameters.Add("@Cabezas", SqlDbType.VarChar).Value = producto.CABEZAS;
              cmd.Parameters.Add("@NCabezas", SqlDbType.VarChar).Value = producto.NCABEZAS;
              cmd.Parameters.Add("@Acabados", SqlDbType.VarChar).Value = producto.ACABADOS;
              cmd.Parameters.Add("@NAcabados", SqlDbType.VarChar).Value = producto.NACABADOS;

              cmd.Parameters.Add("@FechaUltimaCompra", SqlDbType.SmallDateTime).Value = Convert.ToDateTime(producto.FECHA_ULTIMA_COMPRA);
              cmd.Parameters.Add("@CostoUltimaCompra", SqlDbType.Decimal).Value = producto.COSTO_ULTIMA_COMPRA;

              cmd.Parameters.Add("@PrecioMinimo", SqlDbType.Decimal).Value = producto.PMINIMO;
              cmd.Parameters.Add("@Existencia", SqlDbType.VarChar).Value = producto.EXISTENCIA;

              cmd.Parameters.Add("@Moneda", SqlDbType.VarChar).Value = producto.MONEDA;

              cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = producto.TOTAL;
              cmd.Parameters.Add("@PrecioLista1", SqlDbType.Decimal).Value = producto.PRECIOLISTA1;
              cmd.Parameters.Add("@PrecioLista2", SqlDbType.Decimal).Value = producto.PRECIOLISTA2;
              cmd.Parameters.Add("@PrecioLista3", SqlDbType.Decimal).Value = producto.PRECIOLISTA3;
              cmd.Parameters.Add("@PrecioLista4", SqlDbType.Decimal).Value = producto.PRECIOLISTA4;
              cmd.Parameters.Add("@Empaque", SqlDbType.VarChar).Value = producto.EMPAQUE;
              cmd.Parameters.Add("@MultiploReSurtido", SqlDbType.VarChar).Value = producto.MULTIPLO_RESURTIDO;
              cmd.Parameters.Add("@MinimoResurtido", SqlDbType.VarChar).Value = producto.MINIMO_RESURTIDO;
              cmd.Parameters.Add("@Proveedor", SqlDbType.VarChar).Value = producto.PROVEEDOR;
              cmd.Parameters.Add("@NProveedor", SqlDbType.VarChar).Value = producto.NPROVEEDOR;

              cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = producto.TIPO;
              cmd.Parameters.Add("@Baja", SqlDbType.VarChar).Value = producto.BAJA;

              cmd.Parameters.Add("@Caracteristica", SqlDbType.VarChar).Value = producto.CARACTERISTICA;
              cmd.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = producto.CATEGORIA;
              cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = producto.PESO;

              cmd.Parameters.Add("@UnidadCompra", SqlDbType.VarChar).Value = producto.UNIDAD_COMPRA;
              cmd.Parameters.Add("@UnidadVenta", SqlDbType.VarChar).Value = producto.UNIDAD_VENTA;
              cmd.Parameters.Add("@ContenidoUnidadCompra", SqlDbType.VarChar).Value = producto.CONTENIDO_UNIDAD_COMPRA;
              cmd.Parameters.Add("@EsAlmacenable", SqlDbType.VarChar).Value = producto.ES_ALMACENABLE;
              cmd.Parameters.Add("@EsImportado", SqlDbType.VarChar).Value = producto.ES_IMPORTADO;
              cmd.Parameters.Add("@EsSiempreImportado", SqlDbType.VarChar).Value = producto.ES_SIEMPRE_IMPORTADO;
              cmd.Parameters.Add("@Estatus", SqlDbType.Char).Value = producto.ESTATUS;
              cmd.Parameters.Add("@LineaArticuloID", SqlDbType.VarChar).Value = producto.LINEA_ARTICULO_ID;
              cmd.Parameters.Add("@NombreLinea", SqlDbType.VarChar).Value = producto.NOMBRE_LINEA;
              cmd.Parameters.Add("@ExtData", SqlDbType.VarChar).Value = producto.EXTDATA;

              cmd.Parameters.Add("@Keywords", SqlDbType.VarChar).Value = producto.KEYWORDS;
              cmd.Parameters.Add("@AlmacenId", SqlDbType.Int).Value = producto.ALMACEN_ID;


              cmd.ExecuteNonQuery();
              
              cont++;
            }

          }

          sqlCon.Close();

          message = $"Se insertaron: {cont} registros!...";

          return message;

        }

      } catch (Exception ex) {

        throw new Exception($"{message} Error al intentar guardar los siguientes: " +
                            $"{productsList.Count - cont} registros. ERROR {ex.Message}!");
      }

    }


    public List<string> GetListFromSql() {

      List<string> list = new List<string>();
      using (var sqlCon = GetSQLConnect()) {

        SqlCommand command = new SqlCommand("SELECT PRODUCTO.*, GRUPO.DESCRIPCION NGRUPO FROM PRODUCTO LEFT JOIN GRUPO ON PRODUCTO.GRUPO = GRUPO.GRUPO", sqlCon);

        using (SqlDataReader reader = command.ExecuteReader()) {
          if (reader.HasRows) {
            while (reader.Read()) {
              string desc = reader["DESCRIPCION"].ToString();
              list.Add(desc);
            }
          }
        }
        sqlCon.Close();
      }
      return list;
    }


  }
}
