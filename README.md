# Oculus Linkintosh

There's no support for Oculus Quest 2 (and the old one) on Mac for development.
The only way to test your game is to build and run directly on your headset, so inconvenient.

The aim of Linkintosh is to run the project directly into the Unity editor, using your headset as input on MacOS.

## How it works

### Server

Build and run the project directly into your headset, it will silently start a local websocket server (`LTServer.cs`).

### Client

The stream will be received by the client and will change the anchors accordly to the state of the headset.

1. Copy the files `LTClient.cs` and `Models/VRTransform.cs` into your project
2. Attach `LTClient.cs` to an object
3. Setup the anchors
4. Add the server URL (ws://your.headset.local.address:8080) (eg: ws://10.0.0.8:8080)

And you are ready to go, open the app on the headset and press play on Unity.

It supports:

Position/rotation:
- Headset
- Right controller
- Left controller

## Compatibility

I own the Oculus Quest 2, but it should work with all the VR headsets.

I didn't tried on Linux but it should work there as well, the websocket client is based on `System.Net.WebSockets.ClientWebSocket`.

## Streaming

The streaming from the Unity editor is not supported yet, but seems feasible using [Unity Render Streaming](https://github.com/Unity-Technologies/UnityRenderStreaming), if you are willing to help, just open a PR. Cheers.

