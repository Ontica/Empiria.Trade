﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : PackageType                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a package type.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Products.Adapters;
using Newtonsoft.Json;

namespace Empiria.Trade.ShippingAndHandling {


  public class PackageType : GeneralObject {


    #region Constructor and parsers


    public PackageType() {
      //no-op
    }

    static public PackageType Parse(int id) => ParseId<PackageType>(id);

    static public PackageType Parse(int id, bool reload) => ParseId<PackageType>(id, reload);

    static public PackageType Parse(string uid) => ParseKey<PackageType>(uid);

    static public PackageType Empty => ParseEmpty<PackageType>();


    #endregion Constructor and parsers


    #region Properties


    [DataField("ObjectId")]
    public int PackageTypeId {
      get; set;
    }


    [DataField("ObjectKey")]
    public string ObjectKey {
      get; set;
    }


    [DataField("ObjectExtData")]
    public string ObjectExtData {
      get; set;
    }


    public decimal TotalVolume {
      get; set;
    }


    public FixedList<AttributesDto> Attributes {
      get; set;
    } = new FixedList<AttributesDto>();


    public decimal Length {
      get; set;
    }


    public decimal Width {
      get; set;
    }


    public decimal Height {
      get; set;
    }


    #endregion Properties


    public FixedList<AttributesDto> GetExtData() {

      AttributesListDto attrs = new AttributesListDto();
      attrs = JsonConvert.DeserializeObject<AttributesListDto>(ObjectExtData);
      return attrs.Attributes;

    }


    public void GetVolumeAttributes() {

      this.Attributes = this.GetExtData();

      foreach (var attr in this.Attributes) {

        decimal value = Convert.ToDecimal(attr.Value);

        if (attr.Name == "length") {
          this.Length = value;
        }

        if (attr.Name == "width") {
          this.Width = value;
        }

        if (attr.Name == "height") {
          this.Height = value;
        }

      }

      this.TotalVolume = Length * Width * Height;
    }

  } // class PackageType


} // namespace Empiria.Trade.ShippingAndHandling
