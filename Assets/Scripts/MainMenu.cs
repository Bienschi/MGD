using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if(DataPersistenceManager.instance.cleanSaveFile)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void NewGame()
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
