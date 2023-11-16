/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Packaging Management                       Component : Domain Layer                            *
*  Assembly : Empiria.Trade.ShippingAndHandling.dll      Pattern   : Partitioned Type / Information Holder   *
*  Type     : OrderShippingBuilder                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Generate data for shipping and handling.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.ShippingAndHandling.Domain {


  /// <summary>Generate data for shipping and handling.</summary>
  internal class ShippingAndHandlingBuilder {


    #region Constructor

    public ShippingAndHandlingBuilder() {

    }


    #endregion Constructor


    #region Public methods


    internal FixedList<PackagingOrder> CreateOrderPackaging() {



      return new FixedList<PackagingOrder>();
    }


    #endregion Public methods


  } // class ShippingAndHandlingBuilder

} // namespace Empiria.Trade.ShippingAndHandling.Domain
