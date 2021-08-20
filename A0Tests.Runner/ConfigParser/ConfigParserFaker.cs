// $Date: 2021-08-19 10:30:06 +0300 (Чт, 19 авг 2021) $
// $Revision: 565 $
// $Author: eloginov $

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace A0Tests.Runner
{


    /// <summary>
    /// Фейковая реализация IConfigParser
    /// </summary>
    class ConfigParserFaker : IConfigParser
    {
        private readonly ILog log;
        public ConfigParserFaker(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException("ILog must be initialized");
        }
        public Properties Parse(string[] args)
        {

            const bool SettingsFromConfigFile = false ; //Флаг отвечает за чтение настроек из XML файла.
            const string PathConfigFile = "A0Tests.Runner.config"; //Путь к файлу с настройками

            if (SettingsFromConfigFile) 
            {
                log.Info($"Imporing settings from XML config: {PathConfigFile} ");
                if (!File.Exists(PathConfigFile)) //Проверка файла на наличие.
                {
                    throw new FileNotFoundException($"The specified file path '{PathConfigFile}' does not exist");
                }
                return ParseFromXML(PathConfigFile);
            }
            log.Info($"Imporing settings from command line ");

            //Инициализация класса с настройками
            Properties config = new Properties
            {
                PropertiesService = new Properties.ServiceAppProperties(HelpMode: false, ResTxtFilePath: null,
                ResXmlFilePath: null, SaveResInConsole: true, ProtocolFilePath: "protocol.txt"),

                RunService = new Properties.RunProperties(Project: "../../../A0Tests/bin/Debug/A0Tests.dll", 
                Filter: new string[] { "A0Tests.Smoke.Test_Connect" }, ParamsOutput: true)
            };

            return config;
        }

        
        /// <summary>
        /// Чтение данных из конфиг файла и инициализация класса c настройками
        /// </summary>
        /// <param name="xmlSettingsFilePath"></param>
        private Properties ParseFromXML(string xmlSettingsFilePath)
        {
            Properties config = new Properties();
            using (FileStream fs = new FileStream(xmlSettingsFilePath, FileMode.Open))
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(Properties));
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                config = (Properties)dcs.ReadObject(reader);
            }

            return config;
        }
    }
}
