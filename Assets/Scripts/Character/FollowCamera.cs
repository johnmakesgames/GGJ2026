using Unity.Mathematics;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    float cameraSpeed;

    [SerializeField]
    AnimationCurve cameraSpeedCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 cameraDir = Vector3.Normalize(target.transform.position - this.transform.position);
        //float diff = math.abs(Vector3.Dot(this.transform.forward, cameraDir));

        ////if (diff > 0)
        //{
        //    Vector3 targetAdjustedZ = new Vector3(target.transform.position.x, target.transform.position.z, target.transform.position.y);
        //    //targetAdjustedZ.z = 0;

        //    Vector3 selfAdjustedZ = this.transform.position;
        //    selfAdjustedZ.z = target.transform.position.z;

        //    Vector3 dir = targetAdjustedZ - selfAdjustedZ;

        //    this.transform.position += dir.normalized * cameraSpeedCurve.Evaluate((1 - dir.magnitude) * 10.0f) * cameraSpeed * Time.deltaTime;
        //}

        //this.transform.position = new Vector3(target.transform.position.x, 12.65f + target.transform.position.z, this.transform.position.z);
    }
}
