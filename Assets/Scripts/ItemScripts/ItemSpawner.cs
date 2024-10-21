using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Level-Specific Data")]
    [field: SerializeField] public Item[] spawnableItems {get; private set;}
    [field: SerializeField] public Item noneItem {get; private set;}
    [field: SerializeField] public float spawnDuration {get; private set;}

    private Queue<Item> _spawns;
    private GameObject _lastSpawnedItem;
    private Item _currentItem;
    private bool _addBack;
    void Start()
    {
        // Initialize the queue
        _spawns = new Queue<Item>();

        foreach (Item item in spawnableItems)
        {
            _spawns.Enqueue(item); // Add actual items
            _spawns.Enqueue(noneItem); // Add the "none" item as a placeholder
        }

        // Start the spawning loop as a coroutine
        StartCoroutine(SpawnItems());
	    PlayerItemCollector.EOnItemPickUp += handleCollection;
    }

    IEnumerator SpawnItems()
    {
        while (true)
        { // Loop forever. We should have a reference to this from the player or otherwise stop it externally.
          // That stop will occur by destroying the GameObject.
            // Destroy the last spawned item before spawning a new one
            if (_lastSpawnedItem != null)
            {
                Destroy(_lastSpawnedItem);
		        if(_addBack){
		            _spawns.Enqueue(_currentItem);
		        }
                _lastSpawnedItem = null; // Clear the reference after destroying
            }

            if (_spawns.Count > 0)
            {
                _currentItem = _spawns.Dequeue();
                Vector3 spawnPosition = this.transform.position;
		        _addBack = true;
		        if (_currentItem != null && !_currentItem.isNone){
		            _lastSpawnedItem = Instantiate(_currentItem.gameObject, spawnPosition, Quaternion.identity);
		        }
	        }
                
            yield return new WaitForSeconds(spawnDuration);
        }
    }

    private void handleCollection(Item.IngredientType playerIngredient){
	    Debug.Log("pickup event heard");
        if(_lastSpawnedItem != null) {
            if(_lastSpawnedItem.GetComponent<Item>().ingredientID == playerIngredient) {
                Destroy(_lastSpawnedItem);
                _addBack = false;
            }
        }
    }
    
}
