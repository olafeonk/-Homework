using System;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string firstName)
        {
            var days = new string[31];
            var countBirthdayInNumberDay = new double[31];
            for (int i = 0; i < 31; i++)
            {
                days[i] = (i + 1).ToString();
            }
            return new HistogramData(
                $"Рождаемость людей с именем '{firstName}'",
                days,
                GetArrayBirthdayInNumberDay(names, firstName, countBirthdayInNumberDay));
        }

        private static double[] GetArrayBirthdayInNumberDay(
            NameData[] names,
            string firstName,
            double[] countBirthdayInNumberDay)
        {
            foreach (var name in names)
            {
                if (name.Name == firstName && name.BirthDate.Day != 1)
                {
                    countBirthdayInNumberDay[name.BirthDate.Day - 1]++;
                }
            }
            return countBirthdayInNumberDay;
        }
    }
}