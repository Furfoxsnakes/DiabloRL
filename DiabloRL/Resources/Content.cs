using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiabloRL.Resources;

public class Content
{
    private readonly int[] _experienceData = new int[51];

    public int[] ExperienceData => _experienceData;

    public Content()
    {
        
    }
}