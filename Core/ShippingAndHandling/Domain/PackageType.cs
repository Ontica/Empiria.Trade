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


    public FixedList<Attributes> Attributes {
      get; set;
    } = new FixedList<Attributes>();

    
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


    public FixedList<Attributes> GetExtData() {

      AttributesList attrs = new AttributesList();
      attrs = JsonConvert.DeserializeObject<AttributesList>(ObjectExtData);
      return attrs.Attributes;

    }


    public void GetTotalVolume() {
      this.TotalVolume = Length * Width * Height;
    }

  } // class PackageType

} // namespace Empiria.Trade.ShippingAndHandling
