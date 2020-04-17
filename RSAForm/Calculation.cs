using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Calculation
    {
        public static double getPrime() {
            double num = generateNum();
            while (!isPrime(num)) {
                num++;
            }
            return num;
        }

        public static double generateNum() {
            double num;
            Random rnd = new Random();
            num = rnd.Next((int)Math.Pow(2, 6));
            return num;
        }

        protected static bool isPrime(double num) {
            for (int i = 2; i < num/2; i++) {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static double getOpenExp(double phi) {
            double m = 3;
            while (phi % m == 0) {
                m++;
            }
            return m;
        }

        public static double getCloseExp(double phi, double exp) {
            double d;
            int x = 1;
            d = (1 + x * phi) / exp;
            while (d % Math.Floor(d) != 0) 
            {
                x++;
                d = (1 + x * phi) / exp;
            }
            return d;
        }
    }
}
