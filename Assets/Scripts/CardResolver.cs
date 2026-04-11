using UnityEngine;

public class CardResolver : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public EnemyDetection enemyDetection;

    public void ResolveCard(CardData card)
    {   
        if (enemyDetection.enemy == null)
        {
            Debug.LogWarning("No enemy detected to apply the card to!");
            return; 
        }
        GameObject enemyObj = enemyDetection.enemy.gameObject;
        EnemyHealth enemyHealth = enemyObj.GetComponent<EnemyHealth>();

        if (enemyHealth == null)
        {
            Debug.LogWarning("Enemy has no EnemyHealth!");
            return;
        }

        switch (card.cardEffect)
        {
            case CardEffect.StraightStrike:
                enemyHealth.TakeDamage(card.damage);
                Debug.Log($"攻 Straight Strike! Deal {card.damage} damage");
                break;

            case CardEffect.Throw:
                enemyHealth.TakeDamage(card.damage);
                // push enemy away
                Vector3 pushDir = (enemyObj.transform.position - playerController.transform.position).normalized;
                enemyObj.transform.position += pushDir * 10f;
                FindObjectOfType<ThinkingRate>().OnEnemyExitRange();
                Debug.Log("摔 Throw! Enemy pushed out of range");
                break;

            case CardEffect.Guard:
                playerController.SetDamageReduction(card.damageReduction);
                Debug.Log($"守 Guard! Reduce damage by {card.damageReduction * 100}%");
                break;

            default:
                Debug.Log($"Card {card.englishName} not implemented yet");
                break;
        }
    }
}
