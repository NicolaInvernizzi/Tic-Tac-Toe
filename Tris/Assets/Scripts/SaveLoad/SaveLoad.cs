using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public Inventory inventory = new Inventory();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SaveToJson();
        if (Input.GetKeyDown(KeyCode.L))
            LoadFromJson();
    }
    public void SaveToJson()
    {
        // JSON = string file
        // Convert object into JSON.The object has to be Serializable with public field
        string inventoryData = JsonUtility.ToJson(inventory);

        /* Create a filePath: the location of the JSON file. 
        *  PersistentDataPath repository auto created for every Unity game. Neve erased, even when the game is updated. (Works for all
        *  system and devices)
        *  So just create in thi repo the file with this name:/InventoryData (.json is the file type).
        */
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        Debug.Log(filePath);

        // Insert the data in the file.
        System.IO.File.WriteAllText(filePath, inventoryData);
        Debug.Log("Saved");
    }
    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/InventoryData.json";

        // Open, read and save the fille in the filePath into the string variable.
        string inventoryData = System.IO.File.ReadAllText(filePath);

        // Convert string to Inventory type.
        inventory = JsonUtility.FromJson<Inventory>(inventoryData);
        Debug.Log("Loaded");
    }
}

[System.Serializable]
public class Inventory
{
    public int coins;
    public bool isFull;
    public List<int> list = new List<int>();
    public List<Items> items = new List<Items>();
}

[System.Serializable]
public class Items
{
    public string name;
    public string description;
}
