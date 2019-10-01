using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransistion : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 1.0f)] public float m_progress = 0.0f;
    [SerializeField] public Canvas loadingCanvas = null;
    [SerializeField] public Slider loadingSlider = null;
    [SerializeField] public string animationName = "";
    [SerializeField] public Animator animator = null;

    private void OnValidate()
    {
        UpdateValue(m_progress);
    }

    public void UpdateValue(float progress)
    {
        m_progress = progress;
        if (loadingSlider != null)
        {
            loadingSlider.value = progress;
        }

        if (animator != null && animator.isActiveAndEnabled)
        {
            animator.speed = 0.0001f;
            animator.Play(animationName, -1, progress);
            animator.Update(Time.deltaTime);
        }
    }
}


