using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using BenchmarkXamarin;

namespace MobileAppPerformance
{
    public class Tests
    {
        private Track _track;
        private List<Point> points;

        public Tests()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            String trackJSON = ""
                               + "{"
                               + "\"track\": {"
                               + "\"id\": \"1000\","
                               + "\"name\": \"Test Raceway\","
                               + "\"gates\": ["
                               + "{"
                               + "\"gate_type\": \"SPLIT\","
                               + "\"latitude\": \"37.451775\","
                               + "\"longitude\": \"-122.203657\","
                               + "\"bearing\": \"136\","
                               + "\"split_number\": \"1\""
                               + "},"
                               + "{"
                               + "\"gate_type\": \"SPLIT\","
                               + "\"latitude\": \"37.450127\","
                               + "\"longitude\": \"-122.205499\","
                               + "\"bearing\": \"326\","
                               + "\"split_number\": \"2\""
                               + "},"
                               + "{"
                               + "\"gate_type\": \"START_FINISH\","
                               + "\"latitude\": \"37.452602\","
                               + "\"longitude\": \"-122.207069\","
                               + "\"bearing\": \"32\","
                               + "\"split_number\": \"3\""
                               + "}"
                               + "]"
                               + "}"
                               + "}";

            _track = Track.Load(trackJSON)[0];
            string contents;
            var stream = typeof(Tests).Assembly.GetManifestResourceStream("MobileAppPerformance.multi_lap_session.csv");
            using (var sr = new StreamReader(stream))
            {
                contents = sr.ReadToEnd();
            }

            string[] lines = contents.Split('\n');
            points = new List<Point>();
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                points.Add(new Point(
                    latitude: Double.Parse(parts[0]),
                    longitude: Double.Parse(parts[1]),
                    speed: Double.Parse(parts[2]),
                    bearing: Double.Parse(parts[3]),
                    horizontalAccuracy: 5.0,
                    verticalAccuracy: 15.0,
                    timestamp: 0));
            }
        }

        [Benchmark]
        public void Test1000()
        {
            double timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            for (int i = 0; i < 1000; i++)
            {
                SessionManager.Instance().Start(_track);
                foreach (Point point in points)
                {
                    SessionManager.Instance().GPS(point.LatitudeDegrees(),
                        longitude: point.LongitudeDegrees(),
                        speed: point.speed,
                        bearing: point.bearing,
                        horizontalAccuracy: point.hAccuracy,
                        verticalAccuracy: point.vAccuracy,
                        timestamp: timestamp);
                    timestamp++;
                }

                SessionManager.Instance().End();
            }

            //stopWatch.Stop();
        }

        [Benchmark]
        public void Test10K()
        {
            double timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                SessionManager.Instance().Start(_track);
                foreach (Point point in points)
                {
                    SessionManager.Instance().GPS(point.LatitudeDegrees(),
                        longitude: point.LongitudeDegrees(),
                        speed: point.speed,
                        bearing: point.bearing,
                        horizontalAccuracy: point.hAccuracy,
                        verticalAccuracy: point.vAccuracy,
                        timestamp: timestamp);
                    timestamp++;
                }

                SessionManager.Instance().End();
            }

            //stopWatch.Stop();
        }

    }
}