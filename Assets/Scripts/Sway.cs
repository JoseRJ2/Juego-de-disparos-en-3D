using UnityEngine;

public class Sway : MonoBehaviour
{
    private Quaternion OriginLocalRotation;
    
    void Start()
    {
        OriginLocalRotation = transform.localRotation;
    }

    void Update()
    {
        updateSway();
    }

    private void updateSway(){
        float t_xLookInput = Input.GetAxis("Mouse X");
        float t_yLookInput = Input.GetAxis("Mouse Y");

        Quaternion t_xAngleAdjustment = Quaternion.AngleAxis(-t_xLookInput * 1.45f, Vector3.up);
        Quaternion t_yAngleAdjustment = Quaternion.AngleAxis(t_yLookInput * 1.45f, Vector3.right);
        Quaternion t_targetRotation = OriginLocalRotation * t_xAngleAdjustment * t_yAngleAdjustment;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, t_targetRotation, Time.deltaTime * 8f);
    }

}
