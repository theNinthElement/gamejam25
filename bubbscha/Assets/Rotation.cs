using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float speed = 60f;
    [SerializeField] private Vector3 axis = Vector3.left;
    void Update()
    {
        var delta = Time.deltaTime * speed;
        transform.Rotate(axis, delta);
    }
}
