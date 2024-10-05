using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public List<Item.IngredientType> recipe = new List<Item.IngredientType>();
	public List<ItemSlot> slots = new List<ItemSlot>();
	public List<Sprite> itemSprites = new List<Sprite>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        PlayerItemCollector.EOnItemSubmit += checkItemOnRecipe;
	int i = 0;
	foreach (ItemSlot slot in slots){
	    slot.associatedItem = itemSprites[i];
	    slot.setItemSprite();
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
        PlayerItemCollector.EOnItemSubmit -= checkItemOnRecipe;
    }
 }
