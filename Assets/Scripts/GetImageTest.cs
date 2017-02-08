using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace URECA
{
    public class GetImageTest : MonoBehaviour
    {

        // The output of the image
        public Image img;
        public RectTransform imgTransfrom;

        // The source image
        public string url;
        private ImageXML imageXML;

        IEnumerator Start()
        {
            img = GetComponent<Image>();
            size = GetComponent<RectTransform>();
            XMLDecoder.getData("Assets/Save_files/TextData.xml");
            imageXML = (ImageXML)XMLDecoder.accessData()[2];
            url = imageXML.getSource();
            WWW www = new WWW(url);
            yield return www;
            img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            imgTransform.sizeDelta = new Vector2 (www.texture.width, www.texture.height);
        }

    }
}
