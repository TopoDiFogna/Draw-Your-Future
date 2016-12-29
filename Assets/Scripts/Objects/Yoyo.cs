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
            if(goingForward)
            {
                time += Time.deltaTime;
                if (time > timeToHit)
                {
                    time = 0;
                    goingForward = !goingForward;
                }
                else
                {
                    tr.position = Vector3.Lerp(startingPosition, endingPoint, time);
                }

            }
            if (!goingForward)
            {
                time += Time.deltaTime;
                if(time > timeToHit)
                {
                    active = false;
                    gameObject.SetActive(false);
                }else
                {
                    tr.position = Vector3.Lerp(endingPoint, startingPosition, time);
                }
            }
        }
    }

    private void OnEnable()
    {
        tr.position = startingPosition;
        direction = (player_transform.position - startingPosition).normalized;
        endingPoint = startingPosition + max_length * direction;
        goingForward = true;
        time = 0;
        active = true;
    }
}
