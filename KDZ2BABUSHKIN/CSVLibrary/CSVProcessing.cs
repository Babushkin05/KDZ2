namespace CSVLib
{
    public class CSVProcessing
    {
        public static string fPath;

        //CSV-file must have this structure.
        const string firstString = "\"ID\";\"NameOfStation\";\"Line\";\"Longitude_WGS84\";\"Latitude_WGS84\";" +
            "\"AdmArea\";\"District\";\"Year\";\"Month\";\"global_id\";\"geodata_center\";\"geoarea\";";

        /// <summary>
        /// Read table from file.
        /// </summary>
        /// <returns>table from file.</returns>
        /// <exception cref="ArgumentNullException">bad file.</exception>
        public static string[] Read()
        {
            string[] dataFromFile;

            try
            {
                dataFromFile = File.ReadAllLines(fPath);
            }
            catch 
            {
                throw new ArgumentNullException();
            }

            bool isRight = dataFromFile[0] == firstString;

            for(int i = 1; i < dataFromFile.Length; i++)
            {
                isRight &= isRightString(dataFromFile[i]);
            }

            if (!isRight)
                throw new ArgumentNullException();

            return dataFromFile;
        }

        /// <summary>
        /// Write line to file.
        /// </summary>
        /// <param name="addToFile">line to add.</param>
        /// <param name="nPath">path to file.</param>
        /// <exception cref="ArgumentException">Bad arguments.</exception>
        public static void Write(string addToFile, string nPath)
        {
            try
            {
                if (!isRightString(addToFile))
                    throw new ArgumentException();

                using (StreamWriter sw = File.AppendText(nPath))
                    sw.WriteLine(addToFile);

            }
            catch
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Write lines to file.
        /// </summary>
        /// <param name="addToFile">lines to add.</param>
        /// <exception cref="ArgumentException">bad arguments.</exception>
        public static void Write(string[] addToFile)
        {
            if (File.Exists(fPath))
                File.Delete(fPath);

            try
            {
                for(int i = 0; i < addToFile.Length; i++)
                {
                    Write(addToFile[i], fPath);
                }
            }
            catch
            {
                throw new ArgumentException();
            }
            
        }

        /// <summary>
        /// Check string for structure.
        /// </summary>
        /// <param name="toCheck">string that will be checked.</param>
        /// <returns>is string good.</returns>
        /// <exception cref="ArgumentException">string is null.</exception>
        public static bool isRightString(string toCheck)
        {
            if (toCheck is null)
                throw new ArgumentException();

            //String must have this structure.
            const string goodS = "\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";\"\";";
            string s = "";

            for(int i = 0; i < toCheck.Length; i++)
            {
                if (toCheck[i] == ';' || toCheck[i] == '\"') s += toCheck[i];
            }

            return s == goodS;
        }
    }
}


