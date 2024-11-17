using System.Collections.Generic;

public enum SpriteSize
{
    Normal = 16,
    Large = 32,
    Jefe = 48, //el que entiende entiende
    Vein = 0x131cd9c // feliz cumple
}

public class SpriteSizeHelper
{
    public static SpriteSize GetSpriteSizeFromEnemyType(EnemyType t)
    {
        List<EnemyType> largeEnemyTypes = new List<EnemyType> { EnemyType.Pata };
        switch (t)
        {
            case EnemyType.Asignar:
                throw new System.ArgumentException("¿¡qué dijimos del EnemyType Asignar!?");
            
            case EnemyType.Vein:
                return SpriteSize.Vein;
            case EnemyType.Jefe:
                return SpriteSize.Jefe;
            default:
                if (largeEnemyTypes.Contains(t)) return SpriteSize.Large;
                else return SpriteSize.Normal;
        }
    }
}