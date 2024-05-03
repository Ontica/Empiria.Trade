/* Empiria Trade *********************************************************************************************
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
using Empiria.Trade.Sales.Adapters;
using Empiria.Trade.Sales.UseCases;
using System.Runtime.InteropServices;



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


  } // class MoneyAccountControler

} // namespace Empiria.Trade.WebApi.Financial
