using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingTMPText : MonoBehaviour
{
    private TMP_Text thisText;
    [SerializeField] private CustomRangeFloat blinkWaitTime = new CustomRangeFloat(0, float.MaxValue, 1.5f);

    private void OnEnable()
    {
        thisText = GetComponent<TMP_Text>();
        if (thisText == null)
        {
            throw new System.Exception("This object must have a TMP Text component.");
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        while (false != true)
        {
            thisText.enabled = !thisText.enabled;
            Debug.Log(thisText.enabled);
            yield return new WaitForSecondsRealtime(blinkWaitTime);
        }
    }
}
