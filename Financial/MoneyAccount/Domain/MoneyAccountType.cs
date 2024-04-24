/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Domain Layer                            *
*  Assembly : Empiria.Trade.dll                          Pattern   : Partitioned Type / Information Holder   *
*  Type     : Money Account Types                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents money account types.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Core.Common;



namespace Empiria.Trade.Financial {
  /// Represents money account types.
  public class MoneyAccountType : GeneralObject {

    #region Constructor and parsers


    public MoneyAccountType() {
      //no-op
    }

    static public MoneyAccountType Parse(int id) => ParseId<MoneyAccountType>(id);

    static public MoneyAccountType Parse(string uid) => ParseKey<MoneyAccountType>(uid);

    static public MoneyAccountType Empty => ParseEmpty<MoneyAccountType>();


    #endregion Constructor and parsers


    #region Properties


    [DataField("ObjectExtData")]
    public string ObjectExtData {
      get; set;
    }

    #endregion Properties
      

  } //  class MoneyAccountType

} // namespace Empiria.Trade.Financial
