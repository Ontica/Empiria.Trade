﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Solution  : Empiria Trade                                    System   : Ordering System                   *
*  Namespace : Empiria.Trade.Ordering                           Assembly : Empiria.Trade.Ordering.dll        *
*  Type      : StorageUnitKind                                  Pattern  : General Object Type               *
*  Version   : 2.2                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Describes a storage unit kind.                                                                *
*                                                                                                            *
********************************* Copyright (c) 2002-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Trade.Ordering {

  /// <summary>Describes a storage unit kind.</summary>
  public class StorageUnitKind : GeneralObject {

    #region Constructors and parsers

    private StorageUnitKind() {
      // Required by Empiria Framework.
    }

    static public StorageUnitKind Empty {
      get { return BaseObject.ParseEmpty<StorageUnitKind>(); }
    }

    static public StorageUnitKind Parse(int id) {
      return BaseObject.ParseId<StorageUnitKind>(id);
    }

    static public FixedList<StorageUnitKind> GetList() {
      return GeneralObject.GetList<StorageUnitKind>();
    }

    #endregion Constructors and parsers

  } // class StorageUnitKind

} // namespace Empiria.Trade.Ordering
