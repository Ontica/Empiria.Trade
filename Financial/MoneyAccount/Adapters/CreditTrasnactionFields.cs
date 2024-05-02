/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Credit Transaction Management              Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Sales.dll                    Pattern   : Information Holder                      *
*  Type     : CreditTransactionFields                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO for credit transactions.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Trade.Financial.Adapters
{

    /// Input DTO for credit transactions.  
    public class CreditTrasnactionFields
    {

        #region Constructors and parsers

        public CreditTrasnactionFields()
        {
            // Required by Empiria Framework.
        }

        #endregion Constructors and parsers

        #region Properties

        public string UID
        {
            get; set;
        } = string.Empty;

        public int TypeId
        {
            get; set;
        }

        public int CustomerId
        {
            get; set;
        }

        public DateTime TransactionTime
        {
            get; set;
        } = new DateTime(2020, 1, 2);

        public decimal CreditAmount
        {
            get; set;
        } = 0m;

        public decimal DebitAmount
        {
            get; set;
        } = 0m;

        public int PayableOrderId
        {
            get; set;
        }

        public DateTime DueDate
        {
            get; set;
        } = new DateTime(2020, 1, 1);

        public int DaysToPay
        {
            get; set;
        }

        public string ExtData
        {
            get; set;
        } = string.Empty;

        public string Notes
        {
            get;
            internal set;
        } = string.Empty;

        #endregion Properties

    } // class CreditTrasnactionFields


} // namespace Empiria.Trade.Sales.Credits.Adapters
