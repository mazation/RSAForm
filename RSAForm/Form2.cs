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
    public partial class Form2 : Form
    {
        private double[] privateKey = new double[2];
        private string message;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.Filter = "*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    using (StreamReader r = new StreamReader(openFileDialog.FileName))
                    {
                        privateKey[0] = Convert.ToDouble(r.ReadLine());
                        privateKey[1] = Convert.ToDouble(r.ReadLine());
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader r = new StreamReader(openFileDialog.FileName))
                    {
                        message = r.ReadToEnd();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (privateKey == null || message == null) {
                MessageBox.Show("Пожалуста, загрузите всё необходимые файлы", "Ошибка");
            }
            using (var saveFileDialog = new SaveFileDialog())
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    string path = saveFileDialog.FileName;
                    using (StreamWriter w = new StreamWriter(path, false, Encoding.Default))
                    {
                        try
                        {
                            w.Write(Cipher.decipher(message, privateKey));
                        }
                        catch (Exception ex) {
                            MessageBox.Show(ex.Message, "Ошибка");
                        }
                    }
                }
            }
        }
    }
}
