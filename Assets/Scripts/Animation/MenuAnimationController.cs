using UnityEngine;

public class MenuAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        MenuManager.Instance.OnPanelShowRequest += Instance_OnPanelShowRequest;
    }
    private void OnDisable()
    {
        MenuManager.Instance.OnPanelShowRequest -= Instance_OnPanelShowRequest;
    }

    private void Instance_OnPanelShowRequest()
    {
        PlayNewGameAnimation();
    }

    public void PlayNewGameAnimation()
    {
        animator.SetTrigger("NewGame");
    }
}
