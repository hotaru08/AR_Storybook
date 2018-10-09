namespace ATXK.CustomVariables
{
	using UnityEngine;

	[CreateAssetMenu(menuName = "Custom Variable/String", order = 5)]
	public class CV_String : CV_Base<string>
	{
		#region Operator Overloads

		public static CV_String operator +(CV_String a, CV_String b)
		{
			CV_String cv = new CV_String
			{
				value = a.value + b.value
			};

			return cv;
		}

		public static bool operator ==(CV_String a, CV_String b)
		{
			return a.value == b.value;
		}

		public static bool operator !=(CV_String a, CV_String b)
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