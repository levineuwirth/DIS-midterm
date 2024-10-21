using UnityEngine;
using UnityEngine.UI;

public class BackgroundScaler : MonoBehaviour
{
    private Image _backgroundImage;
    private RectTransform _rectTransform;
    private float _ratio;

    void Start()
    {
        _backgroundImage = GetComponent<Image>();
        _rectTransform = _backgroundImage.rectTransform;
        _ratio = _backgroundImage.sprite.bounds.size.x / _backgroundImage.sprite.bounds.size.y;
    }

    void Update()
    {
        if (!_rectTransform)
            return;
            
        if(Screen.height * _ratio >= Screen.width)
        {
            _rectTransform.sizeDelta = new Vector2(Screen.height * _ratio, Screen.height);
        }
        else
        {
            _rectTransform.sizeDelta = new Vector2(Screen.width, Screen.width / _ratio);
        }
    }
}
