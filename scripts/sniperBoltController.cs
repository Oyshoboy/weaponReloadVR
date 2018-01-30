using UnityEngine;
using NewtonVR;

public class sniperBoltController : MonoBehaviour
{

    [Header("Current Object Interactable Item script(auto)")]
    public NVRInteractableItem currentObjectItem;

    [Header("This Guns Slider relative point")]
    public GameObject relativePoint;

    [Header("This guns slider")]
    public GameObject Slider;

    [Header("Slider movement multiplier")]
    public float sliderMoveFactor = 1;

    [Header("Interpolate speed and sliders limits")]
    public float interpolateSpeed = 0.5f;
    public float positiveLimit = 0;
    public float negativeLimit = -0.1f;
    public float positiveRotLimit = 1;
    public float negativeRotLimit = 0;

    public bool boltSlide = false;
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

           // Slider.transform.localPosition = new Vector3(Slider.transform.localPosition.x, Slider.transform.localPosition.y, zInterpolate);
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
                        //Debug.Log(relativeTransform.z);
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
                            //MoveBolt(other);
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
        //Debug.Log(relativeTransform.y * 100 + " and last float is  " + sliderAnimator.GetFloat("BoltRotate"));

        //        sliderRotateOffset.z + sliderOffset.y + (relativeTransform.y * 800));
        if(sliderAnimator.GetFloat("BoltSlide") <= 0)
        {
            sliderAnimator.SetFloat("BoltRotate", sliderRotateOffset + relativeTransform.y * 10);
        }

        if(sliderAnimator.GetFloat("BoltRotate") >= 1)
        {
            sliderAnimator.SetFloat("BoltSlide", -(relativeTransform.z * 10) + sliderOffset);
        }

        //if (!boltSlide) {
        //    sliderAnimator.SetFloat("BoltRotate", sliderRotateOffset + relativeTransform.y * 10);
        //    if (sliderAnimator.GetFloat("BoltRotate") >= 1)
        //    {
        //        boltSlide = true; // CHECK IF NOW WE NEED TO SLIDE
        //    }
       // } else if (boltSlide)
       // {   
       //     sliderAnimator.SetFloat("BoltSlide", -(relativeTransform.z * 10)+ sliderOffset);
       // }
    }

    void detachAndRestore()
    {
        relativeColibrate = false;
        normalizeSliderRot();
        normalizeSliderSlide();
        normalizeAnimatorFloats();
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

    void normalizeAnimatorFloats()
    {
        if (sliderAnimator.GetFloat("BoltRotate") <= sliderAnimator.GetFloat("BoltSlide"))
        {
         //   sliderAnimator.SetFloat("BoltSlide", 0);
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
