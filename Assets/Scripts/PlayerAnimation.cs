using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{   
    private Animator animator;

    private const string IS_RUNNING = "IsRunning";
    private const string IS_FALLING = "IsFalling";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RunningAnimation();
        FallingAnimation();
    }
    private void RunningAnimation() //call the running animation
    {
        animator.SetBool(IS_RUNNING, Exo_Gray.instance.IsRunning());
    }
    
    private void FallingAnimation () //call the falling animation
    {
       animator.SetBool(IS_FALLING,Exo_Gray.instance.IsFalling());
    }
    
}
