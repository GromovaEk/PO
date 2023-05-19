using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    class FEditor : AEditor
    {
        public static string Delim = "|";
        
        public static string Zero
        {
            get { return "0" + Delim + "1"; }
            //private set { }
        }

        public enum FractionStates { numerator = 0, denominator }
        FractionStates FractionState { get; set; }
        

        public override bool IsZero()
        {
            return (Str == Zero || Str == "-" + Zero);
        }

        private bool IsSign(char ch)
        {
            return (ch == '-' || ch == '+' || ch == '*'
                || ch == ':');
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
            if(FractionState == FractionStates.numerator)
            {
                FractionState = FractionStates.denominator;
                Str = Str.Remove(Str.Length - 1);
            }
        }
        public override void AddSign(char sign)
        {
            if (!LastIsSign())
                Str += sign;
            else
            {
                //str[str.Length - 1] = sign;
                Str = Str.Substring(0, Str.Length - 1);
                Str += sign;
            }

            FractionState = FractionStates.numerator;
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
            string s = Converter.longToChar(a).ToString();
            //string s = a.ToString();
            if (IsZero())
            {
                if (Str.Length == 3)
                    Str = s + Delim + "1";
                else
                    Str = "-" + s + Delim + "1";
            }
            else
            {
                if(FractionState == FractionStates.numerator)
                {
                    if (Str.IndexOf(Delim + "1", Str.Length - 2) == Str.Length - 2) // если последние 2 сивола - единичный знаметель - удалить их 
                        Str = Str.Remove(Str.Length - 2);
                    Str += s + Delim + "1";

                    // добавлять символ
                    // добавлять единичный знаменатель

                    //int index = Str.IndexOf(Delim);
                    //Str = Str.Substring(0, index) + s + "/1";
                }
                else
                {
                    if(!(Str.Last() == '|' && s == "0")) // случай ввода знаменателя = 0
                        Str += s;
                    
                }
                    
            }
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
                if(LastIsSign())
                    Str = Str.Remove(Str.Length - 1, 1);
                else if (FractionState == FractionStates.numerator)
                {
                    Str = Str.Remove(Str.Length - 3, 1);

                    // если удалили всё число
                    if (Str.Length == 2 || Str.Length == 3 && Str.First()=='-')
                    {
                        Str = Zero;
                    }
                    else if (Str.Length > 3)
                    {
                        // если удалили всё крайнее число
                        Str = Str.Substring(0, Str.Length - 2);
                    }
                }
                else
                {
                    if (Str.Last().ToString() == Delim)
                    {
                        FractionState = FractionStates.numerator;
                        Str = Str + "1";
                        Backspace();
                    }
                    else
                        Str = Str.Remove(Str.Length - 1, 1);
                }
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

        public override void Clear() { Str = Zero; FractionState = FractionStates.numerator; }

        public FEditor() { Str = Zero; }

        public void Edit() { }

        public override string GetLastNumber()
        {
            int i = Str.Length - 1;
            StringBuilder sb = new StringBuilder();
            while (!IsSign(Str[i]))
            {
                sb.Insert(0, Str[i]);
                i--;
            }
            return sb.ToString();
        }
    }
}