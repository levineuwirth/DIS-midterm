using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    
    private bool isHoldingItem;
    private Item.IngredientType currentIngredient;
    public delegate void OnItemDrop(Item.IngredientType currentIngredientType);
    public static OnItemDrop EOnItemDrop;
    public delegate void OnItemPickUp();
    public static OnItemPickUp EOnItemPickUp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIngredient = Item.IngredientType.None;
        isHoldingItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHoldingItem && Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
            dropItem();
        }
    }

    private void dropItem() {
        EOnItemDrop?.Invoke(currentIngredient);
        currentIngredient = Item.IngredientType.None;
        isHoldingItem = false;
    }

    // on press j, spawn a physics overlap circle - check for item overlap
    private void OnTriggerStay2D(Collider2D other) {
        Debug.Log(other.name);
        
        if(other.tag == "Item") {
            Debug.Log("tagging");
        }

        if(other.tag == "Item" && Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
            currentIngredient = other.GetComponent<Item>().ingredientID;
            isHoldingItem = true;
            Debug.Log("pickedUpItem");
            EOnItemPickUp?.Invoke();
        }
    }
}
