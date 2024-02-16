/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Core Management                            Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Core.dll                     Pattern   : Partitioned Type / Information Holder   *
*  Type     : Warehouse                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a warehouse.                                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Core.Catalogues
{

    /// <summary>Represents a warehouse.</summary>
    public class Warehouse : BaseObject
    {

        #region Constructors and parsers

        internal Warehouse()
        {

        }


        static public Warehouse Parse(int id) => ParseId<Warehouse>(id);

        static public Warehouse Parse(string uid) => ParseKey<Warehouse>(uid);

        static public Warehouse Empty => ParseEmpty<Warehouse>();


        #endregion Constructors and parsers


        #region Properties



        [DataField("WarehouseUID")]
        public string WarehouseUID
        {
            get; set;
        }


        [DataField("WarehouseCode")]
        public string Code
        {
            get; set;
        }


        [DataField("WarehouseName")]
        public string Name
        {
            get; set;
        }


        [DataField("Description")]
        public string Description
        {
            get; set;
        }


        [DataField("OwnerId")]
        public int OwnerId
        {
            get; set;
        }


        [DataField("CompanyId")]
        public int CompanyId
        {
            get; set;
        }


        public decimal Stock
        {
            get; set;
        }

        #endregion Properties


    } // class Warehouse

} // namespace Empiria.Trade.Core.Catalogues
