﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Konsultacije;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsConsultations.Service
{
    public class PDFGenerator : IPDFGenerator
    {
        public byte[] GeneratePDF(List<Konsultacije> konsultacije, UserType userType)
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                writer.CloseStream = false;

                document.Open();

                document.NewPage();

                DateTime today = DateTime.Today;

                Paragraph paragraphForDate = new Paragraph("Spisak konsultacija na dan: " + " " + today);
                paragraphForDate.SpacingBefore = 10;
                paragraphForDate.SpacingAfter = 10;
                paragraphForDate.Alignment = Element.ALIGN_CENTER;
                paragraphForDate.Font = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 15f, BaseColor.Green);
                document.Add(paragraphForDate);

                if (userType == UserType.Student)
                {
                    KonsultacijeZaStudenta(document, konsultacije);
                }
                else
                {
                    KonsultacijeZaNastavnika(document, konsultacije);
                }

                document.Close();
                writer.Close();

                return memoryStream.ToArray();
            }
        }

        private void KonsultacijeZaStudenta(Document document, List<Konsultacije> konsultacije)
        {
            Paragraph paragraphForStudent = new Paragraph("Student:" + " " + konsultacije.FirstOrDefault().Student.Ime + " " + konsultacije.FirstOrDefault().Student.Prezime + " " + konsultacije.FirstOrDefault().Student.BrojIndeksa);
            paragraphForStudent.SpacingBefore = 15;
            paragraphForStudent.SpacingAfter = 15;
            paragraphForStudent.Alignment = Element.ALIGN_CENTER;
            paragraphForStudent.Font = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 15f, BaseColor.Green);
            document.Add(paragraphForStudent);

            PdfPTable table = new PdfPTable(3);

            PdfPCell cellNastavnik = new PdfPCell(new Phrase("Nastavnik"));
            cellNastavnik.FixedHeight = 20;
            table.AddCell(cellNastavnik);

            PdfPCell cellDatum = new PdfPCell(new Phrase("Datum konsultacija"));
            cellDatum.FixedHeight = 20;
            table.AddCell(cellDatum);

            PdfPCell cellOdrzane = new PdfPCell(new Phrase("Odrzane?"));
            cellOdrzane.FixedHeight = 20;
            table.AddCell(cellOdrzane);

            foreach (var k in konsultacije)
            {

                table.AddCell(k.Nastavnik.Ime + " " + k.Nastavnik.Prezime);
                table.AddCell(k.DatumKonsultacija.Date.ToString());

                if (k.Odrzane)
                {
                    table.AddCell("Da");
                }
                else
                {
                    table.AddCell("Ne");
                }
            }
            document.Add(table);
        }

        private void KonsultacijeZaNastavnika(Document document, List<Konsultacije> konsultacije)
        {
            Paragraph paragraphForStudent = new Paragraph("Nastavnik:" + " " + konsultacije.FirstOrDefault().Nastavnik.Ime + " " + konsultacije.FirstOrDefault().Nastavnik.Prezime + " " + konsultacije.FirstOrDefault().Nastavnik.BrojRadneKnjizice);
            paragraphForStudent.SpacingBefore = 15;
            paragraphForStudent.SpacingAfter = 15;
            paragraphForStudent.Alignment = Element.ALIGN_CENTER;
            paragraphForStudent.Font = FontFactory.GetFont(FontFactory.TIMES_ITALIC, 15f, BaseColor.Green);
            document.Add(paragraphForStudent);

            PdfPTable table = new PdfPTable(3);

            PdfPCell cellNastavnik = new PdfPCell(new Phrase("Student"));
            cellNastavnik.FixedHeight = 20;
            table.AddCell(cellNastavnik);

            PdfPCell cellDatum = new PdfPCell(new Phrase("Datum konsultacija"));
            cellDatum.FixedHeight = 20;
            table.AddCell(cellDatum);

            PdfPCell cellOdrzane = new PdfPCell(new Phrase("Odrzane?"));
            cellOdrzane.FixedHeight = 20;
            table.AddCell(cellOdrzane);

            foreach (var k in konsultacije)
            {

                table.AddCell(k.Student.Ime + " " + k.Student.Prezime);
                table.AddCell(k.DatumKonsultacija.Date.ToString());

                if (k.Odrzane)
                {
                    table.AddCell("Da");
                }
                else
                {
                    table.AddCell("Ne");
                }
            }
            document.Add(table);
        }
    }
}
