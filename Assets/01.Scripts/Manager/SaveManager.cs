using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public class SaveManager
{
    
    public static string filePath;
    // JSON ���Ϸ� ����
    public static void SaveToJson(SaveData saveData, string filePath)
    {
        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static SaveData LoadFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<SaveData>(json);
        }
        else
        {
            Debug.LogError("������ ã�� �� �����ϴ�: " + filePath);
            return null;
        }
        
    }

    
}
