using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableCounter : MonoBehaviour
{
    private int collectableCount = 0;
    public void increaseCollectableCount()
    {
        collectableCount++;
        gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = collectableCount.ToString();
    }
}
