using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<Image> healthIcons;
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void UpdateHealthUI(int health)
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            healthIcons[i].enabled = i < health;
        }
    }
}
