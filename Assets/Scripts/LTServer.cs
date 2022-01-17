using System.Collections.Generic;
using UnityEngine;
using Fleck;

class LTServer : MonoBehaviour {

    public Transform[] headsetTransform;
    readonly VRTransform vr = new();
    WebSocketServer server;
    readonly List<IWebSocketConnection> allSockets = new();

    void Start() {
        vr.Setup();
        FleckLog.Level = LogLevel.Debug;
        server = new WebSocketServer("ws://0.0.0.0:8080");
        server.Start(socket => {
            Debug.Log("Setup!");
            socket.OnOpen = () => {
                Debug.Log("Open!");
                allSockets.Add(socket);
            };
            socket.OnClose = () => {
                allSockets.Remove(socket);
            };
            //socket.OnMessage = message =>
            //{
            //    allSockets.ForEach(s => s.Send("Echo: " + message));
            //};
        });
    }

    void Update() {
        SendUpdate();
    }

    async void SendUpdate() {
        foreach (var socket in allSockets) {
            vr.Update(headsetTransform);
            var value = JsonUtility.ToJson(vr);
            await socket.Send(value);
        }
    }
}