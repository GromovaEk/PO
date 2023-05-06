using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    public class TFrac : TANumber
    {
        private int num, den; // числитель и знаменатель

        public int Num
        {
            get { return num; }
            set { num = value; }
        }

        public int Den
        {
            get { return num; }
            set { den = value; }
        }

        public override string ValueStr
        {
            get { return valuestr; }
            set { valuestr = value; }
        }

        public TFrac() { num = 0; den = 1;}

        public TFrac(int n, int dn)
        {
            num = n;
            den = dn;
        }

        // обновить значение строки
        public void updateValueStr()
        {
            ValueStr = Num.ToString() + Delim + Den.ToString();
        }

        // Получить числитель

        //ПОлучить знаменатель

        //Выделить числитель из строки

        //Выделить знаменатель из строки

        // Числ и Знаменатель перевести в строку
        protected override TANumber Add(TANumber den)
        {
            int c = this.num * (den as TFrac).den +
            this.den * (den as TFrac).num;
            int d = this.num * (den as TFrac).den;
            return new TFrac(c, d);
        }

        protected override TANumber Multiply(TANumber den)
        {
            int c = this.num * (den as TFrac).den +
            this.den * (den as TFrac).num;
            int d = this.num * (den as TFrac).den;
            return new TFrac(c, d);
        }

        protected override TANumber Substract(TANumber n)
        {
            return new TFrac(num * (n as TFrac).den - den * (n as TFrac).num, den * (n as TFrac).den);
        }

        protected override TANumber Divide(TANumber n)
        {
            return new TFrac(num * (n as TFrac).den, den * (n as TFrac).num);
        }

        protected virtual TANumber Inverse()
        {
            return new TFrac(den, num);
        }

        protected virtual TANumber Sqare()
        {
            return new TFrac(num * num, den * den);
        }

        protected override TANumber NeutralMul()
        {
            return new TFrac(1, 1);
        }

        public override object Clone()
        {
            return new TFrac(Num, Den);
        }
    }
}