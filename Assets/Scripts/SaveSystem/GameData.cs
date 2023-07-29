using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int[] collectableCounter;
    public SerializableDictionary<string, bool>[] collectedCollectables;
    public bool[] completedLevels;

    public GameData()
    {
        collectableCounter = new int[Constants.numberOfLevels];
        collectedCollectables = new SerializableDictionary<string, bool>[Constants.numberOfLevels];
        for (int i = 0; i < Constants.numberOfLevels; i++)
        {
            collectedCollectables[i] = new SerializableDictionary<string, bool>();
        }

        completedLevels = new bool[Constants.numberOfLevels];
    }
}
