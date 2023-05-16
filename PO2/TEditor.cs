using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    class PEditor : AEditor
    {
        public const string Zero = "0";
        public const string Delim = ",";


        public override bool isZero()
        {
            return (Str == Zero || Str == "-" + Zero);
        }

        public override void Add(char ch) 
        {
            Str.Append(ch); 
        }

        public override void Add(string a)
        {
            Str += a;
        }

        public override void AddSeparator()
        {
            if (Str.Substring(Str.Length - 1) != Delim)
                Add(Delim);
        }
        public override void AddSign(char sign) 
        {
            if (Str.Last() != '+' && Str.Last() != '-' && Str.Last() != ':' && Str.Last() != '*')
            {
                Str += sign;
            }
            else
            {
                //str[str.Length - 1] = sign;
                Str = Str.Substring(0, Str.Length - 1);
                Str += sign;

            }
        }

        public override void AddMinusFront()
        {
            if (Str[0] != '-')
                Str = "-" + Str;
            else
                Str = Str.Substring(1);
        }

        public override void AddDigit(int a) 
        {
            if (isZero())
            {
                if (Str.Length == 1)
                    Str = "";
                else
                    Str = "-";
            }

            char ch = Converter.longToChar(a);
            Str += ch;
        }



        public override void AddZero() 
        {
            if (!isZero())
                Str += Zero; 
        }

        public override void Backspace() 
        {
            if (!isZero())
            {
                Str = Str.Remove(Str.Length - 1, 1);
                
                if (Str.Length == 0)
                    Str = Zero;
                else if(Str == "-")
                    Str = Zero;
            }
        }

        public override void Clear() { Str = Zero;  }
   

        public PEditor() { Str = Zero; }

        public void Edit() { }

    }
}
