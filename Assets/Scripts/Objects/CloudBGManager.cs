using UnityEngine;
using System.Collections;
using System;

public class CloudBGManager : MonoBehaviour {

    public GameObject Light;
    public float mintime = 1.5f;
    public float maxtime = 3f;
    bool stop = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(Lightning());
	}

    private IEnumerator Lightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f,3f));
            Light.SetActive(true);
        }
    }
}
