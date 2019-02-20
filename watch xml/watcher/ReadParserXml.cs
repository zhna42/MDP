using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace watch_xml.watcher
{
    class ReadParserXml
    {
        public string name;
        public string artist;
        public string retransmission;
        public string err;

        public int isRet; //0-нет трека в статусе проигрывается 
        public XmlDocument xmlText;

        public void readXml(string ptah)
        {
            isRet = 0;
            if (File.Exists(ptah))
            {
                xmlText = new XmlDocument();
                try
                {
                    using (FileStream FS = new FileStream(ptah, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        FS.Position = 0;
                        FS.Seek(0, SeekOrigin.Begin);
                        byte[] array = new byte[FS.Length];
                        FS.Read(array, 0, array.Length);
                        string textFromFile = System.Text.Encoding.Default.GetString(array);

                        try
                        {
                            xmlText.LoadXml(textFromFile);
                        }
                        catch
                        {
                            isRet = 0;
                            err = "Файл пуст";
                        }
                    }
                }
                catch { }
            }
            else
            {
                isRet = 0;
                err = "Файл не найден";
            }
        }

        public void parseXml()
        {
            isRet = 0;
            try
            {
                XmlNodeList ELEM = xmlText.GetElementsByTagName("ELEM");
                foreach (XmlNode item in ELEM)
                {
                    if (item.Attributes["STATUS"].Value == "playing")
                    {
                        name = item.ChildNodes.Item(4).InnerText;
                        artist = item.ChildNodes.Item(5).InnerText;
                        retransmission = item.ChildNodes.Item(11).InnerText;
                        isRet = 1;
                    }
                    else if (item.Attributes["STATUS"].Value != "playing" && isRet != 1)
                    {
                        isRet = 0;
                        err = "нет трека в статусе проигрывается";
                    }
                }
            }
            catch
            {
                isRet = 0;
                err = "Файл пуст";
            }
        }
    }
}
