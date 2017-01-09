using UnityEngine;
using System.Collections;

public class Ending : MonoBehaviour {

    public GameController gc;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            gc.ShowWinningMenu();
        }
    }
}
