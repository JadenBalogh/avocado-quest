using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementSystem : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Player player;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color enemyColor;

    private Vector3Int currPos = Vector3Int.zero;

    private void Update()
    {
        if (GameManager.CombatSystem.IsCombatActive)
        {
            return;
        }

        tilemap.RemoveTileFlags(currPos, TileFlags.LockColor);
        tilemap.SetColor(currPos, Color.white);

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseCell = tilemap.WorldToCell(mouseWorld);
        Vector2 cellWorld = (Vector2)tilemap.CellToWorld(mouseCell) + Vector2.one * 0.5f;
        currPos = mouseCell;

        Vector2 playerPos = GameManager.Player.transform.position;
        if (Vector2.SqrMagnitude(playerPos - cellWorld) > 2.4f)
        {
            return;
        }

        EnemyGroup currEnemy = null;

        Collider2D col = Physics2D.OverlapBox(cellWorld, Vector2.one * 0.9f, 0);
        if (col != null)
        {
            if (col.TryGetComponent<Player>(out Player player))
            {
                return;
            }

            if (col.CompareTag("Blocking"))
            {
                return;
            }

            if (col.TryGetComponent<EnemyGroup>(out EnemyGroup enemy))
            {
                tilemap.SetColor(currPos, enemyColor);
                currEnemy = enemy;
            }

            if (col.CompareTag("Avocado"))
            {
                tilemap.SetColor(currPos, highlightColor);
            }
        }
        else
        {
            tilemap.SetColor(currPos, highlightColor);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (col != null && col.CompareTag("Avocado"))
            {
                GameManager.EndGame();
                return;
            }

            if (currEnemy != null)
            {
                GameManager.CombatSystem.StartCombat(player, currEnemy);
            }
            else
            {
                player.SetTarget(cellWorld);
            }
        }
    }
}
