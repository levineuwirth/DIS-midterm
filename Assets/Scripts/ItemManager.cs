using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [field: SerializeField] public List<Item.IngredientType> recipe {get ; private set;}
    [field: SerializeField] public List<ItemSlot> slots {get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        recipe = new List<Item.IngredientType>();
	slots = new List<ItemSlot>();
        PlayerItemCollector.EOnItemDrop += checkItemOnRecipe;
	int i = 0;
	foreach (ItemSlot slot in slots){
	    slot.associatedItem = recipe[i];
	    i++;
    }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void checkItemOnRecipe(Item.IngredientType ingredient) {
        if(recipe.Contains(ingredient)) {
	    int index = recipe.IndexOf(ingredient);
	    recipe.Remove(ingredient);
            Debug.Log("removed");
	    slots[index].onItemCollect();
        } else {
	    // deal damage?
	    // punishment here
	}
    }

    private void OnDestroy() {
        PlayerItemCollector.EOnItemDrop -= checkItemOnRecipe;
    }
 }
