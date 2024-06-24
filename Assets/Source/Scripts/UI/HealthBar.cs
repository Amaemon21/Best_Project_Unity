using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [HideInInspector] [SerializeField] private Image _image;

    private void OnValidate()
    {
        _image ??= GetComponent<Image>();
    }

    public void UpdateValue(float currentHealth, float maxHealth)
    {
        _image.fillAmount = currentHealth / maxHealth;
    }
}
