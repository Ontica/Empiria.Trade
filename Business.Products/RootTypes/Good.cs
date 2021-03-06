﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Solution  : Empiria Trade                                    System   : Product Data Management           *
*  Namespace : Empiria.Products                                 Assembly : Empiria.Products.dll              *
*  Type      : Good                                             Pattern  : Storage Item                      *
*  Version   : 2.2                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a physical good.                                                                   *
*                                                                                                            *
********************************* Copyright (c) 2002-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Products {

  public sealed class Good : Product {

    #region Constructors and parsers

    private Good(ProductType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    static public new Good Parse(int id) {
      return BaseObject.ParseId<Good>(id);
    }

    #endregion Constructors and parsers

  } // class Good

} // namespace Empiria.Products
