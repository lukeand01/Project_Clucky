using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Windows;

public static class SaveHandler
{
    //just want a basic script to turn into json.
    //THIS IS OUTDATE BECAUSE ITS NOT WORKING AND BECAUSE ITS NOT SECURE EVEN IF DID WORK.


    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        string path = Application.persistentDataPath + saveName;
        if(!System.IO.Directory.Exists(path))
        {
            Debug.Log("created directory");
            System.IO.Directory.CreateDirectory(path);
        }
        else
        {
            Debug.Log("has space somewhere");
        }

        //should i do a temp file?


        Debug.Log("got here");

        FileStream file = System.IO.File.Open(path, FileMode.Open); ;      

        Debug.Log("second");

        formatter.Serialize(file, saveData);
        file.Close();
        Debug.Log("data has been saved");
        return true;
    }

    public static object Load(string saveName)
    {
        Debug.Log("start load");

        string path = Application.persistentDataPath + saveName;

        if (!System.IO.File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = System.IO.File.Open(path, FileMode.Open);

        try
        {
            Debug.Log("Load this fella");
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("failed to load file ", path);
            file.Close();
            return null;
        }

    }

    public static bool CanLoad(string path)
    {
        return System.IO.File.Exists(Application.persistentDataPath + path);
      
    }

    public static void DeleteFile(string saveName)
    {
        string path = Application.persistentDataPath + saveName;
        if (!System.IO.Directory.Exists(path))
        {
            Debug.Log("file delete");
            System.IO.File.Delete(path);
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        return new BinaryFormatter();
    }
}
