/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ProductForSearchingDto                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return the entries of products for searching.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Trade.Products.Adapters {

  public class ProductForSearchingDto : ProductDto, IProductEntryDto {


    public FixedList<ProductPresentationForSeach> Presentations {
      get; set;
    } = new FixedList<ProductPresentationForSeach>();


    public string ProductImageUrl {
      get;
      internal set;
    }


  } // class ProductShortEntryDto


  public class ProductPresentationForSeach : ProductPresentationDto {


    public List<VendorDto> Vendors {
      get; set;
    } = new List<VendorDto>();


  } // class ProductPresentationForSeach


} // namespace Empiria.Trade.Products.Adapters
