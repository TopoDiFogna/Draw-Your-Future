using UnityEngine;
using System.Collections;

public class SwingingController : MonoBehaviour {

    HingeJoint2D hinge;
    JointMotor2D motor;
    float time;
    public float max_time;
    public float m_motor_speed;

	// Use this for initialization
	void Start () {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        motor.motorSpeed = m_motor_speed;
        hinge.motor = motor;
        hinge.useMotor = true;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time >= max_time)
        {
            time = 0;
            motor.motorSpeed = -motor.motorSpeed;
            hinge.motor = motor;
            hinge.useMotor = true;
        }
	}
}
