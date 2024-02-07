using System;

using Newtonsoft.Json;

using Empiria.Json;
using Empiria.Trade.Core.Common;

namespace Empiria.Trade.Sales.Adapters {
  public class SearchSalesOrderDto {

    
    public SearchOrderFields Query {
      get; internal set;
    }

    public FixedList<DataTableColumn> Columns {
      get; internal set;
    }

   public FixedList<ISalesOrderDto> Entries {
      get; internal set;
   }

  }
}
