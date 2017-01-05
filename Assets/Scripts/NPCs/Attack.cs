using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    GameObject player;
    Vector2 dir;
    public float speed;
    public float TTL;
    float timer = 0f;
    public bool canhitshaman;

	// Use this for initialization
	void OnEnable()
    {
        canhitshaman = false;
        player = GameObject.FindGameObjectWithTag("Player");
        dir = (player.transform.position - transform.position).normalized;
        //Debug.DrawRay(player.transform.position, dir, Color.red, 10000);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= TTL)
        {
            timer = 0f;
            canhitshaman = false;
            gameObject.SetActive(false);
        }
        else
            transform.position += new Vector3(dir.x*speed*Time.deltaTime, dir.y * speed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Lens")
        {
            dir = -dir;
            canhitshaman = true;
        }
    }
}
