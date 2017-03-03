using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace URECA
{
	public class PageNav : MonoBehaviour {

		private InputField pageInput;
		private Button prevPage;
		private Button nextPage;

		// Use this for initialization
		void Start () {
			pageInput = GetComponent<InputField>();
			prevPage = GameObject.FindWithTag ("prevPage").GetComponent<Button> ();
			nextPage = GameObject.FindWithTag ("nextPage").GetComponent<Button> ();

			pageInput.text = "1";

			pageInput.onEndEdit.AddListener (delegate{jumpPageAndLoad();});
			prevPage.onClick.AddListener (decPageNumAndLoad);
			nextPage.onClick.AddListener (incPageNumAndLoad);
		}

		void jumpPageAndLoad(){
			if (int.Parse (pageInput.text) < 1) {
				Debug.Log ("Page out of range");
				pageInput.text = "1";
			}
			else if (int.Parse (pageInput.text) > XMLDecoder.getNumPages ()) {
				Debug.Log ("Page out of range, last page is " + XMLDecoder.getNumPages());
				pageInput.text = XMLDecoder.getNumPages().ToString ();
			}
			ObjectsLoader.showPageOnWindow(int.Parse(pageInput.text)-1);
		}

		void decPageNumAndLoad(){
			if (int.Parse (pageInput.text) <= 1) {
				Debug.Log ("This is the first page");
			} else {
				pageInput.text = (int.Parse (pageInput.text) - 1).ToString();
				ObjectsLoader.showPageOnWindow(int.Parse(pageInput.text)-1);
			}
		}

		void incPageNumAndLoad(){
			if (XMLDecoder.getNumPages () <= int.Parse (pageInput.text)) {
				Debug.Log ("Last page is number " + pageInput.text);
			} else {
				pageInput.text = (int.Parse (pageInput.text) + 1).ToString();
				ObjectsLoader.showPageOnWindow(int.Parse(pageInput.text)-1);
			}
		}

	}
}
