using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text txtPuntaje;

    private void Update()
    {
        txtPuntaje.text = $"Puntaje: {GameManager.Instance.puntaje}";
    }
}
