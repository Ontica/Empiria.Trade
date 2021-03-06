﻿/* Empiria Trade *********************************************************************************************
*                                                                                                            *
*  Solution  : Empiria Trade                                    System   : Billing System                    *
*  Namespace : Empiria.Trade.Billing                            Assembly : Empiria.Trade.Ordering.dll        *
*  Type      : BillStamp                                        Pattern  : Information holder                *
*  Version   : 2.2                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a bill fiscal stamp.                                                               *
*                                                                                                            *
********************************* Copyright (c) 2013-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Xml;

namespace Empiria.Trade.Billing {

  /// <summary>Represents a bill fiscal stamp.</summary>
  public class BillStamp {

    private XmlDocument xmlDocument = new System.Xml.XmlDocument();

    public BillStamp() {

    }

    #region Properties


    public string AuthorityCertificateNumber {
      get;
      set;
    }

    public string AuthorityDigitalString {
      get {
        return "||1.0|" + this.UUID + "|" + this.Timestamp.ToString(@"yyyy-MM-dd\THH:mm:ss") + "|" +
              this.AuthorityStamp + "|" + this.AuthorityCertificateNumber + "||";
      }
    }

    public string AuthorityStamp {
      get;
      set;
    }

    public string IssuerStamp {
      get;
      set;
    }

    public byte[] QRCode {
      get;
      set;
    }

    public DateTime Timestamp {
      get;
      set;
    }

    public string UUID {
      get;
      set;
    }

    #endregion Properties

    public XmlDocument GetXmlDocument() {
      return this.xmlDocument;
    }

    #region Methods

    internal BillStamp(WSConecFM.Resultados stampResult) {
      Assertion.AssertObject(stampResult, "stampResult");
      Assertion.Assert(stampResult.status,
                        "Bill stamp external web service call return an error status " +
                        stampResult.code);
      this.QRCode = System.Convert.FromBase64String(stampResult.cbbBase64);
      this.xmlDocument = new System.Xml.XmlDocument();
      byte[] xmlByteArray = System.Convert.FromBase64String(stampResult.xmlBase64);
      this.xmlDocument.LoadXml(System.Text.Encoding.UTF8.GetString(xmlByteArray));
      XmlElement item = (XmlElement) this.xmlDocument.GetElementsByTagName("tfd:TimbreFiscalDigital").Item(0);
      this.UUID = item.GetAttribute("UUID");
      this.AuthorityCertificateNumber = item.GetAttribute("noCertificadoSAT");
      this.AuthorityStamp = item.GetAttribute("selloSAT");
      this.IssuerStamp = item.GetAttribute("selloCFD");
      this.Timestamp = XmlConvert.ToDateTime(item.GetAttribute("FechaTimbrado"),
                                             XmlDateTimeSerializationMode.Utc);
    }

    static internal BillStamp Parse(string json) {
      return Empiria.Json.JsonConverter.ToObject<BillStamp>(json);
    }

    public string ToJson() {
      return Empiria.Json.JsonConverter.ToJson(this);
    }

    #endregion Methods

  }   //internal class BillStamp

}  // namespace Empiria.Trade.Billing
