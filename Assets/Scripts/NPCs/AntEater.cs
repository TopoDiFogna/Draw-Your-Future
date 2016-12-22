using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntEater : MonoBehaviour {

    bool is_active = false;
    int counter = 0;
    public GameObject Tongue;
    GameObject[] ants;
    SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        Tongue.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
	}

    public void Activate()
    {
        sr.sortingOrder = 2;
        is_active = true;
        ants = GameObject.FindGameObjectsWithTag("Ant");
        Tongue.SetActive(true);
        StartCoroutine(Eat());
    }

    private IEnumerator Eat()
    {
       
        while (counter<ants.Length)
        {
            //print(ants[0].transform.position);

            Vector3 vectorToTarget = ants[counter].transform.position - Tongue.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            Tongue.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 100);


            //Tongue.transform.LookAt(ants[0].transform,Vector3.up);
            Tongue.transform.localScale = new Vector3(Tongue.transform.localScale.x+0.1f, Tongue.transform.localScale.y, Tongue.transform.localScale.z);
            yield return new WaitForEndOfFrame();
            
        }
        Tongue.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Scratch")
        {
            if(!is_active)
                Activate();
        }
        else if(coll.tag == "Ant")
        {
            Destroy(coll.transform.parent.gameObject);
            Tongue.transform.localScale = new Vector3(0, Tongue.transform.localScale.y, Tongue.transform.localScale.z);
            counter++;
        }
    }
}
