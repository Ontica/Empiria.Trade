/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Web Api                                 *
*  Assembly : Empiria.Trade.WebApi.Core.dll              Pattern   : Controller                              *
*  Type     : PartyController                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Query web API used to managament Parties.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Trade.Core.Adapters;
using Empiria.Trade.Core.UsesCases;

using Empiria.WebApi;


namespace Empiria.Trade.WebApi.Core {

  // Query web API used to managament Parties.
  public class PartyController : WebApiController {


    #region Web Apis

    [HttpGet]
    [Route("v4/trade/contacts/account-holders")]
    public CollectionModel GetAccountHolders([FromUri] string keywords) {

      base.RequireResource(keywords, "keywords");

      using (var usecases = PartyUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> customers = usecases.GetAccountHolders(keywords);

        return new CollectionModel(base.Request, customers);
      }
    }

    [HttpGet]
    [Route("v4/trade/contacts/customers")]
    public CollectionModel GetCustomers([FromUri] string keywords) {

      base.RequireResource(keywords, "keywords");

      using (var usecases = PartyUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> customers = usecases.GetCustomers(keywords);

        return new CollectionModel(base.Request, customers);
      }
    }

    [HttpGet]
    [Route("v4/trade/contacts/customers-with-contacts")]
    public CollectionModel GetCustomerContacts([FromUri] string keywords) {

      using (var usecases = PartyUseCases.UseCaseInteractor()) {
        FixedList<ContactDto> customerContacts = usecases.GetCustomerContacts(keywords);

        return new CollectionModel(base.Request, customerContacts);
      }
    }

    [HttpGet]
    [Route("v4/trade/contacts/suppliers")]
    public CollectionModel GetSuppliers([FromUri] string keywords) {

      using (var usecases = PartyUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> suppliers = usecases.GetSuppliers(keywords);

        return new CollectionModel(base.Request, suppliers);
      }
    }

    [HttpGet]
    [Route("v4/trade/contacts/internal-suppliers")]
    public CollectionModel GetInternalSupppliers() {

      using (var usecases = PartyUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> internalSuppliers = usecases.GetInternalSuppliers();

        return new CollectionModel(base.Request, internalSuppliers);
      }
    }

    [HttpGet]
    [Route("v4/trade/contacts/sales-agents")]
    public CollectionModel GetSalesAgents() {

      using (var usescase = PartyUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> salesAgents = usescase.GetSalesAgents();

        return new CollectionModel(base.Request, salesAgents);
      }
    }


    [HttpGet]
    [Route("v4/trade/contacts/inventory-supervisors")]
    public CollectionModel GetInventorySupervisors() {

      using (var usescase = PartyUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> responsibles = usescase.GetSalesAgents();

        return new CollectionModel(base.Request, responsibles);
      }
    }


    [HttpGet]
    [Route("v4/trade/contacts/warehousemen")]
    public CollectionModel GetWarehousemen() {

      using (var usescase = PartyUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> assignedToList = usescase.GetSalesAgents();

        return new CollectionModel(base.Request, assignedToList);
      }
    }

    #endregion Web Apis


  }
}
