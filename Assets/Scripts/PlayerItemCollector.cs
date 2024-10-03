using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    
    private bool isHoldingItem;
    private Item.IngredientType _currentIngredientType;
    private Collider2D _nearestIngredient;

    public Vector2 pickUpHitboxSize;
    public LayerMask itemLayerMask;
    public LayerMask itemSubmitLayerMask;

    public delegate void OnItemSubmit(Item.IngredientType currentIngredientType);
    public static OnItemSubmit EOnItemSubmit;
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
            Debug.Log("submit item called");
            submitItem();
        }
        else if(Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
            pickUpItem();
        }
    }

    private void submitItem() {
        Collider2D inItemSubmitter = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, itemSubmitLayerMask);

        if(inItemSubmitter != null) {
            EOnItemSubmit?.Invoke(_currentIngredientType);
            _currentIngredientType = Item.IngredientType.None;
            isHoldingItem = false;
        }
    }

    private void checkForItem() {
        _nearestIngredient = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, itemLayerMask);

        if(_nearestIngredient != null) {
            Debug.Log(_nearestIngredient.gameObject.GetComponent<Item>().ingredientID);
            // Invoke event to have popUp
        }
    }

    // move overlap to update cycle and add var nearestIng - indicate that you can press j
    private void pickUpItem() {
        if(_nearestIngredient != null) {
            _currentIngredientType = _nearestIngredient.GetComponent<Item>().ingredientID;
            isHoldingItem = true;

            Debug.Log("pickedUpItem");
            EOnItemPickUp?.Invoke();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, pickUpHitboxSize);
    }
}
