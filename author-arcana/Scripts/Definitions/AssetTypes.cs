using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTHORARCANA.DEFINITIONS
{
    public struct ModelAsset
    {
        public string name;

        public ModelAsset(string name)
        {
            this.name = name;
        }

        public PackedScene GetModel()
        {
            return GD.Load<PackedScene>("res://Assets/Model/" + name + ".tscn");
        }
    }
}
