using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 2f, -10f);

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}