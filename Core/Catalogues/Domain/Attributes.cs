/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : Attributes                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represent attributes for entries.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Newtonsoft.Json;

namespace Empiria.Trade.Core.Catalogues {


  /// <summary>Represent attributes for entries.</summary>
  public class Attributes {

    public string Name {
      get; set;
    } = string.Empty;


    public string Value {
      get; set;
    } = string.Empty;


    public FixedList<Attributes> GetAttributesList(string attributes) {

      AttributesList attrs = new AttributesList();

      if (attributes != "") {
        attrs = JsonConvert.DeserializeObject<AttributesList>(attributes);
      }

      return attrs.Attributes.ToFixedList();

    }

  } // class Attributes


  /// <summary>Object used to return a list of attributes</summary>
  public class AttributesList {

    public FixedList<Attributes> Attributes {
      get; set;
    } = new FixedList<Attributes>();

  } // AttributesList


}
