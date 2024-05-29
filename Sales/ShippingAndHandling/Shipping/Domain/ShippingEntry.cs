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
using Empiria.Trade.Core.Common;
using Empiria.Trade.Orders;
using Empiria.Trade.Sales.ShippingAndHandling.Data;
using Empiria.Trade.Sales.ShippingAndHandling.Domain;
using Empiria.Trade.Sales.ShippingAndHandling.Adapters;
using Empiria.Trade.Inventory;
using System.Linq;

namespace Empiria.Trade.Sales.ShippingAndHandling {

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

            MapToShippingEntry(fields);

        }


        #endregion Constructors and parsers


        #region Properties


        [DataField("ShippingOrderId")]
        public int ShippingOrderId {
            get; set;
        }


        [DataField("ParcelSupplierId")]
        public int ParcelSupplierId {
            get; set;
        }


        [DataField("ShippingUID")]
        public string ShippingUID {
            get; set;
        } = string.Empty;


        [DataField("ShippingGuide")]
        public string ShippingGuide {
            get; set;
        } = string.Empty;


        [DataField("ParcelAmount")]
        public decimal ParcelAmount {
            get; set;
        }


        [DataField("CustomerAmount")]
        public decimal CustomerAmount {
            get; set;
        }


        [DataField("ShippingDate")]
        public DateTime ShippingDate {
            get; set;
        }


        [DataField("DeliveryDate")]
        public DateTime DeliveryDate {
            get; set;
        }


        [DataField("ShippingMethod", Default = ShippingMethods.None)]
        public ShippingMethods ShippingMethod {
            get; set;
        } = ShippingMethods.None;


        [DataField("ShippingStatus", Default = ShippingStatus.EnCaptura)]
        public ShippingStatus Status {
            get; set;
        } = ShippingStatus.EnCaptura;


        internal string Keywords {
            get {
                return EmpiriaString.BuildKeywords(

                  ShippingUID, ShippingGuide
                );
            }
        }


        public string ShippingNumber {
            get; set;
        }


        public string DeliveryNumber {
            get; internal set;
        }


        public decimal OrdersTotal {
            get; internal set;
        }


        public FixedList<ShippingOrderItem> OrdersForShipping {
            get; set;
        } = new FixedList<ShippingOrderItem>();


        public FixedList<ShippingPallet> ShippingPallets {
            get; set;
        } = new FixedList<ShippingPallet>();


        public bool CanEdit {
            get; internal set;
        }


        public INamedEntity Customer {
            get; internal set;
        } = new NamedEntity("", "");


        #endregion Properties


        #region Private methods

        protected override void OnSave() {

            if (this.ShippingOrderId == 0) {

                this.ShippingOrderId = this.Id;
                this.ShippingUID = this.UID;
            }
            ShippingData.WriteShipping(this);
        }


        private void MapToShippingEntry(ShippingFields fields) {

            if (fields.ShippingData.ShippingUID != string.Empty) {
                this.ShippingOrderId = Parse(fields.ShippingData.ShippingUID).ShippingOrderId;
                this.ShippingUID = fields.ShippingData.ShippingUID;
            }

            this.ParcelSupplierId = SimpleObjectData.Parse(fields.ShippingData.ParcelSupplierUID).Id;
            this.ShippingGuide = fields.ShippingData.ShippingGuide;
            this.ParcelAmount = fields.ShippingData.ParcelAmount;
            this.CustomerAmount = fields.ShippingData.CustomerAmount;
            this.ShippingDate = DateTime.Now;
            this.DeliveryDate = DateTime.Now;
            this.ShippingMethod = Order.Parse(fields.Orders.First()).ShippingMethod;
            this.Status = fields.ShippingData.Status;
        }


        #endregion Private methods

    } // class ShippingEntry


} // namespace Empiria.Trade.ShippingAndHandling
