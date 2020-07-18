using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkAdapter : NetworkBehaviour
{

    private bool networking;
    // Start is called before the first frame update
    void Start()
    {
        this.Begin();
    }

    public virtual void Begin() {
        this.networking = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.Tick();
    }

    public virtual void Tick() {

    }

    public bool IsLocal() {
        return this.isLocalPlayer || !this.networking;
    }
}
