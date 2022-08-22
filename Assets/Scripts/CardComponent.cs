using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardComponent : MonoBehaviour
{
    [SerializeField] private Button useButton;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI actionPointText;

    private Card card;

    public void ClearCard()
    {
        card = null;
    }

    public void UpdateCard(Card card)
    {
        this.card = card;
        titleText.text = card.CardName;
        descText.text = card.CardDesc;
        actionPointText.text = card.ActionPointCost.ToString();
    }

    public void Refresh()
    {
        if (card == null || useButton == null)
        {
            return;
        }

        useButton.interactable = card.ActionPointCost <= GameManager.Player.ActionPoints;
    }

    public void Select()
    {
        GameManager.CombatSystem.SelectCard(card);
    }
}
