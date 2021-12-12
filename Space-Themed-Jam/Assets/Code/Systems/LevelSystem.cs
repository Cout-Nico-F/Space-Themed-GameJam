using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private int countdownTime;

    private UISystem _uiSystem;

    // al comenzar el nivel debemos mostrar la cuenta atrás de comienzo del nivel
    // instanciar al Player
    // leer el nivel en que estamos y cargar el parallax correspondiente
    // cuando termine la cuenta atrás empieza la batalla

    private void Start()
    {
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();

        StartCoroutine(Countdown());
    }


    private IEnumerator Countdown()
    {
        _uiSystem.ShowCountdown();
        for (int i = countdownTime; i > 0; i--)
        {
            _uiSystem.SetCountdownText(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        _uiSystem.HideCountdownText();
    }

}
