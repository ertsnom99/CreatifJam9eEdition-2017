using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observable<T> : Singleton<T> where T : class
{
    private List<Observer> observers = new List<Observer>();
    public void RegisterObserver(Observer observer)
    {
        if (!observers.Contains(observer)){ observers.Add(observer); }
    }

    protected void NotifyObservers()
    {
        foreach (Observer observer in observers)
        {
            observer.Update();
        }
    }
}
