using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour
{

    GameController gc;
    Transform tr;
    public float timetolive;
    float timer = 0f;
    bool vanishing = false;
    Vector3 scale;
    ScratchBarController scratchBar;

    // Use this for initialization
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        tr = transform;
        scale = tr.localScale;
        scratchBar = GameObject.FindObjectOfType<ScratchBarController>();
    }

    void OnEnable()
    {
        timer = 0f;
        vanishing = false;
        scratchBar = GameObject.FindObjectOfType<ScratchBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gc.Pause)
        {
            timer += Time.deltaTime;
            if (timer >= timetolive && !vanishing)
            {
                timer = 0f;
                vanishing = true;
            }
            if (vanishing)
            {
                tr.localScale -= new Vector3(0.01f, 0.01f, 0);
                if (tr.localScale.x <= 0)
                {
                    StartCoroutine(DisablePaint());
                    tr.localScale = scale;
                }
            }
        }
    }

    public void DisablePaintRoutine()
    {
        StartCoroutine("DisablePaint");
    }

    IEnumerator DisablePaint()
    {
        tr.position = new Vector2(1000, 1000);
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        scratchBar.ChangeSize(+1);
        tr.localScale = scale;
    }
}
