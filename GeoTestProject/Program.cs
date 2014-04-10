using Geo;
using Geo.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Liste erstellen
            //var myfractionlist = GeneratemyList();
            var myfractionlist = GeneratemyListDB();
            


            // Die Ausgangskoordinate
            var ausgangscoord = new Coordinate(46.8167, 11.7233);
            var radius = 10000;

            var circle = new Circle(ausgangscoord, radius);
            var circlebounds = circle.GetBounds();
           
            //var x = (from t3 in
            //         from t2 in
            //         from t1 in myfractionlist
            //         select Tuple.Create(t1.LTSRid, new Coordinate(t1.East, t1.North))
            //         where circlebounds.Contains(t2.Item2)
            //         select Tuple.Create(t2.Item1, getDistance(ausgangscoord, t2.Item2))
            //         orderby t3.Item2
            //         select t3).FirstOrDefault();

            var y = (from t1 in myfractionlist
                     let coordinate = new Coordinate(t1.North, t1.East)
                     where circlebounds.Contains(coordinate)
                     let distance = getDistance(ausgangscoord, coordinate)
                     orderby distance
                     select new { Id = t1.LTSRid, Name = t1.Name }).FirstOrDefault();

            ////Liste aus DB durchsuchen
            //using (var mycontainer = new MyLTSRidsContainer())
            //{
            //    var result = 
            //        (from t1 in mycontainer.FractionsSet
            //         let coordinate = new Coordinate(t1.East, t1.North)
            //         where circlebounds.Contains(coordinate)
            //         let distance = getDistance(ausgangscoord, coordinate)
            //         orderby distance
            //         select new { Id = t1.LTSRid, Name = t1.Name }).FirstOrDefault();

                
            //}


            Console.WriteLine(y);
            Console.ReadLine();
        }

        public static double getDistance(Coordinate coord1, Coordinate coord2)
        {
            double distance = Math.Abs(coord1.GetBounds().GetLength().Value - coord2.GetBounds().GetLength().Value);
            return distance;
        }

        public static List<Fraction> GeneratemyList()
        {
            List<Fraction> myfraction = new List<Fraction>();

            //Von DB Liste erstellen?
            myfraction.Add(new Fraction{ LTSRid = "1", East = 46.672830, North = 11.061115 });
            myfraction.Add(new Fraction{ LTSRid = "2", East = 46.673184, North = 11.048070 });
            myfraction.Add(new Fraction{ LTSRid = "3", East = 46.640194, North = 11.189862 });
            myfraction.Add(new Fraction{ LTSRid = "4", East = 46.684255, North = 11.050816 });
            myfraction.Add(new Fraction{ LTSRid = "5", East = 46.674576, North = 11.057681 });
            myfraction.Add(new Fraction{ LTSRid = "6", East = 46.672765, North = 11.060170 });
            myfraction.Add(new Fraction{ LTSRid = "7", East = 46.661640, North = 11.159306 });
            myfraction.Add(new Fraction{ LTSRid = "8", East = 46.689201, North =  11.107121 });
            myfraction.Add(new Fraction{ LTSRid = "9", East = 46.657398, North =  11.071416 });
            myfraction.Add(new Fraction{ LTSRid = "10", East = 46.672851, North = 11.060893 });
            myfraction.Add(new Fraction{ LTSRid = "11", East = 46.701102, North = 11.178753 });
            myfraction.Add(new Fraction{ LTSRid = "12", East = 46.684147, North = 11.154034 });
            myfraction.Add(new Fraction{ LTSRid = "13", East = 46.640793, North = 11.071636 });

            return myfraction;
        }

        public static List<Fraction> GeneratemyListDB()
        {
            List<Fraction> myfraction = new List<Fraction>();

            using (var mycontainer = new MyLTSRidsContainer())
            {
                foreach (var element in mycontainer.FractionsSet)
                {
                    myfraction.Add(new Fraction { LTSRid = element.LTSRid, East = element.East, North = element.North, Name = element.Name });
                }
            }
            return myfraction;
        }

        
    }

    

    public class Fraction
    {
        public string LTSRid { get; set; }
        public double North { get; set; }
        public double East { get; set; }
        public string Name { get; set; }
    }
}
