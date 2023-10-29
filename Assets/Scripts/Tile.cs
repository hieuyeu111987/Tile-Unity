using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // private List<GamePlayListener> listeners = new List<GamePlayListener>();
    private List<TilesListener> listeners = new List<TilesListener>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onChooseTile()
    {
        this.addTileToList();
    }

    public void listenerAdd(TilesListener listener)
    {
        this.listeners.Add(listener);
    }

    private void addTileToList()
    {
        foreach (TilesListener listener in this.listeners)
        {
            listener.addTileToList(this);
        }
    }
}
