using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [Header("Level-Specific Data")]
    public Vector3[] positions;  // All positions where items could spawn
    public Item[] spawnableItems;  // All items to be spawned, in order of spawning from level start.
    public Item noneItem;  // Special case item representing "nothing" (DO NOT ADD TO SPAWNABLE ITEMS!)
    public float spawnDuration = 1f; 

    private Queue<Item> spawns;

    void Start() {
        // Initialize the queue
        spawns = new Queue<Item>();
        
        foreach (Item item in spawnableItems) {
            spawns.Enqueue(item);  // Add actual items
            spawns.Enqueue(noneItem);  // Add the "none" item as a placeholder
        }

        // Start the spawning loop as a coroutine
        StartCoroutine(SpawnItems());
    }

    IEnumerator SpawnItems() {
        while (true) {  // Loop forever. We should have a reference to this from the player or otherwise stop it externally.
        // That stop will occur by destroying the GameObject.
            if (spawns.Count > 0) {
                Item currentItem = spawns.Dequeue();
                Vector3? spawnPosition = GetValidSpawnPosition(currentItem);
                if (currentItem != noneItem && spawnPosition.HasValue) {
                    Instantiate(currentItem, spawnPosition.Value, Quaternion.identity);
                }
                spawns.Enqueue(currentItem);
            }
            yield return new WaitForSeconds(spawnDuration);
        }
    }
    Vector3? GetValidSpawnPosition(Item item) {
        List<Vector3> validPositions = new List<Vector3>();

        foreach (Vector3 pos in positions) {
            foreach (Vector3 allowedPos in item.allowedLocations) {
                if (pos == allowedPos) {
                    validPositions.Add(pos);
                }
            }
        }

        if (validPositions.Count > 0) {
            return validPositions[UnityEngine.Random.Range(0, validPositions.Count)];
        }

        return null;
    }

    void Update() {
        // Coroutine handles it, nothing here for now!
    }
}
