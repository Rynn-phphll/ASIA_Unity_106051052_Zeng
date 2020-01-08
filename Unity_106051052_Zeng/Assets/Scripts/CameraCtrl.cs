using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform cam, target;

    public float speed = 3;

    private void Update()
    {
        Vector3 pos = Vector3.Lerp(cam.position, target.position, 0.5f * Time.deltaTime * speed);

        cam.position = pos;
    }
}
