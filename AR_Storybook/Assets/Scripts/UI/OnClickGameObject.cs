using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickGameObject : MonoBehaviour {

    //[Tooltip("Set this GO to be active or inactive.")]
    //public GameObject GameobjectToBeSet;

    //[Tooltip("Set active or inactive.")]
    //public bool b_Active;

    //[Tooltip("Name of GO that is to be clicked")]
    //public string ClickableGameobjectName;

    //[Tooltip("List of GO to be set inactive")]
    //public List<GameObject> inactiveList;
    [SerializeField]
    private GameObject m_holder;
    private int index = 0;

    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
	}

    // Update is called once per frame
    void Update()
    {
        // When clicked, cast a ray from point
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Check if object hit is in list
                foreach (GameObject _objtest in m_holder.GetComponentInChildren<ListHolder>().GOList)
                {
                    if (hit.collider.name == _objtest.name)
                        break;
                    else // not same name
                    {
                        if (index != m_holder.GetComponentInChildren<ListHolder>().GOList.Count - 1)
                        {
                            index++;
                            continue;
                        }
                        else
                        {
                            index = 0;
                            return;
                        }
                    }
                }

                // Setting UI activeness
                foreach (GameObject _obj in m_holder.GetComponentInChildren<ListHolder>().GOList)
                {
                    if (hit.collider.name != _obj.name)
                    {
                        _obj.transform.GetChild(0).gameObject.SetActive(false);
                        continue;
                    }

                    print(hit.collider.name); //print in debug name of GO
                    _obj.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
}
