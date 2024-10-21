using UnityEngine;

public class Health : MonoBehaviour
{
    
    [field: SerializeField] public int maxHealth {get ; private set;}
    [field: SerializeField] public HealthBar healthBar {get ; private set;}
    [field: SerializeField] public AudioSource damageSFX {get ; private set;}
    [field: SerializeField] public float setInvulnerabilityDuration {get ; private set;}
    public delegate void OnDamageTaken();
    public static OnDamageTaken EOnDamageTaken;
    private int _currentHealth;
    private float _invulnerabilityDuration;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        _invulnerabilityDuration = setInvulnerabilityDuration;
    }

    // Update is called once per frame
    void Update()
    {
        _invulnerabilityDuration -= Time.deltaTime;

        if(_currentHealth == 0) {
            PlayerAnimation.Instance.playerAnimator.SetBool("Dead", true);
        }
    }

    private void takeDamage(int damage) {
        if(_invulnerabilityDuration < 0) {
            _currentHealth -= damage;
            healthBar.SetHealth(_currentHealth);
            damageSFX.Play();
            _invulnerabilityDuration = setInvulnerabilityDuration;
            EOnDamageTaken?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            takeDamage(1); 
            // Debug.Log("Player hit by a projectile, taking damage.");
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Boss")){
            takeDamage(1); 
        }
    }
}
