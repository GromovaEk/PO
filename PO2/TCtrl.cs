using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    public class TCtrl<T> where T : TANumber, new()
    {
        public enum States { l_val = 0, op, r_val }
        
        public bool floatMode;

        private const string funcInv = "^(-1)";
        private const string funcSqr = "^2";
        public AEditor Editor { get; set; }
        TProc<T> Processor { get; set; }
        public TMemory<T> Memory { get; set; }

        States State { get; set; }
        
        int P = 10;
        int Acc = 5;
        T Number { get; set; }
        string number;

        private static char ConvertToOperator(int i)
        {
            switch (i)
            {
                case 26:
                    return '+';
                case 27:
                    return '-';
                case 28:
                    return '*';
                case 29:
                    return ':';
                default:
                    return '\0';
            }
        }

        private static bool IsOperator(char ch)
        {
            return (ch == '-' || ch == '+' || ch == '*' || ch == ':');
        }

        public TCtrl()
        {
            if (typeof(T).Name == "TPNumber")
                Editor = new PEditor();
            else if (typeof(T).Name == "TFrac")
                Editor = new FEditor();
            else if (typeof(T).Name == "TComp")
                Editor = new CEditor();

            Processor = new TProc<T>();
            Memory = new TMemory<T>();
            State = States.l_val;
            Number = new T();
            floatMode = false;
        }

        public void ClearAll()
        {
            Editor.Str = PEditor.Zero;
            Processor.Clear();
            State = States.l_val;
        }

        public string Command(int i)
        {
            // Ввод цифры
            if (0 <= i && i <= 15)
            {
                //if (i == 0)
                    //Editor.AddZero();
                //else
                    Editor.AddDigit(i);
                    
                    
                if (State == States.l_val)
                Processor.Lop_Res.SetNumStr(Editor.Str);
                
                else
                {
                    int start;

                    if (typeof(T).Name == "TComp" && ((char)Processor.Operation=='+' || (char)Processor.Operation == '*')) // ищем второе вхождение + или * с конца строки
                    {
                        int start0 = Editor.Str.LastIndexOf((char)Processor.Operation);
                        start = Editor.Str.LastIndexOf((char)Processor.Operation, start0 - 1);
                    }
                    else 
                        start = Editor.Str.IndexOf((char)Processor.Operation, 1);

                    string tmp = Editor.Str.Substring(start + 1);
                    Processor.Rop.SetNumStr(tmp);
                    State = States.r_val;
                }
                number = Editor.Str;
                return Editor.Str;
            }

            // Добавить в память
            if (i == 16)
            {
                if (Memory.FState)
                {
                    if (State == States.l_val)
                        Memory.Add(Processor.Lop_Res);
                }
                return Editor.Str;
            }

            // Сохранить в память
            if (i == 17)
            {
                if (Memory.FState)
                {
                    if (State == States.l_val)
                        Memory.FNumber = (T)Processor.Lop_Res.Clone();
                }
                return Editor.Str;
            }

            // Взять из памяти
            if (i == 18)
            {
                if (Memory.FState)
                {
                    if (State == States.l_val)
                    {
                        Editor.Str = Memory.GetStr();
                        Processor.Lop_Res = (T)Memory.FNumber.Clone();
                    }
                    else if (State == States.op)
                    {
                        Editor.Str += Memory.FNumber.ValueStr;
                        Processor.Rop = (T)Memory.FNumber.Clone();
                        State = States.r_val;
                    }                 
                }
                return Editor.Str;
            }

            // Очистить память
            if (i == 19)
            {
                Memory.Clear();
                return Editor.Str;
            }


            //CE
            if (i == 21)
            {
                State = States.l_val;
                Processor.Clear();
                Editor.Clear();
                return Editor.Str;
            }

            // Backspace
            if (i == 22)
            {
                States oldState = State;
                Editor.Backspace();
                if (oldState == States.r_val)
                {
                    if (Editor.LastIsSign())
                    {
                        State = States.op;
                        Processor.Rop = new T();

                        if (typeof(T).Name == "TPNumber")
                        {
                            (Processor as TProc<TPNumber>).Rop.Acc = Acc;
                            (Processor as TProc<TPNumber>).Rop.P = P;
                        }
                    }
                    else
                        Processor.Rop.SetNumStr(Editor.GetLastNumber()); 
                }
                else if (oldState == States.op)
                    Processor.Reset();
                else
                    Processor.Lop_Res.SetNumStr(Editor.Str);

                return Editor.Str;
            }

            // Смена знака
            if (i == 23)
            {
                if(!Editor.IsZero())
                {
                    if (Processor.Lop_Res.ValueStr.First() != '-')
                        Processor.Lop_Res.SetNumStr("-" + Processor.Lop_Res.ValueStr);
                    else
                        Processor.Lop_Res.SetNumStr(Processor.Lop_Res.ValueStr.Substring(1));
                    Editor.AddMinusFront();
                }

                if (!Editor.IsZero())
                {

                }


                return Editor.Str;
            }

            // Ввод разделителя
            if (i == 24)
            {
                if (typeof(T).Name == "TPNumber")
                    if (!floatMode)
                        return Editor.Str;

                if (State == States.l_val || State == States.r_val)
                    Editor.AddSeparator();
                                
                return Editor.Str;
            }

            // Ввод знака равенства
            if (i == 25)
            {
                try
                {
                    Processor.Exec();
                    Editor.Str = Processor.Lop_Res.ValueStr;
                    State = States.l_val;
                    return Editor.Str;
                }
                catch (Exception e)
                {
                    ClearAll();
                    return e.Message;
                }
            }


            // Ввод оператора
            if (26 <= i && i <= 29)
            {
                char ch = ConvertToOperator(i);
                if (State == States.l_val || State == States.op)
                {                  
                    Editor.AddSign(ch);
                    Processor.Operation = (TProc<T>.Operations)ch;
                    //Processor.ResetFunc();
                    State = States.op;
                }
                else
                {
                    Processor.Exec();
                    Editor.Str = Processor.Lop_Res.ValueStr;
                    Editor.AddSign(ch);
                    State = States.op;
                    Processor.Operation = (TProc<T>.Operations)ch;
                }
                return Editor.Str;
            }


            // Возведение в квадрат
            if (i == 30)
            {
                try
                {
                    if (State == States.l_val)
                    {
                        Processor.Function = TProc<T>.Functions.Sqr;
                        Processor.Exec();
                        Editor.Str = Processor.Lop_Res.ValueStr;
                    }
                    else if (State == States.r_val)
                    {
                        var oldOperation = Processor.Operation;
                        Processor.Function = TProc<T>.Functions.Sqr;
                        T temp = (T)Processor.Lop_Res.Clone();
                        Processor.Lop_Res = (T)Processor.Rop.Clone();
                        Processor.ExecFunction();
                        Editor.PopLastNumber();
                        Editor.Str += Processor.Lop_Res.ValueStr;
                        Processor.Rop = (T)Processor.Lop_Res.Clone();
                        Processor.Lop_Res = temp;
                        Processor.Operation = oldOperation;
                    }
                }
                catch (Exception e)
                {
                    ClearAll();
                    return e.Message;
                }

                return Editor.Str;
            }

            // Инверсия числа
            if (i == 31)
            {
                try
                {
                    if (State == States.l_val)
                    {
                        Processor.Function = TProc<T>.Functions.Inv;
                        Processor.Exec();
                        Editor.Str = Processor.Lop_Res.ValueStr;
                    }
                    else if (State == States.r_val)
                    {
                        var oldOperation = Processor.Operation;
                        Processor.Function = TProc<T>.Functions.Inv;
                        T temp = (T)Processor.Lop_Res.Clone();
                        Processor.Lop_Res = (T)Processor.Rop.Clone();
                        Processor.ExecFunction();
                        Editor.PopLastNumber();
                        Editor.Str += Processor.Lop_Res.ValueStr;
                        Processor.Rop = (T)Processor.Lop_Res.Clone();
                        Processor.Lop_Res = temp;
                        Processor.Operation = oldOperation;
                    }
                }
                catch (Exception e)
                {
                    ClearAll();
                    return e.Message;
                }

                return Editor.Str;
            }            

            // Изменение основания системы счисления
            if (32 <= i && i <= 46)
            {
                P = i - 30;
                if (typeof(T).Name == "TPNumber")
                {
                    (Processor as TProc<TPNumber>).Lop_Res.P = P;
                    (Processor as TProc<TPNumber>).Rop.P = P;
                    Editor.Str = Processor.Lop_Res.ValueStr;
                    if (State != States.l_val)
                    {
                        Editor.Str += (char)Processor.Operation;
                        if (State == States.r_val)
                            Editor.Str += Processor.Rop.ValueStr;
                    }
                    if (Memory.FState)
                    {
                        (Memory as TMemory<TPNumber>).FNumber.P = P;
                    }
                    return Editor.Str;
                }
                //else if (typeof(T).Name == "TFrac")
                //{
                //    P = i - 30;

                //    (Processor as TProc<TFrac>).Lop_Res.Num.P = P;
                //    (Processor as TProc<TFrac>).Lop_Res.Den.P = P;

                //    (Processor as TProc<TFrac>).Rop.Num.P = P;
                //    (Processor as TProc<TFrac>).Rop.Den.P = P;

                //    Editor.Str = Processor.Lop_Res.ValueStr;
                //    if (State != States.l_val)
                //    {
                //        Editor.Str += (char)Processor.Operation;
                //        if (State == States.r_val)
                //            Editor.Str += Processor.Rop.ValueStr;
                //    }
                //    if (Memory.FState)
                //    {
                //        (Memory as TMemory<TFrac>).FNumber.Num.P = P;
                //        (Memory as TMemory<TFrac>).FNumber.Den.P = P;
                //    }
                //    return Editor.Str;
                //}
                //else if (typeof(T).Name == "TComp")
                //{
                //    P = i - 30;

                //    (Processor as TProc<TComp>).Lop_Res.Re.P = P;
                //    (Processor as TProc<TComp>).Lop_Res.Im.P = P;

                //    (Processor as TProc<TComp>).Rop.Re.P = P;
                //    (Processor as TProc<TComp>).Rop.Im.P = P;

                //    Editor.Str = Processor.Lop_Res.ValueStr;
                //    if (State != States.l_val)
                //    {
                //        Editor.Str += (char)Processor.Operation;
                //        if (State == States.r_val)
                //            Editor.Str += Processor.Rop.ValueStr;
                //    }
                //    if (Memory.FState)
                //    {
                //        (Memory as TMemory<TComp>).FNumber.Re.P = P;
                //        (Memory as TMemory<TComp>).FNumber.Im.P = P;
                //    }
                //    return Editor.Str;
                //}

            }

            // Режим целых
            if (i == 47)
            {
                floatMode = false;
                if (State == States.l_val)
                {
                    //Processor.Lop_Res.Num = (int)Processor.Lop_Res.Num;
                    Editor.Str = Processor.Lop_Res.ValueStr;
                }                
                return Editor.Str;
                
            }

            // Режим дробных
            if (i == 48)
            {
                floatMode = true;
                return Editor.Str;
            }

            // Изменение точности
            if (49 <= i && i <= 56)
            {
                Acc = i - 47;
                if (typeof(T).Name == "TPNumber")
                {                 
                    (Processor as TProc<TPNumber>).Lop_Res.Acc = Acc;
                    (Processor as TProc<TPNumber>).Rop.Acc = Acc;
                    Editor.Str = Processor.Lop_Res.ValueStr;
                    if (State != States.l_val)
                    {
                        Editor.Str += (char)Processor.Operation;
                        if (State == States.r_val)
                            Editor.Str += Processor.Rop.ValueStr;
                    }
                    if (Memory.FState)
                    {
                        (Memory as TMemory<TPNumber>).FNumber.Acc = Acc;
                    }
                    return Editor.Str;
                }
            }

            // Выключить память
            if (i == 57)
            {
                Memory.Clear();
                Memory.FState = false;
                return Editor.Str;
            }

            // Включить память
            if (i == 58)
            {
                Memory.FState = true;
                return Editor.Str;
            }

            return "";
        }

    }
}
