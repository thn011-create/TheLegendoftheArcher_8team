using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Linq;
using Newtonsoft.Json;
using System;

[System.Serializable]
public class GameSave
{
    private readonly string saveDirectory;
    private readonly string saveFile;
    private readonly string globalDataFile;  // �۷ι� ������ ����
    private string currentSaveFile;  // ���� ��� ���� ���̺� ����

    private readonly PlayerStats playerStats;

    public class GameData
    {
        public PlayerStats _PlayerStats;
        public List<WeaponInfo> Weapons;
    }


    public GameSave()
    {
        saveDirectory = Path.Combine(Application.dataPath, "/Resources");

        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        //���̺� ���� ��� ����
        saveFile = Path.Combine(saveDirectory, "PlayerData.json");
        
        playerStats = new PlayerStats();
    }


    public void Save(PlayerStats player, string filePath)
    {
        try
        {
            var saveData = new GameData
            {
                _PlayerStats = player,
                //Weapons = player.Inventory.GetItems()
            };

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(saveData, settings);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"���� �� ������ �߻��߽��ϴ�: {ex.Message}");
            Debug.LogError($"�޽���: {ex.Message}");
            Debug.LogError($"���� ����: {ex.GetType().Name}");
            Debug.LogError($"���� Ʈ���̽�: {ex.StackTrace}");
            Thread.Sleep(5000);
        }
    }
    private PlayerStats Load(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                var settings = new JsonSerializerSettings
                {
                    // ��ҹ��� ���� ���� ����
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                    {
                        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                    },
                    // ��ȯ ���� ����
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var saveData = JsonConvert.DeserializeObject<GameData>(json, settings);

                PlayerStats loadedPlayerstats = saveData._PlayerStats;
                
                //TODO: �κ��丮 �߰� �� �۾�
                //loadedPlayerstats.Inventory = new Inventory();

                /*if (saveData.InventoryItems != null) // saveData�� �κ��丮 �������� ������� �ʴٸ�...
                {
                    loadedCharacter.Inventory.AddItem(saveData.InventoryItems); // saveData�� �����۵��� Character�� �κ��丮�� ����
                }*/

                return loadedPlayerstats;
            }
            else
            {
                Debug.LogError("������ ã�� �� �����ϴ�: " + filePath);
                return default;
            }
        }
        catch (Exception ex) // Load() ���� �ؽ�Ʈ ���
        {
            Debug.LogError($"�ҷ����� �� ������ �߻��߽��ϴ�: {ex.Message}");
            Debug.LogError($"���� ��: {ex.StackTrace}");
            Thread.Sleep(5000);
            return null;
        }
    }

    public bool HasAnySaveFile() // ���̺� ������ ���� Ȯ�� �Լ�
    {
        bool isValidSaveFile(string filePath) // �� ���̺� ������ �����ϰ� ��ȿ���� �˻�
        {
            if (!File.Exists(filePath)) return false; // ������ ���ٸ� false => ĳ���� ����

            try
            {
                string jsonString = File.ReadAllText(filePath); //���̺� ������(json)�� �ؽ�Ʈ�� �о� ���ڿ��� ��ȯ => ���̺� ������ jsonString�� ����
                var saveData = JsonConvert.DeserializeObject<GameData>(jsonString); //json ���ڿ� Ÿ��<GameData)�� ��ü�� ��ȯ => saveData�� ������ ����
                return saveData?._PlayerStats != null; // ĳ���� �����Ͱ� �ִ��� Ȯ��
            }
            catch
            {
                return false; //���ڿ� ��ȯ �̳� ��ü ��ȯ ���н� false ��ȯ => ĳ���� ���� ����ִ°����� �Ǵ��Ͽ� ĳ���� ����
            }
        }

        // ������ ���� �� �ϳ��� ���̺� �����Ͱ� �ִٸ� true ��ȯ
        return isValidSaveFile(saveFile);
    }
    private void DeleteSaveFile(string filePath) //���̺� ������ ���� �Լ�
    {
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath); // ���̺� ������ ����
            }
            catch (Exception ex) //���̺� ������ ���� ���� ���
            {
                Console.WriteLine($"���� ���� ���� �� ������ �߻��߽��ϴ�: {ex.Message}");
                Thread.Sleep(5000);
            }
        }
    }

}
