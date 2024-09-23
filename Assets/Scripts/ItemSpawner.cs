using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner: MonoBehaviour {
    [Header("Level-Specific Data")]
    public Vector3[] positions;
    public Item[] spawnableItems;
    // this Item represents when nothing should spawn. Special case.
    // be sure to NOT add this to the spawnableItems. Only here!
    public Item noneItem;
    public float spawnDuration;
    
    void Start(){
	Queue<Item> spawns = new Queue<Item>();
	foreach (Item item in spawnableItems){
	    spawns.Enqueue(item);
	    spawns.Enqueue(noneItem);
	}
	// Queue is now initialized. Send off a coroutine for the spawning loop?
    }

    void Update(){
	// anything here?
	// lean towards starting this logic in a coroutine launched at the end of Start()
    }
}
