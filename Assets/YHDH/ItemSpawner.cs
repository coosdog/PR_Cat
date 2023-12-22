using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Temp
{
    public class ItemSpawner : MonoBehaviour
    {
        public static Dictionary<Item.WEAPON_TYPE, AttackStrategy> weaponDic = new Dictionary<Item.WEAPON_TYPE, AttackStrategy>();
        public List<GameObject> itemPrefabs;
        public List<Transform> spawnPos;
        private void Start()
        {            
            weaponDic = new Dictionary<Item.WEAPON_TYPE, AttackStrategy>();
            weaponDic.Add(Item.WEAPON_TYPE.DEFAULT, new DefaultStrategy());
            weaponDic.Add(Item.WEAPON_TYPE.BAT, new BatStrategy());
            weaponDic.Add(Item.WEAPON_TYPE.SWORD, new SwordStrategy());
            weaponDic.Add(Item.WEAPON_TYPE.GUN, new GunStrategy());
            StartCoroutine(SpawnCo());
        }             

        IEnumerator SpawnCo()
        {
            int itemIndex = 0;
            int posIndex = 0;
            while (true)
            {
                yield return new WaitForSeconds(15f);
                PhotonNetwork.Instantiate(itemPrefabs[itemIndex].name, spawnPos[posIndex].position, transform.rotation);
                itemIndex++;
                posIndex++;
                if (itemIndex >= itemPrefabs.Count)
                {
                    itemIndex = 0;
                }
                if (posIndex >= spawnPos.Count)
                {
                    posIndex = 0;
                }
            }
        }
    }
}
