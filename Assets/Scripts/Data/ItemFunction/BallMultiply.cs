using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Data.ItemFunction
{
    [CreateAssetMenu(fileName = "BallMultiply", menuName = "Scriptable/ItemInteraction/BallMultiply",order = 2)]
    public class BallMultiply : ItemFunction
    {
        public override void Apply(GameObject Player, int effectValue)
        {
            var weapons = GameManager.Instance._currentWeapons.ToList();
            foreach (var weaponObj in weapons)
            {
                for (int i = 0; i < effectValue; i++)
                {
                    GameObject newWeapon = GameManager.Instance._prefabsPoolManager.Get("Weapon");
                    AutoMover mover = newWeapon.GetComponent<AutoMover>();
                    Rigidbody2D rigid = weaponObj.GetComponent<Rigidbody2D>();
                    
                    newWeapon.transform.position = weaponObj.transform.position;
                    newWeapon.SetActive(true);
                    mover.accelate = -rigid.velocity;
                }
            }
        }
    }
}