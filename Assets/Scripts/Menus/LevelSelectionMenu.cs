using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    public void LevelSelected(int Level)
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(Level);
    }

    public void Back()
    {
        mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
