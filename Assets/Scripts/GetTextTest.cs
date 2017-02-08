using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace URECA
{
	public class GetTextTest : MonoBehaviour {

		private Text textUnity;
		private TextXML textXML;
        private ContentSizeFitter textFitter;

        // Use this for initialization
        void Start()
        {
            textUnity = GetComponent<Text>();
            textFitter = GetComponent<ContentSizeFitter>();
        }

		// Update is called once per frame
		void Update () {
			XMLDecoder.getData ("Assets/Save_files/TextData.xml");
			textXML = (TextXML) XMLDecoder.accessData ()[1];

			textUnity.text = textXML.getContent();
            textFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            textFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        }
	}
}