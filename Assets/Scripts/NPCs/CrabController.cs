using UnityEngine;
using System.Collections;

public class CrabController : MonoBehaviour {

    Transform tr;
    public bool goingLeft = false;
    public bool goingRight = true;
    float time = 0.0f;
    public float max_time = 2.0f;
    public Transform m_endingPosition;
    Vector3 m_startingPoint;
    Vector3 m_endingPoint;
    GameController gc;
    SpriteRenderer sr;


    // Use this for initialization
    void Start () {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        tr = GetComponent<Transform>();
        m_startingPoint = tr.position;
        m_endingPoint = new Vector3(m_endingPosition.position.x, m_startingPoint.y, m_startingPoint.z);
        sr = GetComponent<SpriteRenderer>();
        if (goingLeft == true && goingRight == false)
        {
            tr.position = m_endingPoint;
            sr.flipX = !sr.flipX;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!gc.paused)
        {
            if (time >= max_time)
            {
                time = 0.0f;
                goingRight = !goingRight;
                goingLeft = !goingLeft;
                sr.flipX = !sr.flipX;
            }

            if (goingRight)
            {
                tr.position = Vector3.Lerp(m_startingPoint, m_endingPoint, time / max_time);
            }

            if (goingLeft)
            {
                tr.position = Vector3.Lerp(m_endingPoint, m_startingPoint, time / max_time);
            }

            time += Time.deltaTime;
        }

    }

}
