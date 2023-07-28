using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextMeshProUGUI collectableDisplay;
    private int currentCollectableCount;
    private int maxCollectableCount;
    private int currentLevelNumber;

    public void Awake()
    {
        currentLevelNumber = SceneManager.GetActiveScene().buildIndex - Constants.numberOfNoneLevelScenes;
    }

    public void Start()
    {
        maxCollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
        collectableDisplay.text = this.currentCollectableCount.ToString();
    }

    public void increaseCollectableCount()
    {
        currentCollectableCount++;
        collectableDisplay.text = currentCollectableCount.ToString();
    }

    public void LoadData(GameData data)
    {
        currentCollectableCount = data.collectableCounter[currentLevelNumber];
    }

    public void SaveData(GameData data)
    {
        data.collectableCounter[currentLevelNumber] = currentCollectableCount;
    }
}
