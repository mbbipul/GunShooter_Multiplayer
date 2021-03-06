﻿using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{

    [SerializeField]
    private uint roomSize = 6;

    private string roomName;

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void SetRoomName(string _name)
    {
        roomName = _name;
        Debug.Log("room name set to " + roomName);
    }

    public void CreateRoom()
    {
        Debug.Log("room createds " + roomName);

        if (roomName != "" && roomName != null)//check room name is not null
        {
            Debug.Log("Creating Room: " + roomName + " with room for " + roomSize + " players.");
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "","","",0,0, networkManager.OnMatchCreate);
            Debug.Log("room created " + roomName);

        }

    }

}