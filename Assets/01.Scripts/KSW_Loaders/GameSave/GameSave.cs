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
    private readonly string globalDataFile;  // 글로벌 데이터 파일
    private string currentSaveFile;  // 현재 사용 중인 세이브 파일

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

        //세이브 파일 경로 설정
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
            Debug.LogError($"저장 중 오류가 발생했습니다: {ex.Message}");
            Debug.LogError($"메시지: {ex.Message}");
            Debug.LogError($"예외 유형: {ex.GetType().Name}");
            Debug.LogError($"스택 트레이스: {ex.StackTrace}");
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
                    // 대소문자 구분 없이 매핑
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                    {
                        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
                    },
                    // 순환 참조 무시
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var saveData = JsonConvert.DeserializeObject<GameData>(json, settings);

                PlayerStats loadedPlayerstats = saveData._PlayerStats;
                
                //TODO: 인벤토리 추가 후 작업
                //loadedPlayerstats.Inventory = new Inventory();

                /*if (saveData.InventoryItems != null) // saveData에 인벤토리 아이템이 비어있지 않다면...
                {
                    loadedCharacter.Inventory.AddItem(saveData.InventoryItems); // saveData의 아이템들을 Character의 인벤토리에 저장
                }*/

                return loadedPlayerstats;
            }
            else
            {
                Debug.LogError("파일을 찾을 수 없습니다: " + filePath);
                return default;
            }
        }
        catch (Exception ex) // Load() 오류 텍스트 출력
        {
            Debug.LogError($"불러오기 중 오류가 발생했습니다: {ex.Message}");
            Debug.LogError($"예외 상세: {ex.StackTrace}");
            Thread.Sleep(5000);
            return null;
        }
    }

    public bool HasAnySaveFile() // 세이브 데이터 존재 확인 함수
    {
        bool isValidSaveFile(string filePath) // 각 세이브 파일이 존재하고 유효한지 검사
        {
            if (!File.Exists(filePath)) return false; // 파일이 없다면 false => 캐릭터 생성

            try
            {
                string jsonString = File.ReadAllText(filePath); //세이브 데이터(json)의 텍스트를 읽어 문자열로 변환 => 세이브 내용을 jsonString에 저장
                var saveData = JsonConvert.DeserializeObject<GameData>(jsonString); //json 문자열 타입<GameData)를 객체로 변환 => saveData에 변수로 저장
                return saveData?._PlayerStats != null; // 캐릭터 데이터가 있는지 확인
            }
            catch
            {
                return false; //문자열 변환 이나 객체 변환 실패시 false 반환 => 캐릭터 슬롯 비어있는것으로 판단하여 캐릭터 생성
            }
        }

        // 세개의 슬롯 중 하나라도 세이브 데이터가 있다면 true 반환
        return isValidSaveFile(saveFile);
    }
    private void DeleteSaveFile(string filePath) //세이브 데이터 삭제 함수
    {
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath); // 세이브 데이터 삭제
            }
            catch (Exception ex) //세이브 데이터 삭제 오류 출력
            {
                Console.WriteLine($"저장 파일 삭제 중 오류가 발생했습니다: {ex.Message}");
                Thread.Sleep(5000);
            }
        }
    }

}
