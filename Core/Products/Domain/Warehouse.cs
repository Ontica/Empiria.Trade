/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Partitioned Type / Information Holder   *
*  Type     : Warehouse                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a warehouse.                                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products {

  /// <summary>Represents a warehouse.</summary>
  public class Warehouse : BaseObject {

    #region Constructors and parsers

    internal Warehouse() {

    }


    static public Warehouse Parse(int id) => ParseId<Warehouse>(id);

    static public Warehouse Parse(int id, bool reload) => ParseId<Warehouse>(id, reload);

    static public Warehouse Parse(string uid) => ParseKey<Warehouse>(uid);

    static public Warehouse Empty => ParseEmpty<Warehouse>();


    #endregion Constructors and parsers


    #region Properties



    [DataField("WarehouseUID")]
    public string WarehouseUID {
      get;
      internal set;
    }


    [DataField("WarehouseCode")]
    public string Code {
      get;
      internal set;
    }


    [DataField("WarehouseName")]
    public string Name {
      get;
      internal set;
    }


    [DataField("Description")]
    public string Description {
      get;
      internal set;
    }


    [DataField("OwnerId")]
    public int OwnerId {
      get;
      internal set;
    }


    [DataField("CompanyId")]
    public int CompanyId {
      get;
      internal set;
    }


    #endregion Properties


  } // class Warehouse

} // namespace Empiria.Trade.Products
