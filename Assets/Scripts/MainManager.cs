using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
    {
        // Start() and Update() methods deleted - we don't need them right now

        public static MainManager Instance; //class declaration where values are stored and can be shared by all instances
    
        public Color TeamColor;

    private void Awake() // method called anytime an object is created
    { 
        if (Instance != null) //if there is another GameManager 
        {
            Destroy(gameObject); // destroy the extra GameManger
            return;
        }

        Instance = this; // can call the MainManager.Instance from any other script
      
        DontDestroyOnLoad(gameObject); // tells to not destroy from scene to scene
        LoadColor();
        }
    [System.Serializable]
    class SaveData // created a new instance of save data 
    {
        public Color TeamColor; // class member with the variable saved
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data); // transformed instance to JSON

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); //used special method to write string file
    }//                   (path to file)                 ( text you want to write in the file) 

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json"; // method reversal of Savecolor method

        if (File.Exists(path)) //method checking if file exists
        {
            string json = File.ReadAllText(path); // if it does read it
            SaveData data = JsonUtility.FromJson<SaveData>(json); // tells the converting method to transform data back to SaveData

            TeamColor = data.TeamColor;
        }
    }


}

