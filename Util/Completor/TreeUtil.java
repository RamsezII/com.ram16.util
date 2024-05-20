package tal_ymat;

public class TreeUtil 
{
    public static CharNode TreeMaker(String text)
    {
        CharNode 
            allRoot = new CharNode(),
            charNode = allRoot;        
        
        int length = text.length();
        
        for(int i = 0, deep = 0; i < length; i++, deep++)
        {
            Integer c = new Integer(text.charAt(i));
            
            switch((int)c)
            {
                case ' ':
                case ',':
                case '.':
                    charNode.leafs.add(i - deep);
                    deep = -1;
                    charNode = allRoot;
                    break;
                
                default:
                    if(!charNode.nodes.containsKey(c))
                        charNode.nodes.put(c, new CharNode());
                    
                    charNode = charNode.nodes.get(c);
                    charNode.occurences++;
                    
                    break;
            }
        }
        
        return allRoot;
    }
}