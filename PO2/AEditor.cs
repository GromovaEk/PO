using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO2
{
    public abstract class AEditor
    {
		public string Str { get; set; }

		public abstract void Add(string numeral);

		public abstract void Add(char numeral);

		public abstract void AddDigit(int a);

		public abstract void AddZero();

		public abstract void AddSign(char sign);

		public abstract void AddSeparator();

		public abstract void AddMinusFront();

		public abstract void Backspace();

		public abstract void Clear();

		public abstract bool IsZero();

		public abstract void PopLastNumber();

		public abstract string GetLastNumber();

		public abstract bool LastIsSign();


	}
}