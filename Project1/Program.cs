using System.Reflection;
using System.Text.Json;
using System.Text.Encodings.Web;
using CW2.Models;
using System.Text.Unicode;

class Program
{
    //File log.txt will be created or written in \bin\Debug\net6.0 directory
    private static string folderPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    private static string fullPath = Path.Combine(folderPath, "log.txt");

    //Method that adds new error record to log.txt file
    public static void AddErrorToLog(string message)
    {
        DateTime now = DateTime.Now;
        using (StreamWriter writer = File.AppendText(fullPath))
        {
            writer.WriteLine(message + " [" + now + "]");
        }
    }
    public static void Main(string[] args)
    {
        List<Student> students = new List<Student>();
        int row = 0;

        try
        {
            if (args.Length != 2)
            {
                throw new ArgumentException();
            }

            if (!File.Exists(args[0]))
            {
                throw new FileNotFoundException();
            }

            if (!Directory.Exists(args[1])) 
            {
                throw new DirectoryNotFoundException();
            }

            var outputPath = Path.Combine(args[1], "json_file.json");

            using (var reader = new StreamReader(args[0]))
            {
                while (!reader.EndOfStream)
                {
                    row++;
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    bool emptyString = false;

                    //Checks whether row contains empty values or not
                    foreach (var value in values)
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        { 
                            emptyString = true;
                        }
                    }

                    //If conditions are met students are added to list,
                    //otherwise the error is added to log.txt file
                    if (values.Length == 9 && !emptyString)
                    {
                        Student student = new Student(values[4], values[0], values[1],
                                                        values[6], values[5], values[7], values[8],
                                                        new List<StudiesRecord> { new StudiesRecord(values[2], values[3]) });
                        bool duplicate = false;
                        bool diffInStudies = false;
                        foreach (var st in students)
                        {
                            if (student.Equal(st))
                            {
                                duplicate = true;
                                break;
                            }
                            else if (student.DifferentStudiesSameStudent(st))
                            {
                                st.Studies.Add(new StudiesRecord(values[2], values[3]));
                                diffInStudies = true;
                            }
                        }
                        if (!duplicate && !diffInStudies)
                        {
                            students.Add(student);
                        }
                    }
                    else
                    {
                        AddErrorToLog("Error: Not enough values in row number: " + row);
                    }
                }
            }
            //Students serialization to JSON file
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            };

            var json = new
            {
                CreatedAt = DateTime.Now,
                Author = "Miłosz",
                Students = students
            };

            string jsonString = JsonSerializer.Serialize(json, options);

            File.WriteAllText(outputPath, jsonString);
        }
        catch (ArgumentException ex)
        {
            AddErrorToLog("Error: " + ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            AddErrorToLog("Error: " + ex.Message);
        }
        catch (DirectoryNotFoundException ex)
        {
            AddErrorToLog("Error: " + ex.Message);
        }
    }
}

