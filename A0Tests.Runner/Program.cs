// $Date: 2021-08-19 15:18:26 +0300 (Чт, 19 авг 2021) $
// $Revision: 569 $
// $Author: eloginov $

using System;

namespace A0Tests.Runner
{
    class Program
    {      
        static void Main(string[] args)
        {
            ILog log = new NLogLogger();
            try
            {
                //Разбор аргументов комадной строки и структурирование настроек
                IConfigParser configParser = new ConfigParser(log);
                Properties configRes = configParser.Parse(args);
                
               	if (configRes == null)
                {
                    return;
                }
                if (!String.IsNullOrEmpty(configRes.PropertiesService.ProtocolFilePath))
                {
                    //Создание нового логгера, если есть файл протокола
                    log = new NLogLogger(configRes.PropertiesService.ProtocolFilePath);
                }
                
                log.Info("Start");
                
                if (configRes.RunService.PaparamsOutput)
                {
                    log.Info("ServiceApp properties:");
                    log.Info($"HelpMode: {configRes.PropertiesService.HelpMode} ProtocolFilePath: {configRes.PropertiesService.ProtocolFilePath} " +
                        $"ResTxtFilePath: {configRes.PropertiesService.ResTxtFilePath} ResXmlFilePath: {configRes.PropertiesService.ResXmlFilePath} SaveResInConsole: {configRes.PropertiesService.SaveResInConsole} ");
                }
                IApp appRun;
                if (configRes.PropertiesService.HelpMode)
                {
                    appRun = new AppHelp(log); 
                    Console.WriteLine(appRun.Run());
                }
                else
                {
                    SaveOption saveOption = new SaveOption(configRes.PropertiesService.ResTxtFilePath, configRes.PropertiesService.ResXmlFilePath);
                    ISave outFormat = saveOption.SaveSelection();

                    Out outResults = new Out();
                    appRun = new AppRun(configRes.RunService, log,
                        iOutput: outResults, iTestResult: outResults, save: outFormat); //Инициализация настроек для запуска тестов
                    string runResults = appRun.Run();
                } 
            }
            catch (ArgumentNullException msg)
            {
                log.Exception(msg);
            }           
            catch (System.IO.FileNotFoundException msg)
            {
                log.Exception(msg); 
            }
            catch (ArgumentException msg)
            {
                log.Exception(msg);
            }
            catch (Exception msg)
            {
                log.Exception(msg);
              
                throw;
            }
            
        }
    }
}

