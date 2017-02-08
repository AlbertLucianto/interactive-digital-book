using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

namespace URECA
{
    public class GetImageTest : MonoBehaviour
    {

        // The output of the image
        public Image img;
        public RectTransform imgTransform;

        // The source image
        public string url;
        public Texture2D tex;
        private ImageXML imageXML;

        void Start()
        {
            img = GetComponent<Image>();
            imgTransform = GetComponent<RectTransform>();
            XMLDecoder.getData("Assets/Save_files/TextData.xml");
            imageXML = (ImageXML)XMLDecoder.accessData()[2];
            //url = imageXML.getSource();
            //WWW www = new WWW(url);
            //yield return www;
            //img.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            tex = LoadPNG(imageXML.getSource());
            img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
            imgTransform.sizeDelta = new Vector2 (tex.width, tex.height);
        }

        public static Texture2D LoadPNG(string filePath)
        {

            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }

    }
}
