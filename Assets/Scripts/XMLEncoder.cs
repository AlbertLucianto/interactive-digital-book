using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;
using System.Xml.Serialization;
using UnityEngine;

namespace URECA
{
	[XmlRoot("pages")]
	public class XMLEncoder
	{
		[XmlArray("pages")]
		[XmlArrayItem("page", typeof(PageXML))]
		private static List<PageXML> listPages = new List<PageXML>();

		public static void saveToXML(){
			listPages = ObjectSaver.getListSavedPages();
			var xs = new XmlSerializer(typeof(List<PageXML>));
			var stream = new FileStream(@"C:\Users\Keefe Julian\Uni\URECA\URECA\output\encode.xml", FileMode.Create);
			xs.Serialize(stream, listPages);
			stream.Close();
		}
	}
}
