using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card", order = 51)]
public class Card : ScriptableObject
{
    [SerializeField] private string cardName;
    public string CardName { get => cardName; }

    [SerializeField][TextArea] private string cardDesc;
    public string CardDesc { get => cardDesc; }

    [SerializeField] private bool targetsPlayer = false;
    public bool TargetsPlayer { get => targetsPlayer; }

    [SerializeField] private int damage = 0;
    public int Damage { get => damage; }

    [SerializeField] private int block = 0;
    public int Block { get => block; }

    [SerializeField] private int actionPointCost = 1;
    public int ActionPointCost { get => actionPointCost; }

    public void Play(Actor target)
    {
        if (targetsPlayer)
        {
            GameManager.Player.AddBlock(block);
        }
        else
        {
            target.TakeDamage(damage);
        }

        GameManager.Player.OnCardPlayed(this);
    }
}
