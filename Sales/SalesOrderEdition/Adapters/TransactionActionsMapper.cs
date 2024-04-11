/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Order Management                           Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Order.dll                    Pattern   : Mapper class                            *
*  Type     : OrderMapper                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Methods used to map Order transactions actions.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Trade.Sales.Adapters {

  // Methods used to map Order transactions actions.
  internal static class TransactionActionsMapper {

    #region Public methods

    static public TransactionActionsDto Map(TransactionActions transactionActions) {
      var dto = new TransactionActionsDto {
        Can = MapCanDto(transactionActions),
        Show = MapShowDto(transactionActions)
      };
      return dto;
    }


    #endregion Public methods


    #region Private methods

    static private CanDto MapCanDto(TransactionActions transactionActions) {
      var dto = new CanDto {
        Apply = transactionActions.Can.Apply,
        Authorize = transactionActions.Can.Authorize,
        Cancel = transactionActions.Can.Cancel,
        ClosePacking = transactionActions.Can.ClosePacking,
        DeAuthorize = transactionActions.Can.DeAuthorize,
        EditPacking = transactionActions.Can.EditPacking,
        EditShipping = transactionActions.Can.EditShipping,
        SendShipping = transactionActions.Can.SendShipping,
        Update = transactionActions.Can.Update
      };

      return dto;
    }

    static private ShowDto MapShowDto(TransactionActions transactionActions) {
      var dto = new ShowDto {
        OrderData = transactionActions.Show.OrderData,
        CreditData = transactionActions.Show.CreditData,
        PackingData = transactionActions.Show.PackingData,
        ShippingData = transactionActions.Show.ShippingData,
        SendShippingData = transactionActions.Show.SendShippingData
      };

      return dto;
    }

    #endregion Private methods

  } // class TransactionActionsMapper


} // namespace Empiria.Trade.Sales.Adapters
