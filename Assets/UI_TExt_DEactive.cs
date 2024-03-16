using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TExt_DEactive : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI DeactivateText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Invoke("Deactivate", 5f);
    }

    private void Deactivate()
    {
        DeactivateText.enabled = false;
    }
}
