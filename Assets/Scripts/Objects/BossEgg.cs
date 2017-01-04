using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEgg : MonoBehaviour {

    bool active = false;
    public float time_to_fall = 1f;
    float time = 0;
    Transform tr;
    Vector3 startingPosition;
    public Transform endingGO;
    Vector3 endingPosition;

	// Use this for initialization
	void Awake () {
        tr = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            time += Time.deltaTime;
            if(time > time_to_fall)
            {
                time = 0;
                active = false;
                gameObject.SetActive(false);
            }else
            {
                tr.position = Vector3.Lerp(startingPosition, endingPosition, time / time_to_fall);
            }
        }
	}

    private void OnEnable()
    {
        time = 0;
        startingPosition = tr.position;
        endingPosition = new Vector3(startingPosition.x, endingGO.position.y, startingPosition.z);
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        tag = collision.gameObject.tag;
        if (tag == "Player" || tag == "Terrain" || tag == "Tiger")
        {
            if(tag == "Tiger")
            {
                Level2Boss tiger = collision.gameObject.GetComponent<Level2Boss>();
                tiger.hit = true;
                tiger.StopCoroutine("Jump");
                tiger.StartCoroutine("Hit");
            }
            //TODO animazione rottura
            gameObject.SetActive(false);
        }

    }

}
