namespace ATXK.CustomVariables
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "CustomVariable/Int", order = 2)]
	public class CV_Int : CV_Base<int>
	{
		#region Operator Overloads
		public static CV_Int operator *(CV_Int a, CV_Int b)
		{
			CV_Int cv = new CV_Int
			{
				value = a.value * b.value
			};

			return cv;
		}

		public static CV_Int operator /(CV_Int a, CV_Int b)
		{
			CV_Int cv = new CV_Int
			{
				value = a.value / b.value
			};

			return cv;
		}

		public static CV_Int operator +(CV_Int a, CV_Int b)
		{
			CV_Int cv = new CV_Int
			{
				value = a.value + b.value
			};

			return cv;
		}

		public static CV_Int operator -(CV_Int a, CV_Int b)
		{
			CV_Int cv = new CV_Int
			{
				value = a.value - b.value
			};

			return cv;
		}

		public static bool operator >(CV_Int a, CV_Int b)
		{
			return a.value > b.value;
		}

		public static bool operator <(CV_Int a, CV_Int b)
		{
			return a.value < b.value;
		}

		public static bool operator ==(CV_Int a, CV_Int b)
		{
			return a.value == b.value;
		}

		public static bool operator !=(CV_Int a, CV_Int b)
		{
			return a.value != b.value;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object other)
		{
			return base.Equals(other);
		}

		public override string ToString()
		{
			return base.ToString();
		}
		#endregion
	}
}