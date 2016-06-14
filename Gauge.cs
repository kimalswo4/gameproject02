using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public delegate void Action();

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class Gauge : MonoBehaviour {
    private Action _action;

    RectTransform _rectTransform;

    private Image _renderer;
    private Vector2 _originScale;
    private float _minValue;
    private float _maxValue;
    private float _currentValue;
    private float _initValue;

    public void Initialize(int width, int height, float minValue, float maxValue, float value, Vector2 pos)
    {
        _renderer = this.GetComponent<Image>();
        this.transform.SetParent(GameObject.Find("Canvas").transform);
        _rectTransform = this.GetComponent<RectTransform>();

        _originScale = new Vector2(width, height);
        _rectTransform.sizeDelta = _originScale;
        _rectTransform.localPosition = pos;

        _minValue = minValue;
        _maxValue = maxValue;
        _currentValue = value;
        _initValue = value;
    }
    public void Initialize(int width, int height, float minValue, float maxValue, float value, Vector2 pos, Color color)
    {
        Initialize(width, height,minValue, maxValue, value, pos);
        SetColor(color);
    }
    public void Reset()
    {
        ChangeValue(_initValue - _currentValue);
    }
    public void SetColor(Color color)
    {
        _renderer.color = color;
    }
    public void ChangeValue(float value)
    {
        _currentValue += value;
        if (_currentValue >= _maxValue)
        {
            _currentValue = _maxValue;
        }
        else if (_currentValue <= _minValue)
        {
            _currentValue = _minValue;
            _action();
        }

        float changingValue = (value * _originScale.x) / _maxValue;
        _rectTransform.localPosition += new Vector3(changingValue * 0.5f, 0.0f);
        _rectTransform.sizeDelta += new Vector2(changingValue, 0.0f);
    }
    public void SetAction(Action action)
    {
        _action = new Action(action);
    }
    public void SetActive(bool value)
    {
        this.gameObject.SetActive(value);
    }
    public void SetMaxValue(int value)
    {
        _maxValue = value;
        ChangeValue(0);
    }
}
