namespace ATXK.AI
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	[CreateAssetMenu(menuName = "AI/Decision/Wait for Seconds")]
	public class AI_Decision_WaitForSeconds : AI_Decision
	{
		[SerializeField] float time;
		[Range(0, 1)][SerializeField] float timeRange;

		Dictionary<AI_Controller, bool> m_check = new Dictionary<AI_Controller, bool>();

		public override bool Decide(AI_Controller controller)
		{
			if (!m_check.ContainsKey(controller))
			{
				controller.StartCoroutine(WaitForTime(controller));
			}
			else
			{
				if(m_check[controller])
				{
					m_check.Remove(controller);
					return true;
				}
			}

			return false;
		}

		private bool Wait(AI_Controller controller)
		{
			time -= Time.deltaTime;
			return time <= 0f;
		}

		IEnumerator WaitForTime(AI_Controller controller)
		{
			m_check.Add(controller, false);

			float localtimer = time + Random.Range(-(time * timeRange), time * timeRange);
			while(localtimer > 0)
			{
				localtimer -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			m_check[controller] = true;
		}
	}
}