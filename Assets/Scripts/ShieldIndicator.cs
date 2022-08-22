using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textbox;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void UpdateDisplay(int block)
    {
        bool visible = block > 0;
        canvasGroup.alpha = visible ? 1 : 0;
        textbox.text = block.ToString();
    }
}
