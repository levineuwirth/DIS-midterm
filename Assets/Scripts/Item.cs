using System;
using UnityEngine;

public class Item : MonoBehaviour {
    [Header("Properties")]
    // Determines if the item is beneficial to the recipe or not.
    // To be set on the basis of the current level.
    public bool isTrash;
    // Determines if this is the None item.
    // If true, this is what we use to implement pauses in spawning via the ItemSpawner.
    public bool isNone;
    // This has to be public. The ItemSpawner needs to know where the item can spawn.
    // Entries here also have to be in the ItemSpawner's spawn locations array.
    public Vector3[] allowedLocations;
    // other properties that we haven't thought of will go here...

    public void onSubmit(){
	// if it's trash, bad, if it isn't, good!
	// we may move this logic to the player depending on the final design
	// (it isn't a priority right now)
    }
}
