
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
using Empiria.Trade.Core.Data;
using System.Collections.Generic;
using Empiria.Trade.Core.Domain;


namespace Empiria.Trade.Core.Adapters {

  /// <summary>Methods used to map Parties.   </summary>
  static internal class PartyMapper {


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

      internal static ContactDto MapToContact(Party party) {
      var dto = new ContactDto {
        UID = party.UID,
        Name = party.Name,
        Contacts = MapPartyContacts(party.Contacts)
      };

    return dto;
    }

    private static PartyContactsDto MapToPartyContact(PartyContact contact) {
      var dto = new PartyContactsDto {
        id = contact.Index,        
        Name = contact.Name,
        Phone = contact.PhoneNumber
      };


      return dto;
    }


    internal static FixedList<NamedEntityDto> MapToMinimalPartyDto(FixedList<Party> partyList) {      
      return partyList.MapToNamedEntityList();
  }
    

    #endregion Public methods


    #region Private methods
   private static FixedList<PartyContactsDto> MapPartyContacts(FixedList<PartyContact> contacts) {
    List<PartyContactsDto> contactsList = new List<PartyContactsDto>();

    foreach (PartyContact contact in contacts) {
        contactsList.Add(MapToPartyContact(contact));
    }

     return contactsList.ToFixedList();
  }

   
    #endregion Private methods

  } //static internal class PartyMapper

} // namespace Empiria.Trade.Core.Adapters
