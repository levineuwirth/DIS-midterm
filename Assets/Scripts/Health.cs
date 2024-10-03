using UnityEngine;

public class Health : MonoBehaviour
{
    
    [field: SerializeField] public int maxHealth {get ; private set;}
    [field: SerializeField] public HealthBar healthBar {get ; private set;}
    private int _currentHealth;

    // TODO: trigger event for taking damage for UI
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace)) {
            takeDamage(1);
        }

        if(_currentHealth == 0) {
            PlayerAnimation.Instance.playerAnimator.SetBool("Dead", true);
        }
    }

    private void takeDamage(int damage) {
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);
    }
}
