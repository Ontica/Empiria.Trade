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


    public string Description {
      get; set;
    } = string.Empty;


    public ProductType ProductType {
      get; internal set;
    } = new ProductType();


    public FixedList<Presentation> Presentations {
      get; set;
    } = new FixedList<Presentation>();


    //public FixedList<PriceListOfProduct> PriceList {
    //  get; internal set;
    //} = new FixedList<PriceListOfProduct>();


  } // class ProductShortEntryDto


} // namespace Empiria.Trade.Products.Adapters
