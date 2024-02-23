/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : CustomerContactDto                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of customer contact.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Trade.Core.Adapters {

  /// Output DTO used to return the entries of customer contact.  
  public class CustomerContactDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Email {
      get; internal set;
    }

    public string Phone {
      get; internal set;
    } = string.Empty;


  } // class CustomerContactDto

} // namespace Empiria.Trade.Core.Adapters
