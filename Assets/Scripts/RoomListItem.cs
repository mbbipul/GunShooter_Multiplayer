﻿using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour {

    public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
    public JoinRoomDelegate joinRoomCallback;

    [SerializeField]
    private Text roomNameText;

    private MatchInfoSnapshot match;

    public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallbact)
    {
        match = _match;
        joinRoomCallback = _joinRoomCallbact;

        roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
    }

    public void JoinRoom()
    {
        joinRoomCallback.Invoke(match);
    }
}
