using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, bool> collectedCollectables;
    public int currentCollectableCount;

    public GameData()
    {
        this.currentCollectableCount = 0;
        collectedCollectables = new SerializableDictionary<string, bool>();
    }
}
