using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TextMeshProUGUI collectableDisplay;
    private int currentCollectableCount = 0;

    public void LoadData(GameData data)
    {
        this.currentCollectableCount = data.currentCollectableCount;
        collectableDisplay.text = this.currentCollectableCount.ToString();     
    }

    public void SaveData(GameData data)
    {
        data.currentCollectableCount = this.currentCollectableCount;
    }

    public void increaseCollectableCount()
    {
        currentCollectableCount++;
        collectableDisplay.text = currentCollectableCount.ToString();
    }
}
