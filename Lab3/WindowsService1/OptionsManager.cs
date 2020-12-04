using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WindowsService1
{
    class OptionsManager
    {
        public enum OptionsSource
        {
            NONE,
            XML,
            JSON
        }

        public OptionsSource optionsSource { get; private set; } = OptionsSource.NONE;

        public OptionsManager(string path)
        {


            JSONSerialize().GetAwaiter().GetResult();
        }


        private void ParseJSON()
        {
            
        }

        private void ParseXML()
        {
            
        }

        private async Task JSONSerialize()
        {
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {

                List<Options> options = new List<Options>();

           
            
                await JsonSerializer.SerializeAsync<List<Options>>(fs, options);
          
            }

        }


        public Options GetOptions<T>()
        {
            throw new NotImplementedException();
        }

    }
}
