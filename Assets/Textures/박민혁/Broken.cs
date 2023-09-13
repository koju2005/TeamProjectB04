using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Broken : MonoBehaviour
{

    public NShooter change;
    public Text a;
    // Start is called before the first frame update

    private void Start()
    {
        change = transform.GetComponent<NShooter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Health>().GetHealth() == 1) 
        {
            changeW();
        }
    }
    public void changeW()
    {
        string weaponname = this.name;
        Debug.Log(weaponname);
        change.AttackPrefabsName =new[] { weaponname };
    }
}
