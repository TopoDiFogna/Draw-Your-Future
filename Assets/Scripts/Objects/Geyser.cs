using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour {

    public GameObject[] composing_objects;
    public float timeInactive = 2f;
    public float timeBetweenSprites = 0.1f;
    bool active = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!active)
        {
            StartCoroutine("Erupt");
        }
	}

    IEnumerator Erupt()
    {
        active = true;
        composing_objects[0].SetActive(true);
        yield return new WaitForSeconds(timeBetweenSprites);
        composing_objects[0].SetActive(false);
        composing_objects[1].SetActive(true);
        yield return new WaitForSeconds(timeBetweenSprites);
        composing_objects[1].SetActive(false);
        composing_objects[2].SetActive(true);
        yield return new WaitForSeconds(timeBetweenSprites);
        composing_objects[2].SetActive(false);
        yield return new WaitForSeconds(timeInactive);
        active = false;
    }
}
