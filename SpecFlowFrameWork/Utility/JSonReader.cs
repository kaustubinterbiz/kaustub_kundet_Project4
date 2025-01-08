using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject1.Utility
{
    public class JSonReader
    {
        public static string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private static string _jsonFilePath;
       // private dynamic _userData;
        Dictionary<string, object> _userData;
        private string GetFilePath()
        {
            Console.WriteLine("Project Directory "+ projectDirectory);
            return Path.Combine(projectDirectory, "TestData", "TestData.json");
        }

        public string TestDataURL(string urlkey)
        {
            ReadJsonFile();
            if (_userData != null && _userData.ContainsKey(urlkey))
            {
                var value= _userData[urlkey];
                if(value is string)
                {
                    return (string)value;
                }
                else if (value is int)
                {
                    return ((int)value).ToString();
                }
                else if (value is bool)
                {
                    return ((bool)value).ToString();
                }
                else if (value is Dictionary<string, object> nestedDict)
                {
                    
                    return JsonConvert.SerializeObject(nestedDict);
                }
                else if (value is List<object> list)
                {
                    
                    return string.Join(", ", list);
                }
                else
                {
                    
                    return value.ToString();
                }

            }



            return null;
        }

        public Dictionary<string, object> ReadJsonFile()
        {
            _jsonFilePath = GetFilePath();
            Console.WriteLine("File Path "+_jsonFilePath);

            if (File.Exists(_jsonFilePath))
            {
                Console.WriteLine("File Exist");
                string json = File.ReadAllText(_jsonFilePath);
                Console.WriteLine("Json "+json);
                _userData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();

                return _userData;
            }
            else
            {
                Assert.Fail();
                
            }
            return null;
        }
    }
}
