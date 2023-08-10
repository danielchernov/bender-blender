using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    Animator buttonAnimator;

    public void SetBoolForAnimator(bool booleanToGive)
    {
        if (GetComponent<Button>().interactable)
        {
            buttonAnimator = GetComponent<Animator>();
            buttonAnimator.SetBool("isHovered", booleanToGive);
        }
    }
}
