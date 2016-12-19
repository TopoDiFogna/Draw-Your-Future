using UnityEngine;
using System.Collections;

public class CrabController : MonoBehaviour {

    Transform tr;
    bool goingLeft;
    bool goingRight;
    float time = 0.0f;
    public float max_time = 2.0f;
    public Transform m_endingPosition;
    Vector3 m_startingPoint;
    Vector3 m_endingPoint;


    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        m_startingPoint = tr.position;
        m_endingPoint = new Vector3(m_endingPosition.position.x, m_startingPoint.y, m_startingPoint.z);
        goingRight = true;
        goingLeft = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (time >= max_time)
        {
            time = 0.0f;
            goingRight = !goingRight;
            goingLeft = !goingLeft;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().DieWithFade();
        }
    }
}
