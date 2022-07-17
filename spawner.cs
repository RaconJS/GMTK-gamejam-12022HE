using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class spawner : MonoBehaviour
{
	public Tilemap tileMap;
	public GameObject spawnObj;
	public float 
	// Start is called before the first frame update
	void Start()
	{
		int[] len={tileMap.size.x,tileMap.size.y};
		float tilesLeft;
		for(int y=0;y<len[1];++y){
			for(int x=0;x<len[0];++x){
				if(tileMap.HasTile(new Vector3(x,y,0))){
					float val=Random.Range(0f,1f);
					if(val>0)
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
