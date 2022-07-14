// $Date: 2021-07-21 12:07:20 +0300 (Ср, 21 июл 2021) $
// $Revision: 547 $
// $Author: eloginov $

namespace A0Tests.Runner
{
    /// <summary>
    /// Класс хранят основные настройки.
    /// </summary>
    class ServiceApp  
    {
        public ServiceApp(Properties.ServiceAppProperties props, ILog log)
        {
            HelpMode = props.HelpMode;
            ProtocolFilePath = props.ProtocolFilePath;
            
            ResTxtFilePath = props.ResTxtFilePath;
            ResXmlFilePath = props.ResXmlFilePath;
            SaveResInConsole = props.SaveResInConsole;

            log.Info("ServiceApp properties:");
            log.Info($"HelpMode: {HelpMode} ProtocolFilePath: {ProtocolFilePath} " +
                $"ResTxtFilePath: {ResTxtFilePath} ResXmlFilePath: {ResXmlFilePath} SaveResInConsole: {SaveResInConsole} ");
        }

        /// <summary>
        /// Флаг, отвечающий за режим справки. 
        /// </summary>
        public bool HelpMode { get; }

        /// <summary>
        /// Путь сохранения текстового файла с результатами. 
        /// </summary>
        public string ResTxtFilePath { get; }

        /// <summary>
        /// Путь сохранения xml файла с результатами. 
        /// </summary>
        public string ResXmlFilePath { get; }

        /// <summary>
        /// Флаг, отвечающий за сохраннеие результатов в консоль. 
        /// </summary>
        public bool SaveResInConsole;

        /// <summary>
        /// Путь сохранения файла с протоколом. 
        /// </summary> 
        public string ProtocolFilePath { get; }

    }
}
