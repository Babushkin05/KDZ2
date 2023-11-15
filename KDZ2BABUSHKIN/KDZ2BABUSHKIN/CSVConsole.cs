using System;
using CSVLib;

namespace KDZ2BABUSHKIN
{
	public class CSVConsole
	{
		/// <summary>
		/// Display csv table on terminal.
		/// </summary>
		/// <param name="table">table to display.</param>
		static void WriteCSV(string[] table)
		{

			for(int i = 0; i < table.Length; i++)
			{
				string[] thisString = table[i].Split(';');
				for(int j = 0; j < thisString.Length; j++)
				{

					string toWrite = "";
					//Make all columns same lenght.
					if (thisString[j].Trim('"').Length < 8)
					{
                        toWrite = thisString[j].Trim('"');
						for (int k = 0; k < 8 - thisString[j].Trim('"').Length; k++)
							toWrite += ' ';
                    }
					else
						toWrite = thisString[j].Trim('"')[..5] + "...";

					Console.Write(toWrite+"\t" );
				}Console.Write('\n');
			}
		}

		/// <summary>
		/// Talk with user.
		/// </summary>
		/// <returns>path to file.</returns>
		public static string GetPath()
		{
			string path;
			do
			{
				Console.Clear();
				Console.Write("Введите абсолютный путь к csv-файлу :: ");
				path = Console.ReadLine();
			} while (!DataProcessing.IsCorrectFile(path));

			return path;
		}

		/// <summary>
		/// Make menu to select function.
		/// </summary>
		/// <returns>selected function</returns>
		public static int Menu()
		{
			int selected = 1;
			ConsoleKey k = ConsoleKey.UpArrow;

			string[] funcs = { "Произвести выборку по значению NameOfStation", "Произвести выборку по значению Line",
				"Произвести выборку по значению NameOfStation и Month", "Отсортировать таблицу по значению Year (прямой порядок)",
                "Отсортировать таблицу по значению NameOfStation (прямой порядок)","Выйти из программы"};

			//Change selected buttons.
            do
			{
				Console.Clear();

				if (k == ConsoleKey.UpArrow)
					selected = Math.Max(selected - 1, 0);
				else
					selected = Math.Min(selected + 1, funcs.Length - 1);

				//Paint buttons.
				for(int i = 0; i < funcs.Length; i++)
				{
					if(i == selected)
					{
                        Console.ForegroundColor= ConsoleColor.White;
						Console.WriteLine(funcs[i], ConsoleColor.Black);
                    }
					else
					{
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(funcs[i], ConsoleColor.White);
                    }
                }

				k = Console.ReadKey().Key;

            } while ((k != ConsoleKey.Enter) || (k == ConsoleKey.DownArrow) || (k == ConsoleKey.UpArrow));

            Console.ForegroundColor = ConsoleColor.White;

            return selected;
		}

		/// <summary>
		/// Use selected function.
		/// </summary>
		/// <param name="selected">index of selected function.</param>
		public static void FunctionHub(int selected)
		{
			//Take name for selection from table.
			string? name = "";
			if (selected < 3)
				do
				{
					Console.Clear();
					Console.Write("Press name to selection (separator = space if several arguments :: ");
					name = Console.ReadLine();
				} while (name == null);

			//Use functions.
			string[] table = selected switch
			{
				0 => DataProcessing.GetNameOfStation(name),
				1 => DataProcessing.GetLine(name),
				2 => DataProcessing.GetNameOfStationAndMonth(name.Split(' ')[0], name.Split(' ')[1]),
				3 => DataProcessing.SortByYear(),
				4 => DataProcessing.SortByStationName()
			};

			if(table.Length > 2)
			{
				Console.Clear();

                Console.WriteLine("Press 'y' to save table, or some another key to unsave\n");

                WriteCSV(table);

                ConsoleKey k = Console.ReadKey().Key;

				//Save table.
                if (k == ConsoleKey.Y)
                {
                    bool repeat = false;
                    do
                    {
                        try
                        {
                            Console.Clear();

                            Console.Write("Type name for cvs-file to save it :: ");
                            string? path = Console.ReadLine();
                            char separator = Path.DirectorySeparatorChar;
                            CSVProcessing.fPath = $"..{separator}..{separator}..{separator}{path}";
                            CSVProcessing.Write(table);
                        }
                        catch
                        {
                            repeat = true;
                        }
                    } while (repeat);
                }
            }
			//table are empty.
			else
			{
				Console.WriteLine("0 rows are chosen");
				Console.WriteLine("Press any key to continue");
				Console.ReadKey();
			}

		}

	}
}

