using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {

    public GameObject EndMenu;
    public GameController gc;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().enabled = false;
            gc.ended = true;
            coll.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            EndMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
