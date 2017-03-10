using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace URECA
{
	public class BoundBoxes_BoundBox : MonoBehaviour {
		
		public bool colliderBased = false;
		public bool permanent = false; //permanent//onMouseDown
		
		public Color lineColor = new Color(0f,1f, 0.4f,0.74f);

		private Bounds bound;
		
		private Vector3[] corners;
		
		private Vector3[,] lines;
		
		private Quaternion quat;
		
		private Camera mcamera;
		
		private BoundBoxes_drawLines cameralines;
		
		private Renderer[] renderers;
		private MeshFilter[] meshesFil;
		private MeshCollider[] meshesCol;
		
		private Material[][] Materials;
		
		private Vector3 topFrontLeft;
		private Vector3 topFrontRight;
		private Vector3 topBackLeft;
		private Vector3 topBackRight;
		private Vector3 bottomFrontLeft;
		private Vector3 bottomFrontRight;
		private Vector3 bottomBackLeft;
		private Vector3 bottomBackRight;

		private bool success;
		

		void Awake () {	
			renderers = GetComponentsInChildren<Renderer>();
			Materials = new Material[renderers.Length][];
			for(int i = 0; i < renderers.Length; i++) {
				Materials[i]= renderers[i].materials;
			}
		}
		
		
		void Start () {
			success = false;

			if (GetComponentInChildren<MeshFilter> ()) {
				meshesFil = GetComponentsInChildren<MeshFilter> ();
			} else {
//				Debug.Log ("tak ada MeshFilter");
//				GameObject temp = GameObject.CreatePrimitive (PrimitiveType.Cube) as GameObject;
//				meshes = new MeshFilter[1];
//				meshes[0] = Component.Instantiate(temp.GetComponent<MeshFilter> ()) as MeshFilter;
//				Vector3 colliderSize = GetComponent<BoxCollider> ().size;
//				Vector3 colliderScale = transform.lossyScale;
//				meshes[0].transform.localScale = new Vector3 (
//					colliderSize.x * colliderScale.x,
//					colliderSize.y * colliderScale.y,
//					colliderSize.z * colliderScale.z
//				);
//				meshes[0].transform.position = new Vector3(
//					transform.position.x,
//					transform.position.y,
//					transform.position.z
//				);
//				meshes [0].transform.rotation = transform.rotation;
//				Destroy(temp);

				meshesCol = GetComponentsInChildren<MeshCollider> ();
			}

			mcamera = Camera.main;
			cameralines = mcamera.GetComponent<BoundBoxes_drawLines>();
			init();
		}
		
		public void init() {
			if (GetComponentInChildren<MeshFilter> ()) {
				calculateBoundsFilter (meshesFil);
			} else {
				calculateBoundsColl (meshesCol);
			}
			setPoints ();
			setLines ();
			if (success) {
				cameralines.setOutlines (lines, lineColor);
			}
		}
		
		void LateUpdate() {
			if (success) {
				cameralines.setOutlines (lines, lineColor);
			}
		}

		void calculateBoundsColl(MeshCollider[] mcAr) {
			quat = transform.rotation;//object axis AABB

			transform.rotation = Quaternion.Euler(0f,0f,0f);
			for (int i = 0; i < mcAr.Length; i++) {

				Mesh ms = mcAr [i].sharedMesh;
				Vector3 tr = mcAr [i].gameObject.transform.position;
				Vector3 ls = mcAr [i].gameObject.transform.lossyScale;
				Quaternion lr = mcAr [i].gameObject.transform.rotation;
				int vc = ms.vertexCount;
				for (int j = 0; j < vc; j++) {
					if (i == 0 && j == 0) {
						bound = new Bounds (tr + lr * Vector3.Scale (ls, ms.vertices [j]), Vector3.zero);
					} else {
						bound.Encapsulate (tr + lr * Vector3.Scale (ls, ms.vertices [j]));
					}
				}			
			}
			transform.rotation = quat;
			success = true;
		}

		void calculateBoundsFilter(MeshFilter[] mfAr) {
			quat = transform.rotation;//object axis AABB
//			if(renderers[0] && renderers[0].isPartOfStaticBatch) quat = Quaternion.Euler(0f,0f,0f);//world axis
//
//			if(colliderBased){
//				BoxCollider coll = GetComponent<BoxCollider>();
//				if(coll){
//	                GameObject co = new GameObject("dummy");
//	                co.transform.position = transform.position;
//	                co.transform.localScale = transform.lossyScale;
//	                BoxCollider cobc = co.AddComponent<BoxCollider>();
//	                quat = transform.rotation;
//	                cobc.center = coll.center;
//	                cobc.size = coll.size;
//					bound = cobc.bounds;
//	                Destroy(co);
//				}else{
//					Debug.Log("No collider attached");
//				}
//				return;
//			}
//			bound = new Bounds();
//			if(renderers[0] && renderers[0].isPartOfStaticBatch){
//				bound = renderers[0].bounds;
//				for(int i = 1; i < renderers.Length; i++) {
//					bound.Encapsulate(renderers[i].bounds);
//				}
//				return;
//			}
//
//			if (meshes.GetType () == typeof(MeshFilter[])) {
//				ms = Array.ConvertAll (meshes, item => (MeshFilter)item);
//			}
				
			transform.rotation = Quaternion.Euler(0f,0f,0f);
			for (int i = 0; i < mfAr.Length; i++) {

				Mesh ms = mfAr [i].mesh;
				Vector3 tr = mfAr [i].gameObject.transform.position;
				Vector3 ls = mfAr [i].gameObject.transform.lossyScale;
				Quaternion lr = mfAr [i].gameObject.transform.rotation;
				int vc = ms.vertexCount;
				for (int j = 0; j < vc; j++) {
					if (i == 0 && j == 0) {
						bound = new Bounds (tr + lr * Vector3.Scale (ls, ms.vertices [j]), Vector3.zero);
					} else {
						bound.Encapsulate (tr + lr * Vector3.Scale (ls, ms.vertices [j]));
					}
				}			
			}
			transform.rotation = quat;
			success = true;
		}
		
		void setPoints() {
		
			Vector3 bc = transform.position + quat *(bound.center - transform.position);

			topFrontRight = bc +  quat *Vector3.Scale(bound.extents, new Vector3(1, 1, 1)); 
			topFrontLeft = bc +  quat *Vector3.Scale(bound.extents, new Vector3(-1, 1, 1)); 
			topBackLeft = bc +  quat *Vector3.Scale(bound.extents, new Vector3(-1, 1, -1));
			topBackRight = bc +  quat *Vector3.Scale(bound.extents, new Vector3(1, 1, -1)); 
			bottomFrontRight = bc +  quat *Vector3.Scale(bound.extents, new Vector3(1, -1, 1)); 
			bottomFrontLeft = bc +  quat *Vector3.Scale(bound.extents, new Vector3(-1, -1, 1)); 
			bottomBackLeft = bc +  quat *Vector3.Scale(bound.extents, new Vector3(-1, -1, -1));
			bottomBackRight = bc +  quat *Vector3.Scale(bound.extents, new Vector3(1, -1, -1)); 
			corners = new Vector3[]{topFrontRight,topFrontLeft,topBackLeft,topBackRight,bottomFrontRight,bottomFrontLeft,bottomBackLeft,bottomBackRight};
			
		}
		
		void setLines() {
			
			int i1;
			int linesCount = 12;

			lines = new Vector3[linesCount,2];
			for (int i=0; i<4; i++) {
				i1 = (i+1)%4;//top rectangle
				lines[i,0] = corners[i];
				lines[i,1] = corners[i1];
				//break;
				i1 = i + 4;//vertical lines
				lines[i+4,0] = corners[i];
				lines[i+4,1] = corners[i1];
				//bottom rectangle
				lines[i+8,0] = corners[i1];
				i1 = 4 + (i+1)%4;
				lines[i+8,1] = corners[i1];
			}
		}
		
//		void OnMouseDown() {
//			if(permanent) return;
//			enabled = !enabled;
//		}
		
	}
}
