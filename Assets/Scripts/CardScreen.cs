using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScreen : MonoBehaviour
{
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private CardComponent cardPrefab;
    [SerializeField] private RectTransform contentBox;

    private List<CardComponent> cardComponents = new List<CardComponent>();

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void DisplayCards(List<Card> cards)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        foreach (CardComponent cardComponent in cardComponents)
        {
            Destroy(cardComponent.gameObject);
        }

        cardComponents.Clear();

        float offset = 20f;
        foreach (Card card in cards)
        {
            CardComponent cardComp = Instantiate(cardPrefab, contentParent);
            cardComp.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, 0);
            cardComp.UpdateCard(card);
            cardComponents.Add(cardComp);
            offset += 180f;
        }

        contentBox.sizeDelta = new Vector2(offset / 2f, 300f);
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
