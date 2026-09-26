/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Information Holder                      *
*  Type     : PackageType                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a package type.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Trade.Core.Catalogues;
using Newtonsoft.Json;

namespace Empiria.Trade.Core {


  /// <summary>Represents a package type.</summary>
  public class PackageType : CommonStorage {


    #region Constructor and parsers

    public PackageType() {
      //no-op
    }

    static public PackageType Parse(int id) => ParseId<PackageType>(id);

    static public PackageType Parse(string uid) => ParseKey<PackageType>(uid);

    static public PackageType Empty => ParseEmpty<PackageType>();

    static public FixedList<PackageType> GetList() {

      return CommonStorage.GetList<PackageType>().ToFixedList();
    }

    #endregion Constructor and parsers


    #region Properties


    [DataField("Object_Id")]
    public int PackageTypeId {
      get; set;
    }


    [DataField("Object_Named_Key")]
    public string ObjectKey {
      get; set;
    }


    [DataField("Object_Ext_Data")]
    public string ObjectExtData {
      get; set;
    }


    public decimal Length {
      get {
        return ExtData.Get("length", 1);
      }
      private set {
        ExtData.SetIfValue("length", value);
      }
    }


    public decimal Width {
      get {
        return ExtData.Get("width", 1);
      }
      private set {
        ExtData.SetIfValue("width", value);
      }
    }


    public decimal Height {
      get {
        return ExtData.Get("height", 1);
      }
      private set {
        ExtData.SetIfValue("height", value);
      }
    }


    public decimal TotalVolume {
      get {
        return Length  * Width * Height;
      }
    }


    public decimal TotalVolumeInMeters {
      get {
        return (Length/100) * (Width/100) * (Height/100);
      }
    }

    #endregion Properties

  } // class PackageType


} // namespace Empiria.Trade.ShippingAndHandling
