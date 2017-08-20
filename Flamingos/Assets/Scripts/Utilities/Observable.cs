using System.Collections.Generic;

public abstract class Observable<T> : Singleton<T> where T : class
{
    private List<Observer> observers = new List<Observer>();
    public void RegisterObserver(Observer observer)
    {
        if (!observers.Contains(observer)){ observers.Add(observer); }
    }

    public void UnregisterObserver(Observer observer)
    {
        if (observers.Contains(observer)) { observers.Remove(observer); }
    }

    protected void NotifyObservers()
    {
        foreach (Observer observer in observers)
        {
            observer.UpdateObserver();
        }
    }
}
