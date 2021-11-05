using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRender : MonoBehaviour
{

    [SerializeField]
    [Range(1,50)]
    private int width = 10;

    [SerializeField]
    [Range(1,50)]
    private int height = 10;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform wallPrefab = null;

    [SerializeField]
    private Transform floorPrefab = null;

    [SerializeField]
    private Transform roofPrefab = null;


    [SerializeField]
    private Transform playerPrefab = null;

    [SerializeField]
    private Transform GatePrefab = null;


    [SerializeField]
    private Transform coinPrefab = null;

    [SerializeField]
    private Transform KeyPrefab = null;


    public float[] coinPos;

    public static int numofCoins = 0;

    private Transform centerofMaze;
    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);
        centerofMaze = transform;
        centerofMaze.position =new Vector3(centerofMaze.position.x/width,centerofMaze.position.y ,centerofMaze.position.z/height);

        coinPos = new float[width/2];

        for (int i = 0; i < width/2; i++)
        {
            int rand = Random.Range(0, width);

            for (int j = 0; j < width / 2; j++)
            {
                while (rand == coinPos[j])
                {
                    rand = Random.Range(0, width);
                }
            }
            coinPos[i] = rand;
        }
    

        Draw(maze);

    }
    private void Draw(WallState[,]maze)
    {
        int randGatepos = Random.Range(0, width);

        int randKeyposX = Random.Range(1, width / 2);
        int randKeyposY = Random.Range(0, width / 2);

        var floor = Instantiate(floorPrefab,centerofMaze);
        floor.localScale = new Vector3(width, 1, height);
        floor.Translate(new Vector3(-(width/2)-0.5f, 0, height/2-0.5f));

        var Roof = Instantiate(roofPrefab, centerofMaze);
        Roof.localScale = new Vector3(width, 1, height);
        Roof.Translate(new Vector3(-(width / 2) - 0.5f, 0.9f, height / 2 - 0.5f));
       

        for (int i = 0; i < width; i++)
        {
            int p = Random.Range(0, width / 2);

            for (int j = 0; j < height; j++)
            {
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j);

                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0.5f, size/2);
                    topWall.eulerAngles = new Vector3(0, 90, 0);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size/2, 0.5f, 0);           
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                }
                if(i== width -1)
                {
                    if (cell.HasFlag(WallState.RIGTH))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(size/2, 0.5f, 0);                     
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                    }
                }
                if (j == 0)
                {
                    if(i == randGatepos && cell.HasFlag(WallState.DOWN))
                    {
                        var downWall = Instantiate(GatePrefab, transform) as Transform;
                        downWall.position = position + new Vector3(0, 0.4f, -size / 2);
                       

                    }
                    else if (cell.HasFlag(WallState.DOWN))
                    {
                        var downWall = Instantiate(wallPrefab, transform) as Transform;
                        downWall.position = position + new Vector3(0, 0.5f, -size/2);
                        downWall.eulerAngles = new Vector3(0, 90, 0);
                        downWall.localScale = new Vector3(size, downWall.localScale.y, downWall.localScale.z);
                    }
                }
                if(i ==0 && j == 0)
                {

                    var player = Instantiate(playerPrefab, transform) as Transform;
                    player.position = position + new Vector3(0,0.5f,0);

                }
            
                for (int c = 0; c < width / 2; c++)
                {
                    
                    if (i == coinPos[c] && j == coinPos[p]){

                            var Coin = Instantiate(coinPrefab, transform) as Transform;
                            Coin.position = position + new Vector3(0, 0.3f, 0);
                        numofCoins++;
                    }

                }

                if(i == randKeyposX && j == randKeyposY)
                {

                    var Key = Instantiate(KeyPrefab, transform) as Transform;
                    Key.position = position + new Vector3(0, 0.5f, 0);
                    
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
