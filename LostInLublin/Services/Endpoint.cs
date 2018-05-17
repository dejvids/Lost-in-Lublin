using System.Drawing;
using GeoCoordinatePortable;

namespace LostInLublin.Models
{
    public class Endpoint
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public GeoCoordinate Coordinates { get; set; }
    }
}