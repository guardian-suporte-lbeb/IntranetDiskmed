using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.Intranet
{
    public class Configuracoes
    {
        /// <summary>
        /// Nome do arquivo de configurações da Intranet
        /// </summary>
#if (DEBUG)
        public static string ArquivoConfig { get; set; } = "SettingsDebug.config";
#else
            public static string ArquivoConfig { get; set; } = "SettingsDiskmed.config";
#endif
    }
}