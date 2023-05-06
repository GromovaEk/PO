using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    class FEditor : AEditor
    {
        public const string Zero = "0/1";
        public const string Delim = "/";

        public override bool isZero()
        {
            return (Str == Zero || Str == "-" + Zero);
        }

        public override void Add(char ch)
        {
            str.Append(ch);
        }

        public override void Add(string a)
        {
            str += a;
        }

        public override void AddSeparator()
        {
            if (Str.Substring(Str.Length - 1) != Delim)
                Add(Delim);
        }
        public override void AddSign(char sign)
        {
            if (Str.Last() != '+' && Str.Last() != '-' && Str.Last() != '/' && Str.Last() != '*')
            {
                str.Append(sign);
            }
            else
            {
                //str[str.Length - 1] = sign;
                str = str.Substring(0, str.Length - 1);
                str += sign;
            }
        }

        public override void AddMinusFront()
        {
            if (str[0] != '-')
                Str = "-" + Str;
            else
                Str = Str.Substring(1);
        }

        public override void AddDigit(int a)
        {
           
            string s = a.ToString();
            if (isZero())
            {
                if (Str.Length == 3)
                    
                    Str = s;
                else
                    Str = "-" + s;
            }
            else
            {
                str += s;
            }

        }

        public override void AddZero()
        {
            if (!isZero())
                str += Zero;
        }

        public override void Backspace()
        {
            if (!isZero())
            {
                str.Remove(Str.Length - 1, 1);
                if (Str.Length == 0)
                    Str = Zero;
            }
        }

        public override void Clear() { Str = Zero; }


        public FEditor() { str = Zero; }

        public void Edit() { }

    }
}