using System;
using UnityEngine;

public class ItemVisualization : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite itemSprite;
    
    public void Awake(){
	renderer = GetComponent<SpriteRenderer>();
    }

    public void setSprite(Sprite newSprite){
	itemSprite = newSprite;
	renderer.sprite = itemSprite;
    }
}
