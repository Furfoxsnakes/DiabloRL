using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiabloRL.Resources;

public class ExperienceData
{
    private const string fileName = "Resources/ExperienceData.json";

    /// <summary>
    /// Load the level <> experience data from json
    /// </summary>
    /// <param name="content"></param>
    public static void Load(Content content)
    {
        using (StreamReader reader = File.OpenText(fileName))
        {
            var experienceDataObj = (JObject) JToken.ReadFrom(new JsonTextReader(reader));
            var dataArray = experienceDataObj["Levels"].ToArray();

            for (var i = 0; i < dataArray.Length; i++)
                content.ExperienceData[i] = (int) dataArray[i];
        }
    }
}