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
            DeactivateBlock();
        }

        Block SpawnBlock(Vector3 pos)
        {
            //Instantiate our clone
            GameObject clone = Instantiate(blockPrefab);
            clone.transform.position = pos; //Set the position of the clone
            Block currentBlock = clone.GetComponent<Block>();
            GetAdjacentMineCountAt(currentBlock);
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
            //Loop throguh the elements and have each axis between -1 and 1
            for (int x = -1; x <= 1; x++)
            {
                //calculate the adjacent element's index on the x axis
                int desiredX = b.x + x;

                // IF desired x is within range of the blocks array
                if (desiredX < blocks.Length)
                {
                    //If the element at index is a mine
                    if (b.isMine)
                    {
                        //Increment count by 1
                        count++;
                    }

                }

                for (int y = -1; y <= 1; y++)
                {
                    //Calculate the adjacent element's index on the y axis
                    int desiredY = b.y + y;

                    if (desiredY < blocks.Length)
                    {
                        //If the element at index is a mine
                        if (b.isMine)
                        {
                            //Increment count by 1
                            count++;
                        }
                    }

                    for (int z = -1; z <= 1; z++)
                    {
                        int desiredZ = b.z + z;

                        if(desiredZ < blocks.Length)
                        {
                            //If the element at index is a mine
                            if (b.isMine)
                            {
                                //Increment count by 1
                                count++;
                            }
                        }
                    }
                }
                
            }
            return count;
        }

        public void DeactivateBlock()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Input.GetMouseButtonDown(0))
            {
                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.tag == "Block")
                    {
                        hit.collider.gameObject.SetActive(false);
                    }
                }
            }
        }

    }
}