using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string folderPath;
    private string filePath;

    private void Start()
    {
        // 폴더 경로 설정
        folderPath = Path.Combine(Application.persistentDataPath, "GameData");
        filePath = Path.Combine(folderPath, "characterData.json");

        // 폴더가 없으면 생성
        CreateFolderIfNotExists(folderPath);

        // JSON 데이터 생성 및 저장
        SaveJsonData(filePath);

        // JSON 데이터 읽기
        LoadJsonCharacterData(filePath);
    }

    // 폴더가 없으면 생성
    private void CreateFolderIfNotExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log($"Folder created at: {path}");
        }
    }

    // JSON 데이터 저장
    private void SaveJsonData(string path)
    {
        // 예제 데이터 생성
        CharacterData character = new CharacterData
        {
            characterName = "Warrior",
            level = 100,
            health = 100.0f
        };

        // JSON 문자열로 변환
        string jsonData = JsonUtility.ToJson(character, true);

        // 파일에 JSON 저장
        File.WriteAllText(path, jsonData);
        Debug.Log($"JSON saved to: {path}");
    }

    // JSON 데이터 읽기
    private void LoadJsonCharacterData(string path)
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            CharacterData character = JsonUtility.FromJson<CharacterData>(jsonData);

            Debug.Log($"Loaded Character: {character.characterName}, Level: {character.level}, Health: {character.health}");
        }
        else
        {
            Debug.LogWarning($"File not found at: {path}");
        }
    }
}

[System.Serializable]
public struct CharacterData
{
    public string characterName;
    public int level;
    public float health;
}
