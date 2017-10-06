using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public float slamSpeed = 1f;
    public static int gridWeight = 16;
    public static int gridHeight = 10;
    public static Transform[,] grid = new Transform[gridWeight, gridHeight];

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
