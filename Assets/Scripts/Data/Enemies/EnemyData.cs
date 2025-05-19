using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string mobName = "Orc Warrior";
    public float maxHealth = 100f;
    public float baseDamage = 10f;
    public float armor = 2f;
    public float xpcarriedMin = 50f;
    public float xpcarriedMax = 100f;

    public float contactAttackCooldown = 1.0f;
    public float damageIncreaseInterval = 1f;
    public float damageIncreaseAmount = 5f;

    public Sprite icon;
}
