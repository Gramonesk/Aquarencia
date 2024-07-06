using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using FileData;
public class Portal : MonoBehaviour
{
    public string SwitchToScene;
    public int PortalID;

    public bool isSwitching;
    private Vector3 TeleportTo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnInteract();
        }
    }
    public void OnInteract()
    {
        if (!isSwitching)
        {
            isSwitching = true;
            DontDestroyOnLoad(this);
            SceneChanger.Instance.ChangeScene(SwitchToScene, OnLoad);
        }
    }
    public void ChangeScene(string name)
    {
        SceneChanger.Instance.ChangeScene(name);
    }
    public void OnLoad()
    {
        Portal destination = FindObjectsOfType<Portal>().First(x => x != this && x.PortalID == this.PortalID);
        //animation false
        TeleportTo = destination.transform.position;    
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = TeleportTo;
        DataManager.Instance.SaveGame();

        Destroy(this.gameObject);
    }
}
