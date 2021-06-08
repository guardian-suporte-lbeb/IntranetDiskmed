using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.Util
{
    public class Texto_Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        public static string MesNumericoExtenso(int mes)
        {
            string mesConvertido = "";

            switch (mes)
            {
                case 1:
                    mesConvertido = "Janeiro";
                    break;
                case 2:
                    mesConvertido = "Fevereiro";
                    break;
                case 3:
                    mesConvertido = "Março";
                    break;
                case 4:
                    mesConvertido = "Abril";
                    break;
                case 5:
                    mesConvertido = "Maio";
                    break;
                case 6:
                    mesConvertido = "Junho";
                    break;
                case 7:
                    mesConvertido = "Julho";
                    break;
                case 8:
                    mesConvertido = "Agosto";
                    break;
                case 9:
                    mesConvertido = "Setembro";
                    break;
                case 10:
                    mesConvertido = "Outubro";
                    break;
                case 11:
                    mesConvertido = "Novembro";
                    break;
                case 12:
                    mesConvertido = "Dezembro";
                    break;
                default:
                    mesConvertido = "Inválido";
                    break;
            }

            return mesConvertido;
        }


        public static string EstadoExtenso(string estado)
        {
            if (estado == "AC")
                return "ACRE";
            if (estado == "AL")
                return "ALAGOAS";
            if (estado == "AP")
                return "Amapá";
            if (estado == "AM")
                return "AMAZONAS";
            if (estado == "BA")
                return "BAHIA";
            if (estado == "CE")
                return "CEARA";
            if (estado == "DF")
                return "DISTRITO FEDERAL";
            if (estado == "ES")
                return "ESPIRITO SANTO";
            if (estado == "GO")
                return "GOIÁS";
            if (estado == "MA")
                return "MARANHÃO";
            if (estado == "MT")
                return "MATO GROSSO";
            if (estado == "MS")
                return "MATO GROSSO DO SUL";
            if (estado == "MG")
                return "MINAS GERAIS";
            if (estado == "PA")
                return "PARÁ";
            if (estado == "PB")
                return "PARAIBA";
            if (estado == "PR")
                return "PARANÁ";
            if (estado == "PE")
                return "PERNAMBUCO";
            if (estado == "PI")
                return "PIAUÍ";
            if (estado == "RJ")
                return "RIO DE JANEIRO";
            if (estado == "RN")
                return "RIO GRANDE DO NORTE";
            if (estado == "RS")
                return "RIO GRANDE DO SUL";
            if (estado == "RO")
                return "RONDÔNIA";
            if (estado == "RR")
                return "RORAIMA";
            if (estado == "SC")
                return "SANTA CATARINA";
            if (estado == "SP")
                return "SÃO PAULO";
            if (estado == "SE")
                return "SERGIPE";
            if (estado == "TO")
                return "TOCANTIS";
            else
            {
                return "";
            }
        }

        public static string FormatarData(string data)
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
    }
}