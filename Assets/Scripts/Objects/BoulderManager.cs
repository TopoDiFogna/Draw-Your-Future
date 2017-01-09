using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderManager : MonoBehaviour {

    public GameObject crack;
    GameController gc;
    Vector3 spawnPos;
    public GameObject rock;
    CircleCollider2D cc;
    EdgeCollider2D ec;
    bool spawned;
    public float speed;
    Transform tr;

	// Use this for initialization
	void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        cc = GetComponent<CircleCollider2D>();
        ec = GetComponent<EdgeCollider2D>();
        spawned = false;
        tr = GetComponent<Transform>();
        spawnPos = tr.position;
        cc.enabled = false;
        ec.enabled = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(spawned && !gc.paused)
            tr.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (spawned)
            {
                coll.GetComponent<PlayerController>().DieWithFade();
                Set_Reset_Boulder(false);
            }
            else
            {
                Set_Reset_Boulder(true);
            }
        }
        if(coll.tag == "Ending")
        {
            Destroy(coll.gameObject);
            Set_Reset_Boulder(false);
        }
    }

    IEnumerator Crack()
    {
        crack.SetActive(true);
        yield return new WaitForSeconds(1);
        crack.SetActive(false);
        tr.position = spawnPos;
    }

    public void Set_Reset_Boulder(bool status)
    {
        //true -> set false -> reset
        spawned = status;
        cc.enabled = status;
        ec.enabled = !status;
        rock.SetActive(status);
        if (!status)
        {
            StartCoroutine(Crack());
        }
    }
}
