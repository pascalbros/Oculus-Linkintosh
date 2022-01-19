using UnityEngine;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using System;
using System.Text;

public class LTClient : MonoBehaviour {
    ClientWebSocket ws;

    public string serverUrl;

    public LTDeviceSimulator simulator;

    VRState lastState;

    async void Start() {
        await Task.Delay(500);
        _ = Task.Run(() => RunWsClient());
    }

    void Update() {
        if (lastState != null) {
            UpdateWithVrState(lastState);
        }
    }

    private void UpdateWithVrState(VRState state) {
        simulator.UpdateWithStream(state);
    }

    private async Task RunWsClient() {
        ws = new();
        Uri serverUri = new(serverUrl);
        var source = new CancellationTokenSource();
        source.CancelAfter(int.MaxValue);

        await ws.ConnectAsync(serverUri, source.Token);
        while (ws.State == WebSocketState.Open) {
            var receiveBuffer = new byte[32 * 1024];
            var offset = 0;
            var dataPerPacket = 1024;
            while (true) {
                ArraySegment<byte> bytesReceived =
                          new(receiveBuffer, offset, dataPerPacket);
                WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived,
                                                              source.Token);
                offset += result.Count;
                if (result.EndOfMessage)
                    break;
            }
            var stringResult = Encoding.UTF8.GetString(receiveBuffer, 0, offset);
            var vrState = JsonUtility.FromJson<VRState>(stringResult);
            if (vrState != null) {
                lastState = vrState;
            }
        }
    }

    private async Task Send(string message) {
        var source = new CancellationTokenSource();
        source.CancelAfter(5000);
        ArraySegment<byte> bytesToSend = new(Encoding.UTF8.GetBytes(message));
        await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, source.Token);
    }

}