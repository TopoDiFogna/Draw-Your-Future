using UnityEngine;
using System.Collections;

public class LevelObjSpawner : MonoBehaviour {

    public GameObject[] m_objects_To_Spawn;

    private void Start()
    {
        foreach (GameObject obj in m_objects_To_Spawn)
        {
            ObjectPoolingManager.Instance.CreatePool(obj, 5, 200);
        }
    }

    public void Restart()
    {
        foreach(GameObject g in m_objects_To_Spawn)
        {
            
        }
    }
}
