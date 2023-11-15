using System;
using System.Reflection.Emit;

namespace CSVLib
{
	public class DataProcessing
	{
        /// <summary>
        /// Sort by year table.
        /// </summary>
        /// <returns>sorted table.</returns>
        /// <exception cref="ArgumentException">something happend.</exception>
        public static string[] SortByYear()
        {
            try
            {
                return Sorting(7);
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Sort by station name table.
        /// </summary>
        /// <returns>sorted table.</returns>
        /// <exception cref="ArgumentException">something happend.</exception>
        public static string[] SortByStationName()
        {
            try
            {
                return Sorting(1);
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Sort table by columns.
        /// </summary>
        /// <param name="index">index of column.</param>
        /// <returns>sorted table.</returns>
        static string[] Sorting(int index)
        {
            string[] table = CSVProcessing.Read();

            Array.Sort(table, 2, table.Length -2, Comparer<string>.Create((a, b)
                => String.Compare(a.Split(';')[index], b.Split(';')[index])));

            return table;
        }

        /// <summary>
        /// Find rows with this station name from table.
        /// </summary>
        /// <param name="stationName">name to find.</param>
        /// <returns>finded rows.</returns>
		public static string[] GetNameOfStation(string? stationName)
		{
            string[] thisNameStations;

            Selection(new string[] { stationName }, new int[] { 1 }, out thisNameStations);

			return thisNameStations;
        }

        /// <summary>
        /// Find rows with this line name from table.
        /// </summary>
        /// <param name="lineName">name to find.</param>
        /// <returns>finded rows.</returns>
        public static string[] GetLine(string? lineName)
        {
            string[] thisNameLines;

            Selection(new string[] { lineName }, new int[] { 2 },out thisNameLines);

            return thisNameLines;
        }

        /// <summary>
        /// Find rows with this station name and month from table.
        /// </summary>
        /// <param name="stationName">station name to find.</param>
        /// <param name="monthName">month name to find.</param>
        /// <returns>finded rows.</returns>
        public static string[] GetNameOfStationAndMonth (string? stationName, string? monthName)
        {
            string[] thisLineNameAndMonthName;

            Selection(new string[] { stationName, monthName }, new int[] { 1, 8 }, out thisLineNameAndMonthName);

            return thisLineNameAndMonthName;

        }

        /// <summary>
        /// Find rows with this names from table.
        /// </summary>
        /// <param name="name">names to find.</param>
        /// <param name="index">indexes of columns.</param>
        /// <param name="selected">finded rows.</param>
		static void Selection(string[] name, int[] index, out string[] selected)
		{
            string[] fullTable = CSVProcessing.Read();

            //Count amount of selected table.
            int amount = 2;

            for (int i = 2; i < fullTable.Length; i++)
            {
                string[] thatString = fullTable[i].Split(';');

				bool isGood = true;

				for(int j = 0; j < name.Length; j++)
					isGood &= thatString[index[j]].Trim('"') == name[j];

				if (isGood)
					amount++;

            }
            //Fill table.
            selected = new string[amount];
            selected[0] = fullTable[0];
            selected[1] = fullTable[1];

            int lastInd = 2;
            for (int i = 2; i < fullTable.Length; i++)
            {
                string[] thatString = fullTable[i].Split(';');

                bool isGood = true;

                for (int j = 0; j < name.Length; j++)
                    isGood &= thatString[index[j]].Trim('"') == name[j];

                if (isGood)
                {
                    selected[lastInd] = fullTable[i];
                    lastInd++;
                }     
            }
        }

        /// <summary>
        /// Check structure of file.
        /// </summary>
        /// <param name="path"><path to file./param>
        /// <returns>is file correct.</returns>
        public static bool IsCorrectFile(string? path)
        {
            if (!File.Exists(path))
                return false;

            string[] fullTable = File.ReadAllLines(path);
            bool corr = true;

            try
            {
                for (int i = 0; i < fullTable.Length; i++)
                {
                    corr &= CSVProcessing.isRightString(fullTable[i]);
                }
            }
            catch
            {
                return false;
            }

            return corr;
        }
	}
}

