﻿namespace Banks.DateObservers;

public interface IDateSubject
{
    public DateTime CurDate { get; }
    void Attach(IDateObserver observer);
    void Detach(IDateObserver observer);
    void Notify();
}