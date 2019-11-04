using UnityEngine;

public class SliderMove : MonoBehaviour
{
    [SerializeField] Transform Slider = null;
    [SerializeField] public bool leftRight = true;

    public float Value = 0.5f;

    bool active = false;

    void Update()
    {

        Value = (Slider.localPosition.x / 1.3f) / 2 + 0.5f;
        // Debug.Log("Value = " +SliderValue);
    }

    public void ClickDown()
    {
        PlayerMove pm = FindObjectOfType<PlayerMove>();
        Ray ray = new Ray(pm.playerCamera.transform.position, pm.playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pm.SightDistance))
        {
            Slider.position = new Vector3(hit.point.x, Slider.position.y, Slider.position.z);

        }
        Slider.localPosition = new Vector3(Mathf.Clamp(Slider.localPosition.x, -1.3f, 1.3f), Slider.localPosition.y, Slider.localPosition.z);

    }
}
