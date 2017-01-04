using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_tasto : MonoBehaviour
{
    bool[] conf = { true, true, false };
    public GameObject[] doors;
    bool pressed = false;
    public bool reset;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (!pressed)
            {
                if (reset)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if(!doors[i].GetComponent<Puzzle_porta>().up == conf[i])
                        {
                            doors[i].GetComponent<Puzzle_porta>().Switch();
                        }
                    }


                }
                else
                {
                    foreach (GameObject g in doors)
                    {
                        if (g != null)
                        {
                            pressed = true;
                            g.GetComponent<Puzzle_porta>().Switch();
                        }
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            pressed = false;
        }
    }
}
