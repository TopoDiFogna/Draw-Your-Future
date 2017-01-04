using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonedSkeleton : MonoBehaviour
{
    public GameObject Shaman;
    GameObject player;
    public bool Chase;
    float plPos = 0;
    float dir;
    public float speed;
    public bool bossfight = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Chase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            plPos = player.transform.position.x;
            dir = (transform.position.x - plPos) / Mathf.Abs(transform.position.x - plPos);
            transform.position += new Vector3(speed * -dir * Time.deltaTime, 0, 0);
        }
    }

    void OnEnable()
    {
        //Chase = true;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if(!bossfight)
                Shaman.GetComponent<Shaman>().Activate(false);
            coll.gameObject.GetComponent<PlayerController>().DieWithFade();
        }
    }
}
