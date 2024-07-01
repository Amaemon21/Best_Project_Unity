using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void UpdateValue(float currentHealth, float maxHealth)
    {
        _image.fillAmount = currentHealth / maxHealth;
    }
}