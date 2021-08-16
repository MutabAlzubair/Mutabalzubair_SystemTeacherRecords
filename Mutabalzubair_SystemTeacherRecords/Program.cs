using System;
using System.IO;
namespace Mutabalzubair_SystemTeacherRecords
{

    class Teacher
    {
        public int id;
        public string name;
        public int classN;
        public int section;

        public Teacher(int id, string name, int classN, int section)
        {
            this.id = id;
            this.name = name;
            this.classN = classN;
            this.section = section;
        }
    }

    class Program

    {
        public static string fileLocation = "/Users/mutabsa/Projects/Mutabalzubair_SystemTeacherRecords//TeachersRecord.txt";

        public static void WriteData()
        {
            FileStream fs = new FileStream(fileLocation, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("RecordsTeachers");
            sw.WriteLine("ID \t Name        \t Class \t Section");

            sw.Close();
            fs.Close();
        }

        public static void AppendData()
        {

            FileStream fs = new FileStream(fileLocation, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                Console.Write("Add teacher ID ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Add teacher Name ");
                string name = Console.ReadLine();
                Console.Write("Add teacher Class Number ");
                int classN = Convert.ToInt32(Console.ReadLine());
                Console.Write("Add teacher Section ");
                int section = Convert.ToInt32(Console.ReadLine());

                Teacher t1 = new Teacher(id, name, classN, section);
                sw.WriteLine("{0} \t {1} \t {2} \t {3}", t1.id, t1.name, t1.classN, t1.section);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.Message); 
            }
            finally
            {
                sw.Close();
                fs.Close();
            }

        }


        public static void ReadData()
        {
            FileStream fs = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string str = sr.ReadLine();
            while (str != null)
            {
                Console.WriteLine(str);
                str = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
        }

        public static void UpdateData(int id)
        {
            FileStream fs3 = new FileStream(fileLocation, FileMode.Open, FileAccess.Read);
            StreamReader sr3 = new StreamReader(fs3);

            try
            {
                Console.Write("Update teacher Name ");
                string name = Console.ReadLine();
                Console.Write("Update teacher Class Number ");
                int classN = Convert.ToInt32(Console.ReadLine());
                Console.Write("Update teacher Section ");
                int section = Convert.ToInt32(Console.ReadLine());

                Teacher t1 = new Teacher(id, name, classN, section);
                string updatedData = $"{t1.id} \t {t1.name} \t {t1.classN} \t {t1.section}";

                string[] lines;
                using (fs3)
                {
                    using (sr3)
                    {
                        lines = File.ReadAllLines(fileLocation);

                        for (int i = 2; i < lines.Length; i++)
                        {
                            string[] split = lines[i].Split(',');
                            foreach (var item in split)

                            {
                                if (Char.GetNumericValue(item[0]) == id)
                                {
                                    lines[i] = updatedData;
                                }
                            }
                        }

                        foreach (var item in lines)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }

                using (FileStream fs = new FileStream(fileLocation, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (var item in lines)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }

            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.Message);
            }

        }

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine(" RecordsTeacher System  \n\n" +
               "(1)  ADD data \n" +
               "(2)  UPDATE data \n" +
               "(3)  SHOW records teacher  \n" +
               "(4)  FINSH \n");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        WriteData();
                        AppendData();
                        break;
                    case 2:
                        ReadData();
                        Console.Write("Enter teacher ID to update:");
                        int id = Convert.ToInt32(Console.ReadLine());
                        UpdateData(id);
                        break;
                    case 3:
                        ReadData();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Wrong input.");
                        break;
                }
                if (choice == 4) { break; }

            }

        }
    }
}
