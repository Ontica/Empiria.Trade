using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empiria.Trade.Products.Adapters {

  public class BaseProductDto {
    public string ProductUID {
      get; set;
    }

    public string ProductCode {
      get; set;
    }

    public string Description {
      get; set;
    }

    public BaseProductTypeDto ProductType {
      get; set;
    }

  } // class BaseProductDto

  public class VendorDto {


    public string VendorProductUID {
      get; set;
    } = string.Empty;


    public string VendorUID {
      get; set;
    } = string.Empty;


    public string VendorName {
      get; set;
    } = string.Empty;


    public string Sku {
      get; set;
    }


    public decimal Stock {
      get; set;
    }


    public decimal Price {
      get; set;
    }


  } // class VendorDto

  public class PresentationDto {

    public string PresentationUID {
      get; set;
    } = string.Empty;


    public string Description {
      get; set;
    }

    public decimal Units {
      get; set;
    }

  } // class PresentationDto



  public class BaseProductTypeDto {


    public string ProductTypeUID {
      get; set;
    }


    public string Name {
      get; set;
    }


    public FixedList<AttributesDto> Attributes {
      get;  set;
    } = new FixedList<AttributesDto>();


  } // class ProductType

  public class AttributesListDto {

    public FixedList<AttributesDto> Attributes {
      get; internal set;
    } = new FixedList<AttributesDto>();

  } // AttributesListDto

  public class AttributesDto {

    public string Name {
      get; set;
    } = string.Empty;


    public string Value {
      get; set;
    } = string.Empty;


  } // class AttributesDto

}
