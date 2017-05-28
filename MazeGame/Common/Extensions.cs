using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System;

namespace MazeGame.Common
{
    static class Extensions
    {
        public static bool EqualsLower(this string arg1, string arg2)
        {
            return (arg1.ToLower().Equals(arg2.ToLower()));
        }

        public static MazeWrapper ToMazeWrapper(this JObject jObject)
        {
            try
            {
                var mazeWrapper = new MazeWrapper();
                mazeWrapper.Name = jObject["Name"].ToObject<string>();

                string tempStr = jObject["Maze"].ToObject<string>();

                mazeWrapper.MazeStr = Regex.Replace(tempStr, @"\t|\r|\n", string.Empty);
                mazeWrapper.Cols = jObject["Cols"].ToObject<int>();
                mazeWrapper.Rows = jObject["Rows"].ToObject<int>();

                JObject startPos = (JObject)jObject["Start"];
                mazeWrapper.StartCol = startPos["Col"].ToObject<int>();
                mazeWrapper.StartRow = startPos["Row"].ToObject<int>();

                JObject endPos = (JObject)jObject["End"];
                mazeWrapper.EndCol = endPos["Col"].ToObject<int>();
                mazeWrapper.EndRow = endPos["Row"].ToObject<int>();

                return mazeWrapper;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string TrimJasonEnd(this string jasonString)
        {
            int index = jasonString.LastIndexOf('}');
            if (index < 0) return jasonString;

            return jasonString.Substring(0, index + 1);
        }
    }
}
