using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;


namespace DefaultNamespace.Data
{
    [RequireComponent(typeof(Collisionable),typeof(AutoMover),typeof(PooledObject))]
    public class Weapon: MonoBehaviour
    {
        [SerializeField] private int _damage;
        public string owner="Player";
        public int Damage
        {
            get { return _damage;}
            private set { _damage = value; }
        }

        public void OnEnable()
        {
            if (gameObject.layer == LayerMask.NameToLayer("UserWeapon"))
            {
                GameManager.Instance.AddWeapon(gameObject);
                //GameManager.Instance.ballCount += 1;
                //Debug.Log(GameManager.Instance.ballCount);
            }
        }

        public void OnDisable()
        {
            try
            {
                if (!GameManager.isApplicationExit || gameObject.layer == LayerMask.NameToLayer("UserWeapon"))
                {
                    if (GameManager.Instance != null) 
                    {
                        GameManager.Instance.RemoveWeapon(gameObject);
                        if (owner == "player")
                        {
                            GameManager.Instance.ballCount -= 1;
                        }
                    }
                    else
                        Destroy(gameObject);                
                }
            }
            catch(Exception e)
            {
                
            }
           
        }
    }
}