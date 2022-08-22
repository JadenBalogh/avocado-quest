using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatScreen : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private Transform[] enemyPositions;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ClearEnemyObjects()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    }

    public List<Enemy> InitCombat(Player player, EnemyGroup enemyGroup)
    {
        ClearEnemyObjects();

        List<Enemy> enemies = new List<Enemy>();
        playerImage.sprite = player.DisplayImage;

        int enemyIndex = 0;
        foreach (Enemy enemy in enemyGroup.Enemies)
        {
            enemies.Add(Instantiate(enemy, enemyPositions[enemyIndex].position, Quaternion.identity, transform));
            enemyIndex++;
        }

        SetVisible(true);
        return enemies;
    }

    public void SetVisible(bool visible)
    {
        canvasGroup.alpha = visible ? 1 : 0;
        canvasGroup.blocksRaycasts = visible;
    }
}
