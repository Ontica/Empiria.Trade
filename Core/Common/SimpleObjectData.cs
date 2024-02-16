/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Trade Core                                 Component : Common Types                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : SimpleDataObject                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a simple data object.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
//using Empiria.Trade.ShippingAndHandling;

namespace Empiria.Trade.Core.Common {

  /// <summary>Represents a simple data object.</summary>
  public class SimpleObjectData : GeneralObject {

    #region Constructor and parsers


    public SimpleObjectData() {
      //no-op
    }

    static public SimpleObjectData Parse(int id) => ParseId<SimpleObjectData>(id);

    static public SimpleObjectData Parse(string uid) => ParseKey<SimpleObjectData>(uid);

    static public SimpleObjectData Empty => ParseEmpty<SimpleObjectData>();


    #endregion Constructor and parsers


    #region Properties


    [DataField("ObjectId")]
    public int ObjectId {
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

    #endregion Properties


  } // class SimpleDataObject

} // namespace Empiria.Trade.Core.Common
