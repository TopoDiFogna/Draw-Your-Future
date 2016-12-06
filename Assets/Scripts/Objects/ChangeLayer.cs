using UnityEngine;
using System.Collections;

public class ChangeLayer : MonoBehaviour
{
    public int m_scratch_layer = 8;
    int lay;


    // Use this for initialization
    void Start()
    {
        lay = gameObject.layer;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = m_scratch_layer;
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = m_scratch_layer;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Scratch")
        {
            gameObject.layer = lay;
        }
    }
}
