using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public String Level;

    [System.Serializable]
    public class Pair
    {
        public String Name;
        public GameObject Prefab;
    }

    [SerializeField]
    public List<Pair> Tiles = new List<Pair>();

    // Use this for initialization
    void Start()
    {
        try
        {
            StreamReader LevelStream = new StreamReader(this.Level);
            String Line;
            List<String[]> Rows = new List<String[]>();

            while ((Line = LevelStream.ReadLine()) != null)
            {
                String[] Blocks;

                Blocks = Line.Split(new String[] { ",", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

                Rows.Add(Blocks);
            }

            Vector3 BlockOffset = new Vector3(0, Rows.Count);

            foreach (String[] Row in Rows)
            {
                foreach (String Block in Row)
                {
                    Pair TileEntry = Tiles.Find(Tile => Tile.Name.Equals(Block));

                    if (TileEntry != null)
                    {
                        Instantiate(TileEntry.Prefab, BlockOffset, Quaternion.identity);
                    }
                    BlockOffset.x++;
                }

                BlockOffset.x = 0;
                BlockOffset.y--;
            }
        }
        catch (Exception e)
        {
            Debug.Log("The file [ " + this.Level + " ] could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}
