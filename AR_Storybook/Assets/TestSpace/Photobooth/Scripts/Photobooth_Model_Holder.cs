namespace TestSpace
{
	using UnityEngine;
	using ATXK.AI;

	public class Photobooth_Model_Holder : MonoBehaviour
	{
		public GameObject model;

		public void SetModel(GameObject modelPrefab)
		{
			if (model != null)
				Destroy(model);

			// Instantiate a new model
			model = Instantiate(modelPrefab, transform);

			// Model should not have AI controller.
			// But if it does have one, it will be disabled.
			if (model.GetComponent<AI_Controller>())
				model.GetComponent<AI_Controller>().enabled = false;
		}

		public void SetModel(Object modelObject)
		{
			GameObject modelPrefab = modelObject as GameObject;
			if(modelPrefab != null)
			{
				SetModel(modelPrefab);
			}
		}
	}
}