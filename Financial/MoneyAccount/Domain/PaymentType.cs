/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Money Account Management                   Component : Domain Layer                            *
*  Assembly : Empiria.Trade.dll                          Pattern   : Partitioned Type / Information Holder   *
*  Type     : Money Account                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents payment type.                                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Financial {
  /// Represents payment type.
  public class PaymentType :  GeneralObject  {

    #region Constructor and parsers


    public PaymentType() {
      //no-op
    }

    static public PaymentType Parse(int id) => ParseId<PaymentType>(id);

    static public PaymentType Parse(string uid) => ParseKey<PaymentType>(uid);

    static public PaymentType Empty => ParseEmpty<PaymentType>();


    #endregion Constructor and parsers


    #region Properties


    #endregion Properties

  } // class PaymentType 

} // namespace Empiria.Trade.Financial
