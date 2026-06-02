using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
public class Counter : ObservableObject, IReceiver
{
    private Dictionary<string, int> deaths { get; }
    public event Action<string>? DeathsUpdated;
    public Counter()
    {
        this.deaths = new Dictionary<string, int>();
    }

    public void ReceiveDeath(string source)
    {
        if (deaths.ContainsKey(source))
        {
        deaths[source]++;
        }
        else
        {
        deaths[source] = 1;
        }
        DeathsUpdated?.Invoke(getDeathsFormatted());
    }
    public string getDeathsFormatted()
    {
        string text="";
        foreach(KeyValuePair<string, int> item in deaths)
        {
            text += item.Key.ToString() +": "+item.Value.ToString()+"\n";
        }

        return text;
    }
}

