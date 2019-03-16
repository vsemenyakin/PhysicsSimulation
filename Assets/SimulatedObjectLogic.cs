using UnityEngine;

public class SimulatedObjectLogic : MonoBehaviour
{
    private BoxCollider2D _collider = null;
    private Rigidbody2D _rigidBody = null;

    //API
    public void launch(Vector2 inForce) {
        getRigidbody().AddForce(inForce);
    }

    //Method that should be used insted FixedUpdate before simulated scene
    // Simulate(...) call
    public void simulateStep() {
        getRigidbody().AddForce(new Vector2(0.0f, -9.8f));
    }

    public void draw() {
        drawSquare(transform.position, getBoxCollider().size, Color.green, 10.0f);
    }

    //Implementation details

    //NB: As we want to simulate object at start of the test the start
    // of Simulated Object may be called to late. So it's good idea
    // don't save references to components here, but use lazy accessors
    // with caching of component references
    
    //private void Start() {
    //    _collider = gameObject.GetComponent<BoxCollider2D>();
    //    _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    //}

    // VS

    private BoxCollider2D getBoxCollider() {
        if (null == _collider) _collider = gameObject.GetComponent<BoxCollider2D>();
        return _collider;
    }

    private Rigidbody2D getRigidbody() {
        if (null == _rigidBody) _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        return _rigidBody;
    }

    //-Utils
    static void drawSquare(Vector2 inPosition, Vector2 inSize, Color inColor, float inDuration)
    {
        Vector2 theRectCornerPosition = inPosition;
        theRectCornerPosition -= inSize / 2;

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
