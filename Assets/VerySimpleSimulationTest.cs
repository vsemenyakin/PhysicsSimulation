using UnityEngine;
using UnityEngine.SceneManagement;

public class VerySimpleSimulationTest : MonoBehaviour
{
    void Start() {
        performSimpleFlyingInGravityTest();
    }

    void performSimpleFlyingInGravityTest()
    {
        //Create simulation scene
        Scene theScene = SceneManager.CreateScene(
            "PhysicsSimulation", /*Scene name*/
            new CreateSceneParameters(LocalPhysicsMode.Physics2D) /*Type of physics on scene*/
        );
        PhysicsScene2D thePhysicsScene = theScene.GetPhysicsScene2D();

        //Create object that will be simulated
        GameObject theGameObject = new GameObject();
        var theCollider = theGameObject.AddComponent<BoxCollider2D>();
        var theRigidBody = theGameObject.AddComponent<Rigidbody2D>();

        //Object is created in scene that is currently active.
        // So we should move object to simulation scene using
        // SceneManager.MoveGameObjectToScene(...) or
        // set simulation scene as active using
        // SceneManager.SetActiveScene(...)
        SceneManager.MoveGameObjectToScene(theGameObject, theScene);

        //Perform simulation
        //-Give starting force
        theRigidBody.AddForce(new Vector2(200.0f, 500.0f));

        for (int theStepIndex = 0; theStepIndex < 50; ++theStepIndex)
        {
            //-Apply gravity (instead of default created physics
            //- we need to apply it manually)
            theRigidBody.AddForce(new Vector2(0.0f, -9.8f));

            //-Draw current object position in simulation
            drawSquare(theGameObject.transform.position, theCollider.size, Color.green, 10.0f);

            //-Perform scene simulation step with delta time 0.02 seconds
            thePhysicsScene.Simulate(0.02f);
        }
    }

    static void drawSquare(Vector2 inPosition, Vector2 inSize, Color inColor, float inDuration) {
        Vector2 theRectCornerPosition = inPosition;
        theRectCornerPosition -= inSize/2;

        Vector2 theLastRectCornerPosition = theRectCornerPosition;
        theRectCornerPosition.x += inSize.x;
        Debug.DrawLine(theLastRectCornerPosition, theRectCornerPosition, inColor, inDuration, false);

        theLastRectCornerPosition = theRectCornerPosition;
        theRectCornerPosition.y += inSize.y;
        Debug.DrawLine(theLastRectCornerPosition, theRectCornerPosition, inColor, inDuration, false);

        theLastRectCornerPosition = theRectCornerPosition;
        theRectCornerPosition.x -= inSize.x;
        Debug.DrawLine(theLastRectCornerPosition, theRectCornerPosition, inColor, inDuration, false);

        theLastRectCornerPosition = theRectCornerPosition;
        theRectCornerPosition.y -= inSize.y;
        Debug.DrawLine(theLastRectCornerPosition, theRectCornerPosition, inColor, inDuration, false);
    }
}
