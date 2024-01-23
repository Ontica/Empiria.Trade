/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Data Transfer Object                    *
*  Type     : ProductOrderQuery                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query to filter Products by order.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Orders;

namespace Empiria.Trade.Sales.Adapters {

  /// <summary>Query to filter Products by order.</summary>
  public class ProductOrderQuery {


    public string Keywords {
      get; set;
    } = string.Empty;


    public SalesOrderFields Order {
      get; set;
    } = new SalesOrderFields();


  } // class ProductOrderQuery

} // namespace Empiria.Trade.Sales.Adapters
