using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{

    SpriteRenderer sr;
    public Sprite m_on_status;
    public Sprite m_off_status;

    // Use this for initialization
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Scratch")
        {
            print("entering");
            sr.sprite = m_on_status;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Scratch")
        {
            print("exiting");
             sr.sprite = m_off_status;
        }
    }
}
