using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.Models
{
    public class AlteracaoCProduto
    {
        public string Filial { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
        public string User { get; set; }
        public string NomeUser { get; set; }
        public string Origem { get; set; }
        public string Codigo { get; set; }
        public string Desc { get; set; }
        public string Marca { get; set; }
        public string OldUpc { get; set; }
        public string OldStd { get; set; }
        public string NewUpc { get; set; }
        public string NewStd { get; set; }
        public string Config { get; set; }
    }
}