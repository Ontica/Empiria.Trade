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
  internal class MoneyAcccountTransactionItemType : GeneralObject {
    #region Constructor and parsers


    public MoneyAcccountTransactionItemType() {
      //no-op
    }

    static public MoneyAcccountTransactionItemType Parse(int id) => ParseId<MoneyAcccountTransactionItemType>(id);

    static public MoneyAcccountTransactionItemType Parse(string uid) => ParseKey<MoneyAcccountTransactionItemType>(uid);

    static public MoneyAcccountTransactionItemType Empty => ParseEmpty<MoneyAcccountTransactionItemType>();


    #endregion Constructor and parsers



  } // class MoneyAcccountTransactionItemType

} // namespace Empiria.Trade.Financial
