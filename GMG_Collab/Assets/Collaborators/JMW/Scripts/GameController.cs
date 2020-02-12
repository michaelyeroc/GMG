using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jmw
{
    public class GameController : MonoBehaviour
    {
        public GameObject[] BlockPrefabs;
        private void Start()
        {
            GenerateBlocks();
        }

        void Update()
        {
        }

        private void GenerateBlocks()
        {
            var world = new GameObject();

            var rings = 3;
            var ringSpacing = 2;

            var blocksPerRing = 2;
            
            for(int ring=1; ring<=rings; ring++)
            {
                var numBlocks = blocksPerRing*ring;

                for(int block=0; block<numBlocks; block++)
                {
                    var blockObj = CreateBlock();
                    var angle = 2f*Mathf.PI*((float)block / numBlocks);

                    blockObj.transform.position = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * ring*ringSpacing;
                    blockObj.transform.rotation = Quaternion.Euler(0, 0, -Mathf.Rad2Deg * angle);

                    blockObj.transform.SetParent(world.transform);
                }

            }
        }

        private GameObject CreateBlock()
        {
            return Instantiate(BlockPrefabs[UnityEngine.Random.Range(0, BlockPrefabs.Length)]);
        }
    }
}
