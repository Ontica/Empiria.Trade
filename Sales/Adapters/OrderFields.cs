/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : OrderFields                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a order attributes list.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria;

namespace Empiria.Trade.Sales.Adapters {

  public class OrderFields {

    #region Constructors and parsers

    public OrderFields() {
      // Required by Empiria Framework.
    }

    public string Notes {
      get; set;
    } = string.Empty;
        
    public string CustomerUID {
      get; set;
    }

    public string CustomerContactUID {
      get; set;
    }

    public string SupplierUID {
      get; set;
    }

    public string SalesAgentUID {
      get; set;
    }

    #endregion Constructors and parsers

    internal int GetCustomer() {
      return 30;
    }

    internal string GetOrderNumber() {
      return "PR 78 9089 10";
    }

    internal int GetSalesAgent() {
      return 30;
    }

    internal int GetSupplier() {
      return 40;
    }


  }  //  internal class OrderFields
   

} // namespace Empiria.Trade.Sales.Adapters
