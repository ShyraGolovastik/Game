using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private GameState _state;

    private JsonLoader _jsonLoader;
    private JsonSaver _jsonSaver;
    private string _filePath = Application.dataPath + "/save.json";
    private void Awake()
    {
        _jsonLoader = new JsonLoader();
        _jsonSaver = new JsonSaver();
        _saveButton.onClick.AddListener(Save);
        _loadButton.onClick.AddListener(Load);
    }
    private void Save()
    {
        _jsonSaver.Save(_filePath, _state.GetWorldData());
        print("File save successful, path:\n" + _filePath);
    }

    private void Load()
    {
        if (_filePath == default(string))
        {
            Debug.LogWarning("Файл сохранения не найден. Создание нового сохранения...");
            Save(); 
        }

        WorldData loadedData = _jsonLoader.Load(_filePath);
        if (loadedData != null)
        {
            Debug.Log("Игра успешно загружена.");
        }
        else
        {
            Debug.LogError("Не удалось загрузить данные из файла.");
        }
        _state.LoadData(loadedData);
    }
}
