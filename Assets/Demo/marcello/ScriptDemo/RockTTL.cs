using UnityEngine;
using System.Collections;
using System;

public class RockTTL : MonoBehaviour {

    public float timetolive = 5;

	// Use this for initialization
	void Start () {
	}

    private void OnEnable()
    {
        StartCoroutine(TTLRock());
    }

    private IEnumerator TTLRock()
    {
        yield return new WaitForSeconds(timetolive);
        gameObject.SetActive(false);
    }

}
