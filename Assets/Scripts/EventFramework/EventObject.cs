using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventObject
{
    public GameObject sender;
    public object param;

    public EventObject(GameObject sender, object param)
    {
        this.sender = sender;
        this.param = param;
    }
    public EventObject(object param)
    {
        this.sender = null;
        this.param = param;
    }

    public override string ToString()
    {
        return string.Format("sender={0},param={1}", this.sender, this.param);
    }
}
