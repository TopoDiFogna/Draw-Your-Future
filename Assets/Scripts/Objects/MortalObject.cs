using UnityEngine;
using System.Collections;

public class MortalObject : MonoBehaviour {


    public bool m_can_Be_Disabled = true;

    [Range(1f, 10f)]
    public float m_time_to_despawn = 5f;

    public bool deadly = true;

    private void OnEnable()
    {
        deadly = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player" && deadly)
        {
            coll.GetComponent<PlayerController>().DieWithFade();
            if(m_can_Be_Disabled)
                gameObject.SetActive(false);
        }
        if (coll.tag == "Terrain")
        {
            if (m_can_Be_Disabled)
            {
                deadly = false;
                StartCoroutine(DisableObject());
            }
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(m_time_to_despawn);
        gameObject.SetActive(false);
        deadly = true;
    }
}
