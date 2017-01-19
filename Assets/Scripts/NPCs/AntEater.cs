using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntEater : MonoBehaviour
{
    LineRenderer lr;
    Vector3 InitialPos;
    GameController gc;
    bool is_active = false;
    int counter = 0;
    public GameObject Tongue;
    GameObject[] ants;
    SpriteRenderer sr;
    public float speed = 0.1f;
    bool reverse = false;
    Vector2 dir;

    // Use this for initialization
    void Start()
    {
        lr = Tongue.GetComponent<LineRenderer>();
        InitialPos = Tongue.transform.position;
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
        lr.SetPosition(0, InitialPos);
        lr.SetPosition(1, Tongue.transform.position);
        StartCoroutine(Eat());
    }

    private IEnumerator Eat()
    {
        while (counter < ants.Length)
        {
            if (!gc.paused)
            {
                lr.SetPosition(1, Tongue.transform.position);
                if (!reverse)
                {
                    dir = (ants[counter].transform.position - Tongue.transform.position).normalized;

                }
                else
                {
                    dir = (InitialPos - Tongue.transform.position).normalized;
                }
                Tongue.transform.position += new Vector3(dir.x * speed * Time.deltaTime, dir.y * speed * Time.deltaTime, 0);
                if (reverse && Vector2.Distance(InitialPos, Tongue.transform.position) <= 0.3f)
                {
                    Destroy(ants[counter]);
                    counter++;
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
                coll.GetComponent<CrabController>().Eat();
                coll.transform.parent = Tongue.transform;
                reverse = true;
            }
        }
    }
}
