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
        [Range(0, 1)]
        public float mineChance;
        

        // Multi-Dimensional Array storing the blocks(in this case 3d)
        private Block[,,] blocks;

        //Create ray that follows our mouse position
        Ray ray;
        //Create a variable to collect the information of what our ray hits
        RaycastHit hit;

        void Awake()
        {
            if (Instance == null)
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
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //IF MiddleMouse Button
            if(Input.GetMouseButtonUp(2))
            {
                //If Raycast out from the camera hits something

                if (Physics.Raycast(ray, out hit))
                {
                    //Get hit objects block component
                    Block selectedBlock = hit.transform.gameObject.GetComponent<Block>();
                    //Call MarkFlag() and pass the selectedBlock
                    MarkFlag(selectedBlock);
                }
            }



            //If Left Mouse Button is up
            if (Input.GetMouseButtonUp(0))
            {
                //If Raycast out from the camera hits something
                
                if(Physics.Raycast(ray, out hit))
                {
                    //Get hit objects block component
                    Block selectedBlock = hit.transform.gameObject.GetComponent<Block>();
                    //Call selectBlock() and pass in the hit block
                    SelectBlock(selectedBlock);
                }
            }
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
            //Loop throguh the elements and have each axis between -1 and 1
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        //Calculate the adjacent element's index
                        int desiredX = b.x + x;
                        int desiredY = b.y + y;
                        int desiredZ = b.z + z;
                        //Coordinates in range?
                        if (desiredX >= 0 && desiredY >= 0 && desiredZ >= 0 && desiredX < width && desiredY < height && desiredZ < depth)
                        {
                            //Then check for a mine
                            Block currentBlock = blocks[desiredX, desiredY, desiredZ];
                            if (currentBlock.isMine)
                            {
                                //Increment the count
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }
        //public void DeactivateBlock()
        //{
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    //IF we left-Click
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        //Cast our ray
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            //If we hit a Block
        //            if (hit.collider.tag == "Block")
        //            {
        //                //Deactivate it
        //                hit.collider.gameObject.SetActive(false);

        //                //OR DESTROY IT??
        //                //Destroy(hit.collider.gameObject);
        //            }
        //        }
        //    }
        //}
        public void FFUncover(int x, int y, int z, bool[,,] visited)
        {
            //Coordinates in Range?
            if (x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth)
            {
                //Have we visited?
                if (visited[x, y, z])
                    return;
                //Uncover the element
                Block block = blocks[x, y, z];
                int adjacentMines = GetAdjacentMineCountAt(block);
                block.Reveal(adjacentMines);

                //If there are adjacent mines
                if (adjacentMines > 0)
                    return; //no more work necessary

                //Set a flag to let us know this block has been visited
                visited[x, y, z] = true;

                FFUncover(x - 1, y, z - 1, visited);
                FFUncover(x + 1, y, z + 1, visited);
                FFUncover(x, y - 1, z - 1, visited);
                FFUncover(x, y + 1, z + 1, visited);
                FFUncover(x, y, z - 1, visited);
                FFUncover(x, y, z +1, visited);
                FFUncover(x - 1, y, z, visited);
                FFUncover(x + 1, y, z, visited);
                FFUncover(x, y - 1, z, visited);
                FFUncover(x, y + 1, z, visited);
                FFUncover(x - 1, y, z + 1 , visited);
                FFUncover(x + 1, y, z + 1 ,visited);
                FFUncover(x, y - 1, z + 1, visited);
                FFUncover(x, y + 1, z + 1, visited);
            }
        }
        public void UncoverMines()
        {
            //Loop through all elements in an array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //Get currentBlock at index
                        Block currentBlock = blocks[x, y, z];
                        // If the currentBlock is a mine
                        if (currentBlock.isMine)
                        {
                            //reveal the mine
                            currentBlock.Reveal();
                        }
                    }
                }
            }
        }
        //Takes in the block selected by the user and reveals it 
        public void SelectBlock(Block selecetdBlock)
        {
            //reveal the selected Block
            selecetdBlock.Reveal();
            //IF the selected block is a mine
            if(selecetdBlock.isMine)
            {
                //Uncover all other mines
                UncoverMines();
            }
            //Else if there are no adjacent mines
            else
            {
                //Perform the flood fill algorithm to reveal all empty blocks
                FFUncover(selecetdBlock.x, selecetdBlock.y, selecetdBlock.z, new bool[width,height,depth]);
            }
        }
        public void MarkFlag(Block selectedBlock)
        {
            Renderer rend = selectedBlock.GetComponent<Renderer>();
            
            Color endColor = Color.red;
            
            if(rend.material.color == selectedBlock.startColor)
            {
                rend.material.color = endColor;
                selectedBlock.isFlagged = true;
            } 
            else
            {
                rend.material.color = selectedBlock.startColor;
                selectedBlock.isFlagged = false;
            }
        }
    }
}