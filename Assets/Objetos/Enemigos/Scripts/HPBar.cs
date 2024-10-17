using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public float max;
    public float min = 0;
    public float value;
    private bool active = true;

    [SerializeField] private float animSpd = 1;
    [SerializeField] private GameObject barBg;
    [SerializeField] private RectTransform topBar;
    [SerializeField] private RectTransform btmBar;

    private float fullWidth;
    private float TargetWidth => value * fullWidth / max;

    private Coroutine updateWidthCoroutine;

    private void Start()
    {
        value = max;
        fullWidth = topBar.rect.width;
    }

    public void Change(float a)
    {
        if (active)
        {
            value = Mathf.Clamp(value + a, min, max);
            if (updateWidthCoroutine != null)
            {
                StopCoroutine(updateWidthCoroutine);
            }
            updateWidthCoroutine = StartCoroutine(UpdateWidth(a));
        }
    }

    private IEnumerator UpdateWidth(float a)
    {
        RectTransform instantBar = a >= 0 ? btmBar : topBar;
        RectTransform smoothBar = a >= 0 ? topBar : btmBar;

        instantBar.sizeDelta = new Vector2(TargetWidth, instantBar.rect.height);
        while (Mathf.Abs(instantBar.rect.width - smoothBar.rect.width) > 1)
        {
            smoothBar.sizeDelta = new Vector2(Mathf.Lerp(smoothBar.rect.width, TargetWidth, Time.deltaTime * animSpd), smoothBar.rect.height);
            yield return null;
        }
        smoothBar.sizeDelta = new Vector2(TargetWidth, smoothBar.rect.height);
    }
    public void SetActive(bool state)
    {
        barBg.SetActive(state);
        active = state;
    }
}
