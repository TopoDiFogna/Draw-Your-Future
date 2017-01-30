using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFallingCrate : MonoBehaviour
{

    public GameObject m_object;
    public bool m_can_be_disabled = true;
    private bool canBeActivated = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Scratch" && canBeActivated)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = false;
            }
            m_object.gameObject.SetActive(true);
            canBeActivated = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Scratch" && m_can_be_disabled)
        {
            m_object.gameObject.SetActive(false);
        }
    }
}
