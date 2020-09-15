using GeoCoordinatePortable;
using LostInLublin.Models;
using System.Collections.Generic;
using System.Drawing;

namespace LostInLublin.Services
{
    public static class FacebookSettings
    {
        public static string AccessToken = "";
        public static string SpottedLublin = "spottedlublinpl";
        public static string SpottedLublin2 = "spottedLBN";
        public static string SpottedMpk = "spottedmpklublin";
        public static string SpottetPollub = "443478532390799";
        public static string ZgubioneWLublinie = "200522343769029";
        public static List<Endpoint> Endpoints = new List<Endpoint>
        {
            new Endpoint {Id = "508091522569148", Name="spottedlublinpl"},
            new Endpoint {Id = "464650980255422", Name="spottedLBN"},
            new Endpoint {Id = "410968335650128", Name="spottedmpklublin"},
            new Endpoint {Id = "443478532390799", Name="spottedPollub", Coordinates = new GeoCoordinate(51.235045,22.547560)},
            new Endpoint {Id = "338728516237765", Name="spottedWseii", Coordinates = new GeoCoordinate(51.245618, 22.610600)},
            new Endpoint {Id = "200522343769029", Name="ZgubioneWLublinie"}
        };

        public static string[] KeyWords = new string[]
        {
            "znaleziono",
            "znalazłem",
            "znalazłam",
            "znaleziona",
            "znaleziony",
            "znalezione",
        };
    }
}