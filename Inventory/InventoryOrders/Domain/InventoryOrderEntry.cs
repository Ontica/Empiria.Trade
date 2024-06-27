/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : InventoryOrderEntry                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an inventory order entry.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core;
using Empiria.Trade.Inventory;
using Empiria.Trade.Inventory.Adapters;
using Empiria.Trade.Inventory.Data;

namespace Empiria.Trade.Inventory {


  /// <summary>Represents an inventory order entry.</summary>
  public class InventoryOrderEntry : BaseObject {

    #region Constructors and parsers

    public InventoryOrderEntry() {
      //no-op
    }


    static public InventoryOrderEntry Parse(int id) => ParseId<InventoryOrderEntry>(id);

    static public InventoryOrderEntry Parse(string uid) => ParseKey<InventoryOrderEntry>(uid);

    static public InventoryOrderEntry Empty => ParseEmpty<InventoryOrderEntry>();


    public InventoryOrderEntry(InventoryOrderFields fields, string inventoryOrderUID) {

      MapToInventoryOrderEntry(fields, inventoryOrderUID);
    }


    #endregion Constructors and parsers

    #region Properties

    [DataField("InventoryOrderId")]
    internal int InventoryOrderId {
      get; set;
    }


    [DataField("InventoryOrderUID")]
    internal string InventoryOrderUID {
      get; set;
    }


    [DataField("InventoryOrderTypeId")]
    internal InventoryOrderType InventoryOrderType {
      get; set;
    }


    [DataField("InventoryOrderNo")]
    internal string InventoryOrderNo {
      get; set;
    }


    [DataField("ReferenceId")]
    internal int ReferenceId {
      get; set;
    }


    [DataField("ResponsibleId")]
    internal int ResponsibleId {
      get; set;
    }


    [DataField("AssignedToId")]
    internal int AssignedToId {
      get; set;
    }


    [DataField("InventoryOrderNotes")]
    internal string Notes {
      get; set;
    }


    [DataField("InventoryOrderExtData")]
    internal string InventoryOrderExtData {
      get; set;
    } = string.Empty;


    [DataField("InventoryOrderKeywords")]
    internal string InventoryOrderKeywords {
      get; set;
    }


    [DataField("ScheduledTime")]
    internal DateTime ScheduledTime {
      get; set;
    }


    [DataField("ClosingTime")]
    internal DateTime ClosingTime {
      get; set;
    }


    [DataField("PostingTime")]
    internal DateTime PostingTime {
      get; set;
    }


    [DataField("PostedById")]
    internal int PostedById {
      get; set;
    }


    [DataField("InventoryOrderStatus", Default = InventoryStatus.Abierto)]
    internal InventoryStatus Status {
      get; set;
    } = InventoryStatus.Abierto;


    internal FixedList<InventoryOrderItem> InventoryOrderItems {
      get; set;
    } = new FixedList<InventoryOrderItem>();


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(

          InventoryOrderUID, InventoryOrderNo, Notes
        );
      }
    }

    #endregion Properties


    #region Public methods

    

    #endregion

    #region Private methods

    protected override void OnSave() {

      if (this.InventoryOrderId == 0) {

        this.InventoryOrderId = this.Id;
        this.InventoryOrderUID = this.UID;
      }

      this.InventoryOrderNo = GenerateOrderNumber();

      InventoryOrderData.WriteInventoryEntry(this);
    }


    private void MapToInventoryOrderEntry(InventoryOrderFields fields, string inventoryOrderUID) {

      this.InventoryOrderType = InventoryOrderType.Parse(fields.InventoryOrderTypeUID);

      if (inventoryOrderUID != string.Empty) {
        this.InventoryOrderId = Parse(inventoryOrderUID).InventoryOrderId;
        this.InventoryOrderUID = inventoryOrderUID;
      } else {
        this.PostingTime = DateTime.Now;
      }

      this.ReferenceId = fields.ReferenceId;
      this.ResponsibleId = fields.ResponsibleUID == string.Empty ? -1 :
                           Party.Parse(fields.ResponsibleUID).Id;
      this.AssignedToId = fields.AssignedToUID == string.Empty ? -1 :
                          Party.Parse(fields.AssignedToUID).Id;
      this.Notes = fields.Notes;
      this.InventoryOrderExtData = "";
      this.Status = InventoryStatus.Abierto;

      this.PostedById = ExecutionServer.CurrentUserId;
      this.ClosingTime = new DateTime(2049, 01, 01);
      this.ScheduledTime = new DateTime(2049, 01, 01); //TODO ENVIAR FECHA PROGRAMADA

    }


    private string GenerateOrderNumber() {

      string orderNumber = this.InventoryOrderType.InventoryOrderTypeCode.Name;
      
      if (orderNumber != string.Empty) {
        return $"{orderNumber}{this.InventoryOrderId.ToString().PadLeft(9, '0')}";
      }

      return string.Empty;
    }

    #endregion Private methods


  } // class InventoryOrderEntry

} // namespace Empiria.Trade.Inventory.Domain
