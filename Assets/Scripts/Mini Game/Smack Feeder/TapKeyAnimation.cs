using DG.Tweening;
using UnityEngine;

public class TapKeyAnimation : MonoBehaviour
{
    [SerializeField] private Transform InnerCircle;
    [SerializeField] private Transform OutterCircle;

    public void StartApproachAnimation(float duration)
    {
        Vector3 targetScale = InnerCircle.transform.localScale;

        OutterCircle.DOScale(targetScale, duration).SetEase(Ease.Linear);
    }
}
