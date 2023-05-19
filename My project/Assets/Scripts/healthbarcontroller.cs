using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbarcontroller : MonoBehaviour
{
    

    public Slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void maxhealth(int mhp)
    {
            healthbar.maxValue = mhp;
            healthbar.value = mhp;
    }

    public void chealth(int hp)
    {
            healthbar.value = hp;
    }
}
