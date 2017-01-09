using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleLight : MonoBehaviour {

    public GameObject m_dark;
    private SpriteRenderer sr;
    private Color originalColor;
    public float m_alpha_reduction;
    public GameObject m_light_sprite;
    public float m_time_to_dark = 5;
    private bool active = false;

	// Use this for initialization
	void Start () {
        sr = m_dark.GetComponent<SpriteRenderer>();
        originalColor = sr.material.color;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Scratch" && !active)
        {
            active = true;
            m_light_sprite.SetActive(true);
            StartCoroutine(fadeDark());
        }
    }

    IEnumerator fadeDark()
    {
        while (sr.material.color.a > 0)
        {
            sr.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, sr.material.color.a - m_alpha_reduction);
            print(sr.material.color.a);
            yield return new WaitForEndOfFrame();
           
        }
        yield return new WaitForSeconds(m_time_to_dark);
        m_light_sprite.SetActive(false);
        while (sr.material.color.a < originalColor.a)
        {
            sr.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, sr.material.color.a + m_alpha_reduction);
            yield return new WaitForEndOfFrame();

        }
        active = false;
    }
}
