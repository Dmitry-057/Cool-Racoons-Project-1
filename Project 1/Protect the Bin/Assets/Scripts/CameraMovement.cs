using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 0;

    private float xMax;
    private float yMin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            //moves camera upwards when 'W' is pressed
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //moves camera upwards when 'A' is pressed
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //moves camera upwards when 'S' is pressed
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //moves camera upwards when 'D' is pressed
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        //this command actually physically limits camera movement based on xMax and yMax
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -10);
    }

    public void SetLimits(Vector3 maxTile)
    {
        //right bottom of the screen translated into cordinate
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        //camera boundary on x axis
        xMax = maxTile.x - wp.x;

        //camera boundary on y axis
        yMin = maxTile.y - wp.y;
    }
}
