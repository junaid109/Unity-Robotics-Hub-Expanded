public class RobotArm: RobotArmModuleBase {

public RobotArmActuator[] actuators;
    public RobotArmSensor[] sensors;

    public Transform pickupLocation;
    public Transform dropoffLocation;

    private int _currentStep = 0;
    private int[] _stepOrder = new int[] { 0, 1, 2, 1 };

    private Coroutine _operationCoroutine;

    public override void Start() {
        // Initialize all actuators and sensors
        foreach (var actuator in actuators) {
            actuator.Start();
        }
        foreach (var sensor in sensors) {
            sensor.Start();
        }
    }

    public override void Update() {
        // Check if the operation coroutine is running
        if (_operationCoroutine == null) {
            // Start a new operation coroutine
            _operationCoroutine = StartCoroutine(OperationCoroutine());
        }
    }

    public override void Stop() {
        // Stop all actuators and sensors
        foreach (var actuator in actuators) {
            actuator.Stop();
        }
        foreach (var sensor in sensors) {
            sensor.Stop();
        }
        // Stop the operation coroutine
        if (_operationCoroutine != null) {
            StopCoroutine(_operationCoroutine);
            _operationCoroutine = null;
        }
    }

    private IEnumerator OperationCoroutine() {
        while (true) {
            // Get the current step of the operation
            int step = _stepOrder[_currentStep];
            _currentStep = (_currentStep + 1) % _stepOrder.Length;

            // Move the robot arm to the pickup location
            yield return MoveTo(pickupLocation.position);

            // Pick up the box
            foreach (var actuator in actuators) {
                actuator.SetPosition(1.0f);
            }
            yield return new WaitForSeconds(1.0f);

            // Move the robot arm to the dropoff location
            yield return MoveTo(dropoffLocation.position);

            // Drop off the box
            foreach (var actuator in actuators) {
                actuator.SetPosition(0.0f);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator MoveTo(Vector3 position) {
        // Move each actuator to the desired position
        foreach (var actuator in actuators) {
            actuator.SetPosition(0.0f);
        }
    }
}
