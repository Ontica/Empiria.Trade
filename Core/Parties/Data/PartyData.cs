/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Party Management                           Component : Data Layer                              *
*  Assembly : Empiria.Trade.Core.dll v                   Pattern   : Data Service                            *
*  Type     : PartyData                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data layer for Partis.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;


namespace Empiria.Trade.Core.Data {

  /// <summary> Provides data layer for Partis.  </summary>
  internal static class PartyData {

    #region Internal methods 

   

    internal static FixedList<Party> GetPartyListByRole(string role, string keywords = "") {
      
      if (keywords != string.Empty) {
        keywords = $"AND {SearchExpression.ParseOrLikeKeywords("PartyKeywords", keywords)} ";
      }

      var sql = "SELECT * " +
                "FROM TRDParties " +
               $"WHERE PartyStatus = 'A' " +
               $"AND PartyRoles like '%{role}%' " +
               $"{keywords} ";
                                   
      var dataOperation = DataOperation.Parse(sql);
      return DataReader.GetFixedList<Party>(dataOperation);      
    }


    #endregion Internal methods


  } // internal class PartyData


} //namespace Empiria.Trade.Core.Data
