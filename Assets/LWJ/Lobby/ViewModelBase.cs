using System;
using System.ComponentModel;
using UnityEngine;

public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string newPropertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(newPropertyName));
    }
    
    public virtual void Dispose()
    {
        PropertyChanged = null;
    }
}