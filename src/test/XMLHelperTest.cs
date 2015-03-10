using System;
using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// TestFixture for XMLHelper
    /// <seealso cref="XMLHelper"/>
    /// </summary>
    [TestFixture]
    public class XMLHelperTest
    {       
        /// <summary>
        /// Set values required for all tests
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        /// <summary>
        /// Scenario: Test GetXmlDoc with null and empty strings
        /// Expected: xmlString is not specified Exceptions should be raised
        /// </summary>
        [Test]
        public void _001_GetXmlDoc_NullOrEmptyXml()
        {
            Assert.That(delegate { XMLHelper.GetXmlDoc(null, "testXmlString"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testXmlString is not specified"));
            Assert.That(delegate { XMLHelper.GetXmlDoc(string.Empty, "testXmlString"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testXmlString is not specified"));
        }

        /// <summary>
        /// Scenario: Test GetXmlDoc with null and empty strings
        /// Expected: xmlString is not specified Exceptions should be raised
        /// </summary>
        [Test]
        public void _002_GetXmlDoc_InvalidXml()
        {
            Assert.That(delegate { XMLHelper.GetXmlDoc("<ROOT><no end element>", "testXmlString"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testXmlString is not valid"));
        }

        /// <summary>
        /// Scenario: Test GetXmlDoc with validXml
        /// Expected: valid doc is returned
        /// </summary>
        [Test]
        public void _003_GetXmlDoc_ValidXml()
        {
            string validXML = "<ROOT><elem1 id=\"1\"></elem1></ROOT>";
            XmlDocument xmlDoc = XMLHelper.GetXmlDoc(validXML, "testXmlString");
            Assert.That(xmlDoc, Is.Not.Null, "xDoc must not be null");
            Assert.That(xmlDoc.FirstChild.Name, Is.EqualTo("ROOT"), "Root node not as expected");
            Assert.That(xmlDoc.OuterXml, Is.EqualTo(validXML), "Xml toString not as expected");            
        }

        /// <summary>
        /// Scenario: Test ConvertCSVStringToXmlDoc with invalid csv list
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _004_ConvertCSVStringToXmlDoc_InvalidCSVList()
        {
            // null
            Assert.That(delegate { XMLHelper.ConvertCSVStringToXmlDoc(null, string.Empty); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("csvList is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCSVStringToXmlDoc(string.Empty, string.Empty); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("csvList is not specified"));
        }

        /// <summary>
        /// Scenario: Test ConvertCSVStringToXmlDoc with invalid element name
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _005_ConvertCSVStringToXmlDoc_InvalidElementName()
        {
            // null
            Assert.That(delegate { XMLHelper.ConvertCSVStringToXmlDoc("A,B,C", null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCSVStringToXmlDoc("A,B,C", string.Empty); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));            
        }

        /// <summary>
        /// Scenario: Test ConvertCSVStringToXmlDoc with valid params - 1 value in csv list
        /// Expected: valid doc is returned
        /// </summary>
        [Test]
        public void _006_ConvertCSVStringToXmlDoc_ValidParams_Single()
        {
            string csvList = "A";
            XmlDocument doc = XMLHelper.ConvertCSVStringToXmlDoc(csvList, "el");

            XmlNodeList nl = doc.SelectNodes("root/el");

            string[] arr = csvList.Split(',');

            Assert.That(arr.Length, Is.EqualTo(nl.Count), "counts should match");
            for (int i = 0; i < arr.Length; i++)
            {
                XmlNode node = nl[i];
                Assert.That(arr[i], Is.EqualTo(node.InnerText), "innerText should match array");
            }
        }

        /// <summary>
        /// Scenario: Test ConvertCSVStringToXmlDoc with valid params - multiple values in csv list
        /// Expected: valid doc is returned
        /// </summary>
        [Test]
        public void _007_ConvertCSVStringToXmlDoc_ValidParams_Multiple()
        {
            string csvList = "A,B,C";
            XmlDocument doc = XMLHelper.ConvertCSVStringToXmlDoc(csvList, "el");

            XmlNodeList nl = doc.SelectNodes("root/el");

            string[] arr = csvList.Split(',');

            Assert.That(arr.Length, Is.EqualTo(nl.Count), "counts should match");
            for (int i = 0; i < arr.Length; i++)
            {
                XmlNode node = nl[i];
                Assert.That(arr[i], Is.EqualTo(node.InnerText), "innerText should match array");
            }            
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int, string) with invalid rootNode
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _008_ConvertCollectionToXml_int_string_InvalidRootNode()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            Dictionary<int, string> dict2 = new Dictionary<int, string>();
            Dictionary<int, XmlNodeType> dict3 = new Dictionary<int, XmlNodeType>();

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(null, dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(string.Empty, dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));                
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int, string) with invalid data Dictionary parameter
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _009_ConvertCollectionToXml_int_string_InvalidDataDictionary()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            Dictionary<int, string> dict2 = new Dictionary<int, string>();
            Dictionary<int, XmlNodeType> dict3 = new Dictionary<int, XmlNodeType>();

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", null, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("data cannot be null"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("data cannot be empty"));                
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int, string) with invalid nodeNames Dictionary parameter
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _010_ConvertCollectionToXml_int_string_InvalidNodeNamesDictionary()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            Dictionary<int, string> dict2 = new Dictionary<int, string>();
            Dictionary<int, XmlNodeType> dict3 = new Dictionary<int, XmlNodeType>();
            dict1.Add(1, "test");
            dict3.Add(0, XmlNodeType.Attribute);
            dict3.Add(1, XmlNodeType.Attribute);

             // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, null, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeNames cannot be null"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeNames cannot be empty"));                        

            // does not match count            
            dict2.Add(1, "test1");
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeNames count 1 does not match countToMatch 3"));                        

            // invalid int
            dict2.Add(-1, "test");
            dict2.Add(2, "test2");
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeNames can only contain -1, 0 or 1 as the keys"));                        
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int, string) with invalid nodeTypes Dictionary parameter
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _011_ConvertCollectionToXml_int_string_InvalidNodeTypesDictionary()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            Dictionary<int, string> dict2 = new Dictionary<int, string>();
            Dictionary<int, XmlNodeType> dict3 = new Dictionary<int, XmlNodeType>();
            dict1.Add(1, "test");
            dict2.Add(-1, "test");
            dict2.Add(0, "test1");
            dict2.Add(1, "test2");
            
            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeTypes cannot be null"));                        

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeTypes cannot be empty"));                        

            // does not match count            
            dict3.Add(0, XmlNodeType.Attribute);
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeTypes count 1 does not match countToMatch 2"));                        

            // invalid int
            dict3[2] = XmlNodeType.Attribute;
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeTypes can only contain 0 or 1 as the keys"));                        

            // dictionary NodeType not Attribute or element
            dict3.Remove(2);
            dict3.Add(1, XmlNodeType.DocumentFragment);
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("nodeType DocumentFragment is not allowed - only Element or Attribute"));                        
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int, string) for nodes of type Element
        /// Expected: Runs successfully and creates correct Xml
        /// </summary>
        [Test]
        public void _012_ConvertCollectionToXml_int_string_ElementOnly_ValidParam()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            Dictionary<int, string> dict2 = new Dictionary<int, string>();
            Dictionary<int, XmlNodeType> dict3 = new Dictionary<int, XmlNodeType>();
            dict1.Add(1, "testData");
            dict2.Add(-1, "test");
            dict2.Add(0, "testElem1");
            dict2.Add(1, "testElem2");
            dict3.Add(0, XmlNodeType.Element);
            dict3.Add(1, XmlNodeType.Element);

            XmlDocument doc = XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3);

            Assert.That(doc.InnerXml, Is.EqualTo("<root><test><testElem1>1</testElem1><testElem2>testData</testElem2></test></root>"));
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int, string) for nodes of type Attribute
        /// Expected: Runs successfully and creates correct Xml
        /// </summary>
        [Test]
        public void _013_ConvertCollectionToXml_int_string_AttributeOnly_ValidParam()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            Dictionary<int, string> dict2 = new Dictionary<int, string>();
            Dictionary<int, XmlNodeType> dict3 = new Dictionary<int, XmlNodeType>();
            dict1.Add(1, "testData");
            dict2.Add(-1, "test");
            dict2.Add(0, "testAttrib1");
            dict2.Add(1, "testAttrib2");
            dict3.Add(0, XmlNodeType.Attribute);
            dict3.Add(1, XmlNodeType.Attribute);

            XmlDocument doc = XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3);

            Assert.That(doc.InnerXml, Is.EqualTo("<root><test testAttrib1=\"1\" testAttrib2=\"testData\" /></root>"));
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int, string) for nodes of mixed type
        /// Expected: Runs successfully and creates correct Xml
        /// </summary>
        [Test]
        public void _014_ConvertCollectionToXml_int_string_MixedElementAttribute_ValidParam()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            Dictionary<int, string> dict2 = new Dictionary<int, string>();
            Dictionary<int, XmlNodeType> dict3 = new Dictionary<int, XmlNodeType>();            
            dict1.Add(1, "testData");
            dict2.Add(-1, "test");
            dict2.Add(0, "testAttrib");
            dict2.Add(1, "testElem");
            dict3.Add(0, XmlNodeType.Attribute);
            dict3.Add(1, XmlNodeType.Element);

            XmlDocument doc = XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3);

            Assert.That(doc.InnerXml, Is.EqualTo("<root><test testAttrib=\"1\"><testElem>testData</testElem></test></root>"));            
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int, string) with for nodes of mixed type with html encode chars ampersand, gtr, ltr quot
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _015_ConvertCollectionToXml_int_string_MixedElementAttribute_ValidParam_HtmlEncodedChars()
        {
            Dictionary<int, string> dict1 = new Dictionary<int, string>();
            Dictionary<int, string> dict2 = new Dictionary<int, string>();
            Dictionary<int, XmlNodeType> dict3 = new Dictionary<int, XmlNodeType>();
            dict1.Add(1, "testData><&\"");
            dict2.Add(-1, "test");
            dict2.Add(0, "testAttrib");
            dict2.Add(1, "testElem");
            dict3.Add(0, XmlNodeType.Attribute);
            dict3.Add(1, XmlNodeType.Element);

            XmlDocument doc = XMLHelper.ConvertCollectionToXml("root", dict1, dict2, dict3);

            Assert.That(doc.InnerXml, Is.EqualTo("<root><test testAttrib=\"1\"><testElem>testData~~gt;~~lt;~~amp;~~quot;</testElem></test></root>"));
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list int) with invalid rootNode
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _016_ConvertCollectionToXml_list_int_InvalidRootNode()
        {
            List<int> list = new List<int>();           
            
            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(null, list, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));                        

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(string.Empty, list, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));                                    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list int) with invalid data List parameter
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _017_ConvertCollectionToXml_list_int_InvalidDataList()
        {
            List<int> list = new List<int>();           

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("data cannot be empty"));                                    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list int) with invalid elementName
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _018_ConvertCollectionToXml_list_int_InvalidElementName()
        {
            List<int> list = new List<int>();
            list.Add(1);

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, null, "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));                                    
            
            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, string.Empty, "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));                                    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list int) with invalid attributeName
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _019_ConvertCollectionToXml_list_int_InvalidAttributeName()
        {
            List<int> list = new List<int>();
            list.Add(1);

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, "blah", null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attributeName is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, "blah", string.Empty); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attributeName is not specified"));                                    
        }        

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list int) with Valid Params
        /// Expected: Runs successfully and creates correct Xml
        /// </summary>
        [Test]
        public void _020_ConvertCollectionToXml_list_int_ValidParams()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
           
            XmlDocument doc = XMLHelper.ConvertCollectionToXml("root", list, "myelem", "myattrib");
           
            Assert.That(doc.InnerXml, Is.EqualTo("<root><myelem myattrib=\"1\" /><myelem myattrib=\"2\" /><myelem myattrib=\"3\" /></root>"));
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list string) with invalid rootNode
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _021_ConvertCollectionToXml_list_string_InvalidRootNode()
        {
            List<string> list = new List<string>();

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(null, list, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(string.Empty, list, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list string) with invalid data List parameter
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _022_ConvertCollectionToXml_list_string_InvalidDataList()
        {
            List<string> list = new List<string>();

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("data cannot be empty"));
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list string) with invalid elementName
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _023_ConvertCollectionToXml_list_string_InvalidElementName()
        {
            List<string> list = new List<string>();
            list.Add("1");

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, null, "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, string.Empty, "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (list string) with invalid attributeName
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _024_ConvertCollectionToXml_list_string_InvalidAttributeName()
        {
            List<string> list = new List<string>();
            list.Add("1");

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, "blah", null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attributeName is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", list, "blah", string.Empty); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attributeName is not specified"));      
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (string[]) with Valid Params
        /// Expected: Runs successfully and creates correct Xml
        /// </summary>
        [Test]
        public void _025_ConvertCollectionToXml_list_string_ValidParams()
        {
            List<string> list = new List<string>();
            list.Add("1");
            list.Add("2");
            list.Add("3");

            XmlDocument doc = XMLHelper.ConvertCollectionToXml("root", list, "myelem", "myattrib");
            Assert.That(doc.InnerXml, Is.EqualTo("<root><myelem myattrib=\"1\" /><myelem myattrib=\"2\" /><myelem myattrib=\"3\" /></root>"));
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (string[]) with invalid rootNode
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _026_ConvertCollectionToXml_array_string_InvalidRootNode()
        {
            string[] array = { "1", "2", "3" };

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(null, array, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(string.Empty, array, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));                                    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (string[]) with invalid data List parameter
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _027_ConvertCollectionToXml_array_string_InvalidDataList()
        {
            string[] array = { };

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("data cannot be empty"));                                    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (string[]) with invalid elementName
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _028_ConvertCollectionToXml_array_string_InvalidElementName()
        {
            string[] array = { "1", "2", "3" };

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, null, "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, string.Empty, "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));                                    
         }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (string[]) with invalid attributeName
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _029_ConvertCollectionToXml_array_string_InvalidAttributeName()
        {
            string[] array = { "1", "2", "3" };

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, "blah", null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attributeName is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, "blah", string.Empty); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attributeName is not specified"));                                    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (string[]) with Valid Params
        /// Expected: Runs successfully and creates correct Xml
        /// </summary>
        [Test]
        public void _030_ConvertCollectionToXml_array_string_ValidParams()
        {
            string[] array = { "1", "2", "3" };
            XmlDocument doc = XMLHelper.ConvertCollectionToXml("root", array, "myelem", "myattrib");
            Assert.That(doc.InnerXml, Is.EqualTo("<root><myelem myattrib=\"1\" /><myelem myattrib=\"2\" /><myelem myattrib=\"3\" /></root>"));
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int[]) with invalid rootNode
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _031_ConvertCollectionToXml_array_int_InvalidRootNode()
        {
            int[] array = { 1, 2, 3 };

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(null, array, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml(string.Empty, array, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("rootNode is not specified"));                                    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int[]) with invalid data List parameter
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _032_ConvertCollectionToXml_array_int_InvalidDataList()
        {
            int[] array = { };

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, "blah", "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("data cannot be empty"));                                    
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int[]) with invalid elementName
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _033_ConvertCollectionToXml_array_int_InvalidElementName()
        {
            int[] array = { 1, 2, 3 };

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, null, "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, string.Empty, "blah"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("elementName is not specified"));                                          
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int[]) with invalid attributeName
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _034_ConvertCollectionToXml_array_int_InvalidAttributeName()
        {
            int[] array = { 1, 2, 3 };

            // null
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, "blah", null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attributeName is not specified"));

            // empty
            Assert.That(delegate { XMLHelper.ConvertCollectionToXml("root", array, "blah", string.Empty); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attributeName is not specified"));                                          
        }

        /// <summary>
        /// Scenario: Test ConvertCollectionToXml (int[]) with Valid Params
        /// Expected: Runs successfully and creates correct Xml
        /// </summary>
        [Test]
        public void _035_ConvertCollectionToXml_array_int_ValidParams()
        {
            int[] array = { 1, 2, 3 };
            XmlDocument doc = XMLHelper.ConvertCollectionToXml("root", array, "myelem", "myattrib");
            Assert.That(doc.InnerXml, Is.EqualTo("<root><myelem myattrib=\"1\" /><myelem myattrib=\"2\" /><myelem myattrib=\"3\" /></root>"));
        }

        /// <summary>
        /// Scenario: Test CreateDocument
        /// Expected: Runs successfully and creates correct Xml
        /// </summary>
        [Test]
        public void _036_CreateDocument()
        {          
            XmlDocument doc = XMLHelper.CreateDocument("root");
            Assert.That(doc.OuterXml, Is.EqualTo("<root></root>"));
        }

        /// <summary>
        /// Scenario: Call methods with valid arguments
        /// Expected: Valid document
        /// </summary>
        [Test]
        public void _037_GetXmlTextWriter_GetXmlDocument()
        {
            XmlTextWriter xtw = XMLHelper.GetXmlTextWriter("doc");
            xtw.WriteElementString("inner", "value");
            XmlDocument xd = XMLHelper.GetXmlDocument(xtw);

            Assert.That(xd.DocumentElement.Name, Is.EqualTo("doc"));
            Assert.That(xd.SelectSingleNode("/doc/inner"), Is.Not.Null);
            Assert.That(xd.SelectSingleNode("/doc/inner").InnerText, Is.EqualTo("value"));
        }
    }
}