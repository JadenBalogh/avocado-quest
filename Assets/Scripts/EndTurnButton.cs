using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        GameManager.CombatSystem.OnTurnChanged.AddListener(UpdateDisplay);
    }

    private void UpdateDisplay()
    {
        button.interactable = GameManager.CombatSystem.GetCurrentActor().CompareTag("Player");
    }
}
