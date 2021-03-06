﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Solution  : Empiria Trade                                    System   : Product Data Management           *
*  Namespace : Empiria.Products                                 Assembly : Empiria.Products.dll              *
*  Type      : IProductAppliance                                Pattern  : Loose coupling interface          *
*  Version   : 2.2                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Interface that represents a product appliance.                                                *
*                                                                                                            *
********************************* Copyright (c) 2002-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/

namespace Empiria.Products {

  /// <summary>Interface that represents a product appliance.</summary>
  public interface IProductAppliance : IIdentifiable {

    #region Members definition

    string Name { get; }

    #endregion Members definition

  } // interface IProductAppliance

} // namespace Empiria.Products
