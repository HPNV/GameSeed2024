using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NewBehaviourScript : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera1;
    public CinemachineVirtualCamera virtualCamera2;

    public void SwitchCinemachineCameras()
    {
        if (virtualCamera1 != null && virtualCamera2 != null)
        {
            virtualCamera1.Priority = 0;
            virtualCamera2.Priority = 10;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchCinemachineCameraAfterDelay(1f));
    }

    IEnumerator SwitchCinemachineCameraAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Switch the Cinemachine virtual cameras by adjusting their priorities
        if (virtualCamera1 != null && virtualCamera2 != null)
        {
            virtualCamera1.Priority = 0;
            virtualCamera2.Priority = 10;
        }
    }
}
