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

namespace PeerAssessmentApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void WriteButton_Click(object sender, EventArgs e)
        {
            PDFwriter writer = new PDFwriter();
            writer.Write();
        }

        private void ReadButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                string textFromFile = string.Empty;
                try
                {
                    PDFreader reader = new PDFreader();
                    textFromFile+=reader.Read(file);
                }
                catch (IOException)
                {
                }
                richTextBox1.Text = textFromFile;
            }
        }
    }
}
