using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private GameObject winScreen;

    public static CombatSystem CombatSystem { get; private set; }
    public static MovementSystem MovementSystem { get; private set; }

    public static Player Player { get => instance.player; }
    private Player player;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        CombatSystem = GetComponent<CombatSystem>();
        MovementSystem = GetComponent<MovementSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static void SetPlayer(Player player)
    {
        instance.player = player;
    }

    public static void EndGame()
    {
        instance.winScreen.SetActive(true);
    }
}
