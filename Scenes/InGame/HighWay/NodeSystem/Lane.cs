using System.Collections.Generic;
using System.Numerics;

public class Lane <node> {

    // properties
    public string name;
    public List<node> nodes = new List<node>();
    public node lastLoadedNode;
    // surrounding Lanes
    public Lane<node> leftLane;
    public Lane<node> rightLane;

    public Lane(string name) {
        this.name = name;
    }

}