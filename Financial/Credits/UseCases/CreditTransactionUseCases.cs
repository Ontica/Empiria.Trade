/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Order Management                     Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Orders.dll                   Pattern   : Use case interactor class               *
*  Type     : CreditTransactionUseCases                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to management Credits transactions.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Net.NetworkInformation;
using System.Xml.Linq;
using Empiria.Services;
using Empiria.StateEnums;
using Empiria.Trade.Financial.Adapters;


namespace Empiria.Trade.Financial.UseCases {

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

    public CreditTransactionDto AddCustomerCreditTransaction(CreditTrasnactionFields fields) {
      Assertion.Require(fields, "fields");

      fields.TypeId = 1;

      var creditTransaction = new CreditTransaction(fields);
      creditTransaction.Save();

      return CreditTransactionMapper.Map(creditTransaction);
    }

    public CreditTransactionDto Update(CreditTrasnactionFields fields) {
      Assertion.Require(fields, "fields");

      var creditTransaction = CreditTransaction.Parse(fields.UID);
      creditTransaction.Update(fields);
      creditTransaction.Save();

      return CreditTransactionMapper.Map(creditTransaction);
    }

    public CreditTransactionDto Cancel(string creditTransactionUID) {
      Assertion.Require(creditTransactionUID, "creditTransactionUID");

      var creditTransaction = CreditTransaction.Parse(creditTransactionUID);
      creditTransaction.Cancel();
      
      return CreditTransactionMapper.Map(creditTransaction);
    }

    public CreditTransactionDto Cancel(int orderId) {
      Assertion.Require(orderId, "orderId");

      var creditTransaction = CreditTransaction.ParseByOrderId(orderId);
      creditTransaction.Cancel();

      return CreditTransactionMapper.Map(creditTransaction);
    }

    public FixedList<CreditTransactionDto> GetCreditTransactions(int customerId) {
     
      var creditTransactions = CreditTransaction.GetCreditTransactions(customerId);

      return CreditTransactionMapper.MapCreditTransactions(creditTransactions);
    }


    public decimal GetCustomerTotalDebt(int customerId) {
      return CreditTransaction.GetCustomerTotalDebt(customerId);
    }

    public decimal GetCusomerCreditLimit(int customerId) {
      return CreditTransaction.GetCreditLimit(customerId);
    }


    #endregion Use cases

  } // class CreditTransactionUseCases

} // namespace Empiria.Trade.Sales.Credits.UseCases
