using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
public class JsonSaver
{
    public void Save(string path, WorldData data)
    {
        File.WriteAllText(
            path,
           JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings()
           {
               ReferenceLoopHandling = ReferenceLoopHandling.Ignore //Отмена залупливания обращений 
           })
           );
    }
}
public class JsonLoader
{
    public WorldData Load(string path)
    {
        if (File.Exists(path))
        {
                string jsonString = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<WorldData>(jsonString);
        }
        else
        {
            Debug.LogError("Файл не найден: " + path);
            return null;
        }
    }
}
