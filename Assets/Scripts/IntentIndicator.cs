using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntentIndicator : MonoBehaviour
{
    [SerializeField] private Image shieldImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private TextMeshProUGUI textbox;

    public void UpdateDisplay(bool isAttack, int value)
    {
        shieldImage.enabled = !isAttack;
        swordImage.enabled = isAttack;
        textbox.text = value.ToString();
    }
}
