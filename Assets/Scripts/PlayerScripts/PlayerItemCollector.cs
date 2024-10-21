using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    [field: SerializeField] public Vector2 pickUpHitboxSize {get ; private set;}
    [field: SerializeField] public LayerMask itemLayerMask {get ; private set;}
    [field: SerializeField] public LayerMask itemSubmitLayerMask {get ; private set;}
    [field: SerializeField] public AudioSource itemPickupSFX {get ; private set;}
    [field: SerializeField] public AudioSource itemCantPickupSFX {get ; private set;}
    [field: SerializeField] public AudioSource itemSubmitSFX {get ; private set;}

    public delegate void OnItemSubmit(Item.IngredientType currentIngredientType);
    public static OnItemSubmit EOnItemSubmit;
    public delegate void OnItemPickUp(Item.IngredientType currentIngredientType);
    public static OnItemPickUp EOnItemPickUp;

    private bool _isHoldingItem;
    private Item.IngredientType _currentIngredientType;
    private Collider2D _nearestIngredient;
    void Start()
    {
        _currentIngredientType = Item.IngredientType.None;
        _isHoldingItem = false;
    }

    void Update()
    {
        checkForItem();
        
        if(_isHoldingItem) {
            Debug.Log("submit item called");
            submitItem();

            if(_nearestIngredient != null && Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
                itemCantPickupSFX.Play();
            }
        }
        else if(Input.GetKeyDown(PlayerController.Instance.pickOrDropItem)) {
            pickUpItem();
        }
    }

    private void submitItem() {
        Collider2D inItemSubmitter = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, itemSubmitLayerMask);

        if(inItemSubmitter != null) {
            EOnItemSubmit?.Invoke(_currentIngredientType);
            itemSubmitSFX.Play();
            _currentIngredientType = Item.IngredientType.None;
            _isHoldingItem = false;
        }
    }

    private void checkForItem() {
        _nearestIngredient = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, itemLayerMask);
    }
    private void pickUpItem() {
        if(_nearestIngredient != null) {
            _currentIngredientType = _nearestIngredient.GetComponent<Item>().ingredientID;
            _isHoldingItem = true;
            itemPickupSFX.Play();
            Debug.Log(_currentIngredientType);
            EOnItemPickUp?.Invoke(_currentIngredientType);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, pickUpHitboxSize);
    }
}