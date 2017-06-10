using Sharit.Configs;
using Sharit.Models;
using System;
using System.Collections.Generic;

namespace Sharit.Helpers
{
    public class UrlHelper
    {
        private static Dictionary<Type, string> _typeUrlDictionary;

        public UrlHelper()
        {
            _typeUrlDictionary = GenerateDictionary();
        }

        public static Dictionary<Type, string> GenerateDictionary()
        {
            return new Dictionary<Type, string>
            {
                { typeof(SharitItem), GlobalSettings.AzureUrlSharitItemTable },
                { typeof(Category), GlobalSettings.AzureUrlCategoryItemTable },
            };
        }

        public static string GetUrl<T>() where T : EntityBase
        {
            if(_typeUrlDictionary == null) _typeUrlDictionary = GenerateDictionary();

            if (_typeUrlDictionary.ContainsKey(typeof(T)))
                return _typeUrlDictionary[typeof(T)];
            
            //ToDo Exception?
            return "";
        }
    }
}
