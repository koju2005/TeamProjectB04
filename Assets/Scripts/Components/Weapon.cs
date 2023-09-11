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
        public int Damage
        {
            get { return _damage;}
            private set { _damage = value; }
        }

        public void OnEnable()
        {
            if (gameObject.layer == LayerMask.NameToLayer("Projectile"))
            {
                GameManager.Instance.AddWeapon(gameObject);                
            }
            
        }

        public void OnDisable()
        {
            try
            {
                if (!GameManager.isApplicationExit || gameObject.layer == LayerMask.NameToLayer("Projectile"))
                {
                    if (GameManager.Instance != null)
                        GameManager.Instance.RemoveWeapon(gameObject);                
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