using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawnEvent : MonoBehaviour {

    public GameObject[] doors;

	// Use this for initialization
	void Start () {
		foreach(GameObject d in doors)
        {
            d.SetActive(false);
        }
	}
	
	public void Open()
    {
        foreach(GameObject d in doors)
        {
            d.SetActive(false);
        }
    }

    public void Close()
    {
        foreach (GameObject d in doors)
        {
            d.SetActive(true);
        }
    }
}
