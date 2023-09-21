﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Use cases Layer                         *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Use case interactor class               *
*  Type     : TRDProductUseCases                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases used to build TRDProducts.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using Empiria.Services;
using Empiria.Trade.Inventory.Products.Adapters;
using Empiria.Trade.Inventory.Products.Domain;

namespace Empiria.Trade.Inventory.Products.UseCases {

  /// <summary>Use cases used to build TRDProducts.</summary>
  public class TRDProductUseCases : UseCase {


    #region Constructors and parsers

    protected TRDProductUseCases() {
      // no-op
    }

    static public TRDProductUseCases UseCaseInteractor() {
      return CreateInstance<TRDProductUseCases>();
    }


    #endregion Constructors and parsers


    #region Public methods


    public void AddOrUpdateTRDProduct(TRDProductsEntryDto entry) {

      var builder = new TRDProductBuilder();

      var product = new Product();

      //product = TRDProductMapper.MapToEntry(entry);

      builder.AddOrUpdateTRDProduct(product);
    }


    #endregion Public methods


  } // class TRDProductUseCases

} // namespace Empiria.Trade.Inventory.Products.UseCases
