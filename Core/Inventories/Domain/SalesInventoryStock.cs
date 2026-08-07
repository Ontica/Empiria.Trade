/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Information Holder                      *
*  Type     : SalesInventoryStock                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory stock by vendorProduct for sales.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Locations;
using Empiria.StateEnums;
using Empiria.Trade.Core.Catalogues;
using Empiria.Trade.Products;

namespace Empiria.Trade.Core {


  /// <summary></summary>
  public class SalesInventoryStock {


    #region Constructors and parsers


    public SalesInventoryStock() {
      // no-op
    }


    #endregion

    #region Properties


    [DataField("Product_Id")]
    public int ProductId {
      get; set;
    }

    public ProductEntry Product {
      get {
        return ProductEntry.ParseId(ProductId);
      }
    }


    [DataField("Base_Product_Id")]
    internal int BaseProductId {
      get; private set;
    }


    public ProductEntry BaseProduct {
      get {
        return ProductEntry.ParseId(BaseProductId);
      }
    }


    [DataField("Location_Id")]
    public Location Location {
      get; set;
    }


    [DataField("Available_Stock")]
    public decimal Stock {
      get; set;
    }


    [DataField("Real_Stock")]
    public decimal RealStock {
      get; set;
    }


    [DataField("Stock_In_Process")]
    public decimal StockInProcess {
      get; set;
    }


    [DataField("Product_Status", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get; set;
    }


    public WarehouseBin WarehouseBin {
      get; set;
    }


    public VendorProduct VendorProduct {
      get; set;
    }

    #endregion

  } // class SalesInventoryStock

} // namespace Empiria.Trade.Core
