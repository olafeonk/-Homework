using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Names
{
    internal static class HistogramSample
    {
        public static HistogramData GetHistogramBirthsByYear(NameData[] names, int minLengthName, int maxLengthName)
        {
            var countName = new double[maxLengthName - minLengthName + 1];
            foreach (var name in names)
            {
                if (name.Name.Length <= maxLengthName && name.Name.Length >= minLengthName)
                {
                    countName[name.Name.Length - minLengthName]++;
                    
                }
            }
            var lengthName = new string[maxLengthName - minLengthName + 1];
            for (int i = minLengthName; i <= maxLengthName; i++)
            {
                lengthName[i - minLengthName] = i.ToString();
            }
            return new HistogramData("Количество имен в зависимости от длины", lengthName, countName);
        }
    }
}