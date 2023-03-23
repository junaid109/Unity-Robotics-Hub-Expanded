public class ConveyorBelt : RobotArmModuleBase {
    public float speed = 1.0f;

    private Transform _transform;

    public override void Start() {
        _transform = transform;
    }

    public override void Update() {
        _transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
