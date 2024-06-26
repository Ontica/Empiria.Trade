﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Sales Management                           Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.dll                   Pattern   : Controller                              *
*  Type     : MoneyAccountController                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web API used to handle money account.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.Trade.Financial.Adapters;
using Empiria.Trade.Financial.UseCases;

using Empiria.Trade.Financial;



namespace Empiria.Trade.WebApi.Financial {

  /// Web API used to handle money account.
  public class MoneyAccountController : WebApiController {

    [HttpGet]
    [Route("v4/trade/financial/money-accounts/money-accounts-types")]
    public CollectionModel GetMoneyAccountTypes() {

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> moneyAccountTypes = usecases.GetMoneyAccountTypes();

        return new CollectionModel(base.Request, moneyAccountTypes);
      }
    }


    [HttpGet]
    [Route("v4/trade/financial/money-accounts/status")]
    public CollectionModel GetMoneyAccountStatus() {

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> statusList = usecases.GetStatusList();

        return new CollectionModel(base.Request, statusList);
      }

    }


    [HttpPost]
    [Route("v4/trade/financial/money-accounts/search")]
    public SingleObjectModel GetMoneyAccounts([FromBody] SearchMoneyAccountFields fields) {

      base.RequireBody(fields);
      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {
        SearchMoneyAccountDto moneyAccounts = usecases.SearchMoneyAccounts(fields);

        return new SingleObjectModel(base.Request, moneyAccounts);
      } // using 

    }


    [HttpGet]
    [Route("v4/trade/financial/money-accounts/{moneyaccountUID:guid}")]
    public SingleObjectModel GetMoneyAccount([FromUri] string moneyAccountUID) {

      base.RequireResource(moneyAccountUID, "orderUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountDto = usecases.GetMoneyAccount(moneyAccountUID);

        return new SingleObjectModel(this.Request, moneyAccountDto);
      }
    }

    [HttpPost]
    [Route("v4/trade/financial/money-accounts")]
    public SingleObjectModel CreateMoneyAccount([FromBody] MoneyAccountFields fields) {

      base.RequireBody(fields);

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

         var moneyAccountDto = usecases.AddMoneyAccount(fields);

        return new SingleObjectModel(this.Request, moneyAccountDto);
      }
    }

    [HttpPut]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}")]
    public SingleObjectModel UpdateMoneyAccount([FromUri] string moneyAccountUID, [FromBody] MoneyAccountFields fields) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireBody(fields);

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountDto = usecases.UpdateMoneyAccount(fields, moneyAccountUID);

        return new SingleObjectModel(this.Request, moneyAccountDto);
      }

    }


    [HttpDelete]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/cancel")]
    public SingleObjectModel CancelMoneyAccount([FromUri] string moneyAccountUID) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountDto = usecases.CancelMoneyAccount(moneyAccountUID);

        return new SingleObjectModel(this.Request, moneyAccountDto);
      }

    }

    [HttpPost]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/suspend")]
    public SingleObjectModel SuspendMoneyOrder([FromUri] string moneyAccountUID) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountDto = usecases.SuspendMoneyAccount(moneyAccountUID);

        return new SingleObjectModel(this.Request, moneyAccountDto);
      }

    }


    [HttpPost]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/pending")]
    public SingleObjectModel PendingMoneyOrder([FromUri] string moneyAccountUID) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountDto = usecases.PendingMoneyAccount(moneyAccountUID);

        return new SingleObjectModel(this.Request, moneyAccountDto);
      }

    }

    [HttpPost]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/active")]
    public SingleObjectModel ActiveMoneyOrder([FromUri] string moneyAccountUID) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountDto = usecases.ActiveMoneyAccount(moneyAccountUID);

        return new SingleObjectModel(this.Request, moneyAccountDto);
      }

    }

    [HttpPost]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/transactions/")]
    public SingleObjectModel AddMoneyAccountTransaction([FromUri] string moneyAccountUID,[FromBody] MoneyAccountTransactionFields fields) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireBody(fields);

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountTransactionDto = usecases.AddMoneyAccountTransaction(moneyAccountUID,fields);

        return new SingleObjectModel(this.Request, moneyAccountTransactionDto);
      }
    }

    [HttpPut]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/transactions/{moneyAccountTransactionUID:guid}")]
    public SingleObjectModel UpdateMoneyAccountTransaction([FromUri] string moneyAccountUID, [FromUri] string moneyAccountTransactionUID, [FromBody] MoneyAccountTransactionFields fields) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireResource(moneyAccountTransactionUID, "moneyAccountTransactionUID");
      base.RequireBody(fields);

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountTransactionDto = usecases.UpdateMoneyAccountTransaction(fields, moneyAccountTransactionUID);

        return new SingleObjectModel(this.Request, moneyAccountTransactionDto);
      }

    }

    [HttpDelete]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/transactions/{moneyAccountTransactionUID:guid}/cancel")]
    public SingleObjectModel CancelMoneyAccountTransaction([FromUri] string moneyAccountUID, [FromUri] string moneyAccountTransactionUID) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireResource(moneyAccountTransactionUID, "moneyAccountTransactionUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountTransactionDto = usecases.CancelMoneyAccountTransaction(moneyAccountTransactionUID);

        return new SingleObjectModel(this.Request, moneyAccountTransactionDto);
      }

    }

    [HttpGet]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID}/transactions/{moneyAccountTransactionUID:guid}")]
    public SingleObjectModel GetMoneyAccountTransaction([FromUri] string moneyAccountUID,[FromUri] string moneyAccountTransactionUID) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireResource(moneyAccountTransactionUID, "moneyAccountTransactionUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountTransactionDto = usecases.GetMoneyAccountTransaction(moneyAccountTransactionUID);

        return new SingleObjectModel(this.Request, moneyAccountTransactionDto);
      }

    }

    [HttpGet]
    [Route("v4/trade/financial/money-accounts/transactions/items-types")]
    public CollectionModel GetMoneyAccountTransactionItemsTypes() {

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {
        var itemsTypes = usecases.GetMoneyAccountTransactionItemTypes();

        return new CollectionModel(base.Request, itemsTypes);
      }
    }

    [HttpGet]
    [Route("v4/trade/financial/money-accounts/payment-types")]
    public CollectionModel GetPaymentTypes() {

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {
        var paymentTypes = usecases.GetPaymentTypes();

        return new CollectionModel(base.Request, paymentTypes);
      }
    }

    [HttpPost]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/transactions/{transactionUID:guid}/items")]
    public SingleObjectModel AddMoneyAccountTransactionItem([FromUri] string moneyAccountUID, [FromUri] string transactionUID,
                                                            [FromBody] MoneyAccountTransactionItemFields fields) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireResource(transactionUID, "transactionUID");
      base.RequireBody(fields);

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountTransactionItemDto = usecases.AddMoneyAccountTransactionItem(transactionUID,fields);

        return new SingleObjectModel(this.Request, moneyAccountTransactionItemDto);
      }
    }

    [HttpPut]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/transactions/{transactionUID:guid}/items/{itemUID:guid}")]
    public SingleObjectModel UpdateMoneyAccountTransactionItem([FromUri] string moneyAccountUID, [FromUri] string transactionUID,
                                                               [FromUri] string itemUID, [FromBody] MoneyAccountTransactionItemFields fields) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireResource(transactionUID, "transactionUID");
      base.RequireResource(itemUID, "itemUID");
      base.RequireBody(fields);

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountTransactionItemDto = usecases.UpdateMoneyAccountTransactionItem(fields, itemUID);

        return new SingleObjectModel(this.Request, moneyAccountTransactionItemDto);
      }

    }

    [HttpDelete]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/transactions/{transactionUID:guid}/items/{itemUID:guid}/cancel")]
    public SingleObjectModel CancelMoneyAccountTransactionItem([FromUri] string moneyAccountUID, [FromUri] string transactionUID,
                                                              [FromUri] string itemUID) {

      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireResource(transactionUID, "transactionUID");
      base.RequireResource(itemUID, "itemUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountTransactionItemDto = usecases.CancelMoneyAccountTransactionItem(itemUID);

        return new SingleObjectModel(this.Request, moneyAccountTransactionItemDto);
      }

    }

    [HttpGet]
    [Route("v4/trade/financial/money-accounts/{moneyAccountUID:guid}/transactions/{transactionUID:guid}/items")]
    public CollectionModel GetMoneyAccountTransactionItems([FromUri] string moneyAccountUID, [FromUri] string transactionUID) {
      
      base.RequireResource(moneyAccountUID, "moneyAccountUID");
      base.RequireResource(transactionUID, "moneyAccountTransactionUID");

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {
        var moneyAccountTransactionItems = usecases.GetMoneyAccountTransactionItems(transactionUID);

        return new CollectionModel(base.Request, moneyAccountTransactionItems);
      }
    }


    [HttpGet]
    [Route("v4/trade/financial/money-accounts/transactions/transaction-types")]
    public SingleObjectModel GetMoneyAccountTransactionTypes() {

      using (var usecases = MoneyAccountUseCases.UseCaseInteractor()) {

        var moneyAccountTransactionDto = usecases.GetMoneyAccountTransactionTypes();

        return new SingleObjectModel(this.Request, moneyAccountTransactionDto);
      }

    }



  } // class MoneyAccountControler

} // namespace Empiria.Trade.WebApi.Financial
