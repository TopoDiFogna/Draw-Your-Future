using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public bool paused = false;
    public GameObject PauseMenu;
    PlayerController player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !player.dead)
        {
            if (!paused)
            {
                PauseMenu.SetActive(true);
                paused = true;
                Time.timeScale = 0;
            }
            else
            {
                UnPause();
            }
        }
	}

    public void UnPause()
    {
        PauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        UnPause();
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        UnPause();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Scratch"))
        {
            g.SetActive(false);
        }
        SceneManager.LoadScene("Prototipo");
    }
}
