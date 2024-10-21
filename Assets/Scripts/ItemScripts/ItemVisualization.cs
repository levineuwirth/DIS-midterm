using System;
using UnityEngine;

public class ItemVisualization : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer spriteRenderer {get ; private set;}
    [field: SerializeField] public Sprite itemSprite {get ; private set;}
    
    public void Awake(){
	    spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void setSprite(Sprite newSprite){
	    itemSprite = newSprite;
	    spriteRenderer.sprite = itemSprite;
    }
}
