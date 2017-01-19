using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject P_butt, P_cred;

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadGame(int i)
    {
        switch (i)
        {
            case 0:
                SceneManager.LoadScene("Level1");
                break;
            case 1:
                SceneManager.LoadScene("Jungle");
                break;
            case 2:
                SceneManager.LoadScene("Level3_Maya");
                break;
        }
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
