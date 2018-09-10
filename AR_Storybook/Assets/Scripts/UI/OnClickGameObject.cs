using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickGameObject : MonoBehaviour {

    [Tooltip("Set this GO to be active or inactive.")]
    public GameObject GameobjectToBeSet;

    [Tooltip("Set active or inactive.")]
    public bool b_Active;

    [Tooltip("Name of GO that is to be clicked")]
    public string ClickableGameobjectName;

    Ray ray;
    RaycastHit hit;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameobjectToBeSet.activeSelf == true)
            b_Active = true;
        else
            b_Active = false;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == ClickableGameobjectName)
            {
                if (Input.GetMouseButtonDown(0) && !b_Active) //if click and if not active
                {
                    print(hit.collider.name); //print in debug name of GO
                    GameobjectToBeSet.SetActive(true); //Set the GO to be active
                }
            }
            
        }
    }
}
