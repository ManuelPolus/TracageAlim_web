using System;
using System.Collections.Generic;
using System.Text;
using Tracage.Models;

namespace TracageAlimentaireXamarin.BL.Components
{
    public class ProductChecker
    {

        public static bool produitFini(Produit p)
        {
            //TODO décider de la liste des états pouvant être pris par un produit. + ma nière de les enregistrer
            return p.Etat == "produitFini" ? true : false;
        }

    }
}
