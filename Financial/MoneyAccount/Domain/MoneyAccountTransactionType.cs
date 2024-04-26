/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Domain Layer                            *
*  Assembly : Empiria.Trade.dll                          Pattern   : Partitioned Type / Information Holder   *
*  Type     : Money Account Transaction Types            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents money account transaction types.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Financial {
  ///  Represents money account transaction types.      
  public class MoneyAccountTransactionType : GeneralObject {

    #region Constructor and parsers


    public MoneyAccountTransactionType() {
      //no-op
    }

    static public MoneyAccountTransactionType Parse(int id) => ParseId<MoneyAccountTransactionType>(id);

    static public MoneyAccountTransactionType Parse(string uid) => ParseKey<MoneyAccountTransactionType>(uid);

    static public MoneyAccountTransactionType Empty => ParseEmpty<MoneyAccountTransactionType>();


    #endregion Constructor and parsers


    #region Properties


    

    #endregion Properties


  } // class MoneyAccountTransactionType

} // namespace Empiria.Trade.Financial
