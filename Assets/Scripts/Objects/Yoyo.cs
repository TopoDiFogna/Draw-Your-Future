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
    public string active_tag = "Projectile";
    public Animator flamingo_animator;
    private int first_activation = 0;
    GameController gc;

    private void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player_transform = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        tr = GetComponent<Transform>();
        startingPosition = new Vector3(tr.position.x, tr.position.y, tr.position.z);
    }

    private void Update()
    {
        if (active && !gc.paused)
        {
            if (goingForward)
            {
                time += Time.deltaTime;
                if (time >= timeToHit)
                {
                    time = 0;
                    goingForward = !goingForward;
                    gameObject.tag = "Untagged";
                }
                else
                {
                    tr.position = Vector3.Lerp(startingPosition, endingPoint, time/timeToHit);
                }
            }
            if (!goingForward)
            {
                time += Time.deltaTime;
                if (time >= timeToHit)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    tr.position = Vector3.Lerp(endingPoint, startingPosition, time/timeToHit);
                }
            }
        }
    }

    private void OnEnable()
    {
        gameObject.layer = m_player_layer;
        gameObject.tag = active_tag;
        tr.position = startingPosition;
        direction = (player_transform.position - startingPosition).normalized;
        endingPoint = startingPosition + max_length * direction;
        goingForward = true;
        time = 0;
        if (first_activation > 0)
        {
            flamingo_animator.SetTrigger("Throw");
        }
        if(first_activation == 0)
        {
            first_activation++;
        }
        active = true;
    }
    
}
