
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform target;
    public Camera mainCamera;
    [Range(0.1f, 5f)]
    
    public float mouseRotateSpeed = 0.8f;
    
    
    
   
    public float slerpValue = 0.25f;
    public enum RotateMethod { Mouse, Touch };
    [Tooltip("How do you like to rotate the camera")]
    public RotateMethod rotateMethod = RotateMethod.Mouse;


    private Vector2 swipeDirection; //swipe delta vector2
    private Quaternion cameraRot; // store the quaternion after the slerp operation
    private Touch touch;
    private float distanceBetweenCameraAndTarget;

    private float minXRotAngle = -80; //min angle around x axis
    private float maxXRotAngle = 80; // max angle around x axis

    //Mouse rotation related
    private float rotX; // around x
    private float rotY; // around y
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        GameManager.Instance.OnGameSceneLoad += GameManager_OnGameSceneLoad;

    }
    // Start is called before the first frame update
    void Start()
    {
        distanceBetweenCameraAndTarget = Vector3.Distance(mainCamera.transform.position, target.position);

    }

    private void GameManager_OnGameSceneLoad(object sender, System.EventArgs e)
    {
        
        target = GameObject.Find("CameraPoint").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
            if (Input.GetMouseButton(0))
            {
                rotX += -Input.GetAxis("Mouse Y") * mouseRotateSpeed; 
                rotY += Input.GetAxis("Mouse X") * mouseRotateSpeed;
            }

            if (rotX < minXRotAngle)
            {
                rotX = minXRotAngle;
            }
            else if (rotX > maxXRotAngle)
            {
                rotX = maxXRotAngle;
            }
       

    }

    private void LateUpdate()
    {

        Vector3 dir = new Vector3(0, 0, -distanceBetweenCameraAndTarget); //assign value to the distance between the maincamera and the target

        Quaternion newQ; // value equal to the delta change of our mouse  position
       
            newQ = Quaternion.Euler(rotX, rotY, 0); //We are setting the rotation around X, Y, Z axis respectively
        cameraRot = Quaternion.Slerp(cameraRot, newQ, slerpValue);  //let cameraRot value gradually reach newQ which corresponds to our touch
        mainCamera.transform.position = target.position + cameraRot * dir;
        mainCamera.transform.LookAt(target.position);

    }

    public void SetCamPos()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        mainCamera.transform.position = new Vector3(0, 0, -distanceBetweenCameraAndTarget);
    }




}
