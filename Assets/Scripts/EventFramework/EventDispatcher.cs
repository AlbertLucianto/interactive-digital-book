using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void OnNotification(EventObject notific);


public static class EventDispatcher  {


    private static Dictionary<int, List<OnNotification>> eventListenerDic = new Dictionary<int, List<OnNotification>>();


    public static void AddEventListener(SystemEvent eventKey, OnNotification eventListener)
    {
        if (!eventListenerDic.ContainsKey((int)eventKey))
        {
            eventListenerDic.Add((int)eventKey, new List<OnNotification> { eventListener});
        }
        else
        {
            eventListenerDic[(int)eventKey].Add(eventListener);
        }
    }
    public static void RemoveAllEventListener(SystemEvent eventKey)
    {
        if (!eventListenerDic.ContainsKey((int)eventKey)) return;
        eventListenerDic.Remove((int)eventKey);
    }

    public static void RemoveEventListener(SystemEvent eventKey, OnNotification eventListener)
    {
        if (!eventListenerDic.ContainsKey((int)eventKey)) return;
        List<OnNotification> notifications = eventListenerDic[(int)eventKey];
        notifications.Remove(eventListener);
        if (notifications.Count == 0)
        {
            eventListenerDic[(int)eventKey] = null;
            eventListenerDic.Remove((int)eventKey);
            notifications = null;
        }
    }


    public static void TriggerEvents(SystemEvent eventKey, GameObject sender, object param)
    {
        if (!eventListenerDic.ContainsKey((int)eventKey)) return;

        List<OnNotification> eventListeners = eventListenerDic[(int)eventKey];
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i](new EventObject(param));
        }
    }


    
    public static void TriggerEvents(SystemEvent eventKey, object param)
    {
        if (!eventListenerDic.ContainsKey((int)eventKey)) return;
        List<OnNotification> copyList = eventListenerDic[(int)eventKey];
        for (int i = copyList.Count - 1; i >= 0; i--)
        {
            copyList[i](new EventObject(param));
        }
    }

    public static bool HasEventListener(SystemEvent eventKey)
    {
        return eventListenerDic.ContainsKey((int)eventKey);
    }

}
