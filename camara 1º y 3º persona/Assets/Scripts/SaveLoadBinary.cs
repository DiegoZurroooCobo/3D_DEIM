using System.IO; //INPUT OUTPUT 
using UnityEngine;

public class SaveLoadBinary : MonoBehaviour
{
    public string fileName = "test.pdf";
    // Start is called before the first frame update
    void Start()
    {
        fileName = Application.persistentDataPath + '\\' + fileName;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    void Save() 
    {
        BinaryWriter bw = new BinaryWriter(new FileStream(fileName, FileMode.Create)); //Si existe un archivo existente lo abre, si no lo crea 
        bw.Flush();
        bw.Write(transform.position.x);
        bw.Write(transform.position.y);
        bw.Write(transform.position.z);
        bw.Flush(); // limpiar el flujo
        bw.Close(); 
    }

    void Load() 
    { 
        if(!File.Exists(fileName)) 
        { 
            return;
        }
            BinaryReader br = new BinaryReader(new FileStream(fileName, FileMode.Open));    
        try
        {
            float x = br.ReadSingle();
            float y = br.ReadSingle();
            float z = br.ReadSingle();
            transform.position = new Vector3(x, y, z);  
        }
        catch(System.Exception e ) 
        { 
            Debug.Log(e.Message);
        }

            br.Close();

    }
}
