using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour, IDataPersistence
{
    bool reached = false;
    private int currentLevelNumber;

    public void Awake()
    {
        currentLevelNumber = SceneManager.GetActiveScene().buildIndex - Constants.numberOfNoneLevelScenes;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("reachedGoal", 0.5f);
        }
    }
    private void reachedGoal()
    {
        reached = true;
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    public void LoadData(GameData data)
    {
        reached = data.completedLevels[currentLevelNumber];
    }

    public void SaveData(GameData data)
    {
        if(!data.completedLevels[currentLevelNumber])
        data.completedLevels[currentLevelNumber] = reached;
    }
}
