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

    VRTransform lastTransform;


    async void Start() {
        await Task.Delay(2000);
        _ = Task.Run(() => RunWsClient());
        Camera.main.stereoTargetEye = StereoTargetEyeMask.Both;
    }

    void Update() {
        if (lastTransform != null) {
            UpdateWithVrTransform(lastTransform);
        }
    }

    private void UpdateWithVrTransform(VRTransform t) {
        simulator.UpdateWithStream(t);
    }

    private void UpdateVrTransformItem(Transform t, VRItemTransform vrt) {
        if (t != null) {
            t.localPosition = vrt.position;
            t.localEulerAngles = vrt.eulerAngles;
        }
    }

    private async Task RunWsClient() {
        ws = new();
        Uri serverUri = new(serverUrl);
        var source = new CancellationTokenSource();
        source.CancelAfter(Int32.MaxValue);

        await ws.ConnectAsync(serverUri, source.Token);
        while (ws.State == WebSocketState.Open) {
            var receiveBuffer = new byte[32 * 1024];
            var offset = 0;
            var dataPerPacket = 1024; //Just for example
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
            var vrTransform = JsonUtility.FromJson<VRTransform>(stringResult);
            if (vrTransform != null) {
                lastTransform = vrTransform;
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