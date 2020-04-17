using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Cipher
    {
        // Используем ограниченный набор для облегчения вычислений
        private double p, q, n, phi;
        private double[] publicKey = new double[2];
        private double[] privateKey = new double[2];

        public Cipher()
        {
            p = Calculation.getPrime();
            q = Calculation.getPrime();
            n = p * q;
            phi = (p - 1) * (q - 1);
            publicKey = getPublicKey();
            privateKey = getPrivateKey(publicKey[1]);
        }
        public double[] getPublicKey()
        {
            double[] publicKey = new double[2];

            double openExp = Calculation.getOpenExp(phi);
            publicKey[0] = n;
            publicKey[1] = openExp;
            return publicKey;
        }

        public double[] getPrivateKey(double openExp)
        {
            double closeExp = Calculation.getCloseExp(phi, openExp);
            privateKey[0] = n;
            privateKey[1] = closeExp;
            return privateKey;
        }

        public string cipher(string message)
        {
            double openExp = publicKey[1];
            byte[] textBytes = Encoding.ASCII.GetBytes(message);
            string encodedText = "";
            foreach (byte i in textBytes) {              
                double c = Math.Pow(i, openExp) % n;
                string m = c.ToString() + '-';
                encodedText += m;
            }
            return encodedText;
        }

        public static string decipher(string message, double[] privateKey) {
            string[] messageArray = message.Split('-');
            List<double> messageArrayDecihered = new List<double>();
            string decipheredMessage = "";
                foreach (string msg in messageArray)
                {
                    if (msg != "")
                    {
                        double deciperedNum = Convert.ToDouble(msg);
                        messageArrayDecihered.Add(Math.Pow(deciperedNum, privateKey[1]) % privateKey[0]);
                    }
                }
                foreach (double num in messageArrayDecihered)
                {
                    decipheredMessage += Convert.ToChar(Convert.ToInt64(num));
                }

            return decipheredMessage;
        }

        public void savePrivateKey(string path) {
            using (StreamWriter w = new StreamWriter(Path.Combine(path, "privateKey.txt"), false, Encoding.GetEncoding(0)))
            {
                w.WriteLine(privateKey[0]);
                w.WriteLine(privateKey[1]);
            }
        }

        public void savePublicKey(string path) {
            using (StreamWriter w = new StreamWriter(Path.Combine(path, "publicKey.txt"), false, Encoding.GetEncoding(0)))
            {
                w.WriteLine(publicKey[0]);
                w.WriteLine(publicKey[1]);
            }
        }
    }
}
