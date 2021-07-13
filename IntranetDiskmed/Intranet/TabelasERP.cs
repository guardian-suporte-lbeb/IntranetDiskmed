using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.Intranet
{
    public class TabelasERP
    {
#if (DEBUG)
        public static String Add { get; set; } = "990";
#else
            public static String Add { get; set; } = "010";
#endif

        /// <summary>
        /// DADOS DA TABELA DA ANVISA
        /// </summary>
        public static String ZZA { get; set; } = "ZZA" + Add;

        /// <summary>
        /// DADOS DA TABELA DA ALTERACAO DE CUSTO DE PRODUTO
        /// </summary>
        public static String ZZP { get; set; } = "ZZP" + Add;
    }
}