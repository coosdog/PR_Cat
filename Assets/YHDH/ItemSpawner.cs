using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Temp
{
    public class ItemSpawner : MonoBehaviour
    {
        public enum STAGE_TYPE
        {
            BLACKHOLE,
            ROLLSTONE,
            MONSTER
        }

        public STAGE_TYPE stageType;

        public static Dictionary<Item.WEAPON_TYPE, AttackStrategy> weaponDic = new Dictionary<Item.WEAPON_TYPE, AttackStrategy>();
        public List<GameObject> itemPrefabs;
        public List<Transform> spawnPos;
        IEnumerator spawnCo;
        bool isSpawn;

        private void Start()
        {
            spawnCo = SpawnCo();
            weaponDic = new Dictionary<Item.WEAPON_TYPE, AttackStrategy>();
            weaponDic.Add(Item.WEAPON_TYPE.DEFAULT, new DefaultStrategy());
            weaponDic.Add(Item.WEAPON_TYPE.BAT, new BatStrategy());
            weaponDic.Add(Item.WEAPON_TYPE.SWORD, new SwordStrategy());
            weaponDic.Add(Item.WEAPON_TYPE.GUN, new GunStrategy());
            if (stageType != STAGE_TYPE.ROLLSTONE)
                StartCoroutine(spawnCo);
            else
                OnceSpawn();
            if(stageType == STAGE_TYPE.MONSTER && PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate("Monster", new Vector3(0, 1, 0), Quaternion.identity);
            }
        }             

        IEnumerator SpawnCo()
        {            
            int itemIndex = 0;
            int posIndex = 0;
            while (true)
            {
                yield return new WaitForSeconds(15f);
                PhotonNetwork.Instantiate(itemPrefabs[itemIndex].name, spawnPos[posIndex].position, transform.rotation);
                UpdateIndex(ref itemIndex, itemPrefabs.Count);
                UpdateIndex(ref posIndex, spawnPos.Count);
                //itemIndex++;
                //posIndex++;
                //if (itemIndex >= itemPrefabs.Count)
                //    itemIndex = 0;
                //if (posIndex >= spawnPos.Count)
                //    posIndex = 0;
            }
        }

        public void OnceSpawn()
        {
            for(int i = 0; i < itemPrefabs.Count; i++)
            {
                PhotonNetwork.Instantiate(itemPrefabs[i].name, spawnPos[i].position, transform.rotation);
            }
        }

        public void UpdateIndex(ref int index, int length)
        {
            index++;
            if (index >= length)
                index = 0;
        }
    }
}
