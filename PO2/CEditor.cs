using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    class CEditor : AEditor
    {
        public static string Delim = ";";

        public static string Zero
        {
            get { return ConstructNumStr("0", "0");  }
            //private set { }
        }

        public const string lbrace = "[";
        public const string rbrace = "]";

        private string realStr;
        private string imStr;

        private static string ConstructNumStr(string real, string im)
        {
            return lbrace + real + Delim + " " + im + "i" + rbrace;
        }



        public enum ComplexStates { real = 0, imaginary }
        ComplexStates ComplexState { get; set; }

        public override bool IsZero()
        {
            return (Str == Zero);
        }

        public bool IsSign(char ch)
        {
            return (ch == '+' || ch == '-' || ch == '*' || ch == '/');
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
            if (ComplexState == ComplexStates.real)
            {
                ComplexState = ComplexStates.imaginary;
                imStr = "";
            }
            PopLastNumber();
            Str += ConstructNumStr(realStr, imStr);
        }

        public override void AddSign(char sign)
        {
            if (!LastIsSign())
                Str += sign;
            else
            {
                Str = Str.Substring(0, Str.Length - 1);
                Str += sign;
            }

            ComplexState = ComplexStates.real;
        }

        public override void AddMinusFront() // вообще убрать
        {
            if (Str[0] != '-')
                Str = "-" + Str;
            else
                Str = Str.Substring(1);
        }

        public override void AddDigit(int a)
        {
            
            string s = Converter.longToChar(a).ToString();

            if (LastIsSign())
            {
                Str += ConstructNumStr(s, "0");
                realStr = s;
                imStr = "0";
            }
            else
            {
                PopLastNumber();
                if (ComplexState == ComplexStates.real)
                {
                    if (realStr != "0")
                        realStr += s;
                    else
                        realStr = s;
                }
                    
                else
                {
                    if (imStr != "0")
                        imStr += s;
                    else
                        imStr = s;
                }

                Str += ConstructNumStr(realStr, imStr);
            }



            //if (IsZero())
            //{
            //    if (Str.Length == 5)
            //        Str = s + "+i*0";
            //    else
            //        Str = "-" + s + "+i*0";
            //}
            //else
            //{
            //    if (ComplexState == ComplexStates.real)
            //    {
            //        if (Str.IndexOf("+i*0", Str.Length - 4) == Str.Length - 4) // если последние 4 сивола - нулевая мнимая часть - удалить их 
            //            Str = Str.Remove(Str.Length - 4);
            //        Str += s + "+i*0";

            //        // добавлять символ
            //        // добавлять единичный знаменатель

            //        //int index = Str.IndexOf(Delim);
            //        //Str = Str.Substring(0, index) + s + "/1";
            //    }
            //    else
            //    {
            //        if (!(Str.IndexOf("+i*0", Str.Length - 4) == Str.Length - 4 && s == "0")) // случай ввода мнимой части = 0, при этом же значении мнимой части
            //            Str += s;
            //    }
            //}


            
        }

        public override void AddZero()
        {
            if (!IsZero())
                Str += Zero;
        }

        public override void Backspace()
        {
            if (LastIsSign())
                Str = Str.Substring(0, Str.Length - 1);
            else
            {
                PopLastNumber();
                if (ComplexState == ComplexStates.real)
                {
                    if (realStr.Length == 1)
                    {
                        if (realStr != "0")
                            realStr = "0";
                        else
                            return;
                    }
                        
                    else
                        realStr = realStr.Substring(0, realStr.Length - 1);
                }
                else
                {
                    if (imStr.Length == 1)
                    {
                        imStr = "0";
                        ComplexState = ComplexStates.real;
                    }
                    else
                        imStr = imStr.Substring(0, imStr.Length - 1);
                }
                Str += ConstructNumStr(realStr, imStr);
            }

        }

        public override void Clear() { Str = Zero; ComplexState = ComplexStates.real; }

        public CEditor() { Str = Zero; }

        public void Edit() { }

        public override void PopLastNumber()
        {
            int ind = Str.LastIndexOf("[");
            //if (ind != 0)
            Str = Str.Substring(0, ind);
        }

        public override string GetLastNumber()
        {
            int ind = Str.LastIndexOf("[");
            if (!LastIsSign())
                return Str.Substring(ind);
            else
                return Str.Substring(0, Str.Length - 1);
        }

        public override bool LastIsSign()
        {
            return IsSign(Str.Last());
        }
    }
}