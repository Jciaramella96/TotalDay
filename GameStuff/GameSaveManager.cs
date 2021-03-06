using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    public List<ScriptableObject> objects = new List<ScriptableObject>();

    public void ResetScriptables()// reset scriptable objects
    {
        for(int i=0; i<objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i)); //deletes the files in storage, startofdebug system.
            }
        }
    }
    private void OnEnable()
    {
        LoadScriptables();
    }
    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        for(int i=0; i<objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i)); // i will replace the zero, so it loops through it
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]); // take the object currently in loop and make it a json file
            binary.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string) binary.Deserialize(file),objects[i]);
                file.Close();
            }
        }
    }
    // Start is called before the first frame update
    /*
    void Start()
    {
        if(gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
