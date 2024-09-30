using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [field: SerializeField] public List<Item.IngredientType> recipe {get ; private set;}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        recipe = new List<Item.IngredientType>();
        PlayerItemCollector.EOnItemDrop += checkItemOnRecipe;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void checkItemOnRecipe(Item.IngredientType ingredient) {
        if(recipe.Contains(ingredient)) {
            recipe.Remove(ingredient);
            Debug.Log("removed");
        }
    }

    private void OnDestroy() {
        PlayerItemCollector.EOnItemDrop -= checkItemOnRecipe;
    }
 }
