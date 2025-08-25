using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private LevelChanger gameOverSceneLevelChngr;
    private static EnemyType losingEnemyT;
    private static Sprite losingEnemySpr;
    private static Color losingEnemySprRendererColor;

    [SerializeField] private GameObject robotPerdisteNormal;
    [SerializeField] private GameObject robotPerdisteLarge;
    [SerializeField] private GameObject robotPerdisteVein;

    [SerializeField] private GameObject pcPerdiste;
    public static bool Lost { get; private set; } = false;

    public static void LoseGame(GameObject losingEnemyGO, LevelChanger gameSceneLevelChanger)
    {
        if (Lost == false)
        {
            losingEnemyT = EnemyType.Asignar;
            Time.timeScale = 1f;
            scrBotones.dv = 0;
            EnemigoScript losingEnemy = losingEnemyGO.GetComponent<EnemigoScript>();
            if (losingEnemy.EnemyType == EnemyType.Asignar) throw new System.ArgumentException("el enemigo no debería tener el EnemyType 'Asignar'.");
            else
            {
                losingEnemyT = losingEnemy.EnemyType;
                losingEnemySpr = losingEnemyGO.GetComponent<SpriteRenderer>().sprite;
                losingEnemySprRendererColor = losingEnemyGO.GetComponent<SpriteRenderer>().color;
                gameSceneLevelChanger.FadeTo("GameOver");
            }
            Lost = true;
        }
    }

    private void Start()
    {
        GameObject losingEnemyPuppet;
        switch (SpriteSizeHelper.GetSpriteSizeFromEnemyType(losingEnemyT))
        {
            case SpriteSize.Vein:
                losingEnemyPuppet = robotPerdisteVein;
                break;
            case SpriteSize.Large:
                losingEnemyPuppet = robotPerdisteLarge;
                break;
            default:
                losingEnemyPuppet = robotPerdisteNormal;
                break;

        }

        losingEnemyPuppet.GetComponent<RobotPerdiste>()
        .Go(losingEnemyT, losingEnemySpr, losingEnemySprRendererColor);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            gameOverSceneLevelChngr.FadeTo("Inicio", 1);
            AudioSource aus = SoundManager.Instance.GetComponent<AudioSource>();
            aus.clip = SoundManager.Instance.temaPrincipal;
            aus.loop = true;
            aus.Play();
            Lost = false;
        }
    }
}