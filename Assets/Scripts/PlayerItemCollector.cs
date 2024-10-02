using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    
    private bool isHoldingItem;
    private Item.IngredientType currentIngredient;

    public Vector2 pickUpHitboxSize;
    private LayerMask _itemLayerMask;

    public delegate void OnItemDrop(Item.IngredientType currentIngredientType);
    public static OnItemDrop EOnItemDrop;
    public delegate void OnItemPickUp();
    public static OnItemPickUp EOnItemPickUp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentIngredient = Item.IngredientType.None;
        isHoldingItem = false;
        _itemLayerMask = LayerMask.GetMask("Item");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
            pickUpItem();
        }
        
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
    private void pickUpItem() {
        Collider2D hitItem = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, _itemLayerMask);
        
        if(hitItem != null) {
            currentIngredient = hitItem.GetComponent<Item>().ingredientID;
            isHoldingItem = true;
            Debug.Log("pickedUpItem");
            EOnItemPickUp?.Invoke();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, pickUpHitboxSize);
    }
}
