using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Slider slider;

    [field: SerializeField] public Gradient gradient {get ; private set;}
    [field: SerializeField] public Image fill {get ; private set;}

    private void Awake() {
        slider = gameObject.GetComponent<Slider>();
        fill.color = gradient.Evaluate(1f);
    }

    public void SetMaxHealth(int maxHealth) {
        slider.maxValue = maxHealth;
        slider.value = maxHealth; 
    }
    public void SetHealth(int health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
