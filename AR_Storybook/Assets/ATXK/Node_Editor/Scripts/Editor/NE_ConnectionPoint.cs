namespace ATXK.NodeEditor
{
	using System;
	using UnityEngine;

	public enum ConnectionPointType { In, Out }

	public class NE_ConnectionPoint
	{
		public Rect rectNode;

		public ConnectionPointType type;

		public NE_Node node;

		public GUIStyle style;

		public Action<NE_ConnectionPoint> OnClickConnectionPoint;

		public NE_ConnectionPoint(NE_Node node, ConnectionPointType type, GUIStyle style, Action<NE_ConnectionPoint> OnClickConnectionPoint)
		{
			this.node = node;
			this.type = type;
			this.style = style;
			this.OnClickConnectionPoint = OnClickConnectionPoint;
			rectNode = new Rect(0, 0, 10f, 20f);
		}

		public void Draw()
		{
			rectNode.y = node.rectNode.y + (node.rectNode.height * 0.5f) - rectNode.height * 0.5f;

			switch (type)
			{
				case ConnectionPointType.In:
					rectNode.x = node.rectNode.x - rectNode.width + 8f;
					break;

				case ConnectionPointType.Out:
					rectNode.x = node.rectNode.x + node.rectNode.width - 8f;
					break;
			}

			if (GUI.Button(rectNode, "", style))
			{
				if (OnClickConnectionPoint != null)
				{
					OnClickConnectionPoint(this);
				}
			}
		}
	}
}