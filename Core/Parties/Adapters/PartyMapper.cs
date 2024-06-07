
/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Mapper class                            *
*  Type     : PartyMapper                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Parties.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Collections.Generic;
using Empiria.Trade.Core.UsesCases;

namespace Empiria.Trade.Core.Adapters {

  /// <summary>Methods used to map Parties.   </summary>
  public static  class PartyMapper {


    #region Public methods

    static internal ShortPartyDto MapTo(Party party) {
      var dto = new ShortPartyDto  {
        id = party.Id,
        UID = party.UID,
        Name = party.Name,
        Phone = party.PhoneNumbers
      };


      return dto;
    }

    internal static FixedList<ContactDto> MapToContacs(FixedList<Party> partyList) {
      List<ContactDto> contacts = new List<ContactDto>();
      foreach (Party party in partyList) {
        contacts.Add(MapToContact(party));
      }

      return contacts.ToFixedList();
    }

      public static ContactDto MapToContact(Party party) {
      var dto = new ContactDto {
        UID = party.UID,
        Name = party.Name,
        Contacts = MapCustomerContacts(party.Id),
        Addresses = MapCustomerAddresses(party.UID)
      };

    return dto;
    }

    public static FixedList<CustomerShortAddressDto> MapCustomerAddresses(string UID) {
      using (var usescase = CustomerUseCases.UseCaseInteractor()) {
        var addresses = usescase.GetCustomerAddress(UID);

        return addresses;
      }

    }

    public static FixedList<CustomerContactDto> MapCustomerContacts(int customerId) {
      using (var usescase = CustomerUseCases.UseCaseInteractor()) {
        var contacts = usescase.GetCustomerContacts(customerId);

        return contacts;
      }

    }

   
    internal static FixedList<NamedEntityDto> MapToMinimalPartyDto(FixedList<Party> partyList) {      
      return partyList.MapToNamedEntityList();
  }
    

    #endregion Public methods


    #region Private methods
 
   
    #endregion Private methods

  } //static internal class PartyMapper

} // namespace Empiria.Trade.Core.Adapters
