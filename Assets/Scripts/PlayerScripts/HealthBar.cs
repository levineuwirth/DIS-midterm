using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [field: SerializeField] public Gradient gradient {get ; private set;}
    [field: SerializeField] public Image fill {get ; private set;}
    private Slider _slider;

    private void Awake() {
        _slider = gameObject.GetComponent<Slider>();
        fill.color = gradient.Evaluate(1f);
    }

    public void SetMaxHealth(int maxHealth) {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth; 
    }
    public void SetHealth(int health) {
        _slider.value = health;
        fill.color = gradient.Evaluate(_slider.normalizedValue);
    }
}
