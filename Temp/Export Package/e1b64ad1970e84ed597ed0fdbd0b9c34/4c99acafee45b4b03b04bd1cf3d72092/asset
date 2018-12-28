using UnityEngine;
using System.Collections;
using System;
using UdpKit;
using Bolt;
using udpkit.platform.photon.photon;
using System.Collections.Generic;
using Bolt.photon;
using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.SceneManagement;
using System.Linq;


public class Menu : Bolt.GlobalEventListener
{
    enum State
    {
        SelectMode,
        ModeServer,
        ModeClient
    }

    struct InternalSession
    {
        public UdpSession session;
        public DateTime receivedAt;
    }

    State _state;
    Dictionary<Guid, InternalSession> internalSessionList;

    public override void BoltStartBegin()
    {
        //BoltNetwork.RegisterTokenClass<ServerAcceptToken>();
        //BoltNetwork.RegisterTokenClass<ServerConnectToken>();

        // PhotonRoomProperties is used to pass custom properties into your room
        BoltNetwork.RegisterTokenClass<PhotonRoomProperties>();
    }

    public void ButtonCreateRoom()
    {
        BoltLauncher.StartServer();
        _state = State.ModeServer;
        OnGUI();
    }
    public void ButtonJoinRoom()
    {
        BoltLauncher.StartClient();
        _state = State.ModeClient;
        OnGUI();
    }

    void Awake()
    {
        internalSessionList = new Dictionary<Guid, InternalSession>();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));

        switch (_state)
        {
            case State.ModeServer:
                if (BoltNetwork.isRunning && BoltNetwork.isServer)
                {
                    if (GUILayout.Button("Publish HostInfo And Load Map", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
                    {
                        // Use to pass custom properties to your room
                        PhotonRoomProperties roomProperties = new PhotonRoomProperties();

                        // sample room property
                        roomProperties.AddRoomProperty("map_id", 1);

                        roomProperties.IsOpen = true;
                        roomProperties.IsVisible = true;

                        string matchName = "MyPhotonGame #" + UnityEngine.Random.Range(1, 100);

                        BoltNetwork.SetServerInfo(matchName, roomProperties);
                        BoltNetwork.LoadScene("Tutorial1");
                    }
                }
                break;

            case State.ModeClient:
                if (BoltNetwork.isRunning && BoltNetwork.isClient)
                {
                    GUILayout.Label("Session List");
                    ShowSessionList(internalSessionList);
                }
                break;
        }
        GUILayout.EndArea();
    }

    void ShowSessionList(Dictionary<Guid, InternalSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            InternalSession internalSession = session.Value;

            // cast internalSession.session to UdpSession
            UdpSession udpSession = internalSession.session as UdpSession;

            // Skip if is not a Photon session
            if (udpSession.Source != UdpSessionSource.Photon)
            {
                BoltLog.Info("UdpSession with different Source: {0}", udpSession.Source);
                continue;
            }

            PhotonSession photonSession = udpSession as PhotonSession;

            if (photonSession == null)
            {
                continue;
            }

            string sessionDescription = String.Format("{0} ({1})", photonSession.HostName, photonSession.Id);

            object prop_map = -1;

            if (photonSession.Properties.ContainsKey("map_id"))
            {
                prop_map = photonSession.Properties["map_id"];
            }

            sessionDescription += String.Format(" :: Map {0}", prop_map);


            sessionDescription += string.Format(" :: {0}", internalSession.receivedAt);

            if (GUILayout.Button(sessionDescription, GUILayout.ExpandWidth(true), GUILayout.Height(300)))
            {
                //ServerConnectToken connectToken = new ServerConnectToken
                //{
                //    data = "ConnectTokenData"
                //};
                //BoltNetwork.Connect(photonSession, connectToken);

                BoltNetwork.Connect(photonSession);
            }
        }
    }

    // called after BoltLauncher.StartServer(), i.e. when server is started
    //public override void BoltStartDone()
    //{
    //    _state = State.ModeServer;

    //    if (BoltNetwork.isServer)
    //    {
    //        string roomName = Guid.NewGuid().ToString();
    //        BoltNetwork.SetServerInfo(roomName, null);

    //        // to load Tutorial1 scene
    //        BoltNetwork.LoadScene("Tutorial1");
    //    }
    //}

    //public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    //{
    //    Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

    //    foreach (var session in sessionList)
    //    {
    //        UdpSession photonSession = session.Value as UdpSession;

    //        if (photonSession.Source == UdpSessionSource.Photon)
    //        {
    //            BoltNetwork.Connect(photonSession);
    //        }
    //    }
    //}

    // Callback triggered when the session list is updated
    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        BoltLog.Info("Session list updated: {0} total sessions", sessionList.Count);

        foreach (var session in sessionList)
        {
            UdpSession udpSession = session.Value as UdpSession;
            BoltLog.Info("UdpSession {0} Source: {1}", udpSession.HostName, udpSession.Source);

            if (udpSession.Source == UdpSessionSource.Photon)
            {
                internalSessionList[udpSession.Id] = new InternalSession()
                {
                    session = udpSession,
                    receivedAt = DateTime.Now
                };
            }
        }
    }
}