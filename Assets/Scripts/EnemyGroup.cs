using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    public Enemy[] Enemies { get => enemies; }
}
