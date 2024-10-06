using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static event Action EOnRecipeComplete;
    public List<Item.IngredientType> recipe = new List<Item.IngredientType>();
	public List<ItemSlot> slots = new List<ItemSlot>();
	public List<Sprite> itemSprites = new List<Sprite>();
    private int recipeCompleteCounter;
    
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
        recipeCompleteCounter = 0;
    }

    private void checkItemOnRecipe(Item.IngredientType ingredient) {
        if(recipe.Contains(ingredient)) {
            int index = recipe.IndexOf(ingredient);
            slots[index].onItemCollect();
            recipeCompleteCounter++;
            Debug.Log("removed");

            if (recipeCompleteCounter == recipe.Count)
            {
                    Debug.Log("Recipe is complete.");
                    EOnRecipeComplete?.Invoke();
            }
            
        } else {
	    // deal damage?
	    // punishment here
	    }
    }

    private void OnDestroy() {
        PlayerItemCollector.EOnItemSubmit -= checkItemOnRecipe;
    }
 }
