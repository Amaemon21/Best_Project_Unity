using System.Collections;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private readonly float _speedFade = 0.03f;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= _speedFade;
            yield return new WaitForSeconds(_speedFade);
        }

        gameObject.SetActive(false);
    }
}