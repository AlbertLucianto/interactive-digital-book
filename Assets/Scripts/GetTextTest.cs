using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace URECA
{
	public class GetTextTest : MonoBehaviour {

		public Text contentText;
		private TextXML content;

		// Use this for initialization
		void Start () {
	
		}

		// Update is called once per frame
		void Update () {
			XMLDecoder.getData ("Assets/Save_files/TextData.xml");
			content = (TextXML) XMLDecoder.accessData ()[0];

			contentText.text = content.getFontFamily();
		}
	}
}