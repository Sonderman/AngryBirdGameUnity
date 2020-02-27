using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private bool _isBirdLaunched;
    private float _timeSittingAround;
    Vector3 _initialPosition;

    [SerializeField] private float _launchPower = 500;
    

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (_isBirdLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }
        if (transform.position.y > 10 || 
            transform.position.y < -10|| 
            transform.position.x > 10|| 
            transform.position.x < -20|| _timeSittingAround >3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void Awake()
    {
        _initialPosition = transform.position;
    }
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }
    private void OnMouseUp()
    {
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition-transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition*_launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _isBirdLaunched = true;
    }
    private void OnMouseDrag()
    {
         Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }
}
