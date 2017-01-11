using UnityEngine;
using System.Collections;

public class FlyingMovement : MonoBehaviour
{

    public GameObject Target;
    public GameObject carry;
    public bool seek = false;
    public bool hasplayer = false;
    public float speed = 2;
    private Transform tr;
    private SpriteRenderer sr;
    public float m_reset_time = 3f;
    private bool can_grab = true;

    public bool CanGrab
    {
        get { return can_grab; }
    }

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        tr = transform.parent.GetComponent<Transform>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (sr.flipX == false && (Target.transform.position - tr.position).x < 0)
        {
            sr.flipX = true;
        }
        else if (sr.flipX == true && (Target.transform.position - tr.position).x > 0)
        {
            sr.flipX = false;
            tr.localScale = new Vector3(1, 1, 1);
        }
        tr.position += (Target.transform.position - tr.position).normalized * speed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        CheckTriggers(coll);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        CheckTriggers(coll);
    }


    private void CheckTriggers(Collider2D coll)
    {
        if (coll.tag == "FlyingBoundary" && Target.name == coll.gameObject.name && !seek && !hasplayer)
        {
            Target = coll.gameObject.GetComponent<FlyingBoundaries>().OtherSide;
        }
        else if (coll.tag == "Player" && can_grab && coll.GetComponent<PlayerController>().CanBeGrabbed)
        {
            if (!coll.GetComponent<PlayerController>().Dead)
            {
                seek = false;
                hasplayer = true;
                Target = carry;
                coll.GetComponent<Rigidbody2D>().velocity = new Vector3();
                coll.gameObject.transform.parent = gameObject.transform;
                coll.gameObject.GetComponent<PlayerController>().StopAnimation();
                coll.gameObject.GetComponent<PlayerController>().enabled = false;
                coll.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                coll.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
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

    private void DropPlayer()
    {
        hasplayer = false;
        GameObject p = gameObject.transform.GetChild(0).gameObject;
        p.GetComponent<PlayerController>().enabled = true;
        p.GetComponent<PlayerController>().jumping = true;
        p.GetComponent<Rigidbody2D>().isKinematic = false;
        p.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        p.transform.parent = null;
        can_grab = false;
        transform.parent.GetComponentInChildren<FlyingEnemySight>().LeavePlayer();
        StartCoroutine(ResetGrab());
    }

    private IEnumerator ResetGrab()
    {
        yield return new WaitForSeconds(m_reset_time);
        can_grab = true;
    }
}
