using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot: MonoBehaviour
{
    [Header("Slot-Specific Data")]
    [field: SerializeField] public Sprite associatedItem;
    [field: SerializeField] public Sprite notCollectedImage {get; private set;}
    [field: SerializeField] public Sprite holdingImage {get; private set;}
    [field: SerializeField] public Sprite collectedImage {get; private set;}
    [field: SerializeField] public GameObject itemVisualization {get; private set;}    
    [field: SerializeField] public ItemVisualization iz {get; private set;}

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
