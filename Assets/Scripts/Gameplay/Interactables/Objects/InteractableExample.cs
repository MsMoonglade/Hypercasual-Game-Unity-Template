using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DamageNumbersPro;

public class InteractableExample : Interactable
{
    [Header("Variables")]
    public int startHp;

    [Header("Local References")]
    public GameObject model;

    [Header("Private Variables")]
    private int currentHp;

    private void Update()
    {
        if (CheckPlayerPosition())
        {
            Complete(false);
        }
    }

    public override void TakeHit(int i_amount)
    {
        if (!isCompleted)
        {
            currentHp -= i_amount;

            if(currentHp < 0)
                currentHp = 0;

            if(currentHp == 0)
            {
                Complete(true);
            }

            UpdateUi();
        }
    }

    protected override void HandleComplete()
    {
        base.HandleComplete();

        dynamicText.gameObject.transform.DOScale(0 , 0.5f);

        MoveToWorldRoller(this.gameObject);
    }

    protected override void HandleDisable()
    {
        base.HandleDisable(); 

        //actual disable
    }

    protected override void UpdateUi()
    {
        base.UpdateUi();

        dynamicText.text = currentHp.ToString() + "/" + startHp.ToString();
    }   

    protected override void OnSetDifficulty(object sender)
    {
        base.OnSetDifficulty(sender);

        int newHp = 0;
        //Get new hp Value

        startHp = newHp;
        currentHp = startHp;  
    }
}