package tal_ymat;

public class Main 
{
    public static void main(String[] args) 
    {
        String text = "Il existe un pays plus loin que le Soleil, nommé le Paragon. Le pays des rêveurs";
        CharNode tree = TreeUtil.TreeMaker(text.toLowerCase());
        
        tree.log("");
        // System.out.println(tree);
    }    
}
