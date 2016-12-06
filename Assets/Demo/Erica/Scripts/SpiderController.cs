using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour {

    Transform tr;
    bool goingDown;
    bool goingUp;
    float time = 0.0f;
    float max_time = 2.0f;
    public Transform m_startingPosition;
    public Transform m_endingPosition;
    Vector3 m_startingPoint;
    Vector3 m_endingPoint;
    

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        m_startingPoint = new Vector3(m_startingPosition.position.x, m_startingPosition.position.y, m_startingPosition.position.z);
        m_endingPoint = new Vector3(m_startingPosition.position.x, m_endingPosition.position.y, m_startingPosition.position.z);
        goingDown = true;
        goingUp = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(time >= max_time)
        {
            time = 0.0f;
            goingDown = !goingDown;
            goingUp = !goingUp;
        }
        
	    if(goingDown)
        {
            tr.position = Vector3.Lerp(m_startingPoint, m_endingPoint, time/max_time);
        }

        if (goingUp)
        {
            tr.position = Vector3.Lerp(m_endingPoint, m_startingPoint, time/max_time);
        }

        time += Time.deltaTime;
	}

}
