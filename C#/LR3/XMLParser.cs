using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Lr2WindowsService
{
    internal class XMLParser : IConfigurationParser
    {
        public virtual List<Options> Parse()
        {
            string pathToXmlFile = @"C:\PTUIR\3 semestr\ISP\Lr3\Lr2WindowsService\config.xml";
            List<Options> xmlOptionsAfterParsing = new List<Options>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Options));
            Options parametrsAfterXmlParsing = new Options();

            using (var xmlRead = new FileStream(pathToXmlFile, FileMode.OpenOrCreate))
            {
                parametrsAfterXmlParsing = (Options)xmlSerializer.Deserialize(xmlRead);
            }

            if (parametrsAfterXmlParsing != null)
            {
                xmlOptionsAfterParsing.Add(parametrsAfterXmlParsing);
            }
            else
            {
                throw new NullReferenceException();
            }

            return xmlOptionsAfterParsing;
        }

    }
}
