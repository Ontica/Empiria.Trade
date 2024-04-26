/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : MoneyAccount Management                    Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Financial.dll                Pattern   : Mapper class                            *
*  Type     : MoneyAccountMapper                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Search MoneyAccounts.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Financial.Adapters {
  /// Methods used to map Search MoneyAccounts. 
  public class SearchMoneyAccountMapper {

    #region Public methods

    static internal SearchMoneyAccountDto Map(SearchMoneyAccountFields query, FixedList<MoneyAccount> moneyAccounts) {
      return new SearchMoneyAccountDto {
        Query = query,
        Columns = DataColumns(),
        Entries = MapMoneyAccounts(moneyAccounts)
      };
    }

    static private FixedList<DataTableColumn> DataColumns() {
      List<DataTableColumn> columns = new List<DataTableColumn>();

      columns.Add(new DataTableColumn("moneyAccountType", "Tipo de cuenta", "text"));
      columns.Add(new DataTableColumn("moneyAccountNumber", "Número", "text-link"));
      columns.Add(new DataTableColumn("owner", "Cliente", "text"));
      columns.Add(new DataTableColumn("balance", "Saldo", "decimal"));
      columns.Add(new DataTableColumn("status", "Estatus", "text-tag"));

      return columns.ToFixedList();
    }

    static private FixedList<SearchMoneyAccountEntriesDto> MapMoneyAccounts(FixedList<MoneyAccount> moneyAccounts) {

      List<SearchMoneyAccountEntriesDto> moneyAccountDtoList = new List<SearchMoneyAccountEntriesDto>();

      foreach (var moneyAccount in moneyAccounts) {
        moneyAccountDtoList.Add(MapMoneyAccount(moneyAccount));
      }

      return moneyAccountDtoList.ToFixedList();
    }

    static private SearchMoneyAccountEntriesDto MapMoneyAccount(MoneyAccount moneyAccount) {
      var dto = new SearchMoneyAccountEntriesDto {
        UID = moneyAccount.UID,
        MoneyAccountType = moneyAccount.MoneyAccountType.Name,
        MoneyAccountNumber = moneyAccount.Number,
        Owner = moneyAccount.Owner.Name,
        Balance = GetBalance(moneyAccount.Owner.Id),
        Status = "Activo"
      };

      return dto;
    }


    #endregion Public methods

    #region Private methods

    static private decimal GetBalance(int ownerId) {
      return CreditTransaction.GetCustomerTotalDebt(ownerId);
    
    }

    #endregion Private methods

  } // class SearchMoneyAccountMapper

} // namespace Empiria.Trade.Financial.Adapters
