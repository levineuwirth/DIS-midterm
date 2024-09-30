using UnityEngine;

public class PlayerController : MonoBehaviour {

    [field: SerializeField] public KeyCode left {get ; private set;}
    [field: SerializeField] public KeyCode right {get ; private set;}
    [field: SerializeField] public KeyCode jump {get ; private set;}
    [field: SerializeField] public KeyCode pickOrDropItem {get ; private set;}
    public static PlayerController Instance;
    void Awake() {
        Instance = this;
    }
}