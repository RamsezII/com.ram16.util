using UnityEngine;

partial class Util
{
    public static bool TryGetIK(in float dist_ab, in float dist_bc, in float dist_at, out float a1, out float a2)
    {
        a1 = a2 = 0;

        // Vérification de faisabilité
        // La cible est hors de portée si elle est trop loin (dist_at > dist_ab + dist_bc)
        // ou si elle est trop proche (dist_at < |dist_ab - dist_bc|, impossible de former un triangle valide)
        if (dist_at > dist_ab + dist_bc || dist_at < Mathf.Abs(dist_ab - dist_bc))
            return false; // Retourne des angles nuls si la cible est hors de portée

        // Calcul de a2 (l'angle au niveau du coude)
        // Utilisation de la loi des cosinus :
        // cos(a2) = (dist_at^2 - dist_ab^2 - dist_bc^2) / (2 * dist_ab * dist_bc)
        // Cela donne l'angle entre les deux segments dist_ab et dist_bc
        float cosTheta2 = (dist_at * dist_at - dist_ab * dist_ab - dist_bc * dist_bc) / (2 * dist_ab * dist_bc);
        a2 = Mathf.Acos(cosTheta2) * Mathf.Rad2Deg; // Conversion en degrés

        // Calcul de a1 (l'angle à la base, ou racine du bras)
        // Utilisation de la loi des cosinus encore :
        // cos(alpha) = (dist_at^2 + dist_ab^2 - dist_bc^2) / (2 * dist_at * dist_ab)
        // alpha est l'angle entre le premier segment et la ligne reliant l'origine à la cible
        float cosAlpha = (dist_at * dist_at + dist_ab * dist_ab - dist_bc * dist_bc) / (2 * dist_at * dist_ab);
        float alpha = Mathf.Acos(cosAlpha) * Mathf.Rad2Deg; // Conversion en degrés

        // L'angle final a1 est donné par -alpha car on se déplace dans le sens inverse
        a1 = -alpha;

        // Retourne les angles en degrés
        return true;
    }
}