using System.Collections.Generic;
using UnityEngine;
using Fleck;

class LTServer : MonoBehaviour {

    public Transform headset;
    public Transform leftHand;
    public Transform rightHand;

    public VRState vr = new();
    public LTInputListener inputListener;

    WebSocketServer server;
    readonly List<IWebSocketConnection> allSockets = new();

    void Start() {
        FleckLog.Level = LogLevel.Error;
        server = new WebSocketServer("ws://0.0.0.0:8080");
        server.Start(socket => {
            socket.OnOpen = () => {
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
        vr.headset.UpdateTransform(headset);
        vr.leftHand.UpdateTransform(leftHand);
        vr.rightHand.UpdateTransform(rightHand);
        vr.leftInput = inputListener.leftState;
        vr.rightInput = inputListener.rightState;

        foreach (var socket in allSockets) {
            var value = JsonUtility.ToJson(vr);
            await socket.Send(value);
        }
    }
}