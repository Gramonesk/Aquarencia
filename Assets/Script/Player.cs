using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FileData;
using TMPro;
using UnityEngine.Rendering.UI;

public class Player : MonoBehaviour, ISaveFolder<PlayerData>
{
    //LIST YG PERLU DIPOLISH LG CODENYA
    //1. StateMachine masih terlalu berantakan
    //2. Singleton pake yg bener
    //3. Input System
    //4. Bikin lebih modular
    //5. Execution Order dll mulai dipake

    //Alasan ga pake singleton disini 
    //1. Datamanager perlu update interface tiap kali open, mungkin salah design
    //   Alternatif :
    //    a. Tiap Save-able script yg request save sm loadnya bukan dari main / manager
    //    b. Tiap change scene mungkin coba di refresh semua isiannya ulang

    //Review buat manager lain
    //beberapa ada yang digabung ada yang enggak (gak jelas)
    //contoh 1 : UIManager, dipisah sm SceneChanger (handler pindah scene) [Oke oke aja cuman pake static harusnya jg bisa]
    
    //Review buat overall
    //kurang ngedesign code, harus bikin pseudocode dll dlu biar rapih.
    //mulai belajar bikin GDD liburan
    //belajar pake namespace
    //Coding pattern masih ngurang ngena, ada beberapa yg ancur ada yang ga disini (udh terlalu berantakan jdnya diantara start from scratch atau lanjut)

    //Outcomes / visi yg diharapkan
    //Modularity, Code design, software architecture, code patern, AI-based learning
    //Game designing (GDD, diagram dll), animation, time management

    //Yang perlu dipelajari :
    //Post-processing (shader graph dll)
    //Software Architecture
    //Probuilder
    //FUSION (mantepin)
    //UI - UX (mantepin / master)
    
    //Selebihnya belajar berdasarkan kebutuhan, jgn dipaksain kerja kalo emg ga niat (gada hasil sm kode berantakan)

    //NTS : KALO GATAU DAN GADA WAKTU JGN MIKIRIN BAGUSNYA ASAL JALAN AJA DL
    public FolderInfo Folder;
    public string Filename;
    public int Day;
    public FolderInfo folder { get => Folder; set => Folder = value; }
    public string filename { get => Filename; set => Filename = value; }

    public void Load(PlayerData data)
    {
        transform.position = data.position;
        Day = data.Day;
    }

    public void Save(ref PlayerData data)
    {
        data.Day = Day;
        data.position = transform.position;
    }
}
