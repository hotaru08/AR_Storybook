using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ATXK.Helper;

public class FileManager : SingletonBehaviour<FileManager>
{




	// Use this for initialization
	void Start ()
    {
        Debug.Log(Application.persistentDataPath);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
