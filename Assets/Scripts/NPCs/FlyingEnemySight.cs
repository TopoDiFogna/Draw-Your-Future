using UnityEngine;
using System.Collections;

public class FlyingEnemySight : MonoBehaviour
{

    public GameObject m_monster;
    private GameObject old;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !m_monster.GetComponentInParent<FlyingMovement>().seek && m_monster.GetComponentInParent<FlyingMovement>().can_grab && !coll.GetComponent<PlayerController>().Dead)
        {
            old = m_monster.GetComponentInParent<FlyingMovement>().Target;
            m_monster.GetComponentInParent<FlyingMovement>().seek = true;
            m_monster.GetComponentInParent<FlyingMovement>().Target = coll.gameObject;
        }
        
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && m_monster.GetComponentInParent<FlyingMovement>().seek && coll.GetComponent<PlayerController>().Dead)
        {
            LeavePlayer();
        }
    }

    public void LeavePlayer()
    {
        m_monster.GetComponentInParent<FlyingMovement>().seek = false;
        m_monster.GetComponentInParent<FlyingMovement>().Target = old;
    }
}
