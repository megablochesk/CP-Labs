using LabsLibrary;
using McMaster.Extensions.CommandLineUtils;

namespace Labs
{
	public class Program
	{
		public static int Main(string[] args)
		{
			var app = new CommandLineApplication
			{
				Name = "Lab4",
				Description = "Console app for lab work 4",
			};

			app.HelpOption(inherited: true);

			app.Command("version", versionCmd =>
			{
				versionCmd.Description = "Console application info";
				versionCmd.OnExecute(() =>
				{
					Console.WriteLine("Author: Volodymyr Shevchenko");
					Console.WriteLine("Version: 1.0.0");
				});
			});

			app.Command("run", runCmd =>
			{
				runCmd.Description = "Run lab by key";

				var inputPath = runCmd.Option("-i", "Path to input file", CommandOptionType.SingleValue);
				var outputPath = runCmd.Option("-o", "Path to output file", CommandOptionType.SingleValue);

				// Set default values to input/output variables
				var defaultPath = @"C:\";
				inputPath.DefaultValue = Path.Combine(defaultPath, "INPUT.txt");
				outputPath.DefaultValue = Path.Combine(defaultPath, "OUTPUT.txt");


				runCmd.OnExecute(() =>
				{
					Console.WriteLine("\nLab command is not specified");
					runCmd.ShowHelp();
				});

				runCmd.Command("lab1", lab1Cmd =>
				{
					lab1Cmd.Description = "Run lab work 1";
					lab1Cmd.OnExecute(() =>
					{
						Console.WriteLine("Lab1 was started with the next file: " + inputPath.Value());
						try
						{
							Lab1.Run(inputPath.Value(), outputPath.Value());
							Console.WriteLine("\nLab1 result saved to the next file: " + outputPath.Value());
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
					});
				});

				runCmd.Command("lab2", lab1Cmd =>
				{
					lab1Cmd.Description = "Run lab work 2";
					lab1Cmd.OnExecute(() =>
					{
						Console.WriteLine("Lab2 was started with the next file: " + inputPath.Value());
						try
						{
							Lab2.Run(inputPath.Value(), outputPath.Value());
							Console.WriteLine("\nLab2 result saved to the next file: " + outputPath.Value());
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
					});
				});

				runCmd.Command("lab3", lab1Cmd =>
				{
					lab1Cmd.Description = "Run lab work 3";
					lab1Cmd.OnExecute(() =>
					{
						Console.WriteLine("Lab3 was started with the next file: " + inputPath.Value());
						try
						{
							Lab3.Run(inputPath.Value(), outputPath.Value());
							Console.WriteLine("\nLab3 result saved to the next file: " + outputPath.Value());
						}
						catch (Exception ex)
						{
							Console.WriteLine(ex.Message);
						}
					});
				});

			});

			app.Command("set-path", setPathCmd =>
			{
				setPathCmd.Description = "Change standart directory LAB_PATH";

				var path = setPathCmd.Argument("path", "path to input and output folder");

				setPathCmd.OnExecute(() =>
				{
					if (path.Value != null)
						File.WriteAllText("LAB_PATH", path.Value);
					else
						setPathCmd.ShowHelp();
				});

				setPathCmd.Command("default", defaultCmd =>
				{
					defaultCmd.Description = "Set LAB_PATH to default value";
					defaultCmd.OnExecute(() =>
					{
						File.WriteAllText("LAB_PATH", "Data");
					});
				});
			});

			app.OnExecute(() =>
			{
				app.ShowHelp();
				return 1;
			});
			return app.Execute(args);
		}
	}
}
