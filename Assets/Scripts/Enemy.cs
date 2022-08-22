using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] private EnemyAction[] actions;
    [SerializeField] private float turnDuration = 1f;
    [SerializeField] private IntentIndicator intentIndicator;

    private int actionIdx = 0;

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
    }

    public override void StartCombat()
    {
        UpdateIntent();
    }

    public override void StartTurn()
    {
        base.StartTurn();

        Debug.Log("Enemy Turn START");
        StartCoroutine(Turn());
    }

    public override void EndTurn()
    {
        Debug.Log("Enemy Turn END");
        UpdateIntent();
    }

    public override void Die()
    {
        GameManager.CombatSystem.RemoveEnemy(this);
        Destroy(gameObject);
    }

    private IEnumerator Turn()
    {
        EnemyAction action = actions[actionIdx];
        IncrementActionIndex();

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(turnDuration);

        GameManager.Player.TakeDamage(action.damage);
        AddBlock(action.block);

        GameManager.CombatSystem.EndTurn();
    }

    private void UpdateIntent()
    {
        EnemyAction nextAction = actions[actionIdx];
        bool isAttack = nextAction.damage > 0;
        int actionValue = isAttack ? nextAction.damage : nextAction.block;
        intentIndicator.UpdateDisplay(isAttack, actionValue);
    }

    private void IncrementActionIndex()
    {
        actionIdx = (actionIdx + 1) % actions.Length;
    }

    [System.Serializable]
    public class EnemyAction
    {
        public int damage;
        public int block;
    }
}
