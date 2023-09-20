/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Interface adapters                      *
*  Assembly : Empiria.Trade.Inventory.dll                Pattern   : Data Transfer Object                    *
*  Type     : ProductDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO used to return Products.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Trade.Inventory.Products.Adapters
{

    /// <summary>Output DTO used to return Products.</summary>
    public class ProductDto
    {


        public FixedList<IProductEntryDto> ProductList
        {
            get; internal set;
        } = new FixedList<IProductEntryDto>();


    } // class ProductsDto


} // namespace Empiria.Trade.Products.Adapters
