using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}

	public void PlayAttack() {
		animator.SetTrigger("DoAttack");
	}

	public void PlayBlock() {
		animator.SetTrigger("DoBlock");
	}

	public void PlayDodge() {
		animator.SetTrigger("DoDodge");
	}
}
