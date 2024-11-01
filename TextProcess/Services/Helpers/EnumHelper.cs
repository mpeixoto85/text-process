namespace TextProcess.Services.Helpers
{
    public static class EnumHelper
    {
        public static List<TEnum> GetEnumList<TEnum>() where TEnum : Enum
           => ((TEnum[])Enum.GetValues(typeof(TEnum))).ToList();

        public static List<string> GetEnumDescriptionList<TEnum>() where TEnum : Enum
           => GetEnumList<TEnum>().Select(e => e.GetDescription()).ToList();
    }
}
