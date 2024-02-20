/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Data Transfer Object                    *
*  Type     : CustomerAddressDto                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of customer address.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Trade.Core.Adapters {

  /// Output DTO used to return the entries of customer address.
  public class CustomerAddressDto {

    public string UID {
      get; set;
    }

    public string Description {
      get; set;
    }

    public string Address {
      get; set;
    }

    public string Neighborhood {
      get; set;
    }

    public string City {
      get; set;
    }
           
    public string State {
      get; set;
    }
    
    public string ZipCode {
      get; set;
    }
    

  } // class CustomerAddressDto

  public class CustomerShortAddressDto  {
   
    public string UID {
      get; set;
    }

    public string Name {
      get; set;
    }

  } // class CustomerShortAddressDto 


} // namespace Empiria.Trade.Core.Adapters
