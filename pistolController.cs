using UnityEngine;
using NewtonVR;

public class pistolController : MonoBehaviour {

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

    string thisFreeHandName;
    public bool relativeColibrate = false;
    float sliderOffset = 0;
    Vector3 sliderDefPosition;

    void Start()
    {
        sliderDefPosition = Slider.transform.localPosition;
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

            Slider.transform.localPosition = new Vector3(Slider.transform.localPosition.x, Slider.transform.localPosition.y, zInterpolate);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent && thisFreeHandName != null)
        {
            if (thisFreeHandName == other.transform.parent.parent.name)
            {
                relativeColibrate = false;
                Debug.Log("Detatching!");
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.transform.parent && currentObjectItem.AttachedHand)
        {
            if(other.transform.parent.parent.name != currentObjectItem.AttachedHand.name)
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
                            sliderOffset = Slider.transform.localPosition.z;
                            relativeColibrate = true;
                        }
                        else if (relativeColibrate)
                        {
                            Vector3 relativeTransform = relativePoint.transform.InverseTransformPoint(other.transform.position);
                            //  Debug.Log("Can Move Slider, Z position: " + relativeTransform.z);
                            if (relativeTransform.z > positiveLimit || relativeTransform.z < negativeLimit)
                            {
                                Debug.Log("Limit!");
                            }
                            else
                            {
                                Slider.transform.localPosition = new Vector3(Slider.transform.localPosition.x, Slider.transform.localPosition.y, sliderOffset + (relativeTransform.z * sliderMoveFactor));
                            }
                        }
                    }
                    else if (!thisTrackedController.triggerPressed)
                    {
                        relativeColibrate = false;
                    }
                }
            }
        }
    }
}
