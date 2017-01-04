using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BongoPuzzle : MonoBehaviour
{
    bool active;
    public GameObject[] bongos;
    public GameObject door;
    public List<int> sol1, sol2;
    LinkedList<int> current;
    AudioSource auso;
    public AudioClip[] bong;
    bool solved1, solved2;


    // Use this for initialization
    void Start()
    {
        active = false;
        current = new LinkedList<int>();
        solved1 = solved2 = false;
        auso = GetComponent<AudioSource>();
        //StartCoroutine(PlayBongos());
    }

    private IEnumerator PlayBongos()
    {
        if (!solved1)
        {
            for (int i = 0; i < sol1.Count && !solved1; i++)
            {
                auso.clip = bong[sol1[i]];
                auso.Play();
                yield return new WaitForSeconds(1.2f);
            }
        }
        else if (!solved2)
        {
            for (int i = 0; i < sol2.Count && !solved2; i++)
            {
                auso.clip = bong[sol2[i]];
                auso.Play();
                yield return new WaitForSeconds(1.2f);
            }
        }
    }

    public void AddElement(int e)
    {
        current.AddLast(e);
        if (!solved1)
        {
            if (current.Count >= sol1.Count)
            {
                if (current.Count > sol1.Count)
                    current.RemoveFirst();
                if (current.SequenceEqual(sol1))
                    solved1 = true;
                //current = new LinkedList<int>();
            }

        }
        else if (!solved2)
        {
            if (current.Count >= sol2.Count)
            {
                if (current.Count > sol2.Count)
                    current.RemoveFirst();
                if (current.SequenceEqual(sol2))
                {
                    solved2 = true;
                    door.SetActive(false);
                }

                //current = new LinkedList<int>();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Scratch" && !active)
        {
            active = true;
            StartCoroutine(PlayBongos());
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Scratch")
            active = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Scratch" && active)
            active = false;
    }
}
