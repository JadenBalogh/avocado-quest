using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiscardPileComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;

    private void Start()
    {
        GameManager.Player.OnDiscardChanged.AddListener(UpdateDisplay);
    }

    private void UpdateDisplay(int count)
    {
        countText.text = count.ToString();
    }
}
