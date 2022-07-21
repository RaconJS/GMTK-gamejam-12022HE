using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class spawner : MonoBehaviour
{
	public Tilemap tileMap;
	public GameObject spawnObj;
	public float probability=0.04f;
	public float probScale=0.02f;
	private GameObject parentObject;
	private GameObject playerObject;
	public int chunks = 16;
	private int[] len;
	private int currentChunk = 1;

	// Start is called before the first frame update
	void Start()
	{
		parentObject = GameObject.Find("enermies1");
		playerObject = GameObject.Find("player");
		len = new int[] { tileMap.size.x / chunks, tileMap.size.y };
		/*float tilesLeft;
		for(int y=0;y<len[1];++y){
			for(int x=0;x<len[0];++x){
				if(tileMap.HasTile(new Vector3Int(x,y,0))){
					float val=Random.Range(0f,1f);
					if(Mathf.Pow(val,(1+x*probScale))<probability){
						GameObject tempEnemy = Instantiate(spawnObj,new Vector3((float)x+0.5f,(float)y+1f),transform.rotation);
						tempEnemy.transform.parent = parentObject.transform;
					}
				}
			}
		}*/
		generateNextSection();
	}

	// Update is called once per frame
	void Update()
	{
        if (playerObject.transform.position.x > (tileMap.size.x / 16) * currentChunk ||
			playerObject.transform.position.x < (tileMap.size.x / 16) * (currentChunk - 1))
        {
			;
        }
	}

	public void generateNextSection()
    {

		len = new int[] { tileMap.size.x / chunks, tileMap.size.y };
		float tilesLeft;
		for (int y = 0; y < len[1]; ++y)
		{
			for (int x = 0; x < len[0]; ++x)
			{
				if (tileMap.HasTile(new Vector3Int(x, y, 0)) && !tileMap.HasTile(new Vector3Int(x, y+1, 0)))
				{
					float val = Random.Range(0f, 1f);
					if (Mathf.Pow(val, (1 + x * probScale)) < probability)
					{
						GameObject tempEnemy = Instantiate(spawnObj, new Vector3((float)x + 0.5f, (float)y + 1f), transform.rotation);
						tempEnemy.transform.parent = parentObject.transform;
					}
				}
			}
		}

	}

}
