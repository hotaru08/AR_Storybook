namespace ATXK.NodeEditor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	public class NE_Connection
	{
		public NE_ConnectionPoint inPoint;
		public NE_ConnectionPoint outPoint;
		public Action<NE_Connection> OnClickRemoveConnection;

		public NE_Connection(NE_ConnectionPoint inPoint, NE_ConnectionPoint outPoint, Action<NE_Connection> OnClickRemoveConnection)
		{
			this.inPoint = inPoint;
			this.outPoint = outPoint;
			this.OnClickRemoveConnection = OnClickRemoveConnection;
		}

		public void Draw()
		{
			Handles.DrawBezier(
				inPoint.rect.center,
				outPoint.rect.center,
				inPoint.rect.center + Vector2.left * 50f,
				outPoint.rect.center - Vector2.left * 50f,
				Color.white,
				null,
				2f
			);

			if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleCap))
			{
				if (OnClickRemoveConnection != null)
				{
					OnClickRemoveConnection(this);
				}
			}
		}
	}
}