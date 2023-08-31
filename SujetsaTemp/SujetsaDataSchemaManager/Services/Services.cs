using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ConnectionsToFirebirdSujetsa.Adapters;
using ConnectionsToFirebirdSujetsa.Data;

namespace ConnectionsToFirebirdSujetsa.Services {
  public class Services {

    private ConnectionModel conInfo = new ConnectionModel();
    private Helper helper = new Helper();

    public Services(bool isTest = false) {

      conInfo = helper.GetConnectionInfo<ConnectionModel>(isTest);

    }


    public List<ProductosAdapter> GetDataFromDb() {
      //var helper = new Helper();
      //ConnectionModel conInfo = helper.GetConnectionInfo<ConnectionModel>(isTest);

      var productList = helper.GetProductsListByDB(conInfo);

      return productList;

    }


    public string GetDataCountFromDb() {

      var productList = GetDataFromDb();

      int nkbd = productList.FindAll(x => x.ALMACEN_ID == 1).Count();
      int nkhpbd = productList.FindAll(x => x.ALMACEN_ID == 2).Count();

      return $"PRODUCTOS BD NK = {nkbd}. PRODUCTOS BD NKHidroplomex = {nkhpbd}. TOTAL = {nkbd + nkhpbd}";

    }


    public string InsertProductToSql(List<ProductosAdapter> productsToUpdate) {

      var data = new DataService();

      try {

        return data.InsertProductToSql(productsToUpdate);

      } catch (Exception ex) {

        throw new Exception($"ERROR: {ex.Message}");
      }
    }


    public async Task<String> InsertProductToSqlAsync(List<ProductosAdapter> productsToUpdate) {

      var data = new DataService();

      try {

        return await Task.Run(() => data.InsertProductToSql(productsToUpdate)).ConfigureAwait(false);

      } catch (Exception ex) {

        throw new Exception($"ERROR: {ex.Message}");
      }
    }


    public List<string> GetListFromSql() {

      var data = new DataService();
      return data.GetListFromSql();
    }
  }
}
