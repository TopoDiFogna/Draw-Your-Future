using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackgroundSound : MonoBehaviour {

    private static volatile BackgroundSound instance;
    AudioSource source;
    public AudioClip menu_bg;
    public AudioClip level1_bg;
    public AudioClip level2_bg;
    public AudioClip level3_bg;

    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
            source = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    private void OnLevelWasLoaded(int level)
    {
        string active_scene = SceneManager.GetActiveScene().name;
        AudioSource source = GetComponent<AudioSource>();
        if (active_scene == "MainMenu")
        {
            source.clip = menu_bg;
        }
        if (active_scene == "Level1")
        {
            source.clip = level1_bg;
        }
        if (active_scene == "Jungle")
        {
            source.clip = level2_bg;
        }
        if (active_scene == "Level3_Maya")
        {
            source.clip = level3_bg;
        }

        source.Play();
    }
}
