/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Inventory Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Information Holder                      *
*  Type     : Inventory                                  License   : Please read LICENSE.txt file            *
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
    internal int InventoryOrderTypeId {
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



    internal int GetInventoryOrderTypeId(string uid) {

      if (uid == "5851e71b-3a1f-40ab-836f-ac3d2c9408de") {
        return 1;
      } else if (uid == "ab8e950e-94e9-4ae5-943a-49abad514g52") {
        return 2;
      } else if (uid == "wered868-a7ec-47f5-b1b9-8c0f73b04kuk") {
        return 3;
      } else if (uid == "2vgf36bc-535c-4a07-8475-3e6568ebbopi") {
        return 4;
      } else if (uid == "2ft8y5h4-db55-48b3-aa78-63132a8d5e7f") {
        return 5;
      } else {
        return -1;
      }

    }

    #endregion

    #region Private methods

    protected override void OnSave() {

      if (this.InventoryOrderId == 0) {

        this.InventoryOrderId = this.Id;
        this.InventoryOrderUID = this.UID;
        this.InventoryOrderNo = GenerateOrderNumber();
      }
      InventoryOrderData.WriteInventoryEntry(this);
    }


    private void MapToInventoryOrderEntry(InventoryOrderFields fields, string inventoryOrderUID) {

      this.InventoryOrderTypeId = GetInventoryOrderTypeId(fields.InventoryOrderTypeUID); //TODO REGISTRAR TIPOS EN TABLA TYPES

      if (inventoryOrderUID != string.Empty) {
        this.InventoryOrderId = Parse(inventoryOrderUID).InventoryOrderId;
        this.InventoryOrderUID = inventoryOrderUID;
        this.InventoryOrderNo = GenerateOrderNumber();
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
      this.PostingTime = DateTime.Now;
      this.ClosingTime = new DateTime(2049, 01, 01);
      this.ScheduledTime = new DateTime(2049, 01, 01); //TODO ENVIAR FECHA PROGRAMADA

    }


    private string GenerateOrderNumber() {

      string orderNumber = string.Empty;

      if (this.InventoryOrderTypeId == 1) {
        orderNumber = $"OCFI";
      } else if (this.InventoryOrderTypeId == 2) {
        orderNumber = $"OCFM";
      } else if (this.InventoryOrderTypeId == 3) {
        orderNumber = $"OCFA";
      } else if (this.InventoryOrderTypeId == 4) {
        orderNumber = $"OT";
      } else if (this.InventoryOrderTypeId == 5) {
        orderNumber = $"OSV";
      } else {
        return string.Empty;
      }
      return $"{orderNumber}{this.InventoryOrderId.ToString().PadLeft(9, '0')}";
    }

    #endregion Private methods


  } // class InventoryOrderEntry

} // namespace Empiria.Trade.Inventory.Domain
