using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{

    public class TPNumber : TANumber
    {
        const char delim = ',';

        //public override string ValueStr
        //{
        //    get { return Converter.Convert(Value, p, acc, Delim); }
        //    private set;
        //}

        public override string ToString()
        {
            return Converter.Convert(Value, p, acc, Delim);
        }

        //public override string ValueStr { set; get; }


        private int p;
        public int P
        {
            get { return p; }
            set
            {
                if (value < 2 || value > 16)
                    throw new BaseException("Основание должно быть в [2; 16]\n");
                p = value;
            }
        }

        private int acc;


        public int Acc
        {
            get { return acc; }
            set { acc = value; }
        }

        //private double NumP;

        public double Value { set; get; } = 0.0;


        public TPNumber() { Value = 0.0; acc = 5; p = 10; Delim = ','; }

        public TPNumber(double num)
        {
            Value = num;

        }

        public TPNumber(string _Value, int _P, int _Accuracy) { Value = Converter.Convert(_Value, _P, Delim); p = _P; acc = _Accuracy; }

        public TPNumber(double _Num, int _P, int _Accuracy) { Value = _Num; p = _P; acc = _Accuracy; }

        public TPNumber(TPNumber d) 
        {
            Value = d.Value;
            P = d.P;
            Acc = d.Acc;
        }

        public override object Clone()
        {
            return new TPNumber(Value, P, Acc);
        }


        protected override TANumber NeutralMul()
        {
            return new TPNumber(1, P, Acc);
        }

        protected override TANumber Add(TANumber d)
        {
            if (P != (d as TPNumber).P) 
                throw new BaseException("Разные основания в operator+\n");
            double res = Value + (d as TPNumber).Value;
            if (double.IsInfinity(res))
                throw new OverflowException();
            return new TPNumber(res, P, Acc);
        }

        protected override TANumber Substract(TANumber d)
        {
            if (P != (d as TPNumber).P)
                throw new BaseException("Разные основания в operator-\n");
            double res = Value - (d as TPNumber).Value;
            if (double.IsInfinity(res))
                throw new OverflowException();
            return new TPNumber(res, P, Acc);
        }

        protected override TANumber Multiply(TANumber d)
        {
            if (P != (d as TPNumber).P)
                throw new BaseException("Разные основания в operator*\n");
            double res = Value * (d as TPNumber).Value;
            if (double.IsInfinity(res))
                throw new OverflowException();
            return new TPNumber(res, P, Acc);
        }



        protected override TANumber Divide(TANumber d)
        {
            if (P != (d as TPNumber).P)
                throw new BaseException("Разные основания в operator/\n");
            if ((d as TPNumber).Value == 0)
                throw new ArithmeticException("Ошибка: деление на ноль\n");
            double res = Value / (d as TPNumber).Value;
            if (double.IsInfinity(res))
                throw new OverflowException();
            return new TPNumber(res, P, Acc);
        }

        public string StrP()
        {
            return p.ToString();
        }

        public string StrNum()
        {
            return Value.ToString();
        }

        public string StrAcc()
        {
            return acc.ToString();
        }

        public void SetAccStr(string _Acc)
        {
            acc = Convert.ToInt32(_Acc);
        }

        public void SetPStr(string _P)
        {
            p = Convert.ToInt32(_P);
        }

        public override void SetNumStr(string _Num)
        {
            if(_Num.Length > 0)
            {
                if (_Num.First() == '-')
                {
                    Value = Converter.Convert(_Num.Substring(1), p, delim);
                    Value *= -1;
                }
                else
                    Value = Converter.Convert(_Num, p, delim);
            }
        }
    }
}
