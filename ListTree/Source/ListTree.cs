namespace ListTree.Source;

public static class ListTree
{
    private static int? _maxDepth;
    private static readonly List<string> Paths = new List<string>();

    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            bool valueFlag = false;
            bool pathFlag = false;

            for (int index = 0; index < args.Length; index++)
            {
                if (valueFlag)
                {
                    valueFlag = false;
                    continue;
                }

                string keyword = args[index];

                switch (keyword)
                {
                    case "--depth":
                    case "-d":
                        if (args.Length - 1 < index + 1)
                        {
                            Console.WriteLine("Dumb.");
                            return;
                        }

                        string arg = args[index + 1].Trim();

                        bool success = int.TryParse(arg, out int result);
                        if (!success)
                        {
                            Console.WriteLine("lstree: Bad integer format.");
                            return;
                        }

                        _maxDepth = result;

                        valueFlag = true;
                        break;
                    
                    case "--version":
                    case "-v":
                        Console.WriteLine("lstree version 1.0-rc1");
                        Console.WriteLine("License: MIT");

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                        """
                        
                             ####   ####
                            #############
                            #############         With Love,
                             ###########                Evelyn~
                              #########
                                #####
                                  #
                                  
                        """);
                        Console.ResetColor();
                        return;

                    default:
                        if (File.Exists(keyword))
                        {
                            Console.WriteLine("lstree: Cannot list a file.");
                            return;
                        }

                        if (!Directory.Exists(keyword))
                        {
                            Console.WriteLine($"lstree: No such File or Directory {keyword}.");
                            return;
                        }

                        pathFlag = true;
                        Paths.Add(keyword);
                        break;
                }
            }

            if (pathFlag)
            {
                foreach (string path in Paths)
                {
                    FileSystem.ReadHierarchy(_maxDepth, path);
                }

                return;
            }
        }

        FileSystem.ReadHierarchy(_maxDepth);
    }
}
