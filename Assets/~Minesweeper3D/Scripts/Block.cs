using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper3D
{
    [RequireComponent(typeof(Renderer))]
    public class Block : MonoBehaviour
    {
        public int x, y, z;
        public bool isMine = false;
        [Header("References")]
        public Color[] textColors;
        public TextMesh textElement;
        public Transform mine;


        private bool isRevealed = false;
        public bool isFlagged = false;
        private Renderer rend;

        public Color startColor;
        

        // Use this for initialization
        void Awake()
        {
            rend = GetComponent<Renderer>();
            startColor = rend.material.color;
        }
        void Start()
        {
            // Detach the text element from the block
            textElement.transform.SetParent(null);
            // Randomly decide if it's a mine or not
            isMine = Random.value < Grid.Instance.mineChance;
        }
        // Update is called once per frame
        void UpdateText(int adjacentMines)
        {
            // Are there adjacent mines?
            if(adjacentMines >0)
            {
                //Set the text element to the amount of mines
                textElement.text = adjacentMines.ToString();

                // Check if the adjacent mines are within textColor's array
                if(adjacentMines >=0 && adjacentMines < textColors.Length)
                {
                    //Set the text colour what was present
                    textElement.color = textColors[adjacentMines];
                }
            }
        }
        public void Reveal(int adjacentMines = 0)
        {
            //Flags the block as revealed
            isRevealed = true;
            //Checks if block is a mine
            if(isMine)
            {
                //Activate the references mine
                mine.gameObject.SetActive(true);
                //Detach mine from Children
                mine.SetParent(null);
            }
            else
            {
                //Update the text to display adjacent mines
                UpdateText(adjacentMines);
            }
            //Deactivates the Mine
            gameObject.SetActive(false); 
        }
    }
}