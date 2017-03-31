using System;
using System.IO;
using System.Text;

namespace ExtensionBoilerplater
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder strBld = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("using System;");
            strBld.AppendLine("using System;");
            Console.WriteLine("using System.Data;");
            strBld.AppendLine("using System.Data;");
            Console.WriteLine();
            strBld.AppendLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("class ");
            Console.ForegroundColor = ConsoleColor.White;
            string name = Console.ReadLine();
            strBld.AppendLine("class " + name);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("{");
            strBld.AppendLine("{");

            Console.WriteLine("    [DataRow]");
            strBld.AppendLine("    [DataRow]");
            Console.WriteLine("    DataRow DataRow { get; set; }");
            strBld.AppendLine("    DataRow DataRow { get; set; }");
            Console.WriteLine();
            strBld.AppendLine();

            int count = 0;
            string[] parameters = new string[] { "", "", "" };
            string currentInput = "NOTHING";

            while (currentInput != "")
            {
                string s = "";
                switch (count)
                {
                    case 0:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("    [DataField(\"");
                            Console.ForegroundColor = ConsoleColor.White;
                            s = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.SetCursorPosition(16 + s.Length, Console.CursorTop - 1);
                            Console.WriteLine("\")]");
                        }
                        break;
                    case 1:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("    public ");
                            Console.ForegroundColor = ConsoleColor.White;
                            s = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.SetCursorPosition(11 + s.Length, Console.CursorTop - 1);
                        }
                        break;
                    case 2:
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(" ");
                            Console.ForegroundColor = ConsoleColor.White;
                            s = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.SetCursorPosition(12 + parameters[1].Length + s.Length, Console.CursorTop - 1);
                            Console.WriteLine(" { get; set; }");
                            Console.WriteLine();
                        }
                        break;
                }

                parameters[count] = s;
                currentInput = s;

                count++;
                if (count != 0 && count % 3 == 0)
                {
                    strBld.AppendLine(BuildProperty(parameters[0], parameters[1], parameters[2]));
                    count = 0;
                }
            }

            Console.WriteLine("}");
            strBld.AppendLine("}");

            File.WriteAllText(name + ".cs", strBld.ToString());

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Finished! Saved to \"" + name + ".cs\"");
            Console.WriteLine("Press ENTER to quit");
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string BuildProperty(string dbName, string type, string name)
        {
            return String.Format("    [DataField(\"{0}\")]" + Environment.NewLine + 
                                 "    public {1} {2} {3}", dbName, type, name, "{ get; set; }") + Environment.NewLine;
        }
    }
}
