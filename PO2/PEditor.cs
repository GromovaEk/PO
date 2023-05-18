using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    class PEditor : AEditor
    {
        public const string Zero = "0";
        public const string Delim = ",";


        public override bool IsZero()
        {
            return (Str == Zero || Str == "-" + Zero);
        }

        private bool IsSign(char ch)
        {
            return (ch == '-' || ch == '+' || ch == '*'
                || ch == '/');
        }

        public override bool LastIsSign()
        {
            return IsSign(Str.Last());
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
            if (IsZero())
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
            if (!IsZero())
                Str += Zero; 
        }

        public override void Backspace() 
        {
            if (!IsZero())
            {
                Str = Str.Remove(Str.Length - 1, 1);
                
                if (Str.Length == 0)
                    Str = Zero;
                else if(Str == "-")
                    Str = Zero;
            }
        }

        public override void PopLastNumber()
        {
            if (LastIsSign())
                Backspace();
            else
            {
                while (!LastIsSign())
                    Backspace();
            }
        }

        public override string GetLastNumber()
        {
            int i = Str.Length - 1;
            StringBuilder sb = new StringBuilder();
            while (!IsSign(Str[i]))
            {
                sb.Append(Str[i]);
                i--;
            }
            return sb.ToString();
        }

        public override void Clear() { Str = Zero;  }
   

        public PEditor() { Str = Zero; }

        public void Edit() { }


    }
}
