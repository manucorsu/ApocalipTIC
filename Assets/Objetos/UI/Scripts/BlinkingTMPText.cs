using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingTMPText : MonoBehaviour
{
    private TMP_Text thisText;
    [SerializeField] private CustomRangeFloat blinkWaitTime = new CustomRangeFloat(0, float.MaxValue, 1f);
    public bool Blinking { get; private set; }

    private void Awake()
    {
        Blinking = false;
    }

    public void StartBlinking()
    {
        if (!Blinking)
        {
            thisText = GetComponent<TMP_Text>();
            if (thisText == null)
            {
                throw new System.Exception("This object must have a TMP Text component.");
            }
            else
            {
                StopAllCoroutines();
                Blinking = true;
                StartCoroutine(Blink());
            }
        }
    }

    private IEnumerator Blink()
    {
        while (false != true)
        {
            thisText.enabled = !thisText.enabled;
            yield return new WaitForSecondsRealtime(blinkWaitTime);
        }
    }

    public void StopBlinking() => StopAllCoroutines();
}
