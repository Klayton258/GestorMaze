using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gestor_Maze.Controllers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;

namespace Gestor_Maze.Forms
{
    public partial class Rel : Form
    {
        public Rel(string begin, string end)
        {
            InitializeComponent();

            try
            {
                var rel = Task.Run(() => OrderController.GetReport(begin, end));
                rel.Wait();

                double total = 0;
                for (int i = 0; i < rel.Result.data.Count; i++)
                {
                    tableRel.Rows.Add(
                        rel.Result.data[i].product_name,
                        rel.Result.data[i].price,
                        rel.Result.data[i].quantity,
                        rel.Result.data[i].subtotal
                        );
                    total += rel.Result.data[i].subtotal;
                }
                lblTotal.Text = total.ToString();
                lblStartDate.Text = begin;
                lblEndDate.Text = end;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void exportRel(DataGridView datatable, String path, string strHeader)
        {

            System.IO.FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document();
            document.SetPageSize(iTextSharp.text.PageSize.A4);

            PdfWriter write = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Header style
            BaseFont bfnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfnHeader, 16, 1, iTextSharp.text.BaseColor.BLUE);
            Paragraph prgHeader = new Paragraph();
            prgHeader.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeader);

            //Author style
            Paragraph prgContent = new Paragraph();
            BaseFont bfnContent = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntContent = new Font(bfnContent, 8, 2, iTextSharp.text.BaseColor.GRAY);
            prgContent.Alignment = Element.ALIGN_RIGHT;
            prgContent.Add(new Chunk("Author: Maze Managment", fntContent));
            prgContent.Add(new Chunk("\nDate: " + DateTime.Now.ToLocalTime(), fntContent));
            document.Add(prgContent);

            

            //line
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_CENTER, 2)));
            document.Add(p);

            document.Add(new Chunk("\n", fntContent));

            //Generate table
            PdfPTable table = new PdfPTable(datatable.Columns.Count);
            BaseFont ftncolumheader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumheader = new Font(ftncolumheader, 10, 1, iTextSharp.text.BaseColor.WHITE);

            for (int i = 0; i < datatable.Columns.Count; i++)
            {
                if (datatable.Columns != null)
                {

                    PdfPCell cell = new PdfPCell();
                    cell.BackgroundColor = iTextSharp.text.BaseColor.DARK_GRAY;
                    cell.AddElement(new Chunk(datatable.Columns[i].HeaderCell.Value.ToString(), fntColumheader));
                    table.AddCell(cell);
                }
            }

            for (int j = 0; j < datatable.Rows.Count; j++)
            {
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    if (datatable.Columns != null && datatable.Rows != null)
                    {
                        if (datatable.Rows[j].Cells[i].Value != null)
                        {
                        table.AddCell(datatable.Rows[j].Cells[i].Value.ToString());
                        }
                    }
                }
            }
            document.Add(table);

            Paragraph pp = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_CENTER, 2)));
            document.Add(pp);

            //Dates
            Paragraph prgDates = new Paragraph();
            BaseFont bfnDates = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntDates = new Font(bfnContent, 8, 2, iTextSharp.text.BaseColor.GRAY);
            prgDates.Alignment = Element.ALIGN_LEFT;
            prgDates.Add(new Chunk("From: " + lblStartDate.Text, fntContent));
            prgDates.Add(new Chunk("\nTo: " + lblEndDate.Text, fntContent));
            document.Add(prgDates);

            //TOTAL
            Paragraph prgTotal = new Paragraph();
            BaseFont bfnTotal = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntTotal= new Font(bfnTotal, 12, 1, iTextSharp.text.BaseColor.BLACK);
            prgTotal.Alignment = Element.ALIGN_RIGHT;
            prgTotal.Add(new Chunk("TOTAL: " + lblTotal.Text +" MZN", fntTotal));
            document.Add(prgTotal);

            
            document.Close();
            fs.Close();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

            DataGridView table = tableRel;

            try
            {

                FolderBrowserDialog objFDB = new FolderBrowserDialog();
                objFDB.ShowNewFolderButton = true;
                objFDB.ShowDialog();

                exportRel(table, objFDB.SelectedPath + $@"\Rel_{DateTime.Now.ToString("HH_mm")}.pdf", "REPORT");
                System.Diagnostics.Process.Start(objFDB.SelectedPath + $@"\Rel_{DateTime.Now.ToString("HH_mm")}.pdf");
            }
            catch (Exception)
            {

                return;
            }

        }
    }
}
