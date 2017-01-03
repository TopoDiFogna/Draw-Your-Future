using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour {

    private float player_speed;
    private bool activated = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !activated)
        {
            activated = true;
            player_speed = collision.GetComponent<PlayerController>().m_speed;
            collision.GetComponent<PlayerController>().m_speed = player_speed / 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().m_speed = player_speed;
            activated = false;
        }
    }
}
