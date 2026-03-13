using UnityEngine;

public class PlayerCatAnimation : CatAniamtion
{
    public override void PlayTriggerAnimation(string nameAnimation)
    {
        _animator.SetTrigger(nameAnimation);
    }
}
