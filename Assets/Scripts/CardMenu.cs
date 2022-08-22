using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMenu : MonoBehaviour
{
    [SerializeField] private CardComponent[] cardComponents;

    private void Start()
    {
        GameManager.Player.OnActionPointsChanged.AddListener(RefreshCards);
    }

    public void UpdateHand(List<Card> hand)
    {
        int handCount = hand.Count;
        for (int i = 0; i < cardComponents.Length; i++)
        {
            if (i >= handCount)
            {
                cardComponents[i].ClearCard();
                cardComponents[i].gameObject.SetActive(false);
                continue;
            }

            cardComponents[i].gameObject.SetActive(true);
            cardComponents[i].UpdateCard(hand[i]);
        }
    }

    public void RefreshCards()
    {
        foreach (CardComponent cardComponent in cardComponents)
        {
            cardComponent.Refresh();
        }
    }
}
