using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watch_xml
{
    class Json
    {
        public DataJson json { set; get; }

        public void read()
        {
            string jsonFile = File.ReadAllText("./settings.json");
            json = JsonConvert.DeserializeObject<DataJson>(jsonFile);
        }

        public void record()
        {
            string jsonText = JsonConvert.SerializeObject(json);
            File.WriteAllText("./settings.json", jsonText);
        }
    }

    class DataJson
    {
        public List<Retransmission> retransmission { get; set; }
        public List<Broadcasting> broadcasting { get; set; }
    }

    class Retransmission
    {
        public string xml { get; set; }
        public string xml2 { get; set; }
        public string txt { get; set; }
        public string err { get; set; }
        public string status { get; set; }
        public string artist { get; set; }
        public string name { get; set; }
    }

    class Broadcasting
    {
        public string xml { get; set; }
        public string xml2 { get; set; }
        public string txt { get; set; }
        public string err { get; set; }
        public string status { get; set; }
        public string artist { get; set; }
        public string name { get; set; }
    }
}
