using UnityEngine;
using System.Collections;

public class FlyingEnemySight : MonoBehaviour
{

    public GameObject monster;
    GameObject old;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !monster.GetComponentInParent<FlyingMovement>().seek && monster.GetComponentInParent<FlyingMovement>().can_grab && !coll.GetComponent<PlayerController>().dead)
        {
            old = monster.GetComponentInParent<FlyingMovement>().Target;
            monster.GetComponentInParent<FlyingMovement>().seek = true;
            monster.GetComponentInParent<FlyingMovement>().Target = coll.gameObject;
        }
        
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && monster.GetComponentInParent<FlyingMovement>().seek && coll.GetComponent<PlayerController>().dead)
        {
            LeavePlayer();
        }
        /*else if (coll.tag == "Player" && !monster.GetComponentInParent<FlyingMovement>().seek && !coll.GetComponent<PlayerController>().dead)
        {
            old = monster.GetComponentInParent<FlyingMovement>().Target;
            monster.GetComponentInParent<FlyingMovement>().seek = true;
            monster.GetComponentInParent<FlyingMovement>().Target = coll.gameObject;
        }*/
    }

    public void LeavePlayer()
    {

        monster.GetComponentInParent<FlyingMovement>().seek = false;
        monster.GetComponentInParent<FlyingMovement>().Target = old;
    }
}
