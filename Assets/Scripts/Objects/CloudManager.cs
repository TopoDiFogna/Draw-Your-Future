using UnityEngine;
using System.Collections;
using System;

public class CloudManager : MonoBehaviour {

    public GameObject Light;
    public float mintime = 1.5f;
    public float maxtime = 3f;
    bool stop = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(Lightning());
	}

    private IEnumerator Lightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f,3f));
            if (!stop)
            {
                Light.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Scratch")
        {
            print("asd");
            stop = true;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Scratch" && !stop)
        {
            stop = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Scratch")
        {
            stop = false;
        }
    }
}
