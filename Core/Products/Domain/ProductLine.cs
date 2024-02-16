/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : ProductLine                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a category of products.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

namespace Empiria.Trade.Products
{

    public class ProductLine
    {

        [DataField("ProductLineName")]
        internal string Name
        {
            get; private set;
        }


        [DataField("ProductLineDescription")]
        internal string Description
        {
            get; private set;
        }


        [DataField("ParentProductLine")]
        internal string ParentLine
        {
            get; private set;
        }


        internal string Keywords
        {
            get
            {
                return EmpiriaString.BuildKeywords(Name, Description);
            }
        }


        internal EntityStatus Status
        {
            get; private set;
        }


    }  // class ProductLine

}  // namespace Empiria.Trade.Products
