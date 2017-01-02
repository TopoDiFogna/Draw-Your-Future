using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLever : MonoBehaviour {

    public SpriteRenderer left;
    public SpriteRenderer right;
    private bool activated = false;
    public GameObject trapdoor1;
    public GameObject trapdoor2;
    public GameObject platform;
    public Vector3 platformlimit;

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
            else if (platform != null)
            {
                StartCoroutine(MovePlatformDown());
            }
        }
    }

    private IEnumerator MovePlatformDown()
    {
        float speed = 3;
        while (platform.transform.position.y > platformlimit.y)
        {
            platform.transform.position += new Vector3(0,-speed*Time.deltaTime,0);
            print(platform.transform.position.y);
            yield return new WaitForEndOfFrame();
        }
    }
}
