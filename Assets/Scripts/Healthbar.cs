using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private RectTransform barTransform;
    [SerializeField] private TextMeshProUGUI textbox;

    public void UpdateDisplay(int health, int maxHealth)
    {
        barTransform.anchorMax = new Vector2((float)health / maxHealth, 1f);
        textbox.text = health.ToString();
    }
}
