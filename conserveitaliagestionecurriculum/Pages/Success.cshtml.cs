using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using conserveitaliagestionecurriculum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace conserveitaliagestionecurriculum.Pages
{
    public class SuccessModel : PageModel { 

        public string valDisp { get; set; }

    
        public Curriculum cur = new Curriculum();
      
        public void OnGet()
        {
        }

        public void OnGetCurriculumData(Curriculum data)
        {
            
            cur.Nome = data.Nome;
            cur.Cognome = data.Cognome;
            cur.Indirizzo = data.Indirizzo;
            cur.Cap = data.Cap;
            cur.Citta = data.Citta;
            cur.Sesso = data.Sesso;
            cur.CurriculumProvincia = data.CurriculumProvincia;
            cur.TelefonoAbitazione = data.TelefonoAbitazione;
            cur.Email = data.Email;
            cur.CurriculumStato = data.CurriculumStato;
            cur.CurriculumStatoCivile = data.CurriculumStatoCivile;
            cur.CurriculumPatente = data.CurriculumPatente;
            cur.TelefonoCellulare = data.TelefonoCellulare;
            cur.NumeroUfficio = data.NumeroUfficio;
            cur.DataNascita = data.DataNascita;
            cur.CittaNascita = data.CittaNascita;
            cur.Nazionalita = data.Nazionalita;
            cur.CodiceFiscale = data.CodiceFiscale;
            cur.TitoloStudioUno = data.TitoloStudioUno;
            cur.SedeUno = data.SedeUno;
            cur.AnnoUno = data.AnnoUno;
            cur.TipologiaUno = data.TipologiaUno;
            cur.VotoUno = data.VotoUno;
            cur.RangeVotoUno = data.RangeVotoUno;
            cur.LodeUno = data.LodeUno;
            cur.CorsoUno = data.CorsoUno;
            cur.AnnoCorsoUno = data.AnnoCorsoUno;
            cur.IstitutoUno = data.IstitutoUno;
            cur.DurataUno = data.DurataUno;
            cur.Occupato = data.Occupato;
            cur.Settore = data.Settore;
            cur.DatoreLavoro = data.DatoreLavoro;
            cur.OccupatoDal = data.OccupatoDal;
            cur.MansioniSvolte = data.MansioniSvolte;
            cur.TipoAssunzione = data.TipoAssunzione;
            cur.Scadenza = data.Scadenza;
            cur.Esperienze = data.Esperienze;
            cur.LivelloInquadramento = data.LivelloInquadramento;
            cur.Contratto = data.Contratto;
            cur.RAL = data.RAL;
            cur.Madrelingua = data.Madrelingua;
            cur.LinguaConosciutaUno = data.LinguaConosciutaUno;
            cur.ComprensioneAscoltoUno = data.ComprensioneAscoltoUno;
            cur.ComprensioneLetturaUno = data.ComprensioneLetturaUno;
            cur.ParlatoInterazioneUno = data.ParlatoInterazioneUno;
            cur.ParlatoOraleUno = data.ParlatoOraleUno;
            cur.ScrittoUno = data.ScrittoUno;
            cur.LivelloExcel = data.LivelloExcel;
            cur.Gestionale = data.Gestionale;
            cur.LivelloGestionale = data.LivelloGestionale;
            cur.Altro = data.Altro;
            cur.LivelloAltro = data.LivelloAltro;
            cur.PatenteComputer = data.PatenteComputer;
            cur.EsteroDescrizioneUno = data.EsteroDescrizioneUno;
            cur.EsteroDurataUno = data.EsteroDurataUno;
            cur.EsteroAnnoUno = data.EsteroAnnoUno;
            cur.AreaAziendalePrioritaria = data.AreaAziendalePrioritaria;
            cur.AreaAziendaleSecondaria = data.AreaAziendaleSecondaria;
            valDisp = data.DispTrasferimenti ? "Trasferimenti_" : "" + (data.DispItalia ? "TrasferimentiItalia_" : "")+
                (data.DispEstero ? "TrasferimentiEstero_" : "")+ (data.DispTurni ? "Turni_" : "");
            cur.AltreInfo = data.AltreInfo;



        }
    }
}
