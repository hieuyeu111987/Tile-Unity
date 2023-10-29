using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesBox : MonoBehaviour, TilesBoxListener
{
    [SerializeField] protected private Tiles tiles;

    private void OnEnable() {
        this.registerListenEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void registerListenEvent()
    {
        this.tiles.listenerAdd(this);
    }

    public void getTileList()
    {
        // Debug.Log("getTileList");
    }
}
