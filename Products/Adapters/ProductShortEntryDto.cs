/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ProductEntryDto                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of Products.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Products.Adapters {

  public class ProductShortEntryDto : IProductEntryDto {


    public string ProductUID {
      get; set;
    } = string.Empty;


    public string Code {
      get; set;
    } = string.Empty;


    public string Group {
      get; internal set;
    }


    public string Subgroup {
      get; internal set;
    }


    public string Description {
      get; set;
    } = string.Empty;


    public ProductAttributes Attributes {
      get; set;
    } = new ProductAttributes();


    public Presentation Presentations {
      get; set;
    } = new Presentation();


    //public int StoreId {
    //  get; set;
    //}


    //public string ProdServCode {
    //  get; set;
    //} = string.Empty;


    //public string Stock {
    //  get; set;
    //} = string.Empty;


    //public string SalesUnit {
    //  get; set;
    //} = string.Empty;


    //public string Packing {
    //  get; set;
    //} = string.Empty;


    //public string SupplierName {
    //  get; set;
    //} = string.Empty;


    //public FixedList<PriceListOfProduct> PriceList {
    //  get; internal set;
    //} = new FixedList<PriceListOfProduct>();


  } // class ProductShortEntryDto


} // namespace Empiria.Trade.Products.Adapters
