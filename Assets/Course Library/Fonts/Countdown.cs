using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CountScores : MonoBehaviour
{
    [SerializeField] private GameHandler _gameHandler;

    private Coroutine _coroutine;
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _gameHandler.OnGameStart += () => _coroutine = StartCoroutine(Count());
        _gameHandler.OnGameOver += () => StopCoroutine(_coroutine);
    }

    private IEnumerator Count()
    {
        while(true)
        {
            _gameHandler.Player.Scores += _gameHandler.ScoresPerSecond * Time.deltaTime;
            _text.text = "Scores\n" + Mathf.Floor(_gameHandler.Player.Scores);
            yield return null;
        }
    }
}
