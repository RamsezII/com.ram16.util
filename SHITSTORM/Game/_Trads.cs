using _UTIL_;

partial class Util
{
    public static void SetTrads(this Traductable trad, in bool value) => trad.SetTrads(new() { english = value ? "Yes" : "No", french = value ? "Oui" : "Non", });
}