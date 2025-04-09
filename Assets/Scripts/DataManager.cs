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
        // ���� ��� ����
        folderPath = Path.Combine(Application.persistentDataPath, "GameData");
        filePath = Path.Combine(folderPath, "characterData.json");

        // ������ ������ ����
        CreateFolderIfNotExists(folderPath);

        // JSON ������ ���� �� ����
        SaveJsonData(filePath);

        // JSON ������ �б�
        LoadJsonCharacterData(filePath);
    }

    // ������ ������ ����
    private void CreateFolderIfNotExists(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log($"Folder created at: {path}");
        }
    }

    // JSON ������ ����
    private void SaveJsonData(string path)
    {
        // ���� ������ ����
        CharacterData character = new CharacterData
        {
            characterName = "Warrior",
            level = 100,
            health = 100.0f
        };

        // JSON ���ڿ��� ��ȯ
        string jsonData = JsonUtility.ToJson(character, true);

        // ���Ͽ� JSON ����
        File.WriteAllText(path, jsonData);
        Debug.Log($"JSON saved to: {path}");
    }

    // JSON ������ �б�
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
