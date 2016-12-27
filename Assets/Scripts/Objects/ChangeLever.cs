using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLever : MonoBehaviour {

    private SpriteRenderer left;
    private SpriteRenderer right;
    private bool activated = false;
    public GameObject trapdoor1;
    public GameObject trapdoor2;

    // Use this for initialization
    void Start () {
        left = GetComponent<SpriteRenderer>();
        right = GetComponentInChildren<SpriteRenderer>(true);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scratch" && !activated)
        {
            left.enabled = false;
            right.enabled = true;
            activated = true;
            if (trapdoor1 != null && trapdoor2 != null)
            {
                trapdoor1.SetActive(false);
                trapdoor2.SetActive(false);
            }
        }
    }
}
