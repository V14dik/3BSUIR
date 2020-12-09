using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Lr2WindowsService
{
    internal class JSONParser : IConfigurationParser
    {
        private string jsonString = "";
        private List<Options> optionsAfterJsonParsing = new List<Options>();
        public virtual List<Options> Parse()
        {
            using (var jsonStream = new StreamReader(@"C:\PTUIR\3 semestr\ISP\Lr3\Lr2WindowsService\appsettings.json"))
            {
                jsonString = jsonStream.ReadToEnd();
            }
            Options parametrsAfterJsonParsing = JsonSerializer.Deserialize<Options>(jsonString);

            if (parametrsAfterJsonParsing != null)
            {
                optionsAfterJsonParsing.Add(parametrsAfterJsonParsing);
            }
            else
            {
                throw new NullReferenceException();
            }

            return optionsAfterJsonParsing;
        }
    }
}
