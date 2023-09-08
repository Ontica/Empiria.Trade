using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeDataSchemaManager.Adapters;
using TradeDataSchemaManager.Data;
using TradeDataSchemaManager.Mapper;
using Newtonsoft.Json;

namespace TradeDataSchemaManager.Services {
  internal class Helper {

    internal Helper() {
    }

    public T GetConnectionInfo<T>(bool isTest = false) {

      try {

        var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

        string path = Path.Combine(directory.FullName, @"Assets\config.file.json");

        if (isTest) {
          path = Path.Combine(directory.Parent.FullName, @"Assets\config.file.json");
        }

        var json = File.ReadAllText(path);

        var returnedJson = JsonConvert.DeserializeObject<T>(json);

        return returnedJson;

      } catch (Exception ex) {

        throw new Exception($"(Helper.GetConnectionInfo<T>().) { ex.Message }");
      }

    }


    public List<ProductosAdapter> GetProductsListByDB(ConnectionModel conInfo) {

      var productList = new List<ProductosAdapter>();

      foreach (var setting in conInfo.ConnectionSettings) {

        DataTable dt = new DataTable();

        var data = new DataService();

        string query = GetFbQueryString(setting.ConnectionName);

        dt = data.GetDataAdapter(setting.ConnectionString, query);

        var mapper = new ProductosMapper();

        var products = mapper.Map(dt, setting.ConnectionId);

        productList.AddRange(products);
      }

      return productList;
    }


    private string GetFbQueryString(string connectionName) {
      var fbQuery = new FbQueryStrings();

      if (connectionName == "productosNkConn") {
        return fbQuery.productosNkConn;
      }
      if (connectionName == "productosNKHidroplomexConn") {
        return fbQuery.productosNKHidroplomexConn;
      }
      if (connectionName == "articulosMicrosipConn") {
        return fbQuery.articulosMicrosipConn;
      }

      throw new Exception("ERROR: No se encontró conexión en FbQueryStrings()");

    }
  }


}
