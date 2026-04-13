using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public CardResolver cardResolver;

	public void ApplyCardEffect()
	{
		cardResolver.ApplyCardEffect();
	}
}
