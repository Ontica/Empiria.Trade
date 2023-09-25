/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : OrderFields                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a order attributes list.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria;

using Empiria.Trade.Core.UsesCases;
using Empiria.Trade.Core.Adapters;

namespace Empiria.Trade.Sales.Adapters {

  public class OrderFields {

    #region Constructors and parsers

    public OrderFields() {
      // Required by Empiria Framework.
    }

    public string Notes {
      get; set;
    } = string.Empty;
        
    public string CustomerUID {
      get; set;
    }

    public string CustomerContactUID {
      get; set;
    }

    public string SupplierUID {
      get; set;
    }

    public string SalesAgentUID {
      get; set;
    }

    #endregion Constructors and parsers

    #region Internal methods

    internal int GetCustomer() {
      return GetParty(this.CustomerUID).id;
      
    }
       

    internal int GetSalesAgent() {
      return GetParty(this.SalesAgentUID).id;
    }

    internal int GetSupplier() {
      return GetParty(this.SupplierUID).id;
      
    }

    #endregion Internal methods

    #region Private methods

    private ShortPartyDto GetParty(string uid) {
      var usecase = PartyUseCases.UseCaseInteractor();

      return usecase.GetParty(uid);
    }

    #endregion Private methods


  }  //  internal class OrderFields




} // namespace Empiria.Trade.Sales.Adapters
