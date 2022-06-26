namespace Dinex.Infra
{
    public static class EnumConvertion
    {
        public static T StringToEnum<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
