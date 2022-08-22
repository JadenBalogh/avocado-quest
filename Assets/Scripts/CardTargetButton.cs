using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardTargetButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textbox;

    private Card card;
    private Actor target;

    public void Select()
    {
        card.Play(target);
    }

    public void UpdateTarget(Card card, Actor target)
    {
        this.card = card;
        this.target = target;

        textbox.text = target.DisplayName;
    }
}
