using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    public class TFrac : TANumber
    {

        public TPNumber Num { set; get; }

        public TPNumber Den { set; get; }

        public override string ToString()
        {
<<<<<<< HEAD
            return Converter.Convert(Num.Value, Num.P, Num.Acc, Delim) + FEditor.Delim +
                    Converter.Convert(Den.Value, Den.P, Den.Acc, Delim);
=======
            return Converter.Convert(Num.Value, Num.P, Num.Acc, FEditor.Delim) 
                + FEditor.Delim + Converter.Convert(Den.Value, Den.P, Den.Acc, FEditor.Delim);
>>>>>>> unintentional_branch
        }



        private int p;

        public int P
        {
            get { return p; }
            set
            {
                p = value;
                Num.P = value;
                Den.P = value;
            }
        }


        public TFrac() { Num = new TPNumber(0, 10, 5); Den = new TPNumber(1, 10, 5); }

        public TFrac(double n, double dn)
        {
            Num = new TPNumber(n, 10, 5); // или только с одним параметром?
            Den = new TPNumber(dn, 10, 5);
        }

        public TFrac(string num_str)
        {
            double n, d;
            int index = ToString().IndexOf(FEditor.Delim); 
            string[] f;

            f = ToString().Split(FEditor.Delim.ToCharArray());
            
            if (ToString() == FEditor.Delim)
            {
                Num = new TPNumber(0); Den = new TPNumber(1);
                SetNumStr(Num.Value.ToString() + FEditor.Delim + Den.Value.ToString());
            }
            else if (f.Length == 1)
            {
                if (Double.TryParse(f[0], out n)) Num = new TPNumber(n);
                if (f[0] == "")
                    Den = new TPNumber(1);
                else if (Double.TryParse(f[1], out d)) Den = new TPNumber(d);
                Reduce();
                SetNumStr(Num.Value.ToString() + FEditor.Delim + Den.Value.ToString());
            }
            else
            {
                Double.TryParse(ToString(), out n); Num = new TPNumber(n);
                Den = new TPNumber(1);
                SetNumStr(Num.Value.ToString() + FEditor.Delim + Den.Value.ToString());
            }

        }

        private void Reduce()
        {
            double gcd = GCD(Num.Value, Den.Value); if (gcd != 0)
            {
                Num.Value = Num.Value / gcd; Den.Value = Den.Value / gcd;
            }
        }

        private static double GCD(double n, double d)
        {
            n = Math.Abs(n); d = Math.Abs(d);
            while (d != 0 && n != 0)
            {
                if (n % d > 0)
                {
                    var temp = n; n = d;
                    d = temp % d;

                }
                else break;
            }
            if (d != 0 && n != 0) return d; return 0;
        }

        // обновить значение строки
        public void UpdateValueStr()
        {
            SetNumStr(Num.ToString() + Delim + Den.ToString());
        }


        //Выделить числитель из строки
        public string Get_num_from_string()
        {
            int index = ToString().IndexOf(FEditor.Delim);
            return ToString().Substring(0, index);
        }

        //Выделить знаменатель из строки
        public string Get_den_from_string()
        {
            int index = ToString().IndexOf(FEditor.Delim);
            return ToString().Substring(index + 1, ToString().Length - index + 1);
        }

        
        protected override TANumber Add(TANumber Den)
        {
            double c = this.Num.Value * (Den as TFrac).Den.Value +
            this.Den.Value * (Den as TFrac).Num.Value;
            double d = this.Den.Value * (Den as TFrac).Den.Value;

            double gcd = GCD(c, d);
            if (gcd != 0)
                return new TFrac(c / gcd, d / gcd);
            else
                return new TFrac(c, d);

        }

        protected override TANumber Multiply(TANumber Den)
        {
            double c = this.Num.Value * (Den as TFrac).Num.Value;
            double d = this.Den.Value * (Den as TFrac).Den.Value;

            double gcd = GCD(c, d);
            if (gcd != 0)
                return new TFrac(c / gcd, d / gcd);
            else
                return new TFrac(c, d);
        }

        protected override TANumber Substract(TANumber n)
        {
            double c = Num.Value * (n as TFrac).Den.Value - Den.Value * (n as TFrac).Num.Value;
            double d = Den.Value * (n as TFrac).Den.Value;

            double gcd = GCD(c, d);
            if (gcd != 0)
                return new TFrac(c / gcd, d / gcd);
            else
                return new TFrac(c, d);
        }

        protected override TANumber Divide(TANumber n)
        {
            double c = this.Num.Value * (n as TFrac).Den.Value;
            double d = this.Den.Value * (n as TFrac).Num.Value;

            double gcd = GCD(c, d);
            if (gcd != 0)
                return new TFrac(c / gcd, d / gcd);
            else
                return new TFrac(c, d);
        }

        protected virtual TANumber Inverse()
        {
            double c = Den.Value;
            double d = Num.Value;

            double gcd = GCD(c, d);
            if (gcd != 0)
                return new TFrac(c / gcd, d / gcd);
            else
                return new TFrac(c, d);
        }

        protected virtual TANumber Sqare()
        {
            double c = Num.Value * Num.Value;
            double d = Den.Value * Den.Value;

            double gcd = GCD(c, d);
            if (gcd != 0)
                return new TFrac(c / gcd, d / gcd);
            else
                return new TFrac(c, d);
        }

        protected TANumber Minus()
        {
            return this.Substract(new TFrac(0, 1));

        }

        protected override TANumber NeutralMul()
        {
            return new TFrac(1, 1);
        }
        public override object Clone()
        {
            return new TFrac(Num.Value, Den.Value);
        }

        public override void SetNumStr(string _Num)
        {
            int index = _Num.IndexOf(FEditor.Delim);

            Num = new TPNumber(_Num.Substring(0, index), 10, 5);
            Den = new TPNumber(_Num.Substring(index + 1, _Num.Length - index - 1), 10, 5);
        }
    }
}