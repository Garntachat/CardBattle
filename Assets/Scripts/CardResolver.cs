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
        if (!enemyObj.activeInHierarchy) return;
        
        if (enemyHealth == null)
            enemyHealth = enemyObj.GetComponentInParent<EnemyHealth>();
        if (enemyHealth == null)
            enemyHealth = enemyObj.GetComponentInChildren<EnemyHealth>();

        if (enemyHealth == null)
        {
            Debug.LogWarning("Enemy has no EnemyHealth!");
            return;
        }

        switch (card.cardEffect)
        {
            case CardEffect.StraightStrike:
                enemyHealth.TakeDamage(card.damage, "DoPunched");
                Debug.Log($"攻 Straight Strike! Deal {card.damage} damage");
                break;

            case CardEffect.Throw:
                enemyHealth.TakeDamage(card.damage, "DoThrown");
                Vector3 pushDir = (enemyObj.transform.position - playerController.transform.position).normalized;
                enemyObj.transform.position += pushDir * 10f;
                FindObjectOfType<ThinkingRate>().OnEnemyExitRange();
                Debug.Log("摔 Throw! Enemy pushed out of range");
                break;

            case CardEffect.Guard:
                playerController.SetDamageReduction(card.damageReduction);
                playerController.SetHealAmount(card.Heal);
                Debug.Log($"守 Guard! Reduce damage by {card.damageReduction * 100}%heal{card.Heal}");
                break;
            case CardEffect.ConsecutiveStrike:
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                foreach (GameObject enemy in enemies)
                {
                    if (!enemy.activeInHierarchy) continue;

                    EnemyHealth eh = enemy.GetComponent<EnemyHealth>();

                    if (eh == null)
                        eh = enemy.GetComponentInParent<EnemyHealth>();
                    if (eh == null)
                        eh = enemy.GetComponentInChildren<EnemyHealth>();

                    if (eh != null)
                    {
                        eh.TakeDamage(card.damage, "DoPunched");
                    }
                }

                Debug.Log($"攻 Consecutive Strike! Hit all enemies for {card.damage} damage");
                break;
            default:
                Debug.Log($"Card {card.englishName} not implemented yet");
                break;
        }
    }
}
