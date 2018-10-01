namespace ATXK.CustomVariables
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "CustomVariable/Double")]
	public class CV_Double : CV_Base<double>
	{
		public static CV_Double operator *(CV_Double a, CV_Double b)
		{
			CV_Double cv = new CV_Double
			{
				value = a.value * b.value
			};

			return cv;
		}

		public static CV_Double operator /(CV_Double a, CV_Double b)
		{
			CV_Double cv = new CV_Double
			{
				value = a.value / b.value
			};

			return cv;
		}

		public static CV_Double operator +(CV_Double a, CV_Double b)
		{

			CV_Double cv = new CV_Double
			{
				value = a.value + b.value
			};

			return cv;
		}

		public static CV_Double operator -(CV_Double a, CV_Double b)
		{

			CV_Double cv = new CV_Double
			{
				value = a.value - b.value
			};

			return cv;
		}

		public static bool operator >(CV_Double a, CV_Double b)
		{
			return a.value > b.value;
		}

		public static bool operator <(CV_Double a, CV_Double b)
		{
			return a.value < b.value;
		}

		public static bool operator ==(CV_Double a, CV_Double b)
		{
			return a.value == b.value;
		}

		public static bool operator !=(CV_Double a, CV_Double b)
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