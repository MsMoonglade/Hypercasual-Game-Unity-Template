using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameObstacle : MonoBehaviour
{
    [Header("Variables")]
    public float disableAnimationSpeed;

    [Header("Local References")]
    public TMP_Text hpText;
    public GameObject localRewardObject;

    [Header("Private Variables")]
    private bool enable;

    [Header("Private References")]
    private Collider col;

    [HideInInspector]
    public int hp;

    /// <summary>
    /// Set Starting Hp when Enables
    /// </summary>
    /// <param name="i_value"><new Hp Value/param>
    public void SetHp(int i_value)
    {
        hp = i_value;
        UpdateUi();

        col = transform.GetComponent<Collider>();
        enable = true;
    }

    public void TakeHit(int i_amount = 1)
    {
        hp -= i_amount;

        TweenScale(1.1f, 0.25f);

        CheckHp();

        UpdateUi();
    }

    private void CheckHp()
    {
        if (hp < 0)
            hp = 0;

        if (hp == 0 && enable)
        {
            Disable(5, 0.5f);
        }
    }

    /// <summary>
    /// Actual Disable Animation
    /// </summary>
    /// <param name="i_yPos"><Y lower position when anim/param>
    /// <param name="i_tweenTime"><anim time/param>
    private void Disable(float i_yPos, float i_tweenTime)
    {
        enable = false;

        col.enabled = false;

        transform.DOLocalMoveY(-i_yPos, i_tweenTime)
            .SetEase(Ease.InBack);

        Sequence mySequence = DOTween.Sequence();
        mySequence.PrependInterval(i_tweenTime * 0.25f);

        mySequence.AppendCallback(() => localRewardObject.transform.SetParent(transform.parent.transform.parent, true));

        mySequence.Append(localRewardObject.transform.DOMoveY(0.5f, i_tweenTime)
            .SetEase(Ease.InBack));
    }

    /// <summary>
    /// Take Hit Feedback
    /// </summary>
    /// <param name="i_newSize"></param>
    /// <param name="i_tweenTime"></param>
    private void TweenScale(float i_newSize, float i_tweenTime)
    {
        transform.DOScale(1, 0);
        transform.DOScale(i_newSize, i_tweenTime)
            .SetLoops(2, LoopType.Yoyo);
    }

    private void UpdateUi()
    {
        hpText.text = hp.ToString();
    }
}
