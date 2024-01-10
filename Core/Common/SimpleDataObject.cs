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
using Empiria.Trade.ShippingAndHandling;

namespace Empiria.Trade.Core.Common {

  /// <summary>Represents a simple data object.</summary>
  public class SimpleDataObject : GeneralObject {

    #region Constructor and parsers


    public SimpleDataObject() {
      //no-op
    }

    static public SimpleDataObject Parse(int id) => ParseId<SimpleDataObject>(id);

    static public SimpleDataObject Parse(int id, bool reload) => ParseId<SimpleDataObject>(id, reload);

    static public SimpleDataObject Parse(string uid) => ParseKey<SimpleDataObject>(uid);

    static public SimpleDataObject Empty => ParseEmpty<SimpleDataObject>();


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
