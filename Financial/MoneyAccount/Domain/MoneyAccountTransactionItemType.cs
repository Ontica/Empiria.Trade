/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Domain Layer                            *
*  Assembly : Empiria.Trade.dll                          Pattern   : Partitioned Type / Information Holder   *
*  Type     : MoneyAccountTransactionItemType            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents money account transaction item type.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Financial {
  /// Represents money account transaction item type.
  public class MoneyAccountTransactionItemType : GeneralObject {
    #region Constructor and parsers


    public MoneyAccountTransactionItemType() {
      //no-op
    }

    static public MoneyAccountTransactionItemType Parse(int id) => ParseId<MoneyAccountTransactionItemType>(id);

    static public MoneyAccountTransactionItemType Parse(string uid) => ParseKey<MoneyAccountTransactionItemType>(uid);

    static public MoneyAccountTransactionItemType Empty => ParseEmpty<MoneyAccountTransactionItemType>();


    #endregion Constructor and parsers



  } // class MoneyAcccountTransactionItemType

} // namespace Empiria.Trade.Financial
