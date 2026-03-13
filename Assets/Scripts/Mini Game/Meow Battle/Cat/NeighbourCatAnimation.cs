using UnityEngine;

public class NeighbourCatAnimation : CatAniamtion
{
    public override void PlayTriggerAnimation(string nameAnimation)
    {
        _animator.SetTrigger(nameAnimation);
    }
}
