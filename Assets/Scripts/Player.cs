using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Actor
{
    [SerializeField] private Sprite displayImage;
    public Sprite DisplayImage { get => displayImage; }

    [SerializeField] private float moveTime = 0.2f;
    [SerializeField] private int cardsPerTurn = 4;
    [SerializeField] private CardMenu cardMenu;
    [SerializeField] private CardScreen cardScreen;
    [SerializeField] private Card[] starterCards;
    [SerializeField] private int actionPointsPerTurn = 3;
    [SerializeField] private Animator animator;

    private List<Card> draw = new List<Card>();
    private List<Card> hand = new List<Card>();
    private List<Card> discard = new List<Card>();
    private List<Card> deck = new List<Card>();

    public UnityEvent<int> OnDrawChanged { get; private set; }
    public UnityEvent<int> OnDiscardChanged { get; private set; }

    public int ActionPoints { get; private set; }
    public UnityEvent OnActionPointsChanged { get; private set; }

    private Vector2 targetPos;
    private Vector2 currVel;

    protected override void Awake()
    {
        base.Awake();

        GameManager.SetPlayer(this);

        OnDrawChanged = new UnityEvent<int>();
        OnDiscardChanged = new UnityEvent<int>();
        OnActionPointsChanged = new UnityEvent();
    }

    private void Start()
    {
        targetPos = transform.position;

        for (int i = 0; i < starterCards.Length; i++)
        {
            AddCard(starterCards[i]);
        }
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPos, ref currVel, moveTime);
    }

    public void SetTarget(Vector2 newTarget)
    {
        targetPos = newTarget;
    }

    public void AddRandomStarterCard()
    {
        deck.Add(starterCards[Random.Range(0, starterCards.Length)]);
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
    }

    public override void StartCombat()
    {
        health = maxHealth;
        healthbar.UpdateDisplay(health, maxHealth);

        draw.Clear();
        hand.Clear();
        discard.Clear();

        foreach (Card card in deck)
        {
            draw.Add(card);
        }

        OnDrawChanged.Invoke(draw.Count);
        OnDiscardChanged.Invoke(discard.Count);

        ShuffleCards(draw);
    }

    public override void StartTurn()
    {
        base.StartTurn();

        int cardsToDraw = Mathf.Min(cardsPerTurn, deck.Count);
        for (int i = 0; i < cardsToDraw; i++)
        {
            if (draw.Count == 0)
            {
                int discardCount = discard.Count;
                for (int j = 0; j < discardCount; j++)
                {
                    draw.Add(discard[0]);
                    discard.RemoveAt(0);
                }

                ShuffleCards(draw);
            }

            hand.Add(draw[0]);
            draw.RemoveAt(0);
        }

        Debug.Log("Hand: " + hand.Count);

        OnDrawChanged.Invoke(draw.Count);
        OnDiscardChanged.Invoke(discard.Count);

        cardMenu.UpdateHand(hand);
        SetActionPoints(actionPointsPerTurn);
    }

    public override void EndTurn()
    {
        DiscardHand();
    }

    public void OnCardPlayed(Card card)
    {
        animator.SetTrigger("Attack");

        discard.Add(card);
        hand.Remove(card);
        cardMenu.UpdateHand(hand);
        OnDiscardChanged.Invoke(discard.Count);

        SetActionPoints(ActionPoints - card.ActionPointCost);
    }

    public override void Die()
    {
        GameManager.CombatSystem.EndCombat();
    }

    public void ShuffleCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randIdx = Random.Range(0, cards.Count);
            Card tmp = cards[randIdx];
            cards[randIdx] = cards[i];
            cards[i] = tmp;
        }
    }

    public void ShowDeck()
    {
        cardScreen.DisplayCards(deck);
    }

    public void ShowDraw()
    {
        cardScreen.DisplayCards(draw);
    }

    public void ShowDiscard()
    {
        cardScreen.DisplayCards(discard);
    }

    public void DiscardHand()
    {
        int cardsToDiscard = hand.Count;
        for (int i = 0; i < cardsToDiscard; i++)
        {
            discard.Add(hand[0]);
            hand.RemoveAt(0);
        }

        cardMenu.UpdateHand(hand);
        OnDiscardChanged.Invoke(discard.Count);
    }

    private void SetActionPoints(int actionPoints)
    {
        ActionPoints = actionPoints;
        OnActionPointsChanged.Invoke();
    }
}
