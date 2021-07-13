using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.Models
{
    public class Anvisa
    {
        public string Filial { get; set; }
        public string Substancia { get; set; }
        public string Substancia2 { get; set; }
        public string CNPJ { get; set; }
        public string Laboratorio { get; set; }
        public string CodGgrem { get; set; }
        public string Registro { get; set; }
        public string Ean1 { get; set; }
        public string Ean2 { get; set; }
        public string Ean3 { get; set; }
        public string Produto { get; set; }
        public string Apresentacao { get; set; }
        public string ClasseTerapeutica { get; set; }
        public string TipoProduto { get; set; }
        public string RegimePreco { get; set; }
        public int PFSemImpostos { get; set; }
        public int PF0 { get; set; }
        public int PF12 { get; set; }
        public int PF17 { get; set; }
        public int PF17AL { get; set; }
        public int PF175 { get; set; }
        public int PF175AL { get; set; }
        public int PF18 { get; set; }
        public int PF18AL { get; set; }
        public int PF20 { get; set; }
        public int PMC0 { get; set; }
        public int PMC12 { get; set; }
        public int PMC17 { get; set; }
        public int PMC17AL { get; set; }
        public int PMC175 { get; set; }
        public int PMC175AL { get; set; }
        public int PMC18 { get; set; }
        public int PMC18AL { get; set; }
        public int PMC20 { get; set; }
        public string RestHosp { get; set; }
        public string Cap { get; set; }
        public string Confaz87 { get; set; }
        public string ICMS0 { get; set; }
        public string AnaliseRec { get; set; }
        public string Listpc { get; set; }
        public string Comerc19 { get; set; }
        public string Tarja { get; set; }
        public string Idproc { get; set; }
    }
}