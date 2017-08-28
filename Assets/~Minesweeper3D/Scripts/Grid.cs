using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper3D
{
    public class Grid : MonoBehaviour
    {
        public static Grid Instance;
        public GameObject blockPrefab;
        //The grid's dimensions
        public int width = 10;
        public int height = 10;
        public int depth = 10;
        public float spacing = 1.2f; //How much space between each block
        [Range(0,1)]
        public float mineChance;

        // Multi-Dimensional Array storing the blocks(in this case 3d)
        private Block[,,] blocks;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }
        void Start()
        {
            GenerateBlocks();
        }

        // Update is called once per frame
        void Update()
        {

        }

        Block SpawnBlock(Vector3 pos)
        {
            //Instantiate our clone
            GameObject clone = Instantiate(blockPrefab);
            clone.transform.position = pos; //Set the position of the clone
            Block currentBlock = clone.GetComponent<Block>();
            return currentBlock;
        }

        void GenerateBlocks()
        {
            //Create 3D array to store all the blocks
            blocks = new Block[width, height, depth];

            //Loop through the x, y and z axis for the array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        // Calculate half size array using array dimensions 
                        Vector3 halfSize = new Vector3(width / 2, height / 2, depth / 2);
                        //Offset by half to remain centered
                        halfSize -= new Vector3(0.5f, 0.5f, 0.5f);
                        //Create position for element to pivot around Grid zero
                        Vector3 pos = new Vector3(x - halfSize.x, y - halfSize.y, z - halfSize.z);
                        // And apply spacing
                        pos *= spacing;

                        //Spawn the block at that position
                        Block block = SpawnBlock(pos);
                        //Atach the block to the Grid as a child
                        block.transform.SetParent(transform);
                        //Store array coordinate inside the block itself
                        block.x = x;
                        block.y = y;
                        block.z = z;
                        //Store block in the array at coordinates
                        blocks[x, y, z] = block;
                    }
                }
            }
        }
        public int GetAdjacentMineCountAt(Block b)
        {
            int count = 0;
            return b;
        }
    }
}