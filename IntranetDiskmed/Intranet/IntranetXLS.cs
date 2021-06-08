using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.Intranet
{
    public class IntranetXLS
    {
        public static void GerarXLS(string caminhoTemplate, string caminhoSaida, DataTable dtDados, bool alterarCabecalho)
        {
            File.Copy(caminhoTemplate, caminhoSaida);


            using (SpreadsheetDocument template = SpreadsheetDocument.Open(caminhoSaida, true))
            {
                WorkbookPart workbookPart = template.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.Last();
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Cabeçalho
                if (alterarCabecalho)
                {
                    // Seleciona a ulltíma linha válida do template, como cabeçalho
                    var cabecalho = sheetData.LastChild;

                    // Colunas da Datatable
                    for (int col = 0; col < dtDados.Columns.Count; col++)
                    {
                        Cell titulo = (Cell)cabecalho.ChildElements[col];
                        titulo.DataType = CellValues.InlineString;
                        titulo.InlineString = new InlineString { Text = new Text { Text = dtDados.Columns[col].Caption } };
                    }
                }

                // Conteúdo
                for (int row = 0; row < dtDados.Rows.Count; row++)
                {
                    DataRow dtRow = dtDados.Rows[row];

                    // Cria uuma nova linha para a planilha
                    Row linha = new Row();

                    for (int col = 0; col < dtDados.Columns.Count; col++)
                    {
                        // Cria uma nova célula
                        Cell celula = new Cell();

                        // Se o tipo da coluna for Double, faz inserção dos dados na celula em formato double
                        if (dtDados.Columns[col].DataType.Name == "Double")
                        {
                            // Cria uma variavel de valor da celula
                            CellValue valorCelula = new CellValue();
                            // Define como number
                            celula.DataType = CellValues.Number;
                            // Insere o valor formatado na variavel de valor da celular
                            valorCelula.Text = dtDados.Rows[row][col].ToString().Replace(",", ".");
                            // Adicionar o valor da celula para a celula
                            celula.Append(valorCelula);
                        }
                        // Caso contrário os dados são inseridos no tipo string
                        else
                        {
                            celula.DataType = CellValues.InlineString;
                            celula.InlineString = new InlineString { Text = new Text { Text = dtDados.Rows[row][col].ToString() } };
                        }

                        // Adiciona a celular na linha criada
                        linha.Append(celula);
                    }
                    // Adiciona a linha a planilha
                    sheetData.Append(linha);
                }

                worksheetPart.Worksheet.Save();
            }
        }
    }
}