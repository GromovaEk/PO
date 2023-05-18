using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    public class TComp : TANumber
    {
        public TPNumber Re { set; get; } = new TPNumber(0, 10, 5);
        public TPNumber Im { set; get; } = new TPNumber(0, 10, 5);

        public override string ValueStr
        {
            set { ValueStr = value; }
            get
            {
                return Converter.Convert(Re.Value, Re.P, Re.Acc, Delim) + "+i*" +
                    Converter.Convert(Im.Value, Im.P, Im.Acc, Delim);
                //return Num.Value.ToString() + "/" + Den.Value.ToString(); 
            }
        }

        public TComp() { Re = new TPNumber(0, 10, 5); Im = new TPNumber(0, 10, 5); }

        public TComp(double n, double dn)
        {
            Re = new TPNumber(n, 10, 5); // или только с одним параметром?
            Im = new TPNumber(dn, 10, 5);
        }
        /*
        public TComp(string num_str)
        {
            double n, d;
            int index = ValueStr.IndexOf("i*");
            string[] f;

            f = ValueStr.Split('/');

            if (ValueStr == "/")
            {
                Num = new TPNumber(0); Den = new TPNumber(1);
                ValueStr = Num.Value.ToString() + "/" + Den.Value.ToString();
            }
            else if (f.Length == 1)
            {
                if (Double.TryParse(f[0], out n)) Num = new TPNumber(n);
                if (f[0] == "")
                    Den = new TPNumber(1);
                else if (Double.TryParse(f[1], out d)) Den = new TPNumber(d);
                Reduce();
                ValueStr = Num.Value.ToString() + "/" + Den.Value.ToString();
            }
            else
            {
                Double.TryParse(ValueStr, out n); Num = new TPNumber(n);
                Den = new TPNumber(1);
                ValueStr = Num.Value.ToString() + "/" + Den.Value.ToString();
            }

        }*/

        protected override TANumber Add(TANumber b)
        {
            double c = this.Re.Value + (b as TComp).Re.Value;
            double d = this.Im.Value + (b as TComp).Im.Value;
            return new TComp(c, d);
        }

        protected override TANumber Multiply(TANumber b)
        {
            double c = this.Re.Value * (b as TComp).Re.Value - this.Im.Value * (b as TComp).Im.Value;
            double d = this.Re.Value * (b as TComp).Im.Value + (b as TComp).Re.Value * this.Im.Value;
            return new TComp(c, d);
        }

        protected override TANumber Substract(TANumber b)
        {
            double c = this.Re.Value - (b as TComp).Re.Value;
            double d = this.Im.Value - (b as TComp).Im.Value;
            return new TComp(c, d);
        }

        protected override TANumber Divide(TANumber b)
        {
            double c = (this.Re.Value * (b as TComp).Re.Value + this.Im.Value * (b as TComp).Im.Value) / 
                (Math.Pow((b as TComp).Re.Value, 2) + Math.Pow((b as TComp).Im.Value, 2));
            double d = (this.Im.Value  * (b as TComp).Re.Value - this.Re.Value * (b as TComp).Im.Value) /
                (Math.Pow((b as TComp).Re.Value, 2) + Math.Pow((b as TComp).Im.Value, 2));

            return new TComp(c, d);
        }

        protected virtual TANumber Inverse()
        {
            double c = this.Re.Value / (Math.Pow(this.Re.Value, 2) + Math.Pow(this.Im.Value, 2));
            double d = -this.Im.Value / (Math.Pow(this.Re.Value, 2) + Math.Pow(this.Im.Value, 2));

            return new TComp(c, d);
        }

        protected virtual TANumber Sqare()
        {
            return this.Multiply(this);
        }

        protected override TANumber NeutralMul()
        {
            return new TComp(1, 0);
        }

        public override object Clone()
        {
            return new TComp(Re.Value, Im.Value);
        }

        protected TANumber Minus()
        {
            return this.Substract(new TComp(0, 0));
        }
        public override void SetNumStr(string _Num)
        {
            int index = _Num.IndexOf("+i*");

            Re = new TPNumber(_Num.Substring(0, index), 10, 5);
            Im = new TPNumber(_Num.Substring(index + 1, _Num.Length - index - 1), 10, 5);
        }
    }
}