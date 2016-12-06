using UnityEngine;
using System.Collections;

public class FlyingEnemySight : MonoBehaviour
{

    public GameObject m_monster;
    public float m_time_to_get_player = 10f;
    private GameObject old;
    private FlyingMovement monster_script;

    private void Start()
    {
        monster_script = m_monster.GetComponent<FlyingMovement>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && !monster_script.seek && monster_script.CanGrab && !coll.GetComponent<PlayerController>().Dead)
        {
            old = monster_script.Target;
            monster_script.seek = true;
            monster_script.Target = coll.gameObject;
            StartCoroutine(SearchPlayer());
        }
        
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Player" && monster_script.seek && coll.GetComponent<PlayerController>().Dead)
        {
            LeavePlayer();
        }
    }

    public void LeavePlayer()
    {
        monster_script.seek = false;
        monster_script.Target = old;
    }

    IEnumerator SearchPlayer()
    {
        yield return new WaitForSeconds(m_time_to_get_player);
        if (!monster_script.hasplayer)
        {
            LeavePlayer();
        }
    }
}
