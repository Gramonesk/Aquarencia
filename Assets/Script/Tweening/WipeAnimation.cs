using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WipeAnimation : SceneTransition
{
    public Image image;
    public Sprite onIN;
    public Sprite onOUT;

    public float speed;
    public float InStart, InEnd;
    public float OutStart, OutEnd;
    public override IEnumerator AnimateTransitionIn()
    {
        image.sprite = onIN;
        image.rectTransform.anchoredPosition = new Vector2(InStart, 0f);
        var tweener = image.rectTransform.DOAnchorPosX(InEnd, speed);
        tweener.SetUpdate(true);
        yield return tweener.WaitForCompletion();
    }
    public override IEnumerator AnimateTransitionOut()
    {
        image.sprite = onOUT;
        image.rectTransform.anchoredPosition = new Vector2(OutStart, 0f);
        var tweener = image.rectTransform.DOAnchorPosX(OutEnd, speed);
        tweener.SetUpdate(true);
        yield return tweener.WaitForCompletion();
    }
}
