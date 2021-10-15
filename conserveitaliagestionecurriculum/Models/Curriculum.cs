using conserveitaliagestionecurriculum.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace conserveitaliagestionecurriculum.Models
{
    public class Curriculum
    {
        #region InfoTab
       
        public string Nome { get; set; }

        public string Cognome { get; set; }

        public string Indirizzo { get; set; }
       
        public string TelefonoAbitazione { get; set; }

        public string TelefonoCellulare { get; set; }

        public string NumeroUfficio { get; set; }
        
        public string DataNascita { get; set; }

        public string Cap { get; set; }

      
        public string Citta { get; set; }
       
        public string CittaNascita { get; set; }

       
        public string Nazionalita { get; set; }

       
        public string CodiceFiscale { get; set; }

        public string Email { get; set; }

     
      public IFormFile UploadedFile { get; set; }

        public string CurriculumProvincia { get; set; }

     
        public string CurriculumStato { get; set; }

       
        public string CurriculumStatoCivile { get; set; }

        public string CurriculumPatente { get; set; }

       
        public string Sesso { get; set; }

        public string AltroRecapito { get; set; }

        public string UrlSito { get; set; }
        #endregion
        # region Titoli Studio

        
        public string TitoloStudioUno { get; set; }

        
        public string SedeUno { get; set; }

        
        public string AnnoUno { get; set; }

        
        public string TipologiaUno { get; set; }

       
        public string VotoUno { get; set; }

        public string RangeVotoUno { get; set; }

        public string LodeUno { get; set; }

        public string TitoloStudioDue { get; set; }

        
        public string SedeDue { get; set; }

        
        public string AnnoDue { get; set; }

       
        public string TipologiaDue { get; set; }

       
        public string VotoDue { get; set; }

        public string RangeVotoDue { get; set; }

        public string LodeDue { get; set; }

        public string TitoloStudioTre { get; set; }


        public string SedeTre { get; set; }


        public string AnnoTre { get; set; }


        public string TipologiaTre { get; set; }


        public string VotoTre { get; set; }

        public string RangeVotoTre { get; set; }

        public string LodeTre { get; set; }
        #endregion
        # region Corsi di formazione
        public string CorsoUno { get; set; }
        public string AnnoCorsoUno { get; set; }

        public string IstitutoUno { get; set; }
        public string DurataUno { get; set; }

        public string CorsoDue { get; set; }
        public string AnnoCorsoDue { get; set; }

        public string IstitutoDue { get; set; }
        public string DurataDue { get; set; }

        public string CorsoTre { get; set; }
        public string AnnoCorsoTre { get; set; }

        public string IstitutoTre { get; set; }
        public string DurataTre { get; set; }
        #endregion
        #region Occupazione
        public string Occupato { get; set; }

        public string Settore { get; set; }

        public string DatoreLavoro { get; set; }

        public string OccupatoDal { get; set; }

        public string MansioniSvolte { get; set; }

        public string TipoAssunzione { get; set; }

        public string Scadenza { get; set; }

        public string Esperienze { get; set; }

        public string LivelloInquadramento { get; set; }

        public string Contratto { get; set; }

        public string RAL { get; set; }
        #endregion
        # region Lingue
        
        public string Madrelingua { get; set; }

        public string LinguaConosciutaUno { get; set; }

        public string ComprensioneAscoltoUno { get; set; }

        public string ComprensioneLetturaUno { get; set; }

        public string ParlatoInterazioneUno { get; set; }

        public string ParlatoOraleUno { get; set; }

        public string ScrittoUno { get; set; }

        public string LinguaConosciutaDue { get; set; }

        public string ComprensioneAscoltoDue { get; set; }

        public string ComprensioneLetturaDue { get; set; }

        public string ParlatoInterazioneDue { get; set; }

        public string ParlatoOraleDue { get; set; }

        public string ScrittoDue { get; set; }

        public string LinguaConosciutaTre { get; set; }

        public string ComprensioneAscoltoTre { get; set; }

        public string ComprensioneLetturaTre { get; set; }

        public string ParlatoInterazioneTre { get; set; }

        public string ParlatoOraleTre { get; set; }

        public string LinguaConosciutaQuattro { get; set; }

        public string ComprensioneAscoltoQuattro { get; set; }

        public string ComprensioneLetturaQuattro { get; set; }

        public string ParlatoInterazioneQuattro { get; set; }

        public string ParlatoOraleQuattro { get; set; }

        public string ScrittoQuattro { get; set; }

        public string LinguaConosciutaCinque { get; set; }

        public string ComprensioneAscoltoCinque { get; set; }

        public string ComprensioneLetturaCinque { get; set; }

        public string ParlatoInterazioneCinque { get; set; }

        public string ParlatoOraleCinque { get; set; }

        public string ScrittoCinque { get; set; }

        public string ScrittoTre { get; set; }
        #endregion
        #region ConoscenzeInformatiche



        public string LivelloExcel { get; set; }

        public string Gestionale { get; set; }

        public string LivelloGestionale { get; set; }

        public string Altro { get; set; }

        public string LivelloAltro { get; set; }

        public string PatenteComputer { get; set; }

        #endregion

        #region Esperienze Estero

        public string EsteroDescrizioneUno { get; set; }

        public string EsteroDurataUno { get; set; }

        public string EsteroAnnoUno { get; set; }

        public string EsteroDescrizioneDue { get; set; }

        public string EsteroDurataDue { get; set; }

        public string EsteroAnnoDue { get; set; }

        public string EsteroDescrizioneTre { get; set; }

        public string EsteroDurataTre { get; set; }

        public string EsteroAnnoTre { get; set; }
        #endregion
        #region AltreInfo

        public string AltreInfo { get; set; }
        
        public string AreaAziendalePrioritaria { get; set; }

        public string AreaAziendaleSecondaria { get; set; }

        public bool DispTrasferimenti { get; set; }

        public bool DispEstero { get; set; }

        public bool DispItalia { get; set; }

        public bool DispTurni { get; set; }

       
        public bool TerminiRisposta { get; set; }

        #endregion
        

    }
   
}






