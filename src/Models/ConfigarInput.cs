using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConfigAR.Backend.Models
{
    public class ConfigarInput
    {
        public string Model { get; set; }

        public List<Point> Points { get; set; }

        public string GetPointString()
        {
            List<double> points = new List<double>();

            //string points = "{\"points\":[[1,0,0],[0.5,0,0],[1.5,1,0],[-0.5,1.5,0]]}";

            JArray jArr = new JArray();

            foreach (Point point in this.Points)
            {
                JArray ptJArr = new JArray();
                ptJArr.Add(point.X);
                ptJArr.Add(point.Y);
                ptJArr.Add(0);
                jArr.Add(ptJArr);
            }

            JObject jObj = JObject.FromObject(new {
                points = jArr
            });

            return jObj.ToString();
        }

    }
    public class Point
    {
        public Point()
        {

        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

    }
}
