using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml; //here we go
using System.Xml.Linq; //let's try this one
using System.Xml.XPath; //would it be easier to navigate the documents and loop through each node? 

namespace XMLMealCountGenerator
{
    public static class ParseXml
    {



        public static List<Classroom> getEnrollment()
        {
            //init variables
            String path = System.IO.Directory.GetCurrentDirectory();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path + "\\raw.xml");
            XmlNamespaceManager nsmanager = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmanager.AddNamespace("x", "urn:crystal-reports:schemas:report-detail");

            List<Classroom> enrollment = new List<Classroom>();

            XmlNodeList groups = xmlDoc.SelectNodes("x:CrystalReport/x:Group", nsmanager);
            
            int roomCount = 0;
            foreach (XmlNode groupNode in groups)
            {
                
                XmlNode groupName = groupNode.SelectSingleNode("x:GroupHeader/x:Section/x:Text/x:TextValue", nsmanager);
                //Console.WriteLine("=============================");
                
                enrollment.Add(new Classroom(groupName.InnerText));
                //Console.WriteLine(groupName.InnerText);
                //Console.WriteLine("-----------------------------");
                XmlNodeList childList = groupNode.SelectNodes("x:Details", nsmanager);
                foreach (XmlNode childNode in childList)
                {
                    XmlNode childName = childNode.SelectSingleNode("x:Section/x:Field[@Name='ChildFullName1']/x:Value", nsmanager);
                    XmlNode childStatus = childNode.SelectSingleNode("x:Section/x:Field[@Name='Description1']/x:Value", nsmanager);
                    enrollment[roomCount].addChild(new Child(childName.InnerText, childStatus.InnerText));
                    //Console.WriteLine(childName.InnerText + " " + childStatus.InnerText);
                }
                enrollment[roomCount].sortByCategory();
                roomCount++;
                //Console.WriteLine("=============================");
            }

            return enrollment;

            
            //XmlReader example
            //XmlReader xmlReader = XmlReader.Create("C:\\Users\\Ella\\Desktop\\Meal Count Generator\\raw.xml");

            //while (xmlReader.Read()) {
            //    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Text"))
            //    {
            //        if (xmlReader.HasAttributes && xmlReader.GetAttribute("Name") == "Text2") 
            //            Console.WriteLine(xmlReader.GetAttribute("Name"));
            //    }
            //}
            //Console.WriteLine("End of xmlReader\n");
            //XmlNode example
            //foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes)
            //    Console.WriteLine(node.Name + " " + node.InnerText);
            //Console.WriteLine("End of first XmlNode test\n");

            //XmlDocument raw = new XmlDocument();
            //raw.Load("C:\\Users\\Ella\\Desktop\\Meal Count Generator\\raw.xml");
            //XmlElement rawRoot = raw.DocumentElement;
            //XmlNodeList groupNames = rawRoot.SelectNodes("Text");
            //foreach (XmlNode node in groupNames)
            //{
            //    Console.WriteLine(node.Value);
            //}


            //XPathNavigator nav;
            //XPathDocument docNav;

            //docNav = new XPathDocument(@"C:\Users\Ella\Desktop\Meal Count Generator\raw.xml");
            //nav = docNav.CreateNavigator();
            //nav.MoveToRoot();

            //nav.MoveToFirstChild();
            //do {
            //    if(nav.NodeType == XPathNodeType.Element) {
            //        if(nav.HasChildren == true) {
            //            nav.MoveToFirstChild();
            //            do {
            //            if (nav.HasAttributes == true)
            //            {
            //                nav.MoveToFirstAttribute();
            //                do
            //                {

            //                  Console.WriteLine(nav.Value);
            //                } while (nav.MoveToNext());
            //              }
            //           }while (nav.MoveToNext());
            //        }
            //    }
            //}while (nav.MoveToNext());



            //XmlDocument raw = new XmlDocument();
            //raw.Load("C:\\Users\\Ella\\Desktop\\Meal Count Generator\\raw.xml");


            //XDocument raw = XDocument.Load("C:\\Users\\Ella\\Desktop\\Meal Count Generator\\raw.xml");

            //IEnumerable<XElement> groupList = from el in raw.Descendants("Group") where (string)el.Attribute("SectionNumber") == "0" select el;

            //XmlTextReader reader = new XmlTextReader("C:\\Users\\Ella\\Desktop\\Meal Count Generator\\raw.xml");

            //switch (reader.NodeType)
            //{
            //    case XmlNodeType.Element:
            //        Console.Write("<" + reader.Name);
            //        while(reader.MoveToNextAttribute()){
            //            Console.Write(" " + reader.Name + "='" + reader.Value + "'");
            //        }
            //        Console.WriteLine(">");
            //    break;

            //    case XmlNodeType.Text:
            //        Console.WriteLine(reader.Value);
            //    break;

            //    case XmlNodeType.EndElement:
            //        Console.Write("</" + reader.Name);
            //        Console.WriteLine(">");
            //    break;
            //}
        }
    }
}
