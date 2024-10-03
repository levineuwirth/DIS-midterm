using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlot: MonoBehaviour
{
    [Header("Slot-Specific Data")]
    // The item that is in this slot of the recipe. This needs to be accessed by ItemManager.
    public Sprite associatedItem; 
    public Sprite notCollectedImage;
    public Sprite collectedImage;

    private ItemVisualization itemVisualization;
    
    void Awake(){
	gameObject.GetComponent<Image>().sprite = notCollectedImage;
	itemVisualization.renderer.sprite = associatedItem;
	Instantiate(itemVisualization, this.transform.position, Quaternion.identity);
    }

    void Update(){
    }

    public void onItemCollect(){
	gameObject.GetComponent<Image>().sprite = collectedImage;
    }
}
