using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLever : MonoBehaviour {

    GameController gc;
    public SpriteRenderer left;
    public SpriteRenderer right;
    private bool activated = false;
    public GameObject rotating_platform;
    public GameObject platform;
    public Vector3 platformlimit;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scratch" && !activated)
        {
            left.enabled = false;
            right.enabled = true;
            activated = true;

            if (rotating_platform != null)
            {
                rotating_platform.GetComponent<Rigidbody2D>().isKinematic = false;
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
            if (!gc.paused)
            {
                platform.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
                print(platform.transform.position.y);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
