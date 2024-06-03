using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelSave : MonoBehaviour
{
    public void SavePlayerCombat(Inventory inventory)
    {
        string json = JsonUtility.ToJson(inventory);
        string path = Path.Combine(Application.persistentDataPath, "PC.json");
        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    public Inventory LoadPLayerCombat()
    {
        string path = Path.Combine(Application.persistentDataPath, "PC.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<Inventory>(json);
        }
        else
        {
            Debug.LogError("Could not find file in persistentDataPath.");
            return null;
        }
    }
    public void SaveLevel(int Level)
    {
        LevelSaved LS = new LevelSaved();
        LS.Level = Level;
        string json = JsonUtility.ToJson(LS);
        string path = Path.Combine(Application.persistentDataPath, "Level.json");
        Debug.Log(path);
        File.WriteAllText(path, json);
    }

    public LevelSaved LoadLevel()
    {
        string path = Path.Combine(Application.persistentDataPath, "Level.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            LevelSaved data = JsonUtility.FromJson<LevelSaved>(json);
            return data;
        }
        else
        {
            Debug.LogError("Could not find file in persistentDataPath.");
            return null;
        }
    }

    public void DeletePlayerCombat()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "PC.json");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("File đã được xóa: " + filePath);
        }
        else
        {
            Debug.Log("File không tồn tại: " + filePath);
        }
    }    
}

public class LevelSaved
{
    public int Level;
}
