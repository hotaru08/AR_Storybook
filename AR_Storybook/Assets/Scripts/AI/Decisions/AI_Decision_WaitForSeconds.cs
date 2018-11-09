namespace ATXK.AI
{
	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;
    using ATXK.EventSystem;

    [CreateAssetMenu(menuName = "AI/Decision/Wait for Seconds")]
	public class AI_Decision_WaitForSeconds : AI_Decision
	{
		[SerializeField] float time;
		[Range(0, 1)] [SerializeField] float timeRange;

        [Tooltip("Event to start the spawning of projectiles")]
        [SerializeField] private ES_Event_Bool m_CanStartSpawn;

		Dictionary<AI_Controller, bool> m_check = new Dictionary<AI_Controller, bool>();

		public override bool Decide(AI_Controller controller)
		{
            // null check 
            if (m_CanStartSpawn == null) return false;

            if (!m_check.ContainsKey(controller))
            {
                controller.StartCoroutine(WaitForTime(controller));
            }
            else
            {
                if (m_check[controller])
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

            float localTimer = time + Random.Range(-(time * timeRange), time * timeRange);
            while (localTimer > 0f)
            {
                // When paused, wait till not pause then continue reducing
                yield return new WaitUntil(CanSpawn);

                localTimer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            m_check[controller] = true;
        }

        private bool CanSpawn()
        {
            return m_CanStartSpawn.Value;
        }
	}
}