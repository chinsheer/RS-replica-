using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BossHealthUI : MonoBehaviour
{
    public EnemyHealth Boss;

    private UIDocument _uiDocument;
    private ProgressBar _healthBar;

    public void Initialize(EnemyHealth boss)
    {
        if (_uiDocument == null) _uiDocument = GetComponent<UIDocument>();
        var root = _uiDocument.rootVisualElement;
        _healthBar = root.Q<ProgressBar>("BossHP");

        Boss = boss;
        Boss.OnHealthChanged += UpdateHealthBar;
        Boss.OnDeath += OnBossDied;
    }

    void UpdateHealthBar(float currentHP)
    {
        _healthBar.value = currentHP / Boss.MaxHP * 100;
    }

    private void OnBossDied()
    {
        Cleanup();
        Show(false); // hide the bar (or Destroy(gameObject) if you want)
    }

    private void Show(bool visible)
    {
        if (_uiDocument == null) _uiDocument = GetComponent<UIDocument>();
        _uiDocument.rootVisualElement.style.display =
            visible ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void Cleanup()
    {
        if (Boss != null)
        {
            Boss.OnHealthChanged -= UpdateHealthBar;
            Boss.OnDeath -= OnBossDied;
        }
        Boss = null;
    }
}
