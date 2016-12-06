using UnityEngine;
using System.Collections;

public class FlyingMovement : MonoBehaviour
{

    public GameObject Target;
    public GameObject carry;
    public bool seek = false;
    public bool hasplayer = false;
    public float speed = 20;
    public Transform tr;
    public float m_reset_time = 3f;
    public bool can_grab = true;

    // Use this for initialization
    void Start()
    {
        //tr = gameObject.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.position += (Target.transform.position - tr.position).normalized * speed * Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        CheckTriggers(coll);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        CheckTriggers(coll);
    }

    void CheckTriggers(Collider2D coll)
    {
        if (coll.tag == "FlyingBoundary" && Target.name == coll.gameObject.name && !seek && !hasplayer)
        {
            Target = coll.gameObject.GetComponent<FlyingBoundaries>().OtherSide;
        }
        else if (coll.tag == "Player" && can_grab)
        {
            if (!coll.GetComponent<PlayerController>().dead)
            {
                seek = false;
                hasplayer = true;
                Target = carry;
                coll.gameObject.transform.parent = gameObject.transform;
                coll.gameObject.GetComponent<PlayerController>().StopAnimation();
                coll.gameObject.GetComponent<PlayerController>().enabled = false;
                coll.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else
            {
                DropPlayer();
            }
        }
        else if (coll.tag == "FlyingEnd" && hasplayer)
        {
            DropPlayer();
        }
    }

    public void DropPlayer()
    {
        hasplayer = false;
        GameObject p = gameObject.transform.GetChild(0).gameObject;
        p.GetComponent<PlayerController>().enabled = true;
        p.GetComponent<PlayerController>().jumping = true;
        p.GetComponent<Rigidbody2D>().isKinematic = false;
        p.transform.parent = null;
        can_grab = false;
        transform.parent.GetComponentInChildren<FlyingEnemySight>().LeavePlayer();
        StartCoroutine(ResetGrab());
    }

    IEnumerator ResetGrab()
    {
        yield return new WaitForSeconds(m_reset_time);
        can_grab = true;
    }
}
