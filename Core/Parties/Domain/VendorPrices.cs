/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : PartyContact                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a VendorPrices.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Json;
using Empiria.Trade.Products.Adapters;
using Newtonsoft.Json;

namespace Empiria.Trade.Core {

  /// <summary>Represents a PartyContact.  </summary>
  static public class CustomerPrices {

    #region Public methods
    static public FixedList<VendorPrices> GetVendorPrices(int customerId) {
      var customer = Party.Parse(customerId);

      var ExtData = JsonConvert.DeserializeObject<PartyExtData>(customer.ExtData);

     return ExtData.VendorPrices;
    }

    #endregion Public methods

  } // CustomerPrices

  public class PartyExtData {

    public int InternalStatus {
      get; set;
    }

    public string FromDatabase {
      get; set;
    } = string.Empty;

    public int PriceListId {
      get; set;
    }

    public FixedList<VendorPrices> VendorPrices {
      get; set;
    } = new FixedList<VendorPrices>();

  } // class ExtData

  public class VendorPrices {
 
    public int VendorId {
      get; set;
    }

    public int PriceListId {
      get; set;
    }
    
  } // VendorPrices

} // namespace Empiria.Trade.Core.Domain
