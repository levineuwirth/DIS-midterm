using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static event Action EOnRecipeComplete;
    [field: SerializeField] public List<Item.IngredientType> recipe {get; private set;}
	[field: SerializeField] public List<ItemSlot> slots {get; private set;}
	[field: SerializeField] public List<Sprite> itemSprites {get; private set;}
    private int _recipeCompleteCounter;
    
    void Awake()
    {
        PlayerItemCollector.EOnItemSubmit += checkItemOnRecipe;
        PlayerItemCollector.EOnItemPickUp += checkItemOnPickup;
        _recipeCompleteCounter = 0;
    }

    private void checkItemOnPickup(Item.IngredientType ingredient) {
        if(recipe.Contains(ingredient)) {
            int index = recipe.IndexOf(ingredient);
            slots[index].onItemPickup();
        }
    }

    private void checkItemOnRecipe(Item.IngredientType ingredient) {
        if(recipe.Contains(ingredient)) {
            int index = recipe.IndexOf(ingredient);
            slots[index].onItemCollect();
            _recipeCompleteCounter++;
            Debug.Log("removed");

            if (_recipeCompleteCounter == recipe.Count)
            {
                    Debug.Log("Recipe is complete.");
                    EOnRecipeComplete?.Invoke();
            }
        }
    }

    private void OnDestroy() {
        PlayerItemCollector.EOnItemSubmit -= checkItemOnRecipe;
        PlayerItemCollector.EOnItemPickUp -= checkItemOnPickup;
    }
 }
