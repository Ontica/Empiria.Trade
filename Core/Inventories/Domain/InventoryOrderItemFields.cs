/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Material Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Information Holder                      *
*  Type     : InventoryOrderItemFields                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represent Inventory Order Item Fields.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Locations;
using Empiria.Orders;

namespace Empiria.Trade.Core {

  /// <summary>Represent Inventory Order Item Fields.</summary>
  public class InventoryOrderItemFields : OrderItemFields {

    public string Location { 
      get; set; 
    }

    public string Product {
      get; set;
    }

    public override void EnsureValid() {
      base.EnsureValid(); 

      Assertion.Require(Location, "Necesito la localizacion del producto.");
    
      //_ = Location.Parse(Location);

    }

  } // class InventoryOrderItemFields


} // namespace Empiria.Trade.Core