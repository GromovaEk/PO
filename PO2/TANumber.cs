using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    public abstract class TANumber : ICloneable
    {
        //public abstract string ValueStr { get; set; }

        public abstract override string ToString();

        public static char Delim { get; set; }


        //public TANumber() { }

        protected abstract TANumber Add(TANumber d);

        protected abstract TANumber Substract(TANumber d);

        protected abstract TANumber Multiply(TANumber d);

        protected abstract TANumber Divide(TANumber d);

        public abstract object Clone();


        public static TANumber operator +(TANumber d1, TANumber d2) { return d1.Add(d2); }

        public static TANumber operator -(TANumber d1, TANumber d2) { return d1.Substract(d2); }

        public static TANumber operator *(TANumber d1, TANumber d2) { return d1.Multiply(d2); }

        public static TANumber operator /(TANumber d1, TANumber d2) { return d1.Divide(d2); }

        /// <summary>
        /// Возвращает нейтральный по умножению элемент
        /// </summary>
        /// <returns></returns>
        protected abstract TANumber NeutralMul();

        public virtual TANumber Sqare()
        {
            return Multiply(this);
        }

        public virtual TANumber Inverse()
        {
            return NeutralMul().Divide(this);
        }

        public abstract void SetNumStr(string _Num);

    }
}
