using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class PageLoader {

		public List<GameObjectWithTransform> gameObjects = new List<GameObjectWithTransform>();

		public void addObjectsToPage(PageXML pageXML){
			
			foreach (ObjectXML objectXML in pageXML.listObjects) {
				addObjectToPage (objectXML);
			}
		}

		public void addObjectToPage(ObjectXML objectXML){

			GameObjectWithTransform gameObjectToAdd = new GameObjectWithTransform();

			gameObjectToAdd.gameObject = objectXML.instantiateXMLObject ();

			gameObjectToAdd.id = objectXML.getId();

			gameObjectToAdd.position = objectXML.getPosition ();
			gameObjectToAdd.scale = objectXML.getScale ();
			gameObjectToAdd.rotation = objectXML.getRotation ();

			gameObjects.Add (gameObjectToAdd);

		}

	}
}