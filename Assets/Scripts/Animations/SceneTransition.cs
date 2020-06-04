using UnityEngine;

public class SceneTransition : MonoBehaviour
{

    public Animator animator;
    
    // Update is called once per frame
    void Update()
    {
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    } 
    
    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    } 
    
}
