using UnityEngine;
public enum CardType
{
    Attack,
    Control,
    Defense
}

public enum CardEffect
{
    StraightStrike = 0,
    ConsecutiveStrike = 1,
    Throw = 2,
    Guard = 3,
    MultiPunch = 4,
    Dodge = 5,
    GodJud = 6
}
[CreateAssetMenu(fileName = "NewCard", menuName = "CardGame/Card_Data")]
public class CardData : ScriptableObject
{
    [Header("Identity")]
    public string chineeseName;
    public string pinyin;
    public string thaiName;
    public string englishName;
    public CardType cardType;
    public CardEffect cardEffect;

    [Header("Damage")]
    public float damage = 0f;

    [Header("Defense Effects")]
    public float damageReduction = 0f;  // 守
    public float Heal = 0f;

    [Header("Control Effects")]
    public bool pushesEnemyOutOfRange;  // 摔

    [Header("Visual")]
    public Sprite cardArtwork;
    public Color cardColor;
    public string animationTrigger;
    public string description;
}
