/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : PartyDto                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return paty kinds.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Trade.Core.Adapters {
 
  public class ShortPartyDto {
     
    public int id {
      get; internal set;
    }

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Phone {
      get; internal set;
    } = string.Empty;


  } // public class ShortDto 

  public class MinimalPartyDto {
    
    public string UID {
      get; internal set;      
    }

    public string Name {
      get; internal set;
    }

  }

  public class PartyContactsDto {
    public int UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Email  {
      get; internal set;
    }

    public string Phone {
      get; internal set;
    } = string.Empty;
  }


  public class ContactDto {
    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public FixedList<PartyContactsDto> Contacts{
      get; internal set;
    }

    public FixedList<CustomerShortAddressDto> Addresses {
      get; internal set;
    }

  }

} // namespace Empiria.Trade.Core.Adapters
