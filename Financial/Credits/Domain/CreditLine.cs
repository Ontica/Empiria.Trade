using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Empiria.StateEnums;

namespace Empiria.Trade.Financial {

  public class CreditLine : BaseObject {

    #region Constructors and parsers

    public CreditLine() {
      //no-op
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("CreditLineTypeId")]
    public int CreditLineTypeId {
      get; private set;
    }

    [DataField("CustomerId")]
    public int CustomerId {
      get; private set;
    }

    [DataField("SellerId")]
    public int SellerId {
      get; private set;
    }

    [DataField("CreditLimit")]
    public decimal CreditLimit {
      get; private set;
    }

    [DataField("CreditConditions")]
    public string CreditConditions {
      get; private set;
    }

    [DataField("InitialDebt")]
    public decimal InitialDebt {
      get; private set;
    }

    [DataField("CreditLineNotes")]
    public string CreditLineNotes {
      get; private set;
    }

    [DataField("CreditLineExtData")]
    public string CreditLineExtData {
      get; private set;
    }

    [DataField("CreditLineStatus", Default = 'A')]
    public char CreditLineStatus {
      get; private set;
    }

    #endregion

  } //class CreditLine

} // namespace Empiria.Trade.Financial
