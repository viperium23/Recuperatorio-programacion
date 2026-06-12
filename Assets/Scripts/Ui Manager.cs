using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _redScore;
    [SerializeField] private TMP_Text _blueScore;
    [SerializeField] private TMP_Text _timer;

    private void Update()
    {
        if (GameManager.instance == null) return;

        _redScore.text = "Rojo: " + GameManager.instance.RedScore.Value;
        _blueScore.text = "Azul: " + GameManager.instance.BlueScore.Value;
        
        _timer.text = "Tiempo: " + Mathf.CeilToInt(GameManager.instance.Timer.Value);

    }

}
