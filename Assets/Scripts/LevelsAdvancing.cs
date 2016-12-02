using UnityEngine;
using System.Collections;

public class LevelsAdvancing : MonoBehaviour {

    private int currentLevel = 1;
    public LevelsCheckpoint[] m_levels;
    public PlayerController script;

	// Use this for initialization
	void Start () {
        script.StartingPosition = m_levels[currentLevel].StartingPoint;
        script.CheckPointPosition = m_levels[currentLevel].CheckPoint;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
