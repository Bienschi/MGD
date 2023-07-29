using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
{
    private bool[] completedLevels;
    [SerializeField] private TextMeshProUGUI[] collectableDisplay;

    private void Start()
    {
        completedLevels = DataPersistenceManager.instance.getCompletedLevels();
        for (int i=0;i<completedLevels.Length;i++)
        {
            if(!completedLevels[i] && i < completedLevels.Length - 1) transform.GetChild(i+1).gameObject.SetActive(false);
            collectableDisplay[i].text = DataPersistenceManager.instance.getCollectableCounter(i).ToString();
        }
    }

    public void LevelSelected(int Level)
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene(Level+ Constants.numberOfNoneLevelScenes);
    }

    public void Back()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }
}
