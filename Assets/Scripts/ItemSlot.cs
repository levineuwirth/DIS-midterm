using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlot: MonoBehaviour
{
    [Header("Slot-Specific Data")]
    // The item that is in this slot of the recipe. This needs to be accessed by ItemManager.
    public Item.IngredientType associatedItem; 
    public Sprite notCollectedImage;
    public Sprite collectedImage;
    
    void Awake(){
	gameObject.GetComponent<Image>().sprite = notCollectedImage;
    }

    void Update(){
    }

    public void onItemCollect(){
	gameObject.GetComponent<Image>().sprite = collectedImage;
    }
}
