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
    
    // FOR SPECIFIC CONDITIONS ON SPAWNING, e.g. spawning inside the tutorial's oven when it is open,
    // you MUST make a child class extending Item and implement the functionality on a case by case basis

    public IngredientType ingredientID;

    public enum IngredientType {
        Apple,
        Banana,
        Orange,
        Strawberry,
        Blueberry,
        Pineapple,
        None
    }

    private void Awake() {
        Debug.Log(ingredientID);
    }
}
