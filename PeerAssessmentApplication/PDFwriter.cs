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
        public static String[] LANGUAGES_gc = { "English", "German", "Spanish" };
        public void Write()
        {
            fs = new FileStream("Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            document = new Document(PageSize.A4, 72, 72, 108, 108);
            writer = PdfWriter.GetInstance(document, fs);
            document.Open();
            document.Add(new Paragraph("Hello"));
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
                _radioG = new RadioCheckField(writer, _rect, null, LANGUAGES_gc[i]);
                _radioG.BackgroundColor = new GrayColor(0.8f);
                _radioG.BorderColor = GrayColor.BLACK;
                _radioG.CheckType = RadioCheckField.TYPE_CIRCLE;
                _radioField1 = _radioG.RadioField;
                _radioGroup.AddKid(_radioField1);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_LEFT, new Phrase(LANGUAGES_gc[i], new Font(Font.FontFamily.HELVETICA, 18)), 70, 790 - i * 40, 0);
            }
            writer.AddAnnotation(_radioGroup);
            cb = writer.DirectContent;

            TextField _text = new TextField(writer, new Rectangle(40, 806, 160, 788), "g1");
            _text.Alignment = Element.ALIGN_CENTER;
            _text.Options = TextField.MULTILINE;
            _text.Text = "abc";
            PdfFormField textbox = _text.GetTextField();
            textbox.PlaceInPage = 1;
            writer.AddAnnotation(textbox);
            cb = writer.DirectContentUnder;
            document.Close();
        }

        public void AddFields()
        {

        }
    }
}
