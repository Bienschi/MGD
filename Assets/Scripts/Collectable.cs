using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableCounter collectableCounter;
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float hoverSpeed = 1f;
    [SerializeField] private float hoverDistance = 0.5f;
    private Vector3 startPos;

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
            collectableCounter.increaseCollectableCount();
            Destroy(gameObject);
        }
    }
}
