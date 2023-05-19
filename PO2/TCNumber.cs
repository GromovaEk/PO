using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    public class TCNumber : TANumber
    {
        public TPNumber Re { set; get; }
        public TPNumber Im { set; get; }


        private int p;

        public int P
        {
            get { return p;  }
            set
            {
                Re.P = value;
                Im.P = value;
                p = value;
            }
        }

        private int acc;

        public int Acc
        {
            get { return acc; }
            set
            {
                acc = value;
                Re.Acc = acc;
                Im.Acc = acc;
            }
        }

        public override string ToString()
        {
            return CEditor.lbrace + Converter.Convert(Re.Value, Re.P, Re.Acc, CEditor.Delim) 
                + CEditor.Delim + " "  
                + Converter.Convert(Im.Value, Im.P, Im.Acc, CEditor.Delim) 
                + "i" + CEditor.rbrace;
        }

        public TCNumber() 
        {
            p = 10;
            acc = 5;
            Re = new TPNumber(0, p, acc); 
            Im = new TPNumber(0, p, acc); 
        }

        public TCNumber(double n, double dn)
        {
            Re = new TPNumber(n, 10, 5); 
            Im = new TPNumber(dn, 10, 5);
        }

        public TCNumber(double n, double dn, int _p, int _acc)
        {
            p = _p;
            acc = _acc;
            Re = new TPNumber(n, _p, _acc);
            Im = new TPNumber(dn, _p, _acc);
        }

        protected override TANumber Add(TANumber b)
        {
            double c = this.Re.Value + (b as TCNumber).Re.Value;
            double d = this.Im.Value + (b as TCNumber).Im.Value;
            var result = new TCNumber(c, d, p, acc);
            return result;
        }

        protected override TANumber Multiply(TANumber b)
        {
            double c = this.Re.Value * (b as TCNumber).Re.Value - this.Im.Value * (b as TCNumber).Im.Value;
            double d = this.Re.Value * (b as TCNumber).Im.Value + (b as TCNumber).Re.Value * this.Im.Value;
            var result = new TCNumber(c, d, p, acc);
            return result;
        }

        protected override TANumber Substract(TANumber b)
        {
            double c = this.Re.Value - (b as TCNumber).Re.Value;
            double d = this.Im.Value - (b as TCNumber).Im.Value;
            var result = new TCNumber(c, d, p, acc);
            return result;
        }

        protected override TANumber Divide(TANumber b)
        {
            double c = (this.Re.Value * (b as TCNumber).Re.Value + this.Im.Value * (b as TCNumber).Im.Value) / 
                (Math.Pow((b as TCNumber).Re.Value, 2) + Math.Pow((b as TCNumber).Im.Value, 2));
            double d = (this.Im.Value  * (b as TCNumber).Re.Value - this.Re.Value * (b as TCNumber).Im.Value) /
                (Math.Pow((b as TCNumber).Re.Value, 2) + Math.Pow((b as TCNumber).Im.Value, 2));

            var result = new TCNumber(c, d, p, acc);
            return result;
        }

        protected override TANumber NeutralMul()
        {
            return new TCNumber(1, 0);
        }

        public override object Clone()
        {
            return new TCNumber(Re.Value, Im.Value);
        }

        protected TANumber Minus()
        {
            return this.Substract(new TCNumber(0, 0));
        }

        public override void SetNumStr(string _Num)
        {
            int start = _Num.IndexOf(CEditor.Delim);
            

            Re = new TPNumber(_Num.Substring(1, start - 1), p, acc);
            Im = new TPNumber(_Num.Substring(start + 2, _Num.Length - start - 4), p, acc);
        }
    }
}