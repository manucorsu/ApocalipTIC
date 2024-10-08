using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionMuerteScript : MonoBehaviour
{
    public void AnimationEnd() => Destroy(this.gameObject);
}
