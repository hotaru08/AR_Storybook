namespace ATXK.CustomVariables
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "CustomVariable/Float")]
	public class CV_Float : CV_Base<float>
	{
		public static CV_Float operator *(CV_Float a, CV_Float b)
		{
			CV_Float cv = new CV_Float
			{
				value = a.value * b.value
			};

			return cv;
		}

		public static CV_Float operator /(CV_Float a, CV_Float b)
		{
			CV_Float cv = new CV_Float
			{
				value = a.value / b.value
			};

			return cv;
		}

		public static CV_Float operator +(CV_Float a, CV_Float b)
		{

			CV_Float cv = new CV_Float
			{
				value = a.value + b.value
			};

			return cv;
		}

		public static CV_Float operator -(CV_Float a, CV_Float b)
		{

			CV_Float cv = new CV_Float
			{
				value = a.value - b.value
			};

			return cv;
		}

		public static bool operator >(CV_Float a, CV_Float b)
		{
			return a.value > b.value;
		}

		public static bool operator <(CV_Float a, CV_Float b)
		{
			return a.value < b.value;
		}

		public static bool operator ==(CV_Float a, CV_Float b)
		{
			return a.value == b.value;
		}

		public static bool operator !=(CV_Float a, CV_Float b)
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
	}
}