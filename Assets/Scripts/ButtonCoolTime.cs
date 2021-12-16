using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCoolTime : MonoBehaviour
{
    public float attackcool = 0.5f;
    public float leftattackcool = 0.5f;
    public UnityEngine.UI.Button btn;
    public bool disableonstart = true;

    // Start is called before the first frame update
    void Start()
    {
        if (disableonstart) resetcooltime();
    }

    // Update is called once per frame
    void Update()
    {
        if(leftattackcool > 0)
        {
            leftattackcool -= Time.deltaTime;
            if(leftattackcool < 0)
            {
                leftattackcool = 0;
                if (btn) btn.enabled = true;
            }
        }
    }

    public bool checkcooltime()
    {
        if (leftattackcool > 0) return false;
        else return true;
    }

    public void resetcooltime()
    {
        leftattackcool = attackcool;
        if (btn) btn.enabled = false;
    }
}
