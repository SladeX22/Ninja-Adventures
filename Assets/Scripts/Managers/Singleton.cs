using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T i { get; private set; }

    public virtual void Awake()
    {
        i = this as T;
    }
}
