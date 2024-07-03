/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ProductDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query to filter Products.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Products.Adapters {

  /// <summary>Query to filter Products.</summary>
  public class ProductQuery {


    public string Keywords {
      get; set;
    } = string.Empty;


    public string CustomerUID {
      get; set;
    } = string.Empty;


    public string SalesAgentUID {
      get; set;
    } = string.Empty;


    public string SuplierUID {
      get; set;
    } = string.Empty;


    public bool OnStock {
      get; set;
    } = false;

  }


  public class TableQuery {


    public string TableName { get; set; } = string.Empty;

    public string IdName { get; set; } = string.Empty;

    public string UidName { get; set; } = string.Empty;


  }


} // namespace Empiria.Trade.Products.Adapters
