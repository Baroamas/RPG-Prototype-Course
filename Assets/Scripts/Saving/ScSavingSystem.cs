using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

namespace RPG.Saving
{

    public class ScSavingSystem : MonoBehaviour
    {

        //kurasa intinya setiap variable di ubah ke bit yang disimpan di byte; 1 byte = 8 bit
        public void Save(string saveFile)

        {
            using (FileStream stream = File.Open(GetSaveFilePath(saveFile), FileMode.Create))
            {
                #region Basic
                //byte[] hexOrDecimalArray = { 0xC2, 0xA1, 72, 0X6F, 0x6c, 0x61, 0x20, 77, 0x75, 0x6E, 0x64, 0x6F, 0X21 }; //baru sadar apapun Unit yg masuk ke tipe data otomatis convert ke tipedata itu..
                //byte[] hexOrDecimalArray = System.Text.Encoding.UTF8.GetBytes("¡Hola Mundo!");     
                //stream.Write(hexOrDecimalArray, 0, hexOrDecimalArray.Length);
                //stream.Close(); Perlu di Close kalau gak pake using
                #endregion
                Vector3 dataPositionDummy = GameObject.FindGameObjectWithTag("Player").transform.position;
                byte[] dataInByte = SerializeVector3(dataPositionDummy);
                stream.Write(dataInByte, 0, dataInByte.Length);
            }



            Debug.Log($"File {saveFile} Saved with Love to { GetSaveFilePath(saveFile)} ");
        }

        public void Load(string saveFile)
        {
            using (FileStream stream = File.Open(GetSaveFilePath(saveFile), FileMode.Open))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                
                GameObject.FindGameObjectWithTag("Player").transform.position = DeserializeVector3(buffer);
                
                //Debug.Log(System.Text.Encoding.UTF8.GetString(buffer));
            }
            Debug.Log($"File {saveFile} Loaded with Love from {GetSaveFilePath(saveFile)} ");
        }

        byte[] SerializeVector3(Vector3 vector)
        {
            byte[] SerializedVector = new byte[3 * 4]; //1 float 4 bytes
            BitConverter.GetBytes(vector.x).CopyTo(SerializedVector, 0);
            BitConverter.GetBytes(vector.y).CopyTo(SerializedVector, 4);
            BitConverter.GetBytes(vector.z).CopyTo(SerializedVector, 8);
            return SerializedVector;
        }

        Vector3 DeserializeVector3(byte[] SerializedVector)
        { 
            Vector3 vector = new Vector3();
            vector.x = BitConverter.ToSingle(SerializedVector, 0);
            vector.y = BitConverter.ToSingle(SerializedVector, 4);
            vector.z = BitConverter.ToSingle(SerializedVector, 8);
            return vector;
        }

        string GetSaveFilePath(string SaveFile)
        {
            //C:\Users\Baroamas\AppData\LocalLow\DefaultCompany\RPG Prototype
            //string[] paths = { @"C:\Users", System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "BaroDev", Application.productName, SaveFile };
            //string fullpath = Path.Combine(paths);
            return Path.Combine(Application.persistentDataPath, SaveFile + ".sav");
        }
    }

}