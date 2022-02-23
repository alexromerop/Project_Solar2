using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcam : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 3.0f;

    private float _rotationY;
    private float _rotationX;

    [SerializeField]
    private Transform _target;                                  //camera pivot and follow
    [SerializeField]
    public Transform lookme;                                  //camera pivot and follow

    [SerializeField]
    private float _distanceFromTarget = 3.0f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    public Transform cameraTransform;                           //transform actual camera
    public float cameraCollisionRadius= 0.25f;


    public LayerMask collisionLayer;

  

    private void Awake()
    {
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * -1;

        _rotationY += mouseX;
        _rotationX += mouseY;

        // Apply clamping for x rotation 
        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        // Apply damping between rotation changes
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;


        Vector3 heading = this.gameObject.transform.position - _target.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;
        direction.Normalize();
        Debug.DrawRay(_target.position, direction, Color.red, 0.5f);

        RaycastHit hit;
        Ray forwardRay = new Ray(_target.position, direction);
        if (Physics.Raycast(_target.position, direction, out hit, _distanceFromTarget, collisionLayer))
        {
            float dis = hit.distance;
            //float error = distance - hit.distance;
            Debug.Log(dis);
            transform.position = _target.position - transform.forward * dis;

        }
        else
        {

            transform.position = _target.position - transform.forward * _distanceFromTarget;
        }
        //Vector3 finalp = Physics.Raycast(_target.position,direction, out hit, distance, collisionLayer) ? hit.point - direction * cameraCollisionRadius : lookme.position;



        // Substract forward vector of the GameObject to point its forward vector to the target






    }

  

}