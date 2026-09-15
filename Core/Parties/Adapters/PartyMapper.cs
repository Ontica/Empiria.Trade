
/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Mapper class                            *
*  Type     : PartyMapper                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Parties.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Parties;
using Empiria.Trade.Core.Domain;
using Empiria.Trade.Core.UsesCases;
using Empiria.Trade.Products;

namespace Empiria.Trade.Core.Adapters {

  /// <summary>Methods used to map Parties.   </summary>
  public static class PartyMapper {


    #region Public methods

    static internal ShortPartyDto MapTo(Party party) {
      var dto = new ShortPartyDto {
        id = party.Id,
        UID = party.UID,
        Name = party.Name,
        Phone = party.Contact.EMail
      };


      return dto;
    }


    static internal FixedList<ContactDto> MapToCustomers(FixedList<Party> customers) {
      
      var mappedList = customers.Select((x) => MapToCustomer(x));

      return new FixedList<ContactDto>(mappedList);
    }


    public static ContactDto MapToCustomer(Party party) {

      return new ContactDto {
        UID = party.UID,
        Name = party.Name,
        PriceList = GetPriceList(party),
        Contacts = MapCustomerContacts(party.Id),
        Addresses = MapCustomerAddresses(party.UID)
      };
    }

    public static FixedList<CustomerShortAddressDto> MapCustomerAddresses(string customerUID) {

      using (var usescase = CustomerUseCases.UseCaseInteractor()) {
        return usescase.GetCustomerAddress(customerUID);
      }
    }

    public static FixedList<CustomerContactDto> MapCustomerContacts(int customerId) {

      using (var usescase = CustomerUseCases.UseCaseInteractor()) {
        return usescase.GetCustomerContacts(customerId);
      }
    }


    internal static FixedList<NamedEntityDto> MapToMinimalPartyDto(FixedList<Party> partyList) {
      return partyList.MapToNamedEntityList();
    }


    #endregion Public methods


    #region Private methods

    static private string GetPriceList(Party customer) {

      var priceTypeName = ProductPriceType.GetList();
      var customerPriceList = customer.ExtendedData.Get<int>("ListaPrecios", 0);

      switch (customerPriceList) {
        case 1:

          return priceTypeName.Find(a => a.Name.Contains("Lista Precios 1")).Name;
        case 2:

          return priceTypeName.Find(a => a.Name.Contains("Lista Precios 2")).Name;
        case 3:

          return priceTypeName.Find(a => a.Name.Contains("Lista Precios 3")).Name;
        case 4:

          return priceTypeName.Find(a => a.Name.Contains("Lista Precios 3")).Name;
        case 5:

          return priceTypeName.Find(a => a.Name.Contains("Lista Precios 5")).Name;
        case 6:

          return priceTypeName.Find(a => a.Name.Contains("Lista Precios 3")).Name;
        case 7:

          return priceTypeName.Find(a => a.Name.Contains("Lista Precios 7")).Name;

        default:
          return priceTypeName.Find(a => a.Name.Contains("Lista Precios 3")).Name;
      }

    }

    #endregion Private methods

  } //static internal class PartyMapper

} // namespace Empiria.Trade.Core.Adapters
