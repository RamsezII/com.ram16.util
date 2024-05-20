package tal_ymat;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class CharNode 
{
    public Map<Integer, CharNode> nodes;
    public Set<Integer> leafs;
    public int occurences;
    
    public CharNode()
    {
        nodes = new HashMap<>();
        leafs = new HashSet<>();
    }
    
    @Override
    public String toString()
    {
        String log = "";
        
        for(int c : nodes.keySet())
        {
            log += "" + (char)c + ": " + nodes.get(c).occurences + "\n";
            log += nodes.get(c).toString();
        }
            
        return log + "\n";
    }
    
    public void log(String word)
    {
        if(nodes.size() > 0)
            for(int c : nodes.keySet())
                nodes.get(c).log(word + (char)c);
        else
        {
            String msg = word + ": ";
            
            for(int leaf : leafs)
                msg += leaf + ", ";
            
            System.out.println(msg);
        }
    }
}