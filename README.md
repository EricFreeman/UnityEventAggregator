Unity Event Aggregator
======================

Event aggregation in Unity made easy!  Decouple your GameObjects for simpler and cleaner code.

#### Why Should I Care?
[Look at how gross this is.](https://docs.unity3d.com/410/Documentation/ScriptReference/index.Accessing_Other_Game_Objects.html)  Disgusting, huh?  Now what if there was an easy way to send messages to other game objects in Unity3D without coupling everything to hell or using skittles magic?

#### That way is here.
Just drop [the .dll](https://github.com/EricFreeman/UnityEventAggregator/releases/tag/v1.0.0.0) into your existing Unity project and get started.  Messages must be a class.

To start listening to events, your MonoBehaviour must also inherit from `IListener<(message)>`.  This will create the handler for the message.

Make sure you register and unregister with the `EventAggregator` in your MonoBehaviour's `Start` and `OnDestroy` methods.  I'll probably create a base class to inherit from that will do this automatically for you...when I'm feeling less lazy.

## Download
[v1.0.0.0 Download](https://github.com/EricFreeman/UnityEventAggregator/releases/tag/v1.0.0.0)

## Example Usage
https://github.com/EricFreeman/1800ContactsEndlessRunner/blob/master/src/Assets/Scripts/UI/Score.cs
