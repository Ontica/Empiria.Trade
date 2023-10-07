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

using Empiria.StateEnums;

using Empiria.Trade.Core.Domain;
using Empiria.Trade.Core.Adapters;

namespace Empiria.Trade.Sales.Adapters {

  public class OrderFields {

    #region Constructors and parsers

    public OrderFields() {
      // Required by Empiria Framework.
    }

    public string UID {
      get; set;
    } = string.Empty;


    public string OrderNumber {
      get; set;
    } = string.Empty;


    public DateTime OrderTime {
      get; set;
    } = DateTime.Today;

    public string Notes {
      get; set;
    } = string.Empty;


    public EntityStatus Status {
      get; set;
    } = EntityStatus.Active;


    public NamedEntity Customer {
      get; set;
    }

    public PartyContactsDto CustomerContact {
      get; set;
    }

    public NamedEntity Supplier {
      get; set;
    }

    public NamedEntity SalesAgent {
      get; set;
    }

    public string PaymentCondition {
      get; set;
    }

    public int Items {
      get; set;
    }

    public double ItemsTotal {
      get; set;
    }

    public double Shipment {
      get; set;
    }

    public double Taxes {
      get; set;
    }

    public double OrderTotal {
      get; set;
    }

    #endregion Constructors and parsers

    #region Internal methods

    internal Party GetCustomer() {
      return Party.Parse(this.Customer.UID);
    }


    internal Party GetSalesAgent() {
      return Party.Parse(this.SalesAgent.UID);
    }

    internal Party GetSupplier() {
      return Party.Parse(this.Supplier.UID);

    }

    #endregion Internal methods

    #region Private methods


    #endregion Private methods


  }  //  internal class OrderFields




} // namespace Empiria.Trade.Sales.Adapters
