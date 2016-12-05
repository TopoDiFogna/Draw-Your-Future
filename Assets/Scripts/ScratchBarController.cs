using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScratchBarController : MonoBehaviour {

    public Image fullBar;
    private float basic_width;
    public float max_pool_size = 200;
    private float current_pool_size;

	// Use this for initialization
	void Start () {
        basic_width = fullBar.rectTransform.rect.height;
        current_pool_size = max_pool_size;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeSize(int increment)
    {
        current_pool_size += increment;
        float width_percentage = (current_pool_size / max_pool_size);
        fullBar.transform.localScale = new Vector3(width_percentage, fullBar.transform.localScale.y);


        /*fullBar.rectTransform.rect.Set(fullBar.rectTransform.rect.position.x,
            fullBar.rectTransform.rect.position.y,
            basic_width * width_percentage,
            fullBar.rectTransform.rect.height);*/
            
    }
}
