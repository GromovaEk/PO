using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    class TProc<T> where T : TANumber, new()
    {
        public enum Operations { None = '\0', Add = '+', Sub = '-', Mul = '*', Dvd = '/' }

        public enum Functions { None = 0, Inv, Sqr }
        public Operations Operation { get;  set; }
        public Functions Function { get;  set; }

        public TProc()
        {
            Clear();
        }

        public void Clear()
        {
            Lop_Res = new T();
            Rop = new T();
            ResetFunc();
            ResetOp();
        }

        public void ResetOp()
        {
            Operation = Operations.None;
        }

        public void ResetFunc()
        {
            Function = Functions.None;
        }

        public void Reset()
        {
            ResetOp();
            ResetFunc();
        }

        public void ExecOperation()
        {
            switch (Operation)
            {
                case Operations.None:
                    return;
                case Operations.Add:
                    Lop_Res = (T)(Lop_Res + Rop);
                    break;
                case Operations.Sub:
                    Lop_Res = (T)(Lop_Res - Rop);
                    break;
                case Operations.Mul:
                    Lop_Res = (T)(Lop_Res * Rop);
                    break;
                case Operations.Dvd:
                    Lop_Res = (T)(Lop_Res / Rop);
                    break;
            }
        }

        public void ExecFunction()
        {
            switch(Function)
            {
                case Functions.Inv:
                    Lop_Res = (T)(Lop_Res.Inverse());
                    break;
                case Functions.Sqr:
                    Lop_Res = (T)(Lop_Res.Sqare());
                    break;
            }
        }

        public T Lop_Res { get; set; }
        
        public T Rop { get; set; }


    }
}
