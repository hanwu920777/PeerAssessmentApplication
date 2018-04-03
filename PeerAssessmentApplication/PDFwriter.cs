// creating form fields example -> https://simpledotnetsolutions.wordpress.com/2012/11/01/itextsharp-creating-form-fields/
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerAssessmentApplication
{
    public class PDFwriter
    {
        private FileStream fs;
        private Document document;
        private PdfWriter writer;
        private PdfPTable table;
        private PdfPTable idTable;
        private Font defaultFont;
        private Font headingFont;
        public static String[] LANGUAGES_gc = { "English", "German", "Spanish" };
        public void Write()
        {
            fs = new FileStream("Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            document = new Document(PageSize.A4);
            document.SetMargins(ConvertX(25), ConvertX(25), ConvertX(25), ConvertX(25));
            writer = PdfWriter.GetInstance(document, fs);
            defaultFont = new Font(Font.FontFamily.COURIER, 10, Font.NORMAL);
            headingFont = new Font(Font.FontFamily.COURIER, 26, Font.BOLD);
            document.Open();
            AddTextElements();
            AddFields();
            
            document.Close();
        }

        public void AddTextElements()
        {
            //Set and add title
            Paragraph heading = new Paragraph(new Phrase("Peer Assessment Form", headingFont));
            document.Add(heading);

            Paragraph subHeading = new Paragraph(new Phrase(
                "Complete the highlighted spaces below", 
                defaultFont));
            document.Add(subHeading);

            Paragraph instructions = new Paragraph(
                new Phrase("Peer assessments which do not follow this template, and peer assessments which " +
                "are not complete will not be counted.",
                defaultFont));
            document.Add(instructions);

            //set id table text
            idTable = new PdfPTable(2);
            idTable.TotalWidth = ConvertX(157);
            PdfPCell cell = new PdfPCell(new Phrase("Your Student ID", defaultFont));
            cell.FixedHeight = ConvertY(15);
            idTable.AddCell(cell);
            cell.Phrase = new Phrase();
            idTable.AddCell(cell);

            //set input table text
            table = new PdfPTable(new float[] { 30, 30, 98 });
            table.TotalWidth = ConvertX(157);
            cell.Phrase = new Phrase("Group Member Student ID", defaultFont);
            cell.FixedHeight = ConvertY(10);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Contribution to group work as a percentage", defaultFont);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Comments", defaultFont);
            table.AddCell(cell);
            cell.Phrase = new Phrase();
            for(int i = 0; i < 12; i++)
            {
                table.AddCell(cell);
            }
            cell.Phrase = new Phrase("TOTAL", defaultFont);
            table.AddCell(cell);
            cell.Phrase = new Phrase("100%", defaultFont);
            table.AddCell(cell);
            cell.Phrase = new Phrase();
            table.AddCell(cell);
        }

        public void AddFields()
        {
            PdfContentByte cb = writer.DirectContent;
            Font _bf = new Font(Font.FontFamily.HELVETICA, 9);
            PdfFormField _radioGroup = PdfFormField.CreateRadioButton(writer, true);
            _radioGroup.FieldName = "language_gc";
            _radioGroup.PlaceInPage = 0;
            Rectangle _rect;
            RadioCheckField _radioG;
            PdfFormField _radioField1;

            for (int i = 0; i < LANGUAGES_gc.Length; i++)
            {
                _rect = new Rectangle(40, 806 - i * 40, 60, 788 - i * 40);
                _radioG = new RadioCheckField(writer, _rect, null, LANGUAGES_gc[i])
                {
                    BackgroundColor = new GrayColor(0.8f),
                    BorderColor = GrayColor.BLACK,
                    CheckType = RadioCheckField.TYPE_CIRCLE
                };
                _radioField1 = _radioG.RadioField;
                _radioGroup.AddKid(_radioField1);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, new Phrase(LANGUAGES_gc[i], new Font(Font.FontFamily.HELVETICA, 18)), 70, 790 - i * 40, 0);
            }
            writer.AddAnnotation(_radioGroup);
            cb = writer.DirectContent;

            TextField _text = new TextField(writer, new Rectangle(ConvertX(103), ConvertY(190), ConvertX(182), ConvertY(199)), "g1");
            _text.Alignment = Element.ALIGN_CENTER;
            _text.Options = TextField.MULTILINE;
            _text.Text = "abc";
            PdfFormField textbox = _text.GetTextField();
            textbox.PlaceInPage = 1;
            writer.AddAnnotation(textbox);
            cb = writer.DirectContentUnder;
            table.WriteSelectedRows(0, -1, ConvertX(25), ConvertY(133), cb);
            idTable.WriteSelectedRows(0, -1, ConvertX(25), ConvertY(205), cb);
        }

        public int ConvertX(double unit)
        {
            return (int)(unit * 2.847);
        }

        public int ConvertY(double unit)
        {
            return (int)(unit * 2.854);
        }
    }
}
