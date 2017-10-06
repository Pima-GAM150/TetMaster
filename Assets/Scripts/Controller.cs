using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	const float blockSize = 1.51f;

	public Tracker tracker;

	public float slamSpeed = 1f;

  public Transform leftWall;
	public Transform rightWall;

	[HideInInspector] public Piece currentPiece;

	public void TakeControlOf( Piece pieceToControl ) {
		currentPiece = pieceToControl;
	}

	void Update() {
		float horiz = 0f;
		if( Input.GetButtonDown("Left") ) horiz = -1f;
		if( Input.GetButtonDown("Right") ) horiz = 1f;

		if( horiz != 0f ) {

			bool canMoveThatWay = true;
			foreach( Transform block in currentPiece.blocks ) {
				if( block.position.x + horiz * 2f < leftWall.position.x || block.position.x + horiz * 2f > rightWall.position.x ) {
					canMoveThatWay = false;
				}
			}

			// look at all other blocks and see if they have the same y
			// then see if they would have the same x if moved

			for( int otherIndex = 0; otherIndex < tracker.blocksInGame.Count; otherIndex++ ) {
				Transform otherBlock = tracker.blocksInGame[otherIndex];

				if( currentPiece.blocks.Contains( otherBlock ) ) continue;

				for( int myIndex = 0; myIndex < currentPiece.blocks.Count; myIndex++ ) {
					Transform myBlock = currentPiece.blocks[myIndex];

					float yDistToBlock = Mathf.Abs( myBlock.transform.position.y - otherBlock.transform.position.y );
					if( yDistToBlock > blockSize ) continue;

					float xTravelToBlock = otherBlock.transform.position.x - myBlock.transform.position.x;

					if( horiz > 0f ) {
						if( xTravelToBlock < blockSize ) {
							canMoveThatWay = false;
						}
					}
					else {
						if( xTravelToBlock > -blockSize ) {
							canMoveThatWay = false;
						}
					}
				}
			}
			
			if( canMoveThatWay ) currentPiece.transform.position += Vector3.right * horiz;
		}

		if( Input.GetButton("Slam") ) {
			currentPiece.rbody.AddForce( Vector3.down * slamSpeed );
		}

		if( Input.GetButtonDown("Rotate") ) {
			currentPiece.transform.Rotate( 0f, 0f, 90f );

			foreach( Transform block in currentPiece.blocks ) {
				if( block.position.x + horiz * 2f < leftWall.position.x || block.position.x + horiz * 2f > rightWall.position.x ) {
					currentPiece.transform.Rotate( 0f, 0f, -90f );
				}
			}
		}
	}
}
