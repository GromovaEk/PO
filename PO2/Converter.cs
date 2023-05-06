using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    /// <summary>
    /// Конвертирует строковое представление p-ичного 
    /// в десятичное число и наоборот
    /// </summary>
    static class Converter
    {
        /// <summary>
        /// Перевод десятичного числа в p-ичное в строковом представлении
        /// </summary>
        /// <param name="n">Десятичное число</param>
        /// <param name="p">Основание системы счисления результата</param>
        /// <param name="c">Точность дробной части</param>
        /// <param name="delim">Символ разделителя</param>
        /// <returns></returns>
        public static string Convert(double n, int p, int c, char delim)
        {
            string res = ConverP10.Do(Math.Abs(n), p, c, delim);
            if (n < 0)
                res = "-" + res;
            return res;
        }

        /// <summary>
        /// Перевод p-ичного числа в строковом представлении в десятичное
        /// </summary>
        /// <param name="P_num">Строковое представление p-ичного числа</param>
        /// <param name="p">Основание системы счисления p-ичного числа</param>
        /// <param name="delim">Символ разделителя</param>
        /// <returns></returns>
        public static double Convert(string P_num, int p, char delim)
        {
            return Conver10P.Dval(P_num, p, delim);
        }

        /// <summary>
        /// Возвращает десятичное значение цифры в p-ичной системе счисления
        /// </summary>
        /// <param name="ch">Символ цифры в p-ичной системе счисления</param>
        /// <returns></returns>
        static public long CharToNum(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return ch - '0';
            if (ch >= 'A' && ch <= 'F')
                return ch - 'A' + 10;
            return 0;
        }

        /// <summary>
        /// Конвертирует цифру в виде числа в цифру в виде символа
        /// </summary>
        /// <param name="d">Числовое представление цифры</param>
        /// <returns></returns>
        public static char longToChar(long d)
        {
            if (d < 10)
                return (char)('0' + d);
            else
                return (char)(d - 10 + 'A');
        }

        /// <summary>
        /// Конвертер десятичного в p-ичное
        /// </summary>
        private static class ConverP10
        {
            /// <summary>
            /// Выделяет из числа целую и дробную часть,
            /// затем конвертирует в p-ичное
            /// </summary>
            /// <param name="n"></param>
            /// <param name="p"></param>
            /// <param name="c"></param>
            /// <param name="delim"></param>
            /// <returns></returns>
            public static string Do(double n, int p, int c, char delim)
            {
                if (n == 0)
                    return "0";
                // Выделение целой части
                string integer = checked(longToP((long)n, p));
                // Выделение дробной части
                string fraction;
                if (n - (int)n == 0)
                    return integer;
                else
                    fraction = fltToP(n - (long)n, p, c);
                return integer + delim + fraction;
            }



            /// <summary>
            /// Конвертирует целое в p-ичное
            /// </summary>
            /// <param name="n">Конвертируемое число</param>
            /// <param name="p">Основание системы счисления</param>
            /// <returns></returns>
            private static string longToP(long n, int p)
            {
                StringBuilder result = new StringBuilder("");
                StringBuilder tmp = new StringBuilder("");
                do
                {
                    tmp.Append(longToChar(n % p));
                    n /= p;
                } while (n > 0);
                result = new StringBuilder(new string('@', tmp.Length));
                for (int i = 0; i < tmp.Length; i++)
                    result[i] = tmp[tmp.Length - 1 - i];
                return result.ToString();
            }

            /// <summary>
            /// Конвертирует дробную часть числа (< 1) в p-ичную дробь
            /// </summary>
            /// <param name="d">Конвертируемая дробь</param>
            /// <param name="p">Основание системы счисления</param>
            /// <param name="c">Точность</param>
            /// <returns></returns>
            private static string fltToP(double d, int p, int c)
            {
                StringBuilder result = new StringBuilder();
                double delim = 1.0 / p;
                for (int i = 1; i <= c; i++)
                {
                    int temp = (int)(d / delim);
                    if (temp >= 1)
                        d -= delim * temp;
                    result.Append(longToChar(temp));
                    if (Math.Abs(d) < 1 / Math.Pow(p, c))
                        break;
                    delim /= p;
                }
                return result.ToString();
            }
        }

        /// <summary>
        /// Конвертер p-ичного числа в строковом представлении в десятичное
        /// </summary>
        private static class Conver10P
        {
            /// <summary>
            /// Обрабатывает строку с p-ичным числом и конвертирует в
            /// десятичное
            /// </summary>
            /// <param name="P_num">Строка с числом</param>
            /// <param name="P">Основание системы счисления</param>
            /// <param name="delim">Символ разделителя</param>
            /// <returns></returns>
            public static double Dval(string P_num, int P, char delim)
            {
                int pos = P_num.IndexOf(delim);
                double weight;
                int sign = 1;
                if (P_num[0] == '-')
                {
                    sign = -1;
                    P_num = P_num.Remove(0, 1);
                }
                if (pos == -1)
                    weight = Math.Pow(P, P_num.Length - 1);
                else
                {              
                    P_num = P_num.Remove(pos, 1);
                    weight = Math.Pow(P, pos - 1);
                }
                return sign * Convert(P_num, P, weight);
            }



            /// <summary>
            /// Конверирует обработанную строку в десятичное число
            /// </summary>
            /// <param name="P_num">Обработанная строка</param>
            /// <param name="P">Основание системы счисления</param>
            /// <param name="weight">Максимальная степень десятки</param>
            /// <returns></returns>
            private static double Convert(string P_num, int P, double weight)
            {
                double result = 0;
                for (int i = 0; i < P_num.Length; i++)
                {
                    result += CharToNum(P_num[i]) * weight;
                    weight /= P;
                }
                return result;
            }
        }
    }
}
