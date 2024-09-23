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
	
    }
}
