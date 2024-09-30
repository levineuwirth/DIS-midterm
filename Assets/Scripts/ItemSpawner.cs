using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Level-Specific Data")]
    public Vector3[] positions; // All positions where items could spawn
    public Item[] spawnableItems; // All items to be spawned, in order of spawning from level start.
    public Item noneItem; // Special case item representing "nothing" (DO NOT ADD TO SPAWNABLE ITEMS!)
    public float spawnDuration = 1f;

    private Queue<Item> spawns;
    private GameObject lastSpawnedItem; // Reference to the last spawned item
    private Item currentItem; // The current item, to use when we need to destroy it.
    private bool addBack; // If we should add the item back into the queue.
    void Start()
    {
        // Initialize the queue
        spawns = new Queue<Item>();

        foreach (Item item in spawnableItems)
        {
            spawns.Enqueue(item); // Add actual items
            spawns.Enqueue(noneItem); // Add the "none" item as a placeholder
        }

        // Start the spawning loop as a coroutine
        StartCoroutine(SpawnItems());
	    PlayerItemCollector.EOnItemPickUp += () => handleCollection();
    }

    IEnumerator SpawnItems()
    {
        while (true)
        { // Loop forever. We should have a reference to this from the player or otherwise stop it externally.
          // That stop will occur by destroying the GameObject.
            // Destroy the last spawned item before spawning a new one
            if (lastSpawnedItem != null)
            {
                Destroy(lastSpawnedItem);
		if(addBack){
		    spawns.Enqueue(currentItem);
		}
                lastSpawnedItem = null; // Clear the reference after destroying
            }

            if (spawns.Count > 0)
            {
                currentItem = spawns.Dequeue();
                Vector3 spawnPosition = this.transform.position;
		addBack = true;
		if (currentItem != null && !currentItem.isNone){
		    lastSpawnedItem = Instantiate(currentItem.gameObject, spawnPosition, Quaternion.identity);
		}
	    }
                
            yield return new WaitForSeconds(spawnDuration);
        }
    }

    Vector3? GetValidSpawnPosition(Item item)
    {
        List<Vector3> validPositions = new List<Vector3>();

        foreach (Vector3 pos in positions)
        {
            foreach (Vector3 allowedPos in item.allowedLocations)
            {
                if (pos == allowedPos)
                {
                    validPositions.Add(pos);
                }
            }
        }

        if (validPositions.Count > 0)
        {
            return validPositions[UnityEngine.Random.Range(0, validPositions.Count)];
        }

        return null;
    }

    private void handleCollection(){
	Destroy(lastSpawnedItem);
	addBack = false;
    }
    
}
