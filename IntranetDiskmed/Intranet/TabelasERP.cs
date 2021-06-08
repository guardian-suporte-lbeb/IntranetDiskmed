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
            public static String Add { get; set; } = "020";
#endif

        /// <summary>
        /// Cadastro de Cliente
        /// </summary>
        public static String CC2 { get; set; } = "CC2" + Add;
    }
}