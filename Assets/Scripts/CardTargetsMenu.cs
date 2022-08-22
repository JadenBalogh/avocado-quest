using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTargetsMenu : MonoBehaviour
{
    [SerializeField] private CardTargetButton[] targetButtons;

    public void UpdateTargets<T>(Card card, List<T> targets) where T : Actor
    {
        int targetCount = targets.Count;
        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (i >= targetCount)
            {
                targetButtons[i].gameObject.SetActive(false);
                continue;
            }

            targetButtons[i].gameObject.SetActive(true);
            targetButtons[i].UpdateTarget(card, targets[i]);
        }
    }
}
