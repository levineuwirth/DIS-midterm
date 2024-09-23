using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner: MonoBehaviour {
    [Header("Level-Specific Data")]
    public Vector3[] positions;
    public Item[] spawnableItems;
    public float spawnDuration;
    
    void Start(){
	Queue<Item> spawns = new Queue<Item>();
	foreach (Item item in spawnableItems){
	    spawns.Enqueue(item);
	}
	// Queue is now initialized. Send off a coroutine for the spawning loop?
    }

    void Update(){
	// anything here?
    }
}
