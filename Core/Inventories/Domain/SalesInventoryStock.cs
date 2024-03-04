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

namespace Empiria.Trade.Core {


  /// <summary></summary>
  public class SalesInventoryStock {


    #region Constructors and parsers


    internal SalesInventoryStock() {

    }


    #endregion

    #region Properties


    [DataField("VendorProductId")]
    public int VendorProductId {
      get; private set;
    }


    [DataField("Stock")]
    public decimal Stock {
      get; private set;
    }


    #endregion

  } // class SalesInventoryStock

} // namespace Empiria.Trade.Core
