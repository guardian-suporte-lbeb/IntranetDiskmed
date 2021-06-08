using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace IntranetDiskmed.Intranet
{
    public class Formatacao
    {
        static CultureInfo culture = new CultureInfo("pt-BR");

        public static string formatarValores(string val)
        {
            double f;
            if (val.Trim() != "0")
            {
                if (!string.IsNullOrEmpty(val.Trim()))
                {
                    f = Convert.ToDouble(val.Replace('.', ','), culture);
                    val = String.Format(culture, "{0:C}", f);
                }
            }
            else
            {
                val = "R$ " + val + ",00";
            }

            return val;
        }

        public string formatarCEP(string cep)
        {
            cep = cep.Trim();

            if (cep == "" || cep == null)
            {
                return " ";
            }

            cep = cep.Substring(0, 5) + "-" + cep.Substring(5);

            return cep;
        }

        public static string formatarCGC(string cgc)
        {
            cgc = cgc.Trim();

            if (cgc == "" || cgc == null)
            {
                return " ";
            }

            if (cgc.Length > 11)
            {
                cgc = cgc.Substring(0, 2) + "." + cgc.Substring(2, 3) + "." + cgc.Substring(5, 3) + "/" + cgc.Substring(8, 4) + "-" + cgc.Substring(12, 2);
            }
            else
            {
                cgc = cgc.Substring(0, 3) + "." + cgc.Substring(3, 3) + "." + cgc.Substring(6, 3) + "-" + cgc.Substring(9, 2);
            }

            return cgc;
        }

        public string formatarData(string data)
        {
            data = data.Trim();

            DateTime dT;

            if (data.Length > 6)
            {
                if (DateTime.TryParseExact(data, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dT))
                {
                    // the string was successfully parsed into theDate
                    data = dT.ToString("dd'/'MM'/'yyyy");
                }
                else if (DateTime.TryParseExact(data, "yyyyMMdd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dT))
                {
                    // the string was successfully parsed into theDate
                    data = dT.ToString("dd'/'MM'/'yyyy");
                }
            }
            else
            {
                if (DateTime.TryParseExact(data, "yy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out dT))
                {
                    // the string was successfully parsed into theDate
                    data = dT.ToString("dd'/'MM'/'yyyy");
                }
            }

            return data;
        }

        public string formatarHora(string hora)
        {
            //hora = hora.Trim();

            DateTime dT;

            if (DateTime.TryParseExact(hora, "HH:mm:ss",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out dT))
            {
                // the string was successfully parsed into theDate
                hora = dT.ToString("HH':'mm':'ss");
            }

            return hora;
        }

        public string FormataPeso(string peso)
        {
            if (peso != "0")
            {
                peso = peso.Replace(".", ",");
                peso = string.Format(culture, "{0:#,##0.###}", Convert.ToDecimal(peso, culture));
            }

            return peso;
        }

        public string FormatarQtd(string qtd, string tipo)
        {
            if (qtd != "0")
            {
                qtd = qtd.Replace(".", ",");

                if (tipo == "med")
                    qtd = string.Format(culture, "{0:#,##0.000}", Convert.ToDecimal(qtd, culture));
                else
                    qtd = string.Format(culture, "{0:#,##0}", Convert.ToDecimal(qtd, culture));
            }

            return qtd;
        }

        public string FormatarCaracter(string str)
        {
            string validos = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.-";

            foreach (char c in str)
            {
                if (c == (char)13 || c == (char)10 || c == ' ')
                {
                    continue;
                }
                else if (!validos.Contains(c))
                {
                    str = str.Replace(c, '-');
                }
            }

            return str;
        }

        public bool ValidarFormatarCaracter(string str)
        {
            string validos = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.-";
            bool status = false;
            foreach (char c in str)
            {
                if (c == (char)13 || c == (char)10 || c == ' ')
                {
                    continue;
                }
                if (!validos.Contains(c))
                {
                    status = true;
                }
            }

            return status;
        }

        public string RemoverAcentos(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static string LetrasCapitais(string texto)
        {
            string retorno = "";
            var lista = texto.Split(' ');

            foreach (string item in lista)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    retorno += item.Substring(0, 1).ToUpper() + item.Substring(1).ToLower() + " ";
                }
            }

            retorno = retorno.TrimEnd();

            return retorno;
        }

        public static bool ValidarEmail(string email)
        {
            bool emailValido = false;
            int indexArroba = email.IndexOf("@");
            if (indexArroba > 0)
            {
                // Multiplos "@"
                if (email.IndexOf("@", indexArroba + 1) > 0)
                {
                    emailValido = false;
                }
                else
                {
                    int indexPonto = email.IndexOf(".", indexArroba);
                    if (indexPonto - 1 > indexArroba)
                    {
                        if (indexPonto + 1 < email.Length)
                        {
                            string indexDot2 = email.Substring(indexPonto + 1, 1);
                            if (indexDot2 != ".")
                            {
                                emailValido = true;
                            }
                        }
                    }
                    //Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
                    //if (!rg.IsMatch(email))
                    //    emailValido = false;
                }

            }

            return emailValido;
        }
    }
}