using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float hoverSpeed = 1f;
    [SerializeField] private float hoverDistance = 0.5f;
    private Vector3 startPos;
    public bool collected = false;
    private int currentLevelNumber;

    public void Awake()
    {
        currentLevelNumber = SceneManager.GetActiveScene().buildIndex - Constants.numberOfNoneLevelScenes;
    }

    private void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Cos(Time.time * hoverSpeed) * hoverDistance + startPos.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<CollectableManager>().increaseCollectableCount();
            collected = true;
            gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        data.collectedCollectables[currentLevelNumber].TryGetValue(gameObject.name, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.collectedCollectables[currentLevelNumber].ContainsKey(gameObject.name))
        {
            data.collectedCollectables[currentLevelNumber].Remove(gameObject.name);
        }
        data.collectedCollectables[currentLevelNumber].Add(gameObject.name, collected);
    }
}
