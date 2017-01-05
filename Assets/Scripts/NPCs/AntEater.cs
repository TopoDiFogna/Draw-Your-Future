using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntEater : MonoBehaviour
{

    GameObject curant;
    GameController gc;
    bool is_active = false;
    int counter = 0;
    public GameObject Tongue;
    GameObject[] ants;
    SpriteRenderer sr;
    public float speed = 0.1f;
    bool reverse = false;

    // Use this for initialization
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
        while (counter < ants.Length)
        {
            //print(ants[0].transform.position);
            if (!gc.paused)
            {
                if (!reverse)
                {
                    Vector3 vectorToTarget = ants[counter].transform.position - Tongue.transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    Tongue.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 100);
                    Tongue.transform.localScale = new Vector3(Tongue.transform.localScale.x + speed, Tongue.transform.localScale.y, Tongue.transform.localScale.z);
                }
                else
                    Tongue.transform.localScale = new Vector3(Tongue.transform.localScale.x - speed, Tongue.transform.localScale.y, Tongue.transform.localScale.z);
                if (reverse && Tongue.transform.localScale.x <= 0)
                {
                    counter++;
                    //Destroy(curant);
                    reverse = false;
                }
            }
            yield return new WaitForEndOfFrame();

        }
        Tongue.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Scratch")
        {
            if (!is_active)
                Activate();
        }
        else if (coll.tag == "Ant")
        {
            if (coll.gameObject == ants[counter])
            {
                //Destroy(coll.transform.parent.gameObject);
                //coll.transform.parent = Tongue.transform;
                curant = coll.gameObject;
                Destroy(curant);
                reverse = true;
                //Tongue.transform.localScale = new Vector3(0, Tongue.transform.localScale.y, Tongue.transform.localScale.z);
                //counter++;
            }
        }
    }
}
