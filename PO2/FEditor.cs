﻿using System;
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

        public enum FractionStates { numerator = 0, denominator }
        FractionStates FractionState { get; set; }

        // состояние тут

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
            if(FractionState == FractionStates.numerator)
            {
                FractionState = FractionStates.denominator;
                Str = Str.Remove(Str.Length - 1);
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
            if (isZero())
            {
                if (Str.Length == 3)
                    Str = s + "/1";
                else
                    Str = "-" + s + "/1";
            }
            else
            {
                if(FractionState == FractionStates.numerator)
                {
                    if (Str.IndexOf("/1", Str.Length - 2) == Str.Length - 2) // если последние 2 сивола - единичный знаметель - удалить их 
                        Str = Str.Remove(Str.Length - 2);
                    Str += s + "/1";

                    // добавлять символ
                    // добавлять единичный знаменатель


                    //int index = Str.IndexOf(Delim);
                    //Str = Str.Substring(0, index) + s + "/1";
                }
                else
                {
                    if(!(Str.Last() == '/' && s == "0")) // случай ввода знаменателя = 0
                        Str += s;
                    
                }
                    
            }
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
                if(Str.Last() == '+' || Str.Last() == '-' || Str.Last() == '*' || Str.Last() == ':')
                    Str = Str.Remove(Str.Length - 1, 1);
                else if (FractionState == FractionStates.numerator)
                {
                    Str = Str.Remove(Str.Length - 3, 1);

                    // если удалили всё число
                    if (Str.Length == 2 || Str.Length == 3 && Str.First()=='-')
                    {
                        Str = Zero;
                    }
                    else
                    {
                        // если удалили всё крайнее число
                        string last = Str.Substring(Str.Length - 3, 1);
                        if (last == "+" || last == "-" || last == "*" || last == ":")
                            Str = Str.Remove(Str.Length - 2, 2);
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

        public override void Clear() { Str = Zero; FractionState = FractionStates.numerator; }

        public FEditor() { Str = Zero; }

        public void Edit() { }

    }
}