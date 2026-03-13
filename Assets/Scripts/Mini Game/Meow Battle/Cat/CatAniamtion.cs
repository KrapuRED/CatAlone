using UnityEngine;

public class CatAniamtion : MonoBehaviour
{
    protected Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void PlayTriggerAnimation(string nameAnimation)
    {
        
    }
}
