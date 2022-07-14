// $Date: 2021-08-09 22:20:17 +0300 (Пн, 09 авг 2021) $
// $Revision: 557 $
// $Author: eloginov $

using System;

namespace A0Tests.Runner
{
    class AppHelp : AppBase
    {
        private ILog log;
        public AppHelp(ILog log) : base(log)
        {
            this.log = log ?? throw new ArgumentNullException("ILog must be initialized");
        }

        public override string Run() 
        {
            log.Info("Help mode selected");
            string help = "help";
            return help;
        }

    }
}
