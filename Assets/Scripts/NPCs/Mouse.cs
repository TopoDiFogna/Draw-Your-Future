using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {

    public GameObject m_cheese;
    public float m_speed;
    private Vector3 startingPos;
    private bool atCheese = false;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_cheese.activeSelf)
        {
            GetComponent<MovingNPC>().enabled = false;
            atCheese = true;
            GetComponent<SpriteRenderer>().flipX = false;
            StartCoroutine(MoveToCheese());
        }
        else if (atCheese)
        {
            StopCoroutine(MoveToCheese());
            GetComponent<SpriteRenderer>().flipX = true;
            StartCoroutine(RestoreStatus());
        }
	}

    IEnumerator MoveToCheese()
    {
        while(Vector2.Distance(transform.position, m_cheese.transform.position) > 0.5){
            transform.position += new Vector3(-Vector3.right.x * m_speed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator RestoreStatus()
    {
        while (Vector2.Distance(transform.position, startingPos) > 0.5)
        {
            transform.position += new Vector3(Vector3.right.x * m_speed * Time.deltaTime, 0, 0);
            yield return new WaitForEndOfFrame();
        }
        GetComponent<MovingNPC>().enabled = true;
        atCheese = false;
    }
}
