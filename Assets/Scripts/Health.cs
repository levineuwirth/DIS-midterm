using UnityEngine;

public class Health : MonoBehaviour
{
    
    [field: SerializeField] public float healthValue {get ; private set;}

    // TODO: trigger event for taking damage for UI
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void takeDamage() {
        healthValue -= 1;
    }
}
