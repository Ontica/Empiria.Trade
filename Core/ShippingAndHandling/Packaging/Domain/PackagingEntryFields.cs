/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packing Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Data Transfer Object                    *
*  Type     : PackagingEntryFields                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to manage packing and handling fields.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.IO.Packaging;
using System.Linq;

namespace Empiria.Trade.Core {


  /// <summary>DTO used to manage order packing fields.</summary>
  public class PackagingEntryFields {


    public string OrderUID {
      get; set;
    }


    public string PackageTypeUID {
      get;  set;
    }


    public string PackageID {
      get; set;
    }


    public virtual void EnsureIsValid(string orderUID, string packageForItemUID) {

      FixedList<PackagingEntry> packages = PackagingData.GetPackagingEntriesByOrder(orderUID);

      var existPackage = packages.FirstOrDefault(x => x.PackageID.ToUpper() == this.PackageID.ToUpper());

      if (packageForItemUID != string.Empty) {

        existPackage = packages.FirstOrDefault(x => x.PackageID.ToUpper() == this.PackageID.ToUpper() &&
                                               x.OrderPackingUID != packageForItemUID);

      }
      Assertion.Require(existPackage == null, $"Ya existe paquete con el nombre: '{this.PackageID}'");
    }

  } // class PackagingEntryFields


  public class MissingItemField {


    public string orderItemUID {
      get; set;
    }


    public string WarehouseUID {
      get; set;
    }


    public string WarehouseBinUID {
      get; set;
    }


    public decimal Quantity {
      get; set;
    }


  } // class MissingItemField


} // namespace Empiria.Trade.ShippingAndHandling.Adapters
