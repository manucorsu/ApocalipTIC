using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [field: SerializeField] public float Max { get; private set; }
    [field: SerializeField] public float Value { get; private set; }

    [SerializeField] private RectTransform _topBar;
    [SerializeField] private RectTransform _btmBar;

    private float _fullWidth;
    private float TargetWidth => Value * _fullWidth / Max;
    
    private void Start()
    {
        _fullWidth = _topBar.rect.width;
    }

    public void Change(float p)
    {
        Value = Mathf.Clamp(Value+p, 0, Max);
    }

    //private IEnumerator UpdateBar(float a)
    //{
    //    RectTransform instantBar; // la roja
    //    if (a >= 0) instantBar = _btmBar;
    //    else instantBar = _topBar;

    //    RectTransform smoothBar;
    //}
    private void Update()
    {
        float a = 25;
        if (Input.GetKeyDown(KeyCode.P))
        {
            Change(a);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Change(-a);
        }
    }
}
