using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class spawner : MonoBehaviour
{
	public Tilemap tileMap;
	public GameObject spawnObj;
	public float probability=0.02f;
	public float probScale=0.1f;
	// Start is called before the first frame update
	void Start()
	{
		int[] len={tileMap.size.x,tileMap.size.y};
		float tilesLeft;
		for(int y=0;y<len[1];++y){
			for(int x=0;x<len[0];++x){
				if(tileMap.HasTile(new Vector3((float)x,(float)y,0))){
					float val=Random.Range(0f,1f);
					if(val**(1+y*probScale)<probability){
						Instantiate(spawnObj,Vector2((float)x+0.5f,(float)y+1f));
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
