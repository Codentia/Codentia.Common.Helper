using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Codentia.Common.Helper
{
    /// <summary>
    /// Class for assisting with XML functionality
    /// </summary>
    public static class XMLHelper
    {        
        //// <summary>
        //// Get a NodeList From an XmlDoc for a specified xPath
        //// </summary>
        //// <param name="xDoc">Xml Document</param>
        //// <param name="xPath">XPath Query</param>
        //// <returns>XmlNodeList</returns>
        //// public static XmlNodeList GetNodeListFromXmlDoc(XmlDocument xDoc, string xPath)
        //// {
        ////    XmlNodeList xNodes = null;

        ////    if (xDoc != null)
        ////    {
        ////        try
        ////        {
        ////            xNodes = xDoc.SelectNodes(xPath);
        ////        }

        ////        catch (Exception ex)
        ////        {
        ////            ////log here
        ////            throw new Exception("Invalid xPath", ex);
        ////        }
        ////    }

        ////    return xNodes;
        //// }

        //// <summary>
        //// Get a single Node From an XmlDoc for a specified xPath
        //// </summary>
        //// <param name="xDoc">Xml Document</param>
        //// <param name="xPath">XPath Query</param>
        //// <returns>XmlNode</returns>
        //// public static XmlNode GetNodeFromXmlDoc(XmlDocument xDoc, string xPath)
        //// {
        ////    XmlNode xNode = null;

        ////    if (xDoc != null)
        ////    {
        ////        try
        ////        {
        ////            xNode = xDoc.SelectSingleNode(xPath);
        ////        }

        ////        catch (Exception ex)
        ////        {
        ////            ////log here
        ////            throw new Exception("Invalid xPath", ex);
        ////        }
        ////    }

        ////    return xNode;
        //// }

        //// <summary>
        //// Get inner Xml for the XPath query with the xmlString
        //// </summary>
        //// <param name="xmlString">Xml String</param>
        //// <param name="xPath">XPath Query</param>
        //// <returns>string</returns>
        //// public static string GetInnerXmlFromXmlString(string xmlString, string xPath)
        ////{
        ////    XmlDocument xDoc= GetXmlDoc(xmlString);                        
        ////    XmlNodeList xnl = XMLHelper.GetNodeListFromXmlDoc(xDoc, xPath);

        ////    string innerX = string.Empty;
        ////    if (xnl != null)
        ////    {
        ////        if (xnl.Count > 0)
        ////        {
        ////            innerX = xnl[0].InnerXml;
        ////        }
        ////    }

        ////    return innerX;
        //// }

        //// <summary>
        //// Get the root node string for a given xml
        //// Note it must be in the format of a root node
        //// </summary>
        //// <param name="xmlString">Xml String</param>
        //// <returns>string</returns>
        ////public static string GetRootNodeFromXmlString(string xmlString)
        ////{
        ////    XmlDocument xDoc = GetXmlDoc(xmlString);
        ////    return xDoc.FirstChild.Name;
        ////}

        //// <summary>
        //// Get a NodeList From a Node for a specified xPath
        //// </summary>
        //// <param name="node">Xml Node</param>
        //// <param name="xPath">XPath Query</param>
        //// <returns>XmlNodeList</returns>
        //// public static XmlNodeList GetNodeListFromNode(XmlNode node, string xPath)
        //// {
        //// XmlNodeList xNodes = null;

        //// if (node != null)
        //// {
        //// try
        //// {
        ////            xNodes = node.SelectNodes(xPath);
        ////        }

        ////        catch (Exception ex)
        ////        {
        ////           //// log here
        ////            throw new Exception("Invalid xPath", ex);
        ////        }
        ////    }

        ////    return xNodes;
        ////}

        /// <summary>
        /// Get an Xml Doc for a specified xmlString
        /// </summary>
        /// <param name="xmlString">String representing an Xml Document</param>   
        /// <param name="stringName">string name (for exception message)</param>  
        /// <returns>XmlDocument after conversion</returns>
        public static XmlDocument GetXmlDoc(string xmlString, string stringName)
        {
            ParameterCheckHelper.CheckIsValidString(xmlString, stringName, false);

            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.LoadXml(xmlString);
            }
            catch (Exception ex)
            {
                // log here
                throw new ArgumentException(string.Format("{0} is not valid", stringName), ex);
            }           

            return xmlDoc;
        }

        /// <summary>
        /// Creates an Xml document with a specified root node
        /// </summary>
        /// <param name="rootNodeName">Name of the root node.</param>
        /// <returns>XmlDocument after creation</returns>
        public static XmlDocument CreateDocument(string rootNodeName)
        {
            ParameterCheckHelper.CheckIsValidString("rootNodeName", rootNodeName, false);

            XmlDocument xmlDoc = new XmlDocument();
            string rootNodeXml = string.Format("<{0}></{0}>", rootNodeName);
            xmlDoc.LoadXml(rootNodeXml);

            return xmlDoc;
        }

        /// <summary>
        /// Return a comma delimited list as an xml doc with root as root node and each element with elementname
        /// </summary>
        /// <param name="csvList">Comma delimited list</param>
        /// <param name="elementName">element name to give each value</param>
        /// <returns>XmlDocument after conversion</returns>
        public static XmlDocument ConvertCSVStringToXmlDoc(string csvList, string elementName)
        {
            ParameterCheckHelper.CheckIsValidString(csvList, "csvList", false);
            ParameterCheckHelper.CheckIsValidString(elementName, "elementName", false);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<root></root>");

            XmlElement root = xml.DocumentElement;

            string[] arr = csvList.Split(',');

            for (int i = 0; i < arr.Length; i++)
            {
                XmlNode node = xml.CreateNode("element", elementName, string.Empty);
                node.InnerText = arr[i];
                root.AppendChild(node);
            }

            return xml;
        }

        /// <summary>
        /// Check an Xml string contains 
        /// </summary>
        /// <param name="node">an xml node with child nodes to check</param>   
        /// <param name="stringName">string name (for exception message)</param>
        /// <param name="attributesToCheck">string array of attributes to check</param>
        public static void CheckAttributesInXmlNodeChildren(XmlNode node, string stringName, string[] attributesToCheck)
        {
            if (node.ChildNodes.Count == 0)
            {
                throw new ArgumentException("node does not have any child nodes");
            }

            for (int i = 0; i < attributesToCheck.Length; i++)
            {
                ParameterCheckHelper.CheckIsValidString(attributesToCheck[i], string.Format("attribute {0}", i), false);
            }

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                XmlNode currentNode = node.ChildNodes[i];
                for (int j = 0; j < attributesToCheck.Length; j++)
                {
                    try
                    {
                        string value = currentNode.Attributes[attributesToCheck[j]].Value;
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("Required attribute(s) are missing from node", ex);
                    }
                }
            }
        }
       
        /// <summary>
        /// Convert A Collection of data to Xml (Dictionary int string version)
        /// </summary>
        /// <param name="rootNode">Root Node Name</param>
        /// <param name="data">actual data </param>
        /// <param name="nodeNames">int -1, 0, 2 -- -1=element node, 0,1 =position of the node in the data parameter) string=nodeName</param>
        /// <param name="nodeTypes">int -1, 0, 2 -- -1=element node, 0,1= position of the node in the data parameter) XmlNodeType=Attribute or Element (no other types allowed)</param>
        /// <returns>XmlDocument after conversion</returns>
        public static XmlDocument ConvertCollectionToXml(string rootNode, Dictionary<int, string> data, Dictionary<int, string> nodeNames,  Dictionary<int, XmlNodeType> nodeTypes)
        {            
            ParameterCheckHelper.CheckIsValidString(rootNode, "rootNode", false);
            ParameterCheckHelper.CheckICollectionIsNotNullOrEmpty(data, "data");
            ParameterCheckHelper.CheckICollectionCount(3, nodeNames, "nodeNames");
            ParameterCheckHelper.CheckICollectionCount(2, nodeTypes, "nodeTypes");

            // check nodeNames
            for (int i = -1; i <= 1; i++)
            {
                try
                {
                    string nodeName = nodeNames[i];
                }
                catch
                {
                    throw new ArgumentException("nodeNames can only contain -1, 0 or 1 as the keys");
                }
            }

            // check nodeTypes
            for (int i = 0; i <= 1; i++)
            {
                XmlNodeType nodeType;

                try
                {
                    nodeType = nodeTypes[i];
                }
                catch
                {
                    throw new ArgumentException("nodeTypes can only contain 0 or 1 as the keys");
                }
                
                if (nodeType != XmlNodeType.Element && nodeType != XmlNodeType.Attribute)
                {
                    throw new ArgumentException(string.Format("nodeType {0} is not allowed - only Element or Attribute", nodeType.ToString()));
                }               
            }    
      
            // add -1 Element
            nodeTypes.Add(-1, XmlNodeType.Element);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(string.Format("<{0}></{0}>", rootNode));
            XmlElement root = xmlDoc.DocumentElement;
            string mainElementName = nodeNames[-1];

            IEnumerator<int> ie = data.Keys.GetEnumerator();

            while (ie.MoveNext())
            {
                XmlNode node = CreateElementNode(xmlDoc, mainElementName);

                for (int j = 0; j <= 1; j++)
                {
                    string nodeValue = data[ie.Current];
                    if (j == 0)
                    {
                        nodeValue = Convert.ToString(ie.Current);
                    }
                    else
                    {
                        nodeValue = StringHelper.EncodeForSavingToSql(data[ie.Current]);
                    }

                    string nodeChildName = nodeNames[j];

                    switch (nodeTypes[j])
                    {
                        case XmlNodeType.Element: AppendElementChildNode(xmlDoc, node, nodeChildName, nodeValue);                           
                                                  break;
                        case XmlNodeType.Attribute: AppendAttributeNode(xmlDoc, node, nodeChildName, nodeValue);                            
                                                  break;
                    }

                    root.AppendChild(node);
                }
            }

            return xmlDoc;
        }

        /// <summary>
        /// Convert A Collection of data to Xml (List int version)
        /// </summary>
        /// <param name="rootNode">Root Node Name</param>
        /// <param name="data">actual data </param>
        /// <param name="elementName">element name</param>
        /// <param name="attributeName">attribute name</param>
        /// <returns>XmlDocument after conversion</returns>
        public static XmlDocument ConvertCollectionToXml(string rootNode, List<int> data, string elementName, string attributeName)
        {
            ParameterCheckHelper.CheckIsValidString(rootNode, "rootNode", false);
            ParameterCheckHelper.CheckICollectionIsNotNullOrEmpty(data, "data");
            ParameterCheckHelper.CheckIsValidString(elementName, "elementName", false);
            ParameterCheckHelper.CheckIsValidString(attributeName, "attributeName", false);

            Dictionary<int, string> names = CreateNamesDictionary(elementName, attributeName);            
            List<string> stringList = new List<string>();
            IEnumerator ie = data.GetEnumerator();

            while (ie.MoveNext())
            {
                stringList.Add(Convert.ToString(ie.Current));
            }

            return ConvertListToXml(rootNode, stringList, names);
        }

        /// <summary>
        /// Convert A Collection of data to Xml (List string version)
        /// </summary>
        /// <param name="rootNode">Root Node Name</param>
        /// <param name="data">actual data </param>
        /// <param name="elementName">element name</param>
        /// <param name="attributeName">attribute name</param>
        /// <returns>XmlDocument after conversion</returns>
        public static XmlDocument ConvertCollectionToXml(string rootNode, List<string> data, string elementName, string attributeName)
        {
            ParameterCheckHelper.CheckIsValidString(rootNode, "rootNode", false);
            ParameterCheckHelper.CheckICollectionIsNotNullOrEmpty(data, "data");
            ParameterCheckHelper.CheckIsValidString(elementName, "elementName", false);
            ParameterCheckHelper.CheckIsValidString(attributeName, "attributeName", false);

            Dictionary<int, string> names = CreateNamesDictionary(elementName, attributeName);              
            return ConvertListToXml(rootNode, data, names);
        }

        /// <summary>
        /// Convert A Collection of data to Xml (string[] version)
        /// </summary>
        /// <param name="rootNode">Root Node Name</param>
        /// <param name="data">actual data </param>
        /// <param name="elementName">element name</param>
        /// <param name="attributeName">attribute name</param>
        /// <returns>XmlDocument after conversion</returns>
        public static XmlDocument ConvertCollectionToXml(string rootNode, string[] data, string elementName, string attributeName)
        {
            ParameterCheckHelper.CheckIsValidString(rootNode, "rootNode", false);
            ParameterCheckHelper.CheckICollectionIsNotNullOrEmpty(data, "data");
            ParameterCheckHelper.CheckIsValidString(elementName, "elementName", false);
            ParameterCheckHelper.CheckIsValidString(attributeName, "attributeName", false);

            Dictionary<int, string> names = CreateNamesDictionary(elementName, attributeName);
            List<string> stringList = new List<string>();

            for (int i = 0; i < data.Length; i++)
            {
                stringList.Add(data[i]);
            }

            return ConvertListToXml(rootNode, stringList, names);
        }

        /// <summary>
        /// Convert A Collection of data to Xml (int[] version)
        /// </summary>
        /// <param name="rootNode">Root Node Name</param>
        /// <param name="data">actual data </param>
        /// <param name="elementName">element name</param>
        /// <param name="attributeName">attribute name</param>
        /// <returns>XmlDocument after conversion</returns>
        public static XmlDocument ConvertCollectionToXml(string rootNode, int[] data, string elementName, string attributeName)
        {
            ParameterCheckHelper.CheckIsValidString(rootNode, "rootNode", false);
            ParameterCheckHelper.CheckICollectionIsNotNullOrEmpty(data, "data");
            ParameterCheckHelper.CheckIsValidString(elementName, "elementName", false);
            ParameterCheckHelper.CheckIsValidString(attributeName, "attributeName", false);

            Dictionary<int, string> names = CreateNamesDictionary(elementName, attributeName);
            List<string> stringList = new List<string>();

            for (int i = 0; i < data.Length; i++)
            {
                stringList.Add(Convert.ToString(data[i]));
            }

            return ConvertListToXml(rootNode, stringList, names);
        }

        /// <summary>
        /// Create Element Node
        /// </summary>
        /// <param name="xmlDoc">Xml Document to create node in</param>
        /// <param name="elementName">Name of Element</param>
        /// <returns>An XmlNode</returns>
        public static XmlNode CreateElementNode(XmlDocument xmlDoc, string elementName)
        {
            ParameterCheckHelper.CheckIsValidString(elementName, "elementName", false);
            XmlNode node = xmlDoc.CreateNode("element", elementName, string.Empty);
            return node;
        }

        /// <summary>
        /// Add an element node with a value
        /// </summary>
        /// <param name="xmlDoc">Xml Document to create node in</param>
        /// <param name="parentNode">Parent Node</param>
        /// <param name="elementName">Name of element</param>
        /// <param name="elementValue">Value of element</param>
        public static void AppendElementChildNode(XmlDocument xmlDoc, XmlNode parentNode, string elementName, string elementValue)
        {
            ParameterCheckHelper.CheckIsValidString(elementName, "elementName", false);
            XmlNode nodeChild = xmlDoc.CreateElement(elementName);
            nodeChild.InnerXml = elementValue;
            parentNode.AppendChild(nodeChild);
        }

        /// <summary>
        /// Add an attribute node with a value
        /// </summary>
        /// <param name="xmlDoc">Xml Document to create node in</param>
        /// <param name="parentNode">Parent Node</param>
        /// <param name="attributeName">Name of attribute</param>
        /// <param name="attributeValue">Value of attribute</param>
        public static void AppendAttributeNode(XmlDocument xmlDoc, XmlNode parentNode, string attributeName, string attributeValue)
        {
            ParameterCheckHelper.CheckIsValidString(attributeName, "attributeName", false);
            XmlAttribute xa = (XmlAttribute)xmlDoc.CreateNode("attribute", attributeName, string.Empty);
            xa.Value = attributeValue;
            parentNode.Attributes.Append(xa);            
        }

        /// <summary>
        /// Gets the XML text writer.
        /// </summary>
        /// <param name="openingTag">The opening tag.</param>
        /// <returns>XmlTextWriter for writing</returns>
        public static XmlTextWriter GetXmlTextWriter(string openingTag)
        {
            MemoryStream outputStream = new MemoryStream();
            XmlTextWriter xmlOut = new XmlTextWriter(outputStream, Encoding.Default);

            xmlOut.WriteStartDocument();

            xmlOut.WriteStartElement(openingTag);

            return xmlOut;
        }

        /// <summary>
        /// Gets the XML document.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <returns>XmlDocument from XmlTextWriter</returns>
        public static XmlDocument GetXmlDocument(XmlTextWriter writer)
        {
            XmlDocument result = new XmlDocument();

            writer.WriteEndDocument();

            writer.Flush();

            writer.BaseStream.Seek(0, SeekOrigin.Begin);

            result.Load(writer.BaseStream);

            writer.BaseStream.Close();
            writer.BaseStream.Dispose();
            writer.Close();

            return result;
        }   

        /// <summary>
        /// Convert A List of string data to Xml
        /// </summary>
        /// <param name="rootNode">Root Node Name</param>
        /// <param name="data">actual data </param>
        /// <param name="nodeNames">int -1, 0 -- -1=element node, 0 =position of the node in the data parameter) string=nodeName</param>
        /// <returns>An XmlDocument</returns>
        private static XmlDocument ConvertListToXml(string rootNode, List<string> data, Dictionary<int, string> nodeNames)
        {          
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(string.Format("<{0}></{0}>", rootNode));
            XmlElement root = xmlDoc.DocumentElement;

            IEnumerator<string> ie = data.GetEnumerator();

            while (ie.MoveNext())
            {
                string nodeValue = Convert.ToString(ie.Current);

                XmlNode node = CreateElementNode(xmlDoc, nodeNames[-1]);

                string nodeChildName = nodeNames[0];
                AppendAttributeNode(xmlDoc, node, nodeChildName, nodeValue);

                root.AppendChild(node);
            }

            return xmlDoc;
        }

        private static Dictionary<int, string> CreateNamesDictionary(string elementName, string attributeName)
        {
            Dictionary<int, string> names = new Dictionary<int, string>();
            names.Add(-1, elementName);
            names.Add(0, attributeName);
            return names;
        }
    }
}
