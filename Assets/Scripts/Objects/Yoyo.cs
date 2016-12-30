using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yoyo : MonoBehaviour {

    Transform player_transform;
    Transform tr;
    Vector3 startingPosition;
    Vector3 endingPoint;
    Vector3 direction;
    bool goingForward;
    bool active = false;
    float time = 0;
    public float timeToHit = 1f;
    public float max_length = 10f;
    public int m_player_layer = 9;

    private void Awake()
    {
        player_transform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        tr = GetComponent<Transform>();
        startingPosition = new Vector3(tr.position.x, tr.position.y, tr.position.z);
    }

    private void Update()
    {
        if (active)
        {
                time += Time.deltaTime;
                if (time > timeToHit)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    tr.position = Vector3.Lerp(startingPosition, endingPoint, time);
                }

        }
    }

    private void OnEnable()
    {
        gameObject.layer = m_player_layer;
        tr.position = startingPosition;
        direction = (player_transform.position - startingPosition).normalized;
        endingPoint = startingPosition + max_length * direction;
        goingForward = true;
        time = 0;
        active = true;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Scratch")
        {
            Debug.Log("on collision enter");
            //TODO animazione rottura
            gameObject.SetActive(false);
        }
    }
}
