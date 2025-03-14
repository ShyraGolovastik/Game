using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private Canvas _pauseCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameHandler.IsPaused = !_gameHandler.IsPaused;
            _pauseCanvas.enabled = _gameHandler.IsPaused;

            Time.timeScale = _gameHandler.IsPaused ? 0.0f : 1.0f ;
        }
    }
}
