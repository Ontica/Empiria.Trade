/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Base controllers                      *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Web Api Controller                    *
*  Type     : SystemController                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods to get and set system configuration settings.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

namespace Empiria.Trade.WebApi
{

    /// <summary> Web api methods to get and set system configuration settings.</summary>
    public class SysController : WebApiController
    {

        #region Public APIs

        /// <summary>Gets the Empiria license name.</summary>
        [HttpGet]
        [Route("v4/trade/license")]
        public SingleObjectModel GetLicense()
        {
            try
            {
                return new SingleObjectModel(Request, ExecutionServer.LicenseName);
            }
            catch (Exception e)
            {
                throw CreateHttpException(e);
            }
        }

        #endregion Public APIs

    }  // class SystemController

}  // namespace Empiria.Trade.Products.WebApi
