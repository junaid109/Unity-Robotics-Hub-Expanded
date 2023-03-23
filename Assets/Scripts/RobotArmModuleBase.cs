using UnityEngine;

public abstract class RobotArmModuleBase : MonoBehaviour, IRobotArmModule {
    public virtual void Start() { }
    public virtual void Update() { }
    public virtual void Stop() { }
}
