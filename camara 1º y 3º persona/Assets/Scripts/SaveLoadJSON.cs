using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
struct PlayerData  // objeto serializable
{
    public Vector3 position; //un objeto que es capaz de convertise en un archivo json o binario y viceversa se denomina objeto serializable 
}
public class SaveLoadJSON : MonoBehaviour
{
    public string fileName = "test.json";
    // Start is called before the first frame update
    void Start()
    {
        fileName = Application.persistentDataPath + '\\' + fileName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            Save();
        }

        if(Input.GetKeyDown(KeyCode.L))  
        {
            Load();
        }

    }

    void Save() 
    {
        StreamWriter streamwriter = new StreamWriter(fileName);

        PlayerData playerdata = new PlayerData(); // instancio objeto que vamos a guardar 
        playerdata.position = transform.position; // se rellena de info

        string json = JsonUtility.ToJson(playerdata);   // pasar de un objeto serializable a un formato JSON con un formato string
        streamwriter.WriteLine(json);
 

        streamwriter.Close();
    }

    void Load() 
    { 
        if(File.Exists(fileName)) 
        {
            StreamReader streamReader = new StreamReader(fileName);
            try 
            {                                                                                       // de formato JSON a objeto serializable
                PlayerData playerdata = JsonUtility.FromJson<PlayerData>(streamReader.ReadToEnd()); // el streamReader lee el json entero y lo pasa a objeto serializable
                transform.position = playerdata.position;
            }
            catch (System.Exception e) 
            {
                // sale el topo
                Debug.Log(e.Message);
            }
            streamReader.Close();
        }
    }
}
