using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour {

    [SerializeField] bool isShowingLaser = false;

    public void FireLaser()
    {
        if (isShowingLaser)
        {
            return;
        }

        isShowingLaser = true;
        StartCoroutine(ShowLaser());
        isShowingLaser = false;
    }

    IEnumerator ShowLaser()
    {

        this.enabled = true;
        yield return new WaitForSeconds(5);
        this.enabled = false;
    }

    void ResetLaser()
    {
        this.enabled = false;
    }

}
