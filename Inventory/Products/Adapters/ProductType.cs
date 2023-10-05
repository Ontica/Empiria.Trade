/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ProductType                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Object used to return the type of Products.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Products.Adapters {

  /// <summary>Object used to return the type of Products.</summary>
  public class ProductType {


    public string ProductTypeUID {
      get; set;
    }


    public string Name {
      get; set;
    }


    public FixedList<Attributes> Attributes {
      get; internal set;
    } = new FixedList<Attributes>();


  } // class ProductType


  public class AttributesList {

    public FixedList<Attributes> Attributes {
      get; internal set;
    } = new FixedList<Attributes>();

  }


  public class Attributes {

    public string Name {
      get; set;
    } = string.Empty;


    public string Value {
      get; set;
    } = string.Empty;


  }


  public class Presentation {

    public string PresentationUID {
      get; set;
    } = string.Empty;


    public string Description {
      get;
      internal set;
    }


    public string Units {
      get;
      internal set;
    }


    public FixedList<Vendor> Vendors {
      get;
      internal set;
    } = new FixedList<Vendor>();

  } // class Presentation


  public class Vendor {


    public string VendorUID {
      get; set;
    } = string.Empty;


    public string VendorName {
      get;
      internal set;
    }


    public string Sku {
      get;
      internal set;
    }


    public string Stock {
      get;
      internal set;
    }


    public decimal Price {
      get;
      internal set;
    }


  } // class Vendor


} // namespace Empiria.Trade.Products.Adapters
