using System.Linq;
using UnityEngine;

public class PlayerConstair : MonoBehaviour
{
    [SerializeField] private float _fieldLength;
    [SerializeField] private float _fieldWidth;

    public float FieldLength { get => _fieldLength; }
    public float FieldWidth { get => _fieldWidth; }

    private void LateUpdate()
    {
        var offset = transform.position;
        float newX = transform.position.x;
        float newZ = transform.position.z;

        if (Mathf.Abs(offset.x) > FieldLength)
        {
            if (offset.x < 0)
                newX = -FieldLength;
            else if (offset.x > 0)
                newX = FieldLength;
            else
                newX = 0;
        }

        if(Mathf.Abs(offset.z) > FieldWidth)
        {
            if (offset.z < 0)
                newZ =-FieldWidth;
            else if (offset.z > 0)
                newZ =FieldWidth;
            else
                newZ = 0;
        }
            
        transform.position = new Vector3 (newX, transform.position.y, newZ);
    }
}
