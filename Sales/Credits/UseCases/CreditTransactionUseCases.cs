/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Orders.dll                   Pattern   : Use case interactor class               *
*  Type     : CreditTransactionUseCases                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to management Credits transactions.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;
using Empiria.Trade.Sales.Adapters;

namespace Empiria.Trade.Sales.Credits.UseCases {

  /// Use cases used to management Credits transactions.
  public class CreditTransactionUseCases : UseCase {

    #region Constructors and parsers

    public CreditTransactionUseCases() {
      // no-op
    }

    static public CreditTransactionUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<CreditTransactionUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<CreditTransactionDto> GetCreditTransactions(int customerId) {
     
      var creditTransactions = CreditTransaction.GetCreditTransactions(customerId);

      return CreditTransactionMapper.MapCreditTransactions(creditTransactions);
    }

    #endregion Use cases

  } // class CreditTransactionUseCases

} // namespace Empiria.Trade.Sales.Credits.UseCases
