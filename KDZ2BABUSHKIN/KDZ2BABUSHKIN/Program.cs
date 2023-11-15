using CSVLib;
namespace KDZ2BABUSHKIN
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Gets path to next work.
            CSVProcessing.fPath = CSVConsole.GetPath();

            do
            {
                //Select finction.
                int selectedFunction = CSVConsole.Menu();

                //User wants to brake.
                if (selectedFunction == 5)
                    break;

                //Use selected function.
                CSVConsole.FunctionHub(selectedFunction);

            } while (true);
        }
    }
}
