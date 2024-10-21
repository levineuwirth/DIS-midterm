using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot: MonoBehaviour
{
    [Header("Slot-Specific Data")]
    // The item that is in this slot of the recipe. This needs to be accessed by ItemManager.             
    public Sprite associatedItem;
    public Sprite notCollectedImage;
    public Sprite holdingImage;
    public Sprite collectedImage;
    public GameObject itemVisualization;
    public ItemVisualization iz;
    void Awake(){
    }

    void Update(){
    }

    public void onItemCollect(){
        this.gameObject.GetComponent<Image>().sprite = collectedImage;
    }

    public void setItemSprite(){
        Debug.Log("Begin instantiation process.");
        
        GameObject newItemVisualization = Instantiate(itemVisualization, transform);
        newItemVisualization.transform.localPosition = Vector3.zero;
        iz = newItemVisualization.GetComponentInChildren<ItemVisualization>();
        
        if (iz != null) {
            iz.setSprite(associatedItem);
            Debug.Log("ItemVisualization instance created and sprite set.");
        } else {
            Debug.LogError("ItemVisualization component not found in the instantiated object or its children.");
        }
    }

    public void onItemPickup(){
        this.gameObject.GetComponent<Image>().sprite = holdingImage;
    }
}
