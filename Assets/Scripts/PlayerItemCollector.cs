using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    
    private bool isHoldingItem;
    private Item.IngredientType _currentIngredientType;
    private Collider2D _nearestIngredient;
    private GameObject _currentIngredient;

    public Vector2 pickUpHitboxSize;
    public LayerMask _itemLayerMask;

    public delegate void OnItemDrop(Item.IngredientType currentIngredientType);
    public static OnItemDrop EOnItemDrop;
    public delegate void OnItemPickUp();
    public static OnItemPickUp EOnItemPickUp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentIngredientType = Item.IngredientType.None;
        isHoldingItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkForItem();
        
        if(isHoldingItem && Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
            Debug.Log("dropitem called");
            dropItem();
        }
        else if(Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
            pickUpItem();
        }
    }

    private void dropItem() {
        EOnItemDrop?.Invoke(_currentIngredientType);
        Instantiate(_currentIngredient, transform.position, Quaternion.identity);
        _currentIngredientType = Item.IngredientType.None;
        isHoldingItem = false;
    }

    private void checkForItem() {
        _nearestIngredient = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, _itemLayerMask);

        if(_nearestIngredient != null) {
            // Invoke event to have popUp
        }
    }

    // move overlap to update cycle and add var nearestIng - indicate that you can press j
    private void pickUpItem() {
        if(_nearestIngredient != null) {
            _currentIngredientType = _nearestIngredient.GetComponent<Item>().ingredientID;
            _currentIngredient = _nearestIngredient.gameObject;
            isHoldingItem = true;

            Debug.Log("pickedUpItem");
            EOnItemPickUp?.Invoke();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, pickUpHitboxSize);
    }
}
