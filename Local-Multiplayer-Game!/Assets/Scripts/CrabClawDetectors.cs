using UnityEngine;

public class CrabClawDetector : MonoBehaviour
{
    [SerializeField] LayerMask grabbableLayers;
    [SerializeField] string clawName;
    [SerializeField] MultiplePlayerKeyboard multiplePlayerKeyboard;

    private void OnTriggerEnter(Collider other)
    {
        
        int objectLayerMask = (1 << other.gameObject.layer);

        if ((objectLayerMask & grabbableLayers) != 0 )
        {
            if (clawName == "p1ClawR" && multiplePlayerKeyboard.p1GrabbableObjectR == null) { multiplePlayerKeyboard.p1GrabbableObjectR = other.gameObject; }
            else if (clawName == "p1ClawL" && multiplePlayerKeyboard.p1GrabbableObjectL == null) { multiplePlayerKeyboard.p1GrabbableObjectL = other.gameObject; }
            else if (clawName == "p2ClawR" && multiplePlayerKeyboard.p2GrabbableObjectR == null) { multiplePlayerKeyboard.p2GrabbableObjectR = other.gameObject; }
            else if (clawName == "p2ClawL" && multiplePlayerKeyboard.p2GrabbableObjectL == null) { multiplePlayerKeyboard.p2GrabbableObjectL = other.gameObject; }

        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (clawName == "p1ClawR")
        {
            if (other.gameObject != multiplePlayerKeyboard.p1GrabbableObjectR) { }
            int objectLayerMask = (1 << other.gameObject.layer);

            if ((objectLayerMask & grabbableLayers) != 0)
            {
                multiplePlayerKeyboard.p1GrabbableObjectR = null;
                

            }
        }

        else if (clawName == "p1ClawL")
        {
            if (other.gameObject != multiplePlayerKeyboard.p1GrabbableObjectL) { }
            int objectLayerMask = (1 << other.gameObject.layer);

            if ((objectLayerMask & grabbableLayers) != 0)
            {             
                multiplePlayerKeyboard.p1GrabbableObjectL = null; 
            }
        }

        else if (clawName == "p2ClawR")
        {
            if (other.gameObject != multiplePlayerKeyboard.p2GrabbableObjectR) { }
            int objectLayerMask = (1 << other.gameObject.layer);

            if ((objectLayerMask & grabbableLayers) != 0)
            {                
                multiplePlayerKeyboard.p2GrabbableObjectR = null;               
            }
        }

        else if (clawName == "p2ClawL")
        {
            if (other.gameObject != multiplePlayerKeyboard.p2GrabbableObjectL) { }
            int objectLayerMask = (1 << other.gameObject.layer);

            if ((objectLayerMask & grabbableLayers) != 0)
            {               
            multiplePlayerKeyboard.p2GrabbableObjectL = null;
            }
        }
     

    }
}
