using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour {

	public int score;
	public float threshold;
	public int rowLength;

	public Text scoreLabel;

	List<Transform> blocksInGame = new List<Transform>();

	public void AddPiece( Piece newPiece ) {
		foreach( Transform block in newPiece.blocks ) {
			blocksInGame.Add( block );
		}
	}

	public void CountRowFor( Piece thisPiece ) {

		foreach( Transform thisBlock in thisPiece.blocks ) {
			if( thisBlock == null ) continue;

			List<Transform> blocksInRow = new List<Transform>();
			blocksInRow.Add( thisBlock );

			foreach( Transform otherBlock in blocksInGame ) {
				if( thisBlock == otherBlock ) continue;

				if( Mathf.Abs( thisBlock.position.y - otherBlock.position.y ) < threshold ) {
					blocksInRow.Add( otherBlock );
				}
			}

			if( blocksInRow.Count >= rowLength ) {
				foreach( Transform blockInRow in blocksInRow ) {
					blocksInGame.Remove( blockInRow );
					Destroy( blockInRow.gameObject );
				}
				score += 1;
				scoreLabel.text = score.ToString();
				
				continue;
			}
		}
	}
}
