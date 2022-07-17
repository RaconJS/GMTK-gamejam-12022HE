using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class spawner : MonoBehaviour
{
	public Tilemap tileMap;
	public GameObject enemyPrefab;
	// Start is called before the first frame update
	void Start()
	{
		int[] len={tileMap.size.x,tileMap.size.y};
		for(int y=0;y<len[1];++y){
			for(int x=0;x<len[0];++x){

			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
