using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class CustomRangeFloat : ISerializationCallbackReceiver
{
    private float min;
    private float max;
    [SerializeField] private float value;

    public float Value
    {
        get => value;
        set => this.value = Mathf.Clamp(value, min, max);
    }

    public CustomRangeFloat(float minIncluive, float maxIncluive, float initialValue)
    {
        this.min = minIncluive;
        this.max = maxIncluive;
        Value = initialValue;
    }

    public void OnBeforeSerialize()
    {
        value = Mathf.Clamp(value, min, max);
    }
    public void OnAfterDeserialize()
    {
        value = Mathf.Clamp(value, min, max);
    }

    public static implicit operator float(CustomRangeFloat crf)
    {
        return crf.Value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}