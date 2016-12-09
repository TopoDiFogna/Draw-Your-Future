using UnityEngine;
using System.Collections;
using System;

public class RockTTL : MonoBehaviour {

    public float timetolive = 5;
    public AudioSource m_rock_effects;
    public AudioClip m_player_impact;
    public AudioClip m_general_impact;
    private bool played = false;

    // Use this for initialization
    void Start () {
	}

    private void OnEnable()
    {
        played = false;
        StartCoroutine(TTLRock());
    }

    private IEnumerator TTLRock()
    {
        yield return new WaitForSeconds(timetolive);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !played)
        {
            m_rock_effects.PlayOneShot(m_player_impact);
            played = true;
        }
        if(!played)
        {
            played = true;
            m_rock_effects.PlayOneShot(m_general_impact);
        }
    }

}
