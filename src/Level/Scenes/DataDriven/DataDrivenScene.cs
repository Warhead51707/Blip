using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Blip.src.Level.Scenes.DataDriven;
public class DataDrivenScene
{
    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }

    [JsonPropertyName("grids")]
    public List<Grid> Grids { get; set; }

    public class Grid
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("layer")]
        public float Layer { get; set; }

        [JsonPropertyName("tiles")]
        public List<Tile> Tiles { get; set; }
    }

    public class Tile
    {
        public Tile(int x, int y, string identifier)
        {
            X = x;
            Y = y;
            Identifier = identifier;
        }

        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
    }
}
