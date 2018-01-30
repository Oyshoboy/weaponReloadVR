using UnityEngine;
using NewtonVR;

public class sniperBoltController : MonoBehaviour
{

    [Header("Current Object Interactable Item script")]
    public NVRInteractableItem currentObjectItem;

    [Header("This Guns Slider relative point")]
    public GameObject relativePoint;

    [Header("This guns slider")]
    public GameObject Slider;

    [Header("Slider movement multiplier")]
    public float sliderMoveFactor = 1;

    [Header("Interpolate speed")]
    public float interpolateSpeed = 0.5f;

    string thisFreeHandName;
    public bool relativeColibrate = false;
    float sliderOffset;
    float sliderRotateOffset;
    Vector3 sliderDefPosition;
    float normalizedSliderAngle = 0;
    Animator sliderAnimator;

    void Start()
    {
        sliderDefPosition = Slider.transform.localPosition;
        sliderAnimator = Slider.GetComponent<Animator>();
    }

    public void debugLogShit(string message)
    {
        Debug.Log(message);
    }

    public void attachControl()
    {
        currentObjectItem.CanAttach = !currentObjectItem.CanAttach;
        Debug.Log("can attach:" + currentObjectItem.CanAttach);
    }

    private void FixedUpdate()
    {
        if (!relativeColibrate)
        {
            float zInterpolate = Mathf.Lerp(Slider.transform.localPosition.z, sliderDefPosition.z, interpolateSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent && thisFreeHandName != null)
        {
            if (thisFreeHandName == other.transform.parent.parent.name)
            {
                detachAndRestore();
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.parent && currentObjectItem.AttachedHand)
        {
            if (other.transform.parent.parent.name != currentObjectItem.AttachedHand.name)
            {
                thisFreeHandName = other.transform.parent.parent.name;
                SteamVR_TrackedController thisTrackedController = other.transform.parent.parent.GetComponent<SteamVR_TrackedController>();
                if(thisTrackedController != null)
                {
                    if (thisTrackedController.triggerPressed)
                    {
                        if (!relativeColibrate)
                        {
                            Debug.Log("COLIBRATING RELATIVE!");
                            relativePoint.transform.position = other.transform.position;
                            sliderOffset = sliderAnimator.GetFloat("BoltSlide");
                            sliderRotateOffset = sliderAnimator.GetFloat("BoltRotate");
                            relativeColibrate = true;
                        }
                        else if (relativeColibrate)
                        {
                            rotateBolt(other);
                        }
                    }
                    else if (!thisTrackedController.triggerPressed)
                    {
                        detachAndRestore();
                    }
                }
                
            }
        }
    }

    void rotateBolt(Collider other)
    {
        Vector3 relativeTransform = relativePoint.transform.InverseTransformPoint(other.transform.position);
        if(sliderAnimator.GetFloat("BoltSlide") <= 0)
        {
            sliderAnimator.SetFloat("BoltRotate", sliderRotateOffset + relativeTransform.y * 10);
        }

        if(sliderAnimator.GetFloat("BoltRotate") >= 1)
        {
            sliderAnimator.SetFloat("BoltSlide", -(relativeTransform.z * 10) + sliderOffset);
        }
    }

    void detachAndRestore()
    {
        relativeColibrate = false;
        normalizeSliderRot();
        normalizeSliderSlide();
        Debug.Log("Detatching!");
    }

    void normalizeSliderSlide()
    {
        if (sliderAnimator.GetFloat("BoltSlide") > 1)
        {
            //Debug.Log("Setting flot to 1 cuz it's bigger!");
            sliderAnimator.SetFloat("BoltSlide", 1);
        }
        else if (sliderAnimator.GetFloat("BoltSlide") < 0)
        {
           // Debug.Log("Setting flot to 0 cuz it's smaller!");
            sliderAnimator.SetFloat("BoltSlide", 0);
        }
    }


    void normalizeSliderRot()
    {
        if (sliderAnimator.GetFloat("BoltRotate") > 1)
        {
            //Debug.Log("Setting flot to 1 cuz it's bigger!");
            sliderAnimator.SetFloat("BoltRotate", 1);
        }
        else if (sliderAnimator.GetFloat("BoltRotate") < 0)
        {
            //Debug.Log("Setting flot to 0 cuz it's smaller!");
            sliderAnimator.SetFloat("BoltRotate", 0);
        }
    }
}
