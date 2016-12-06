using UnityEngine;
using System.Collections;

public class MouseSpawn : MonoBehaviour {

    public GameObject m_scratch;
    Vector3 mouse_position;

    int number_of_scratches = 200;
    public int NumberOfScratches
    {
        get { return number_of_scratches; }
        set { number_of_scratches = value; }
    }

    ScratchBarController scratchBar;
    GameController gc;

	// Use this for initialization
	void Start () {
        ObjectPoolingManager.Instance.CreatePool(m_scratch, number_of_scratches, number_of_scratches);
        gc = gameObject.GetComponent<GameController>();
        scratchBar = gameObject.GetComponent<ScratchBarController>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && !gc.Pause) 
        {
            mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject go = ObjectPoolingManager.Instance.GetObject("Paint");
            if (go != null)
            {
                go.transform.position = new Vector3(mouse_position.x, mouse_position.y, 0);
                go.transform.rotation = Quaternion.identity;
                scratchBar.ChangeSize(-1);
            }
        }
	}
}
