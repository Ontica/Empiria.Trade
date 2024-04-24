/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Data Transfer Object                    *
*  Type     : SearchMoneyAcountDto                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return serach money accounts info.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.StateEnums;
using Empiria.Trade.Core.Common;


namespace Empiria.Trade.Financial.Adapters {



  public class SearchMoneyAccountDto {

    public SearchMoneyAccountFields Query {
      get; internal set;
    }

    public FixedList<DataTableColumn> Columns {
      get; internal set;
    }

    public FixedList<SearchMoneyAccountEntriesDto> Entries {
      get; internal set;
    }

  } // class SearchSalesOrderDto

  /// Output DTO used to return serach money accounts info.
  public class SearchMoneyAccountEntriesDto {

    public string UID {
      get; internal set;
    }

    public string MoneyAccountType {
      get; internal set;
    }

    public string Owner {
      get; internal set;
    }

    public decimal Balance {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }
     

  } // namespace Empiria.Trade.Financial.Adapters

} // class SearchMoneyAccountDto
