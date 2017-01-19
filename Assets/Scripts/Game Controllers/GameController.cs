using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public bool ended = false;
    public bool paused = false;
    public bool Pause
    {
        get { return paused; }
    }
    public GameObject PauseMenu;
    public GameObject WinMenu;
    PlayerController player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !player.Dead && !ended)
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
        string active_scene_name = SceneManager.GetActiveScene().name;
        if (active_scene_name == "Jungle")
        {
            DeactivateRockForGeyser();
        }
        SceneManager.LoadScene(active_scene_name);
    }

    public void ShowWinningMenu()
    {
        WinMenu.SetActive(true);
        paused = true;
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        string active_name = SceneManager.GetActiveScene().name;
        if (active_name == "Level1")
        {
            SceneManager.LoadScene("Jungle");
        }
        if (active_name == "Jungle")
        {
            DeactivateRockForGeyser();
            SceneManager.LoadScene("Level3_Maya");
        }
        paused = false;
        Time.timeScale = 1;
    }

    void DeactivateRockForGeyser()
    {
        //GameObject rock = GameObject.Find("RockForGeyser");
        //rock.SetActive(false);
        GameObject.FindObjectOfType<Geyser>().enabled = true;
        RockSpawner spwnr = GameObject.FindGameObjectWithTag("SpawnerForGeyser").GetComponent<RockSpawner>();
        spwnr.spawned_objects = 0;
        spwnr.DeactivateRockForGeyser();
    }
}
