using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDeadAnimation()
    {
        Debug.Log("Player is dead");
    }

}
