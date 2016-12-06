using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject P_butt, P_cred;

	public void Exit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Prototipo");
    }

    public void CreditsOn()
    {
        P_butt.SetActive(false);
        P_cred.SetActive(true);
    }

    public void CreditsOff()
    {
        P_butt.SetActive(true);
        P_cred.SetActive(false);
    }
}
