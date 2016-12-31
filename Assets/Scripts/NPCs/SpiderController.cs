using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour {

    Transform tr;
    public bool goingDown;
    public bool goingUp;
    float time = 0.0f;
    public float max_time = 2.0f;
    public Transform m_endingPosition;
    Vector3 m_startingPoint;
    Vector3 m_endingPoint;
    

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        m_startingPoint = tr.position;
        m_endingPoint = new Vector3(m_startingPoint.x, m_endingPosition.position.y, m_startingPoint.z);

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
