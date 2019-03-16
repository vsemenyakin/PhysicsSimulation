using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSimulationTest : MonoBehaviour
{
    public GameObject ObjectToSimulatePrefab = null;

    PhysicsScene2D _physicsScene;
    GameObject _objectToSimulte = null;

    void Start() {
        //Create simulation scene
        Scene theScene = SceneManager.CreateScene(
            "PhysicsSimulation",
            new CreateSceneParameters(LocalPhysicsMode.Physics2D)
        );
        _physicsScene = theScene.GetPhysicsScene2D();

        //Create object that will be simulated
        _objectToSimulte = GameObject.Instantiate(ObjectToSimulatePrefab);
        SceneManager.MoveGameObjectToScene(_objectToSimulte, theScene);

        //Perform starting simulation
        SimulatedObjectLogic theSimulatedObjectLogic =
                _objectToSimulte.GetComponent<SimulatedObjectLogic>();

        theSimulatedObjectLogic.launch(new Vector2(200.0f, 500.0f));
        for (int theStepIndex = 0; theStepIndex < 50; ++theStepIndex)
        {
            theSimulatedObjectLogic.draw();

            theSimulatedObjectLogic.simulateStep();
            _physicsScene.Simulate(0.02f);
        }
    }

    private void FixedUpdate() {
        //Simulate next steps in main scene FixedUpdate
        SimulatedObjectLogic theSimulatedObjectLogic =
                _objectToSimulte.GetComponent<SimulatedObjectLogic>();

        theSimulatedObjectLogic.draw();

        theSimulatedObjectLogic.simulateStep();
        _physicsScene.Simulate(0.02f);
    }
}
