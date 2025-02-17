using AlexisUI.EngineUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AlexisUI.ContentHandling
{
    public class Serializer
    {
        public static void SaveFile<T>(T content, string filePath)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    xmlSerializer.Serialize(writer, content);
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }
    
        public static IDictionary<string, string> ReadMetadataFile(string metadataPath)
        {
            IDictionary<string, string> metaDataContent = new Dictionary<string, string>();
            try
            {
                using(XmlReader xmlReader = XmlReader.Create(metadataPath))
                {
                    while (xmlReader.Read())
                    {
                        if (xmlReader.IsStartElement())
                        {
                            switch (xmlReader.Name.ToString())
                            {
                                case "Project":
                                    metaDataContent.Add("Project", xmlReader.ReadString());break;
                                case "ProjectPath":
                                    metaDataContent.Add("ProjectPath", xmlReader.ReadString()); break;
                                case "LastUpdate":
                                    metaDataContent.Add("LastUpdate", xmlReader.ReadString()); break;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.Write(ex);
            }
            return metaDataContent;
        }

        public static IDictionary<string, string> ReadProjectFile(string metadataPath)
        {
            IDictionary<string, string> metaDataContent = new Dictionary<string, string>();
            try
            {
                using (XmlReader xmlReader = XmlReader.Create(metadataPath))
                {
                    while (xmlReader.Read())
                    {
                        if (xmlReader.IsStartElement())
                        {
                            switch (xmlReader.Name.ToString())
                            {
                                case "Project":
                                    metaDataContent.Add("Project", xmlReader.ReadString()); break;
                                case "ProjectPath":
                                    metaDataContent.Add("ProjectPath", xmlReader.ReadString()); break;
                                case "LastUpdate":
                                    metaDataContent.Add("LastUpdate", xmlReader.ReadString()); break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
            return metaDataContent;
        }
    }
}
