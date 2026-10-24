/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Shipping Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : ShippingEntry                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a shipping entry.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Parties;
using Empiria.Trade.Core;
using Empiria.Trade.Core.Common;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Sales.ShippingAndHandling.Data;

namespace Empiria.Trade.Sales {

  /// <summary>Represents a shipping entry.</summary>
  public class ShippingEntry : BaseObject {


    #region Constructors and parsers

    public ShippingEntry() {
      //no-op
    }


    static public ShippingEntry Parse(int id) => ParseId<ShippingEntry>(id);

    static public ShippingEntry Parse(string uid) => ParseKey<ShippingEntry>(uid);

    static public ShippingEntry Empty => ParseEmpty<ShippingEntry>();


    public ShippingEntry(ShippingFields fields) {

      Update(fields);
    }


    #endregion Constructors and parsers


    #region Properties


    [DataField("Shipping_Order_Id")]
    public int ShippingOrderId {
      get; set;
    }


    [DataField("Shipping_Order_UID")]
    public string ShippingOrderUID {
      get; set;
    }


    [DataField("Parcel_Supplier_Id")]
    public ParcelSupplier ParcelSupplier {
      get; set;
    }


    [DataField("Shipping_Guide")]
    public string ShippingGuide {
      get; set;
    } = string.Empty;


    [DataField("Parcel_Amount")]
    public decimal ParcelAmount {
      get; set;
    }


    [DataField("Customer_Amount")]
    public decimal CustomerAmount {
      get; set;
    }


    [DataField("Shipping_Date")]
    public DateTime ShippingDate {
      get; set;
    }


    [DataField("Delivery_Date")]
    public DateTime DeliveryDate {
      get; set;
    }


    [DataField("Shipping_Method", Default = ShippingMethods.None)]
    public ShippingMethods ShippingMethod {
      get; set;
    } = ShippingMethods.None;


    [DataField("Shipping_Status", Default = ShippingStatus.EnCaptura)]
    public ShippingStatus Status {
      get; set;
    } = ShippingStatus.EnCaptura;


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(

          UID, ShippingGuide
        );
      }
    }


    public string ShippingNumber {
      get; set;
    }


    public string DeliveryNumber {
      get; internal set;
    } = string.Empty;


    public decimal OrdersTotal {
      get; internal set;
    }


    public FixedList<ShippingOrderItem> ShippingOrderItems {
      get; set;
    } = new FixedList<ShippingOrderItem>();


    public FixedList<ShippingPallet> ShippingPallets {
      get; set;
    } = new FixedList<ShippingPallet>();


    public bool CanEdit {
      get; internal set;
    }


    public Party Customer {
      get; set;
    }

    //public INamedEntity Customer {
    //  get; internal set;
    //} = new NamedEntity("Empty", "Empty");


    #endregion Properties


    #region Private methods

    protected override void OnSave() {

      if (IsNew) {

        this.ShippingOrderId = this.Id;
        this.ShippingOrderUID = this.UID;
      }
      ShippingData.WriteShipping(this);
    }


    internal void Update(ShippingFields fields) {

      var salesOrder = SalesOrder.Parse(fields.Orders[0].ToString());

      this.ParcelSupplier = ParcelSupplier.Parse(fields.ShippingData.ParcelSupplierUID);
      this.ShippingGuide = fields.ShippingData.ShippingGuide;
      this.ParcelAmount = fields.ShippingData.ParcelAmount;
      this.CustomerAmount = fields.ShippingData.CustomerAmount;
      this.ShippingDate = DateTime.Now;
      this.DeliveryDate = DateTime.Now;
      this.ShippingMethod = EnumExtensions.GetShippingMethodEnum(salesOrder.ShippingMethod);
      this.Status = fields.ShippingData.Status;
    }


    #endregion Private methods

  } // class ShippingEntry


} // namespace Empiria.Trade.ShippingAndHandling
