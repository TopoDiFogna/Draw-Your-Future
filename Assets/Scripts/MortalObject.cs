using UnityEngine;
using System.Collections;

public class MortalObject : MonoBehaviour {


    public bool CanDisable = true;

    [Range(1f, 10f)]
    public float m_time_to_despawn = 5f;

    bool deadly = true;

    private void OnEnable()
    {
        deadly = true;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // print(gameObject.name + " collided with " + coll.gameObject.name + " with tag " + coll.gameObject.tag + " and it was deadly " + deadly);
        if (coll.tag == "Player" && deadly)
        {
            coll.GetComponent<PlayerController>().DieWithFade();
            if(CanDisable)
                gameObject.SetActive(false);
        }
        if (coll.tag == "Terrain")
        {
            deadly = false;
            if(CanDisable)
                StartCoroutine(DisableObject());
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(m_time_to_despawn);
        gameObject.SetActive(false);
        deadly = true;
    }
}
