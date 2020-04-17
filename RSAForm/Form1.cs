using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using App;

namespace RSAForm
{
    public partial class Form1 : Form
    {
        private string privateKey;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string cipheredMessage = "";
            Cipher cip = new Cipher();
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    using (StreamReader r = new StreamReader(openFileDialog.FileName)) {
                        cipheredMessage = cip.cipher(r.ReadToEnd());
                    }
                }
            }
            using (var saveFileDialog = new SaveFileDialog()) {
                saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string path = saveFileDialog.FileName;
                    string dirPath = Path.GetDirectoryName(path);
                    using (StreamWriter w = new StreamWriter(path, false, Encoding.Default))
                    {
                        w.Write(cipheredMessage);
                    }
                    cip.savePublicKey(dirPath);
                    cip.savePrivateKey(dirPath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 F2 = new Form2();
            F2.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
