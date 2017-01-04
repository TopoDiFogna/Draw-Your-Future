using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasp : MonoBehaviour {

    public float speed;
    GameObject player;
    bool chasing = false;
    Vector2 dir;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chasing = true;
    }

	// Update is called once per frame

	void Update () {
        if (chasing)
        {
            dir = (player.transform.position - transform.position).normalized;
            transform.position += new Vector3(speed*dir.x*Time.deltaTime,speed*dir.y*Time.deltaTime,0);
        }
	}
}
