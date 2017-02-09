using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URECA
{
    public class Get3DmodelTest : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            // Instantiates a prefab named "enemy" located in any Resources
            // folder in your project's Assets folder.
            GameObject model = Instantiate(Resources.Load("petronas", typeof(GameObject))) as GameObject;
            model.transform.SetParent(GetComponent<Canvas>().transform, false);
            model.transform.SetAsFirstSibling();
        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}
