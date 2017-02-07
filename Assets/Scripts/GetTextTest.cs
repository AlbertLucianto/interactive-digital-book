using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace URECA
{
	public class GetTextTest : MonoBehaviour {

		private Text textUnity;
		private TextXML textXML;

        // Use this for initialization
        void Start()
        {
            textUnity = GetComponent<Text>();
        }

		// Update is called once per frame
		void Update () {
			XMLDecoder.getData ("Assets/Save_files/TextData.xml");
			textXML = (TextXML) XMLDecoder.accessData ()[1];

			textUnity.text = textXML.getContent();
		}
	}
}