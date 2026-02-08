using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BossHealthUI : MonoBehaviour
{
    public BossHealth Boss;

    void Awake()
    {
        Boss.OnHealthChanged += UpdateHealthBar;
    }

    void UpdateHealthBar(float currentHP)
    {
        UIDocument uiDocument = gameObject.GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;
        ProgressBar healthBar = root.Q<ProgressBar>("BossHP");
        healthBar.value = currentHP / Boss.MaxHP * 100;
    }
}
