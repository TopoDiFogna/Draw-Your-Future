using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotTakeShaman : MonoBehaviour
{

    GameController gc;
    public GameObject parent;
    public GameObject shaman;
    public Transform fleepoint;
    public float TakeDistance;
    public float speed;
    bool gottheshaman;

    Vector3 dir;

    // Use this for initialization
    void Start()
    {
        gottheshaman = false;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.paused)
        {
            if (!gottheshaman)
            {
                dir = (shaman.transform.position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
                if (Vector2.Distance(transform.position, shaman.transform.position) <= TakeDistance)
                {
                    shaman.transform.parent = transform;
                    Shaman sh = shaman.GetComponent<Shaman>();
                    sh.Activate(false);
                    sh.enabled = false;
                    gottheshaman = true;
                }
            }
            else
            {
                dir = (fleepoint.transform.position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
                if (Vector2.Distance(transform.position, fleepoint.transform.position) <= TakeDistance)
                {
                    shaman.GetComponent<Shaman>().dse.Open();
                    parent.SetActive(false);
                }
            }
        }
    }
}
