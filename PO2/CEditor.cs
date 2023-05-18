using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    class CEditor : AEditor
    {
        public const string Zero = "0+i*0"; //??
        public const string Delim = "+i*";

        public enum ComplexStates { real = 0, imaginary }
        ComplexStates ComplexState { get; set; }

        public override bool IsZero()
        {
            return (Str == Zero || Str == "-" + Zero); //????
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
                Str = Str.Remove(Str.Length - 1); /// ???????
            }
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
            //string s = a.ToString();


            if (IsZero())
            {
                if (Str.Length == 5)
                    Str = s + "+i*0";
                else
                    Str = "-" + s + "+i*0";
            }
            else
            {
                if (ComplexState == ComplexStates.real)
                {
                    if (Str.IndexOf("+i*0", Str.Length - 4) == Str.Length - 4) // если последние 4 сивола - нулевая мнимая часть - удалить их 
                        Str = Str.Remove(Str.Length - 4);
                    Str += s + "+i*0";

                    // добавлять символ
                    // добавлять единичный знаменатель

                    //int index = Str.IndexOf(Delim);
                    //Str = Str.Substring(0, index) + s + "/1";
                }
                else
                {
                    if (!(Str.IndexOf("+i*0", Str.Length - 4) == Str.Length - 4 && s == "0")) // случай ввода мнимой части = 0, при этом же значении мнимой части
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
                if (Str.Last() == '+' || Str.Last() == '-' || Str.Last() == '*' || Str.Last() == ':')
                    Str = Str.Remove(Str.Length - 1, 1);
                else if (ComplexState == ComplexStates.real)
                {
                    Str = Str.Remove(Str.Length - 5, 1);

                    // если удалили всё число
                    if (Str.Length == 4 || Str.Length == 5 && Str.First() == '-')
                    {
                        Str = Zero;
                    }
                    else
                    {
                        // если удалили всё крайнее число
                        string last = Str.Substring(Str.Length - 5, 1);
                        if (last == "+" || last == "-" || last == "*" || last == ":")
                            Str = Str.Remove(Str.Length - 5, 4);
                    }
                }
                else
                {
                    if (Str.Last().ToString() == Delim)
                    {
                        ComplexState = ComplexStates.real;
                        Str = Str + "0";
                        Backspace();
                    }
                    else
                        Str = Str.Remove(Str.Length - 1, 1);
                }
            }
            
        }

        public override void Clear() { Str = Zero; ComplexState = ComplexStates.real; }

        public CEditor() { Str = Zero; }

        public void Edit() { }

        public override void PopLastNumber()
        {
            throw new NotImplementedException();
        }

        public override string GetLastNumber()
        {
            throw new NotImplementedException();
        }

        public override bool LastIsSign()
        {
            throw new NotImplementedException();
        }
    }
}