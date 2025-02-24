using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> where T : class, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return instance = new T();
            }
        }
    }
}
