using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextMeshProUGUI collectableDisplay;
    [SerializeField] private TextMeshProUGUI maxCollectableDisplay;
    private int currentCollectableCount;
    private int maxCollectableCount;
    private int currentLevelNumber;

    public void Awake()
    {
        currentLevelNumber = SceneManager.GetActiveScene().buildIndex - Constants.numberOfNoneLevelScenes;
        maxCollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
    }

    public void Start()
    {
        collectableDisplay.text = this.currentCollectableCount.ToString();
        maxCollectableDisplay.text = this.maxCollectableCount.ToString();
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
