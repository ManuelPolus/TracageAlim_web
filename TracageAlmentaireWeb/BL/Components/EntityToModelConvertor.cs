using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.Ajax.Utilities;
using Tracage.Models;
using TracageAlmentaireWeb.BL.Entities;
using TracageAlmentaireWeb.DAL;
using TracageAlmentaireWeb.Models;

namespace TracageAlmentaireWeb.BL.Components
{
    public class EntityToModelConvertor
    {
        public static FoodTrackerDao<object> Dao { get; set; }


        public static State ToState(StateEntity se)
        {
            State s = new State();
            s.Status = se.Status;
            return s;
        }

        public static Product ToProduct(EntiteProduit ep)
        {
            Product p = new Product();
            p.QRCode = ep.QRCode;
            p.Name = ep.Nom;
            p.Description = ep.Description;
            p.States = (List<State>) Dao.Get();
            
            return p;
        }

        public static Role ToRole(EntiteRole er)
        {
            Role r = new Role();
            r.Name = er.Nom;
            r.Description = er.DescriptionRole;
            return r;
        }

        public static Treatment ToTreatement(EntiteTraitement et)
        {
            Treatment t = new Treatment();
            t.Name = et.Nom;
            t.Desrciption = et.TreatementDescription;
            t.Position = et.Position;
            Dao = new FoodTrackerDao<object>("Etats");
            t.OutgoingState = (State) Dao.GetByIdentifier(et.IdEtatSortant);
            return t;
        }

        //GALEEEEERE
        



    }
}