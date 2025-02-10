using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConverterData
{
    public class Data
    {
        public int like {  get; set; }
        public int dislike { get; set; }

        public Data() { like = 0; dislike = 0;}
        public Data(int like, int dislike) { this.like = like; this.dislike = dislike;}
        public Data(string like, string dislike) { this.like = Convert.ToInt32(like); this.dislike = Convert.ToInt32(dislike);}
    }
    internal class Program
    {
        

        static void Main(string[] args)
        {
            string path = "./";

            Console.WriteLine("Введите путь или оставте пустым если программа находиться в папке с фалами: ");
            string new_path = Console.ReadLine();

            path = new_path != "" ? new_path : path;

            string[] files = Directory.GetFiles(path, "*.txt");

            foreach (string file in files)
            {
                if (File.Exists(file))
                {
                    string like = "", dislike = "";
                    string[] lines = File.ReadAllLines(file);

                    foreach (string line in lines)
                    {
                        string temp = line.Remove(0, 13);

                        foreach(char c in temp)
                        {
                            if(c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                            {
                                like += c;
                            }
                            else if(c == ' ')
                            {
                                break;
                            }
                        }

                        temp = temp.Remove(0, like.Length + 19);

                        foreach (char c in temp)
                        {
                            if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                            {
                                dislike += c;
                            }
                        }

                        string p = file.Remove(file.Length - 4, 4);

                        Data data = new Data(like, dislike);
                        var json = JsonConvert.SerializeObject(data);

                        File.WriteAllText($"{p}.json", json);

                        Console.WriteLine($"Преобразовал: {file} -> {p}.json");
                    }
                }
            }
            Console.WriteLine("Нажмите return для продолжения");
            Console.ReadKey();
        }
    }
}
