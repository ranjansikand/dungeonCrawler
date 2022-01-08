using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private float _health, _maxHealth;
    private float _lerpTimer;
    public float _chipSpeed = 3f;
    public Image _frontHealthBar, _backHealthBar;

    bool _updateComplete;

    public void InitializeHealth(int givenHealth)
    {
        // Set all healthbars with given value
        _health = _maxHealth = givenHealth;
    }

    public void UpdateHealthUI()
    {
        float fillF = _frontHealthBar.fillAmount;
        float fillB = _backHealthBar.fillAmount;
        float hFraction = _health / _maxHealth;
        // Player is hurt
        if (fillB > hFraction) {
            _frontHealthBar.fillAmount = hFraction;
            _backHealthBar.color = Color.yellow;
            _lerpTimer += Time.deltaTime;
            float percentComplete = _lerpTimer / _chipSpeed;
            percentComplete = percentComplete * percentComplete;
            _backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        // Player is healed
        else if (fillF < hFraction) {
            _backHealthBar.color = Color.green;
            _backHealthBar.fillAmount = hFraction;
            _lerpTimer += Time.deltaTime;
            float percentComplete = _lerpTimer / _chipSpeed;
            percentComplete = percentComplete * percentComplete;
            _frontHealthBar.fillAmount = Mathf.Lerp(fillF, _backHealthBar.fillAmount, percentComplete);
        } 
        // No action to complete; end coroutine
        else {
            _updateComplete = true;
            StopCoroutine(IUpdateHealth());
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _lerpTimer = 0f;
        _updateComplete = false;
        StartCoroutine(IUpdateHealth());
    }

    public void RestoreHealth(float healAmount)
    {
        _health += healAmount;
        _lerpTimer = 0f;
        _updateComplete = false;
        StartCoroutine(IUpdateHealth());
    }

    IEnumerator IUpdateHealth()
    {
        while (!_updateComplete) {
            _health = Mathf.Clamp(_health, 0, _maxHealth);
            UpdateHealthUI();
            yield return null;
        }
    }
}
