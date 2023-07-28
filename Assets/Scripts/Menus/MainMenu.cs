using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelSelectionMenu levelSelectionMenu;
    private void Start()
    {
        if(!DataPersistenceManager.instance.HasGameData())
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }
    public void PlayGame()
    {
        levelSelectionMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void NewGame()
    {
        DataPersistenceManager.instance.NewGame();
        levelSelectionMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
