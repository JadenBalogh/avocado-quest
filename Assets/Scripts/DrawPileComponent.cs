using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawPileComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;

    private void Start()
    {
        GameManager.Player.OnDrawChanged.AddListener(UpdateDisplay);
    }

    private void UpdateDisplay(int count)
    {
        countText.text = count.ToString();
    }
}
