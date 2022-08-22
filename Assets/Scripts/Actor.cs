using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [SerializeField] private string displayName;
    public string DisplayName { get => displayName; }

    [SerializeField] protected int maxHealth = 1;
    [SerializeField] protected Healthbar healthbar;
    [SerializeField] protected ShieldIndicator shieldIndicator;

    protected int health;
    protected int block;

    protected virtual void Awake()
    {
        health = maxHealth;
        healthbar.UpdateDisplay(health, maxHealth);
    }

    public abstract void StartCombat();

    public virtual void StartTurn()
    {
        block = 0;
        shieldIndicator.UpdateDisplay(this.block);
    }

    public abstract void EndTurn();

    public void AddBlock(int block)
    {
        this.block += block;
        shieldIndicator.UpdateDisplay(this.block);
    }

    public void TakeDamage(int damage)
    {
        if (block > 0)
        {
            int damageReduction = block;
            block = Mathf.Max(block - damage, 0);
            damage -= damageReduction;
            shieldIndicator.UpdateDisplay(block);
        }

        if (damage <= 0)
        {
            return;
        }

        health = Mathf.Max(health - damage, 0);
        healthbar.UpdateDisplay(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public abstract void Die();
}
