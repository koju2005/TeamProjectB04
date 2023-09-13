using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Broken : MonoBehaviour
{

    public Shooter change;
    bool one = true;
    // Start is called before the first frame update

    private void Start()
    {
        change = transform.GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Health>().GetHealth() == 1 && one) 
        {
            changeW();
        }
    }
    public void changeW()
    {

        string weaponname = this.name;
        Debug.Log(weaponname);

        change.AttackPrefabsName =new[] { weaponname };
        change.AttackDelay = new[] {3f};

        
        one=false;
    }

}
