namespace ListTree.Source;

public static class FileSystem
{
    public static void ReadHierarchy(int? lifeTime, string? path = null, int shift = 0, int curDepth = 0)
    {
	string shifter = new String(' ', shift);

	string[] files, directories;
	try {
	    path ??= Directory.GetCurrentDirectory();

	    directories = Directory.GetDirectories(path);
	    files = Directory.GetFiles(path);
	}
	catch (UnauthorizedAccessException) {
	    Console.WriteLine($"lstree: Cannot open {path!.TrimEnd(Path.DirectorySeparatorChar)}. Permission denied.");
	    return;
	}

	string dirFmt = Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar));
	string dirType = dirFmt.StartsWith('.') ? "󱞞" : "󰉋";

	Console.ForegroundColor = ConsoleColor.DarkYellow;
	    Console.WriteLine($"{shifter}{dirType} {dirFmt}");
	Console.ResetColor();
	
	if (lifeTime is not null && curDepth > lifeTime)
	{
	    return;
	}

	foreach (string file in files) 
	{
	    string fileFmt = Path.GetFileName(file.TrimEnd(Path.DirectorySeparatorChar));
	    string fileType = Path.GetFileName(fileFmt.StartsWith('.') ? "󰘓" : "");

	    Console.ForegroundColor = ConsoleColor.DarkBlue;
		Console.Write($"{shifter}  {fileType} ");
	    Console.ResetColor();

	    Console.WriteLine($"{fileFmt}");
	}

	foreach (string subDirectory in directories)
	{
	    ReadHierarchy(
		lifeTime,
		subDirectory,
		shift + 2,
		curDepth + 1
	    );
	}
    }
}
