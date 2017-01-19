using UnityEngine;
using System.Collections;

public class ObjectInPools : MonoBehaviour {

    public GameObject[] objects;

	// Use this for initialization
	void Start () {
	    foreach (GameObject obj in objects)
        {
            ObjectPoolingManager.Instance.CreatePool(obj, 5, 20);
        }
	}

}
