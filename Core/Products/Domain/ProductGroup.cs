/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Module   : Product Management                         Component : Domain Layer                            *
*  Assembly : Empiria.Trade.Products.dll                 Pattern   : Information Holder                      *
*  Type     : ProductGroup                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a product's group.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Trade.Products.Data;

namespace Empiria.Trade.Products {

  /// <summary>Represents a product's group.</summary>
  public class ProductGroup  {

    #region Constructors and parsers

    internal ProductGroup() {

    }

    static internal FixedList<ProductGroup> GetListFor(int groupId) {
      Assertion.Require(groupId, nameof(groupId));
      
      if (groupId == 0) {
        return new FixedList<ProductGroup>();
      }

      return ProductDataService.GetProductGroups(551, groupId);
    }


    static internal FixedList<ProductGroup> GetListFor(int groupId, int subgroupId) {
      Assertion.Require(groupId, nameof(groupId));
      Assertion.Require(subgroupId, nameof(subgroupId));
      
      if (groupId == 0) {
        return new FixedList<ProductGroup>();
      }

      return ProductDataService.GetProductGroups(553, groupId, subgroupId);
    }

    #endregion Constructors and parsers


    #region Properties

    [DataField("Object_Id")]
    public int Id {
      get;
      set;
    }


    [DataField("Object_UID")]
    public string UID {
      get;
      internal set;
    }


    [DataField("Object_Type_Id")]
    public int ObjectTypeId {
      get; private set;
    }


    [DataField("Object_Classification_Id")]
    public int ObjectClassificationId {
      get; private set;
    }


    [DataField("Object_Name")] 
    public string Name {
      get;
      internal set;
    }


    [DataField("Object_Tags")]
    public string Tags {
      get;
      internal set;
    }


    #endregion Properties


  } // class ProductGroup

} // namespace Empiria.Trade.Products
