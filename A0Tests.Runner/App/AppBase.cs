// $Date: 2021-08-09 22:20:17 +0300 (Пн, 09 авг 2021) $
// $Revision: 557 $
// $Author: eloginov $

namespace A0Tests.Runner
{
    class AppBase : IApp
    {
        private ILog log;
        private Properties.RunProperties properties;

        public AppBase(Properties.RunProperties properties, ILog log)
        {
            this.log = log;
            this.properties = properties;
        }
        public AppBase(ILog log)
        {
            this.log = log;
        }
        public virtual string Run()
        { 
            if (properties.PaparamsOutput == true) //Выводить ли настройки
            {
                log.Info("Run properties:");
                log.Info($"Project path: {properties.Project} Paparms Output: {properties.PaparamsOutput}");
                foreach (string f in properties.Filter)
                {
                    log.Info($"Filter: {f} ");
                }
            }

            return "";
        }
    }
}
