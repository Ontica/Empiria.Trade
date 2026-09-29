/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : PackagingEntry                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a Packaging order.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Orders;
using Empiria.Parties;
using Empiria.Trade.Core.Catalogues;

namespace Empiria.Trade.Core {


  /// <summary>Represents a Packaging order.</summary>
  public class PackagingEntry : BaseObject {


    #region Constructor and parsers

    public PackagingEntry() {
      //no-op
    }

    static public PackagingEntry Parse(int id) => ParseId<PackagingEntry>(id);

    static public PackagingEntry Parse(string uid) => ParseKey<PackagingEntry>(uid);

    static public PackagingEntry Empty => ParseEmpty<PackagingEntry>();

    public PackagingEntry(string orderUID, PackagingEntryFields orderFields, string packagingUID) {

      this.SalesOrder = SalesOrder.Parse(orderUID);

      Update(orderUID, orderFields, packagingUID);
    }

    #endregion Constructor and parsers


    #region Properties

    [DataField("Order_Packing_Id")]
    public int OrderPackingId {
      get; private set;
    }


    [DataField("Order_Packing_UID")]
    public string OrderPackingUID {
      get; private set;
    }


    [DataField("Order_Id")]
    public SalesOrder SalesOrder {
      get; private set;
    }


    [DataField("Package_Type_Id")]
    public PackageType PackageType {
      get; private set;
    }


    [DataField("Package_ID")]
    public string PackageID {
      get; private set;
    }


    [DataField("Posted_By_Id")]
    public Party PostedBy {
      get; private set;
    }


    [DataField("Posting_Time")]
    public DateTime PostingTime {
      get; private set;
    }

    #endregion Properties


    #region Private methods

    protected override void OnSave() {

      if (base.IsNew) {

        OrderPackingId = Id;

        PostedBy = Party.ParseWithContact(ExecutionServer.CurrentContact);
        PostingTime = DateTime.Now;
      }
      PackagingData.WritePacking(this);
    }


    private void Update(string orderUID, PackagingEntryFields orderFields, string packagingUID) {

      var packaging = Parse(packagingUID);

      if (packaging.Id > 0) {
        OrderPackingId = packaging.OrderPackingId;
        OrderPackingUID = packagingUID;
      }

      PackageType = PackageType.Parse(orderFields.PackageTypeUID);
      PackageID = orderFields.PackageID;
    }

    #endregion Private methods


  } // class PackageForItem

} // namespace Empiria.Trade.ShippingAndHandling.Domain
