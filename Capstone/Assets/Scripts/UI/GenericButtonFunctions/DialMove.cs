using UnityEngine;

public class DialMove : MonoBehaviour
{

    [SerializeField] Transform Dial = null;
    [SerializeField] float valueRate = 2f;
    [SerializeField] public float Value = 0.5f;

    bool active = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && active)
        {
            Dial.Rotate(0, (Input.GetAxis("Mouse X") * valueRate), 0, Space.Self);

        }
        if (!Input.GetMouseButton(0) && active)
        {
            active = false;
        }
        Value = ((Dial.rotation.y / 3.6f) * 2 + 0.5f);

        Value = Mathf.Clamp01(Value);
    }

    public void ClickDown()
    {
        active = true;
    }
}
