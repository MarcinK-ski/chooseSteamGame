﻿using System;
using System.IO;
using System.Xml.Linq;

namespace playSteam
{
    class Helper
    {
        public const string DEFAULT_LAST_SETTINGS_XML = "lastSettings.xml";

        /*
         * load XML
         */
        public static XElement xRead(string url)
        {
            XElement xelement;
            try
            {
                xelement = XElement.Load(url);
                return xelement;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjatek przy xRead (czytanie XML): " + ex.Message);
                return null;
            }
        }

        /*
         * Checking is file with settings exists
         */
        public static bool isSettFileEx(string url = DEFAULT_LAST_SETTINGS_XML)
        {
            if (File.Exists(url))
                return true;

            return false;
        }

        /*
         * save last UID and APIKey to XML
         */
        public static bool xSettingsSave(string uid, string apiKey, string url = DEFAULT_LAST_SETTINGS_XML)
        {
            XElement settingsFile = new XElement("settings", 
                                                    new XElement("uid", uid), 
                                                    new XElement("api", apiKey));

            if (isSettFileEx(url))
                File.Replace(url, url + ".bak", url + ".bak2");     //2 backup kinds old and older

            settingsFile.Save(url);
            
            return false;
        }

        /*
         * Read one value from settings XML
         */
        public static string xReadSettingVal(string val, string xmlUri = DEFAULT_LAST_SETTINGS_XML)
        {
            XElement xel = xRead(xmlUri);

            return xel?.Element(val)?.Value;
        }
    }
}
