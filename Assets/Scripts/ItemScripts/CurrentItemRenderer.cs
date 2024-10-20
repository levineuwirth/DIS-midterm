using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrentItemRenderer : MonoBehaviour
{
    // create array of size = to the # of ingredients in our enum
    public GameObject[] ingredientPrefabs = new GameObject[Enum.GetNames(typeof(Item.IngredientType)).Length];

    private Dictionary<Item.IngredientType, GameObject> ingredientDictionary;

    void Awake() {
        //PlayerItemCollector.EOnItemPickUp += displayCurrentItem;

        Array ingredientTypes = Enum.GetValues(typeof(Item.IngredientType));

        for(int i = 0; i < ingredientTypes.Length; i++) {
            ingredientDictionary.Add((Item.IngredientType) ingredientTypes.GetValue(i), ingredientPrefabs[i]);
        }

    }

    private void displayCurrentItem(Item.IngredientType currentItem) {
        Debug.Log("display Item");
        Instantiate(ingredientDictionary[currentItem], transform.position, Quaternion.identity);
    }

    private void OnDestroy() {
        PlayerItemCollector.EOnItemPickUp -= displayCurrentItem;
    }
}
