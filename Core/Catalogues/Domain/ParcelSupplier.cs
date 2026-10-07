using System;

namespace Empiria.Trade.Core {
  
  /// <summary></summary>
  public class ParcelSupplier : CommonStorage {

    #region Constructor and parsers

    public ParcelSupplier() {
      //no-op
    }

    static public ParcelSupplier Parse(int id) => ParseId<ParcelSupplier>(id);

    static public ParcelSupplier Parse(string uid) => ParseKey<ParcelSupplier>(uid);

    static public ParcelSupplier Empty => ParseEmpty<ParcelSupplier>();

    static public FixedList<ParcelSupplier> GetList() {

      return CommonStorage.GetList<ParcelSupplier>().ToFixedList();
    }

    #endregion Constructor and parsers

  } // class ParcelSupplier

} // namespace Empiria.Trade.Core
