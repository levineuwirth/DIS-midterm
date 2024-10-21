using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private bool isHoldingItem;
    private Item.IngredientType _currentIngredientType;
    private Collider2D _nearestIngredient;

    public Vector2 pickUpHitboxSize;
    public LayerMask itemLayerMask;
    public LayerMask itemSubmitLayerMask;

    [field: SerializeField] public AudioSource itemPickupSFX {get ; private set;}
    [field: SerializeField] public AudioSource itemCantPickupSFX {get ; private set;}
    [field: SerializeField] public AudioSource itemSubmitSFX {get ; private set;}

    public delegate void OnItemSubmit(Item.IngredientType currentIngredientType);
    public static OnItemSubmit EOnItemSubmit;
    public delegate void OnItemPickUp(Item.IngredientType currentIngredientType);
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
        
        if(isHoldingItem) {
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
            isHoldingItem = false;
        }
    }

    private void checkForItem() {
        _nearestIngredient = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, itemLayerMask);

        if(_nearestIngredient != null) {
            // Debug.Log(_nearestIngredient.gameObject.GetComponent<Item>().ingredientID);
            // Invoke event to have popUp
        }
    }

    // move overlap to update cycle and add var nearestIng - indicate that you can press j
    private void pickUpItem() {
        if(_nearestIngredient != null) {
            _currentIngredientType = _nearestIngredient.GetComponent<Item>().ingredientID;
            isHoldingItem = true;
            itemPickupSFX.Play();
            Debug.Log("pickedUpItem");
            EOnItemPickUp?.Invoke(_currentIngredientType);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, pickUpHitboxSize);
    }
}

/*
using UnityEngine;
using UnityEngine.UI; // Add this for UI Text

public class PlayerItemCollector : MonoBehaviour
{
    private bool isHoldingItem;
    private Item.IngredientType _currentIngredientType;
    private Collider2D _nearestIngredient;

    public Vector2 pickUpHitboxSize;
    public LayerMask itemLayerMask;
    public LayerMask itemSubmitLayerMask;

    public Text feedbackText; // Reference to the UI Text for feedback
    public float feedbackDuration = 2f; // Duration for how long the feedback will show

    public delegate void OnItemSubmit(Item.IngredientType currentIngredientType);
    public static OnItemSubmit EOnItemSubmit;
    public delegate void OnItemPickUp(Item.IngredientType currentIngredientType);
    public static OnItemPickUp EOnItemPickUp;

    void Start()
    {
        _currentIngredientType = Item.IngredientType.None;
        isHoldingItem = false;
        feedbackText.text = ""; // Make sure feedback text is initially empty
    }

    void Update()
    {
        checkForItem();

        if (isHoldingItem && Input.GetKeyDown(PlayerController.Instance.pickOrDropItem))
        {
            Debug.Log("submit item called");
            submitItem();
        }
        else if (Input.GetKeyDown(PlayerController.Instance.pickOrDropItem))
        {
            pickUpItem();
        }
    }

    private void submitItem()
    {
        Collider2D inItemSubmitter = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, itemSubmitLayerMask);

        if (inItemSubmitter != null)
        {
            EOnItemSubmit?.Invoke(_currentIngredientType);
            _currentIngredientType = Item.IngredientType.None;
            isHoldingItem = false;
            feedbackText.text = ""; // Clear the feedback when the item is submitted
        }
    }

    private void checkForItem()
    {
        _nearestIngredient = Physics2D.OverlapBox(transform.position, pickUpHitboxSize, 0, itemLayerMask);

        if (_nearestIngredient != null)
        {
            // You can add a visual indicator that an item is nearby
        }
    }

    private void pickUpItem()
    {
        if (isHoldingItem)
        {
            // Show feedback if already holding an item
            ShowFeedback("You are already holding an item!");
            return;
        }

        if (_nearestIngredient != null)
        {
            _currentIngredientType = _nearestIngredient.GetComponent<Item>().ingredientID;
            isHoldingItem = true;

            Debug.Log("pickedUpItem");
            EOnItemPickUp?.Invoke(_currentIngredientType);
            ShowFeedback("Item picked up!");
        }
    }

    // Display feedback message on the UI
    private void ShowFeedback(string message)
    {
        feedbackText.text = message;
        Invoke("ClearFeedback", feedbackDuration);
    }

    // Clear the feedback after a set duration
    private void ClearFeedback()
    {
        feedbackText.text = "";
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, pickUpHitboxSize);
    }
}

*/
