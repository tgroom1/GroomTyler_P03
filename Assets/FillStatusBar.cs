using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStatusBar : MonoBehaviour
{
    public Health _playerhealth;
    public Image _fillImage;
    private Slider _slider;

    // Start is called before the first frame update
    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_slider.value <= _slider.minValue)
        {
            _fillImage.enabled = false;
        }
        
        if (_slider.value > _slider.minValue && !_fillImage.enabled)
        {
            _fillImage.enabled = true;
        }

        float fillValue = _playerhealth._currentHealth / _playerhealth._maxhealth;
        if (fillValue <= _slider.maxValue / 3)
        {
            _fillImage.color = Color.red;
        }
        else if (fillValue > _slider.maxValue / 3)
        {
            _fillImage.color = Color.green;
        }
        _slider.value = fillValue;
    }
}
