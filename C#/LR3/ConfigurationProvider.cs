using System.Collections.Generic;
using System.IO;

namespace Lr2WindowsService
{
    internal class ConfigurationProvider
    {
        private readonly JSONParser jsonObject;
        private readonly XMLParser xmlObject;
        private List<Options> option;

        public ConfigurationProvider()
        {
            jsonObject = new JSONParser();
            xmlObject = new XMLParser();
            option = new List<Options>();
        }

        internal List<Options> GetOption()
        {
            string[] Files = new string[100];
            Files = Directory.GetFiles(@"C:\PTUIR\3 semestr\ISP\Lr3\Lr2WindowsService");
            foreach (string s in Files)
            {

                if (Path.GetExtension(s) == ".json")
                {
                    option = jsonObject.Parse();
                }
                else
                {
                    if (Path.GetExtension(s) == ".xml")
                    {
                        option = xmlObject.Parse();
                    }
                }
            }

            return option;
        }

    }
}
