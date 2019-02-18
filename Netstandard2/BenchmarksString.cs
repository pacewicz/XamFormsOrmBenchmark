using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BenchmarkXamarin.Sample
{
	public class ListData
	{
		public int id { get; set; }
		public string label { get; set; }
	}
	class BenchmarksString
    {
        [Benchmark]
        public void Substring()
        {
            "musculus cremaster".Substring(8, 9);
        }

        [Benchmark]
        public void Split()
        {
            "  lingua latina non  verpa canina  ".Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        [Benchmark]
        public void Trim()
        {
            "    lorem ipsum      ".Trim();
        }

        [Benchmark]
        public void StartWith()
        {
            "hello world, ребята".StartsWith("h");
        }

		//[Benchmark]
		public void DataBuilding10000()
		{
			var d = BuildData(100);
		}

        public List<ListData> BuildData(int count = 1000)
        {
			var id = 1;
	        List<ListData> items = new List<ListData>(count);

	        string[] adjectives = { "pretty", "large", "big", "small", "tall", "short", "long", "handsome", "plain", "quaint", "clean", "elegant", "easy", "angry", "crazy", "helpful", "mushy", "odd", "unsightly", "adorable", "important", "inexpensive", "cheap", "expensive", "fancy" };
	        string[] colours = { "red", "yellow", "blue", "green", "pink", "brown", "purple", "brown", "white", "black", "orange" };
	        string[] nouns = { "table", "chair", "house", "bbq", "desk", "car", "pony", "cookie", "sandwich", "burger", "pizza", "mouse", "keyboard" };

	        Random rnd = new Random();

	        for (int i = 0; i < count; i++)
	        {
		        items.Add(new ListData { id = id, label = adjectives[rnd.Next(0, adjectives.Length)] + " " + colours[rnd.Next(0, colours.Length)] + " " + nouns[rnd.Next(0, nouns.Length)] });
		        id++;
	        }

	        return items;
        }
	}
}
