using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [field: SerializeField] public float Max { get; private set; }
    [field: SerializeField] public float Value { get; private set; }

    public void Change(float p)
    {
        Value = Mathf.Clamp(Value+p, 0, Max);
    }

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
