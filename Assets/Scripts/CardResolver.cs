using UnityEngine;

public class CardResolver : MonoBehaviour
{
    [Header("References")]
    public PlayerController playerController;
    public EnemyDetection enemyDetection;
    public PlayerAnimationController playerAnimationController;

    private CardData pendingCard;

    public void ResolveCard(CardData card)
    {   
        // animator.SetTrigger("DoIdle");
        if (enemyDetection.enemy == null)
        {
            Debug.LogWarning("No enemy detected to apply the card to!");
            return; 
        }

        pendingCard = card;

        switch (card.cardEffect)
        {
            case CardEffect.StraightStrike:
                playerAnimationController.PlayAttack();
                break;

            case CardEffect.Throw:
                playerAnimationController.PlayThrow();
                break;

            case CardEffect.Guard:
                playerAnimationController.PlayBlock();
                playerController.SetDamageReduction(card.damageReduction);
                playerController.SetHealAmount(card.Heal);
                Debug.Log($"守 Guard! Reduce damage by {card.damageReduction * 100}%heal{card.Heal}");
                pendingCard = null;
                break;
            case CardEffect.ConsecutiveStrike:
                playerAnimationController.PlayMultiPunch();
                break;
            case CardEffect.Dodge:
                playerAnimationController.PlayDodge();
                playerController.DodgeNextAttack(1.0f);
                pendingCard = null;
                break;
            case CardEffect.GodJud:
                playerAnimationController.PlayGodJud();
                break;

            default:
                Debug.Log($"Card {card.englishName} not implemented yet");
                break;
        }
    }

    public void ApplyCardEffect()
    {
        if (pendingCard == null) return;

        GameObject enemyObj = enemyDetection.enemy?.gameObject;
        if (enemyObj == null || !enemyObj.activeInHierarchy)
        {
            pendingCard = null;
            return;
        }

        EnemyHealth enemyHealth = enemyObj.GetComponent<EnemyHealth>();
        if (enemyHealth == null) enemyHealth = enemyObj.GetComponentInParent<EnemyHealth>();
        if (enemyHealth == null) enemyHealth = enemyObj.GetComponentInChildren<EnemyHealth>();

        if (enemyHealth == null)
        {
            Debug.LogWarning("Enemy has no EnemyHealth!");
            pendingCard = null;
            return;
        }

        switch (pendingCard.cardEffect)
        {
            case CardEffect.StraightStrike:
                enemyHealth.TakeDamage(pendingCard.damage, "DoPunched");
                Debug.Log($"攻 Straight Strike! Deal {pendingCard.damage} damage");
                break;

            case CardEffect.Throw:
                enemyHealth.TakeDamage(pendingCard.damage, "DoThrown");
                Debug.Log("摔 Throw! Enemy pushed out of range");
                break;

            case CardEffect.ConsecutiveStrike:
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    if (!enemy.activeInHierarchy) continue;
                    EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
                    if (eh == null) eh = enemy.GetComponentInParent<EnemyHealth>();
                    if (eh == null) eh = enemy.GetComponentInChildren<EnemyHealth>();
                    if (eh != null) eh.TakeDamage(pendingCard.damage, "DoPunched");
                }
                Debug.Log($"攻 Consecutive Strike! Hit all enemies for {pendingCard.damage} damage");
                break;
            
            case CardEffect.GodJud:
                Vector3 pushDir = (enemyObj.transform.position - playerController.transform.position).normalized;
                pushDir.y = 0;

                enemyObj.transform.position += pushDir * 5f;
                enemyHealth.TakeDamage(pendingCard.damage, "DoPunched");


                Debug.Log($"ผลัก GodJud! Enemy pushed away & took {pendingCard.damage} damage");
                break;

        }

        playerAnimationController.PlayIdle();
        pendingCard = null;
    }
}
