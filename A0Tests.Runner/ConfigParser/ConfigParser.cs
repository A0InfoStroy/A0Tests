// $Date: 2021-08-19 15:17:05 +0300 (Чт, 19 авг 2021) $
// $Revision: 568 $
// $Author: eloginov $

using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace A0Tests.Runner
{
    /// <summary>
    /// Класс для храннения команды импорта настроек из config файла.
    /// </summary>
    [Verb("import", HelpText = "Import settings from config file.")]
    public class ImportOption
    {
        private string settingsPath; //Путь к файлу с настройками.
        private const string defaultConfigPath = "A0Tests.Runner.config";

        [Option('c', "configPath", Required = false, HelpText = "The path to the file with xml settigns", Default = defaultConfigPath)]
        public string PathConfigFile
        {
            get { return settingsPath; }
            private set
            {
                //Если путь не был введен, то вычисляется стандартное значение на основе названия текущего проекта.
                if (value == defaultConfigPath) 
                {
                    //Получение названия текущего проекта.
                    settingsPath = Assembly.GetEntryAssembly().GetName().Name + ".config";
                }

                if (!File.Exists(value)) //Проверка файла на наличие.
                {
                    throw new FileNotFoundException($"The specified file path '{value}' does not exist"); 
                }
                settingsPath = value; //Присваивание значения пути, введенного пользователем.
            }
        }
    }


    /// <summary>
    /// Класс для храннения аргументов командной строки ручного ввода настроек с их описанием.
    /// </summary>
    [Verb("input", HelpText = "Manual entry of Runner settings.")]
    public class InputOptions
    {
        /// <summary>
        /// Отсуствие значения у аргумента.
        /// </summary>
        private const string none = "-";
        private string txtPath;
        private string xmlPath;
        private string protocolPath;

        //Service
        [Option('t', "txt", SetName = "OutputToTxt", Required = false, HelpText = "Result txt file path", Default = none)]
        public string ResTxtFilePath
        {
            get 
            {
                if (txtPath == none)
                    return null;
                return txtPath; 
            }
            private set
            {
                txtPath = value;
            }
        }


        [Option('x', "xml", SetName = "OutputToXml", Required = false, HelpText = "Result xml file path", Default = none)]
        public string ResXmlFilePath 
        {
            get
            {
                if (xmlPath == none)
                    return null;
                return xmlPath;
            }
            private set
            {
                xmlPath = value;
            }
        }

        [Option('c', "consoleOutput", SetName = "OutputToConsole", Required = false, HelpText = "Output result to console")]
        public bool SaveResInConsole { get; private set; }

        [Option('l', "protocol", Required = false, HelpText = "Protocol file path", Default = none)]
        public string ProtocolFilePath 
        {
            get
            {
                if (protocolPath == none)
                    return null;
                return protocolPath;
            }
            private set
            {
                protocolPath = value;
            }
        }


        //Run
        [Option('p', "project", Required = false, HelpText = "Testing project", Default = "../../../A0Tests/bin/Debug/A0Tests.dll")]
        public string Project { get; private set; }

        [Option('f', "filter", Required = false, HelpText = "Filter", Default = new string[] { "A0Tests.Smoke.Test_Connect" })]
        public IEnumerable<string> Filter { get; private set; }

        [Option('d', "paparmsOutput", Required = false, HelpText = "Output selected parametrs", Default = false)]
        public bool ParamsOutput { get; private set; }

    }

    class ConfigParser : IConfigParser
    {
        private readonly ILog log;
        public ConfigParser(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException("ILog must be initialized");
        }
        public Properties Parse(string[] args)
        {
            Properties config = new Properties();

            //Загрузка всех команд
            Type[] types = LoadVerbs();

            //Разбор аргументов командной строки.
            ParserResult<object> result = new Parser(confParser =>
            {
                confParser.IgnoreUnknownArguments = false;
                confParser.EnableDashDash = true; // Экранирование "-" при использовании "--".
            }).ParseArguments(args, types)
                .WithParsed(Run);

            result.WithNotParsed<object>(errs => HandleParseError(result, errs));

            if (result.Tag == ParserResultType.NotParsed)  //Возврат null и выход в main, в случае если была вызвана справка или допущена ошибка при вводе аргументов.
            {
                return null;
            }

            //Инициализация класса с настройками в зависимости от выбранной команды.
            void Run(object obj)
            {
                switch (obj)
                {
                    case ImportOption import: 
                        log.Info($"Importing settings from XML config: {import.PathConfigFile} ");
                        config = ParseFromXML(import.PathConfigFile);
                        break;

                    case InputOptions input:
                        log.Info($"Importing settings from command line ");
                        config.PropertiesService = new Properties.ServiceAppProperties(HelpMode: false, ResTxtFilePath: input.ResTxtFilePath,
                            ResXmlFilePath: input.ResXmlFilePath, SaveResInConsole: input.SaveResInConsole, ProtocolFilePath: input.ProtocolFilePath);
                        config.RunService = new Properties.RunProperties(Project: input.Project, Filter: input.Filter.ToArray(), ParamsOutput: input.ParamsOutput);
                        break;
                }
            }

            return config;
        }

        /// <summary>
        /// Вывод справки и информации о неверных входных аргументах.
        /// </summary>
        private void HandleParseError<T>(ParserResult<T> result, IEnumerable<Error> errs)
        {
            //Генерация справки.
            HelpText helpText = HelpText.AutoBuild(result, h =>
            {
                return HelpText.DefaultParsingErrorsHandler(result, h);
            },
            e => e,
            verbsIndex: true); //Установка verbsIndex для отображения справки по командам
            Console.WriteLine(helpText);
        }

        /// <summary>
        /// Загрузка всех классов с командами.
        /// </summary>
        private Type[] LoadVerbs()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
        }

        /// <summary>
        /// Чтение данных из конфиг файла и создание структуры c настройками
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
