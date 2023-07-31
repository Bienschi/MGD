using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collectableDisplay;
    private int totalCollectableCount= 0;

    private void Start()
    {
        for (int i=0;i<Constants.numberOfLevels;i++)
        {
            totalCollectableCount += DataPersistenceManager.instance.getCollectableCounter(i);
        }
        collectableDisplay.text = totalCollectableCount.ToString();
    }

    public void Back()
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadScene("LevelSelectionMenu");
    }
}
