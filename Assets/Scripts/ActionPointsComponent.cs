using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionPointsComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;

    private void Start()
    {
        GameManager.Player.OnActionPointsChanged.AddListener(UpdateDisplay);
    }

    private void UpdateDisplay()
    {
        countText.text = GameManager.Player.ActionPoints.ToString();
    }
}
