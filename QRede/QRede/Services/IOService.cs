using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using QRede.Model;

namespace QRede.Services
{
    public static class IOService<T>
    {
        public static Task WriteAsync(T dataToSave, string path)
        {
            return Task.Run(()=> {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (StreamWriter outputFile = new StreamWriter(path, false))
                {
                    using(JsonTextWriter JW = new JsonTextWriter(outputFile))
                    {
                        JsonSerializer JS = new JsonSerializer();
                        JS.Serialize(JW, dataToSave);
                    }
                }
            });
        }
        public static Task<List<T>> ReadAsync(string path)
        {
            return Task.Run(() => {                 
                List<T> QRRead = new List<T>();
                if (!Directory.Exists(path))
                {
                    return QRRead;
                }
                using (StreamReader inputFile = new StreamReader(path))
                {
                    using (JsonTextReader JR = new JsonTextReader(inputFile))
                    {                        
                        JsonSerializer JS = new JsonSerializer();
                        QRRead = JS.Deserialize<List<T>>(JR);
                    }
                }
                return QRRead;
            });
        }
    }
}
