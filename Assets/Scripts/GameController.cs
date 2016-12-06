using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public bool paused = false;
    public GameObject PauseMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        SceneManager.LoadScene("Prototipo");
    }
}
