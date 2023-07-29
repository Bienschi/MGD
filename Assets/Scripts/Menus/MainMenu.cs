using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        if(!DataPersistenceManager.instance.HasGameData())
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
    public void PlayGame()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    public void NewGame()
    {
        DataPersistenceManager.instance.NewGame();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
