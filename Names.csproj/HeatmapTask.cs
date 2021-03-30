namespace Names
{
    internal static class HeatmapTask
    {
        public static string[] GetStringArray(int firstValue, int lengthArray)
        {
            var stringArray = new string[lengthArray];
            for (var i = 0; i < lengthArray; i++)
            {
                stringArray[i] = (i + firstValue).ToString();
            }

            return stringArray;
        }
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var data = new double[30, 12];
            foreach (var name in names)
            {
                if (name.BirthDate.Day != 1)
                {
                    data[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
                }
            }

            return new HeatmapData(
                "Тепловая карта рождаемости в зависимости от дня и месяца",
                data,
                GetStringArray(2, 30),
                GetStringArray(1, 12));
        }
    }
}