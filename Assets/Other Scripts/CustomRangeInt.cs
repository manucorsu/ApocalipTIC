using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CustomRangeInt
/** 
 * Su constructor recibe int minVal e int maxVal, ambos inclusivos.
 * Empieza valiendo minVal.
 * Si se le hace ++, se incrementa normalmente hasta que es mayor que maxVal, ahí da la vuelta y pasa a valer minVal
 * Si se le hace --  se resta normalmente hasta que queda menor que minVal, ahí pasa a valer maxVal.
*/
/*Ej:
CustomRangeInt cri = new CustomRangeInt(0,2) // valor actual: 0
cri++; // valor actual = 1
cri++; // valor actual = 2
cri++; // OVERFLOW: valor actual = 0
cri--; // UNDERFLOW: valor actual = 2*/
{
    [SerializeField] private int value;
    [SerializeField] private int min;
    [SerializeField] private int max;

    public CustomRangeInt(int minVal, int maxVal)
    {
        if (minVal > maxVal)
        {
            throw new System.ArgumentException("constructor de CustomRangeInt: minVal no puede ser más grande que maxVal.");
        }
        min = minVal;
        max = maxVal;
        value = minVal;
    }

    public int Value
    {
        get => value;
        private set => this.value = value;
    }
    
    public static CustomRangeInt operator ++(CustomRangeInt cri)
    {
        cri.value++;
        if (cri.value > cri.max) cri.value = cri.min;
        return cri;
    }

    public static CustomRangeInt operator --(CustomRangeInt cri)
    {
        cri.value--;
        if (cri.value < cri.min) cri.value = cri.max;
        return cri;
    }

    public static implicit operator int(CustomRangeInt cri)
    {
        return cri.value;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}