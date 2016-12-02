using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class LevelsCheckpoint : ScriptableObject {

    public Transform startingObject;
    public Vector3 StartingPoint{
        get
        {
            return startingPoint;
        }
    }
    private Vector3 startingPoint;

    public Transform checkpointObject;
    public Vector3 CheckPoint
    {
        get
        {
            return checkPoint;
        }
    }
    private Vector3 checkPoint;

    private void OnEnable()
    {
        startingPoint = new Vector3(startingObject.position.x, startingObject.position.y, 0);
        checkPoint = new Vector3(checkpointObject.position.x, checkpointObject.position.y, 0);
    }
}
