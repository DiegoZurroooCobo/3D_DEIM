using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadTXT : MonoBehaviour
{
    public string fileName = "test.txt";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            Save();
        }
        else if(Input.GetKeyDown(KeyCode.L)) 
        { 
            Load();
        }

    }
    void Save() 
    {
        // guardar
            StreamWriter streamwriter = new StreamWriter(Application.persistentDataPath + '\\' + fileName);
        streamwriter.WriteLine("Archivo de guardado");
        streamwriter.WriteLine(Random.Range(0, 100));
        streamwriter.WriteLine(transform.position.x);
        streamwriter.WriteLine(transform.position.y);
        streamwriter.WriteLine(transform.position.z);

        streamwriter.Close(); // muy importante cerrar los cambios
    }

    void Load() 
    {
        // cargar informacion
        if (File.Exists(Application.persistentDataPath + '\\' + fileName))
        {
            try
            {

                StreamReader streamreader = new StreamReader(Application.persistentDataPath + '\\' + fileName);
                streamreader.ReadLine(); // las primeras lineas no es importar
                                         // movemos el cursor del archivo a la siguiente linea
                streamreader.ReadLine();
                float x = float.Parse(streamreader.ReadLine()); // Parse = pasar de un string a un tipo concreto
                float y = float.Parse(streamreader.ReadLine()); // Parse = pasar de un string a un tipo concreto
                float z = float.Parse(streamreader.ReadLine()); // Parse = pasar de un string a un tipo concreto

                streamreader.Close();

                transform.position = new Vector3(x, y, z); // establecemos la posicion del gameobject
            }
            catch (System.Exception e) // como no guardamos info en ningun servidor,
                                       //guardamos en LOCAL, no tenemos control sobre los archivos del usuario. 
                                       //Nos guardamos de que si algo va mal, este todo controlado 
            {
                Debug.Log(e.Message);
            }
        }
    }
}
