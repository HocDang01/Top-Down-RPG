using UnityEngine;

public class WinDialog : Dialog
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override void Show(bool isShow)
    {
        gameObject.SetActive(isShow);

        if (isShow && animator != null)
        {
            animator.Rebind();  
            animator.Update(0); 
        }
    }

}
