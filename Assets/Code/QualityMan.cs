using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityMan : MonoBehaviour
{
    // Start is called before the first frame update

    public void GoodBtn()
    {
        QualitySettings.SetQualityLevel(1, true);
    } 
    public void BadBtn()
    {
        QualitySettings.SetQualityLevel(0, true);
    }
    
}
