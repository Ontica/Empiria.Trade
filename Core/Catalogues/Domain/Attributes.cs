/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : Attributes                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represent attributes for entries.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Trade.Core {


  /// <summary>Represent attributes for entries.</summary>
  public class Attributes {

    public string Name {
      get; set;
    } = string.Empty;


    public string Value {
      get; set;
    } = string.Empty;

  } // class Attributes

}
