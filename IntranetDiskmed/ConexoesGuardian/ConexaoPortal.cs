using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.ConexoesGuardian
{
    public class ConexaoPortal
    {
        public static string Banco { get; set; } = "guardianportal";
        public static string Login { get; set; } = "lbeb";
        public static string Senha { get; set; } = "1xq@DVNCyG";
        public static string Servidor { get; set; } = "kifferti.ddns.net";

        public static string Conexao()
        {
            return "Server=" + Servidor + ";Database=" + Banco + ";UID=" + Login + ";PWD=" + Senha + ";";
        }
    }
}