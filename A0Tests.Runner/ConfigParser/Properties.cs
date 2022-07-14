// $Date: 2021-08-19 10:30:06 +0300 (Чт, 19 авг 2021) $
// $Revision: 565 $
// $Author: eloginov $

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace A0Tests.Runner
{
    /// <summary>
    /// Класс содержит два вложенных класса с полями, необходимыми для настройки и запуска приложения. 
    /// </summary>
    [DataContract(Name = "Properties", Namespace = "")]
    public class Properties
    {

        [DataMember(Name = "ServiceAppProperties", Order = 0, IsRequired = true)] // IsRequired = true позволяет сгенерировать исключение при неудачной попытке спарсить данные.
        public Properties.ServiceAppProperties PropertiesService { get; set; }
        [DataMember(Name = "RunProperties", Order = 1, IsRequired = true)]
        public Properties.RunProperties RunService { get; set; }

        /// <summary>
        /// Класс содержит настройки для AppService. 
        /// </summary>
        [DataContract(Name = "ServiceAppProperties", Namespace = "")]
        public class ServiceAppProperties
        {
            /// <summary>
            /// Флаг, отвечающий за режим справки. 
            /// </summary>
            [DataMember(Name = "HelpMode", Order = 0, IsRequired = true)]
            public bool HelpMode { get; private set; }

            /// <summary>
            /// Путь сохранения текстового файла с результатами. 
            /// </summary>
            [DataMember(Name = "ResTxtFilePath", Order = 1, IsRequired = true)]
            public string ResTxtFilePath { get; private set; }

            /// <summary>
            /// Путь сохранения xml файла с результатами. 
            /// </summary>
            [DataMember(Name = "ResXmlFilePath", Order = 2, IsRequired = true)]
            public string ResXmlFilePath { get; private set; }

            /// <summary>
            /// Флаг, отвечающий за сохраннеие результатов в консоль. 
            /// </summary>
            [DataMember(Name = "SaveResInConsole", Order = 3, IsRequired = true)]
            public bool SaveResInConsole { get; private set; }

            /// <summary>
            /// Путь сохранения файла с протоколом. 
            /// </summary>
            [DataMember(Name = "ProtocolFilePath", Order = 4, IsRequired = true)]
            public string ProtocolFilePath { get; private set; }

            public ServiceAppProperties(bool HelpMode, string ResTxtFilePath, string ResXmlFilePath, bool SaveResInConsole, string ProtocolFilePath)
            {
                this.HelpMode = HelpMode;
                this.ResTxtFilePath = ResTxtFilePath;
                this.ResXmlFilePath = ResXmlFilePath;
                this.SaveResInConsole = SaveResInConsole;
                this.ProtocolFilePath = ProtocolFilePath;
            }
        }

        /// <summary>
        /// Класс содержит настройки для запуска тестов.
        /// </summary>
        [DataContract(Name = "RunProperties", Namespace = "")]
        public class RunProperties
        {
            /// <summary>
            /// Тестируемый проект
            /// </summary>
            [DataMember(Name = "Project", Order = 0)]
            public string Project { get; private set; }
            /// <summary>
            /// ВЫбранная категория(фильтры)
            /// </summary>
            [DataMember(Name = "Filters", Order = 1, IsRequired = true)]
            public Filters Filter { get; private set; }

            [CollectionDataContract(Name = "Filters", ItemName = "Filter", Namespace = "")]
            public class Filters : List<string>
            {
                public Filters() { } //Конструктор для десериализации настроек из xml

                public Filters(string[] filters) 
                {
                    foreach (string filter in filters)
                    {
                        this.Add(filter);
                    }
                }
            }

            /// <summary>
            /// Флаг, отвечающий за вывод на консоль и в файл протокола переданных параметров.
            /// </summary>
            [DataMember(Name = "PaparamsOutput", Order = 2, IsRequired = true)]
            public bool PaparamsOutput { get; private set; }

            public RunProperties(string Project, string[] Filter, bool ParamsOutput)
            {
                this.Project = Project;
                this.Filter = new Filters(filters: Filter);
                this.PaparamsOutput = ParamsOutput;
            }
        }
    }
}
