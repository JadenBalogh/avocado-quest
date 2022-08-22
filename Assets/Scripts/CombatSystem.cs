using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private CombatScreen combatScreen;
    [SerializeField] private CardTargetsMenu targetsMenu;

    private Player player = null;
    private List<Enemy> enemies = new List<Enemy>();
    private List<Actor> turnOrder = new List<Actor>();
    private EnemyGroup enemyGroup;
    private int turnIndex = 0;

    public UnityEvent OnTurnChanged { get; private set; }

    public bool IsCombatActive { get; private set; }

    private void Awake()
    {
        OnTurnChanged = new UnityEvent();
    }

    private void Update()
    {
        if (!IsCombatActive)
        {
            return;
        }
    }

    public void StartCombat(Player player, EnemyGroup enemyGroup)
    {
        enemies = combatScreen.InitCombat(player, enemyGroup);
        this.player = player;
        this.enemyGroup = enemyGroup;

        turnOrder.Clear();
        turnIndex = 0;

        turnOrder.Add(player);
        player.StartCombat();
        foreach (Enemy enemy in enemies)
        {
            turnOrder.Add(enemy);
            enemy.StartCombat();
        }

        turnOrder[turnIndex].StartTurn();
        IsCombatActive = true;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        turnOrder.Remove(enemy);

        if (enemies.Count == 0)
        {
            EndCombat();
        }
    }

    public void EndCombat()
    {
        IsCombatActive = false;
        Destroy(enemyGroup.gameObject);
        player.AddRandomStarterCard();
        combatScreen.SetVisible(false);
    }

    public void SelectCard(Card card)
    {
        targetsMenu.gameObject.SetActive(true);

        if (card.TargetsPlayer)
        {
            List<Actor> targets = new List<Actor>();
            targets.Add(player);
            targetsMenu.UpdateTargets(card, targets);
        }
        else
        {
            targetsMenu.UpdateTargets(card, enemies);
        }
    }

    public void EndTurn()
    {
        Actor currActor = turnOrder[turnIndex];
        currActor.EndTurn();

        IncrementTurnIndex();

        Actor nextActor = turnOrder[turnIndex];
        nextActor.StartTurn();
    }

    public Actor GetCurrentActor()
    {
        return turnOrder[turnIndex];
    }

    private void IncrementTurnIndex()
    {
        turnIndex = (turnIndex + 1) % turnOrder.Count;
        OnTurnChanged.Invoke();
    }
}
