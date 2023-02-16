using Newtonsoft.Json;
namespace Project_Genetic
{
    public static class ExtensionMethods
    {
        public static Tree Clone<Tree>(this Tree source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<Tree>(serialized);
        }
        public static string ToHumanReadableString (this TimeSpan t)
    {
        if (t.TotalSeconds <= 1) {
            return $@"{t:s\.ff} seconds";
        }
        if (t.TotalMinutes <= 1) {
            return $@"{t:%s} seconds";
        }
        if (t.TotalHours <= 1) {
            return $@"{t:%m} minutes and {t:%s} seconds";
        }
        if (t.TotalDays <= 1) {
            return $@"{t:%h} hours and {t:%m} minutes";
        }

        return $@"{t:%d} days";
    }
    }
}