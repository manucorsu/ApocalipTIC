using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewTutorialData", menuName = "Tutorial Data", order = 51)]
public class TutorialData : ScriptableObject
{
    public string[] cuadroOcultar;
    public string[] cuadroMostrar;
    public Vector2 posicionCuadro;
    public string texto;
}
