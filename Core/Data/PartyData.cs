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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Empiria.Data;
using Empiria.Trade.Core.Domain;

namespace Empiria.Trade.Core.Data {

  /// <summary> Provides data layer for Partis.  </summary>
  internal static class PartyData {

    #region Internal methods 

    internal static Party GetParty(int id) {
      var sql = "SELECT PartyId, PartyUID, PartyName, PartyShortName, PartyAddressLine1,PartyAddressLine2,PartyLocationId, " +
                "PartyZipCode, PartyEMail, PartyPhoneNumbers,PartyContacts,PartyTaxationID,PartyKeywords,PartyExtData, " +
                "PartyStatus " +
                "FROM TRDParties " +
                $"WHERE PartyId  = {id} ";
      
      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObject<Party>(dataOperation);
    }

    internal static Party GetParty(string uid) {
      var sql = "SELECT PartyId, PartyUID, PartyName, PartyShortName, PartyAddressLine1,PartyAddressLine2,PartyLocationId, " +
                "PartyZipCode, PartyEMail, PartyPhoneNumbers,PartyContacts,PartyTaxationID,PartyKeywords,PartyExtData, " +
                "PartyStatus " +
                "FROM TRDParties " +
               $"WHERE PartyUID  = '{uid}' ";
     

      var dataOperation = DataOperation.Parse(sql);

      return DataReader.GetPlainObject<Party>(dataOperation);
    }
    
    #endregion Internal methods


  } // internal class PartyData


} //namespace Empiria.Trade.Core.Data
