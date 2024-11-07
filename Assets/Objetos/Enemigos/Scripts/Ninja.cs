using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : EnemigoScript
{
    [SerializeField] private CustomRangeFloat transparentAlpha = new CustomRangeFloat(0, 1, 0);
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ninjaRevealSfx;
    public bool Invisible { get; private set; }

    private float baseSpd;

    protected override void AsignarTodo()
    {
        base.AsignarTodo();
        this.baseSpd = spd;
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log("Apareció un Ninja.");
        SetInvis(true, 0);
    }

    public void SetInvis(bool status, float time)
    {
        StartCoroutine(InvisCoroutine(status, time));
    }

    private IEnumerator InvisCoroutine(bool status, float time)
    {
        this.hpBar.SetActive(!status);
        switch (status)
        {
            case true:
                canBeEaten = false;
                canBeShot = false;
                break;
            case false:
                canBeEaten = true;
                canBeShot = true;
                break;
        }
        Invisible = status;
        if (time > 0)
        {
            SoundManager.Instance.PlaySound(audioSource, ninjaRevealSfx, 0.5f);
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;

                float newTransparency;
                if (status == true) newTransparency = Mathf.Lerp(1, transparentAlpha, elapsedTime / time);
                else newTransparency = Mathf.Lerp(transparentAlpha, 1, elapsedTime / time);

                ChangeSpriteRendererAlpha(newTransparency);

                yield return null;
            }
        }
        if (status == true) ChangeSpriteRendererAlpha(transparentAlpha);
        else ChangeSpriteRendererAlpha(1);
    }

    private void ChangeSpriteRendererAlpha(float alpha)
    {
        CustomRangeFloat a = new CustomRangeFloat(0, 1, alpha);
        this.sr.color = new Color(this.sr.color.r, this.sr.color.g, this.sr.color.b, a);
    }

    public override void Sufrir(float dmg)
    {
        if (Invisible) return;
        else base.Sufrir(dmg);
    }
}
