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
using Newtonsoft.Json;

namespace Empiria.Trade.Core.Catalogues {


  /// <summary>Represents a package type.</summary>
  public class PackageType : GeneralObject {


    #region Constructor and parsers


    public PackageType() {
      //no-op
    }

    static public PackageType Parse(int id) => ParseId<PackageType>(id);

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


    public void GetVolumeAttributes() {

      this.Attributes = new Attributes().GetAttributes(ObjectExtData);

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

      this.TotalVolume = (Length == 0 ? 1 : Length) *
                         (Width == 0 ? 1 : Width) *
                         (Height == 0 ? 1 : Height);
    }

  } // class PackageType


} // namespace Empiria.Trade.ShippingAndHandling
