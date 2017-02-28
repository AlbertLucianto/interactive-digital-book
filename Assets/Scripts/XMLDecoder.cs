using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;
using UnityEngine;

namespace URECA
{
	public class XMLDecoder: MonoBehaviour
	{

		private static List<PageXML> listPages = new List<PageXML>();
		private static PageXML pageToAdd;
		private static int numOfPages;

		public static void loadData(string xmlPath){
			var settings = new XmlReaderSettings ();
			settings.ProhibitDtd = false;
			settings.ValidationType = ValidationType.DTD;
			//settings.DtdProcessing = DtdProcessing.Parse;
			settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler (ValidationCallBack);
			//Console.WriteLine(settings.IgnoreProcessingInstructions);
			settings.IgnoreWhitespace = true;

			XmlReader reader = XmlReader.Create(xmlPath, settings);
			XDocument doc = XDocument.Load(reader);
			var pages = doc.Descendants("pages").Elements();

			//===============Exporting XML to C# Classes===============

			numOfPages = 0;

			foreach (XElement page in pages)
			{
				pageToAdd = new PageXML (numOfPages++);
				pageToAdd.construct (page);

				listPages.Add(pageToAdd);
			}

		}

		private static void ValidationCallBack(object sender, ValidationEventArgs e)
		{
			if (e.Severity == XmlSeverityType.Warning)
				Console.WriteLine("Warning: Matching schema not found.  No validation occurred." + e.Message);
			else // Error
				Console.WriteLine("Validation error: " + e.Message);
		}



		//=================== Return List of Objects ===================
		public static List<PageXML> getData(){
			return listPages;
		}



		//=================== Clear List / Buffer ======================
		public static void clearData(){
			
			foreach (PageXML page in listPages) {
				page.listObjects.Clear ();
			}

			listPages.Clear ();
		}
			
	}
}
