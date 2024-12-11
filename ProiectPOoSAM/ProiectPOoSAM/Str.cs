namespace ProiectPOoSAM;

public static class Str
{
    public static string Capitalize(string a)
    {
        return a[0].ToString().ToUpper() + a.Substring(1);
    }
}

// concateneaza prima poz din sirul de caractere(care e facuta ToUpper) cu restul subsirului