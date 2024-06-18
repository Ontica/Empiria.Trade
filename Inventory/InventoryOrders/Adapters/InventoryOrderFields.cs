/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : InventoryOrderFields                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage inventory order fields.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Adapters {


  public enum InventoryStatus {

    Todos = 'T',

    Abierto = 'A',

    EnProceso = 'E',

    Cerrado = 'C'

  }


  /// <summary>DTO used to manage inventory order fields.</summary>
  public class InventoryOrderFields {


    public string InventoryOrderTypeUID {
      get; set;
    } = string.Empty;


    //public string ExternalObjectReferenceUID {
    //    get; set;
    //} = string.Empty;


    public int ReferenceId {
      get; internal set;
    } = -1;


    public string ResponsibleUID {
      get; set;
    } = string.Empty;


    public string AssignedToUID {
      get; set;
    } = string.Empty;


    public string Notes {
      get; set;
    } = string.Empty;


  } // class InventoryOrderFields


  public class InventoryOrderItemFields {


    public string UID {
      get; set;
    } = string.Empty;


    public string Notes {
      get; set;
    } = string.Empty;


    public string VendorProductUID {
      get; set;
    } = string.Empty;


    public string WarehouseBinUID {
      get; set;
    } = string.Empty;


    public int InventoryOrderTypeItemId {
      get;
      internal set;
    } = -1;


    public int ItemReferenceId {
      get;
      internal set;
    } = -1;


    public decimal Quantity {
      get; set;
    }


    public decimal InProcessInputQuantity {
      get; set;
    }


    public decimal InProcessOutputQuantity {
      get; set;
    }


    public decimal InputQuantity {
      get; set;
    }


    public decimal OutputQuantity {
      get; set;
    }


    public int UnitId {
      get; set;
    } = -1;


    public decimal InputCost {
      get; set;
    }


    public decimal OutputCost {
      get; set;
    }


    public int CurrencyId {
      get; set;
    } = -1;


    //public string ExternalObjectItemReferenceUID {
    //    get; set;
    //} = string.Empty;


  } // class InventoryOrderItemFields

} // namespace Empiria.Trade.Inventory.Adapters
