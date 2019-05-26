using Matcha.BackgroundService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ServicioXF.Services
{
    public class PeriodicWebCall : IPeriodicTask
    {
        int count;

        public PeriodicWebCall(int seconds)
        {
            PeriodicWebCallTest(seconds);
        }

        public void PeriodicWebCallTest(int seconds)
        {
            Interval = TimeSpan.FromSeconds(seconds);
        }

        public TimeSpan Interval { get; set; }

        public async Task<bool> StartJob()
        {
            if (!Application.Current.Properties.ContainsKey("Contador"))
            {
                count = 0;
                //Application.Current.Properties["Contador"] = count;
                //await Application.Current.SavePropertiesAsync();
            }
            else
            {
                count = Convert.ToInt32(Application.Current.Properties["Contador"]);
            }

            // YOUR CODE HERE
            // THIS CODE WILL BE EXECUTE EVERY INTERVAL
            //return true; //return false when you want to stop or trigger only once
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(
                "http://neoapi.neotecnologias.com/floranueva/api/SeleccionTablaEspecifica?NombreTabla=AbejasNativasVisitaGrupal");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            var result = response.Content.ReadAsStringAsync().Result;
            var resultado = JsonConvert.DeserializeObject<string>(result);
            var resulta = resultado;
            if (resulta == "-109")
            {

                return false;
            }
            if (resulta != "-102")
            {
                var listaAbejasNativasProductores = JsonConvert.DeserializeObject<List<AbejasNativasVisitaGrupal>>(resulta);

                if (listaAbejasNativasProductores != null)
                {
                    
                }
            }

            count++;
            Application.Current.Properties["Contador"] = count;
            await Application.Current.SavePropertiesAsync();

            return true;

        }

        public class AbejasNativasVisitaGrupal
        {

            #region Properties
            public int IdAbejasNativasVisitaGrupalLocal { get; set; }
            public int IdAbejasNativasVisitaGrupal { get; set; }
            public string Identificador { get; set; }

            public int? IdGrupo
            {
                get;
                set;
            }

            public string NombreGrupo
            {
                get;
                set;
            }

            public DateTime FechaDesde
            {
                get;
                set;

            }

            public string FechaDesdeString
            {
                get;
                set;

            }

            public DateTime FechaHasta
            {
                get;
                set;

            }

            public string FechaHastaString
            {
                get;
                set;
            }
            public string EquipoFloraNuevaPresente { get; set; }
            public string OtrosParticipantes { get; set; }
            public string TemaYObjetivoDeLaVisita { get; set; }
            public string DesarrolloDeLaVisitaAcciones { get; set; }
            public string MaterialEntregado { get; set; }
            public string ProblemasDetectados { get; set; }
            public string RecomendacionesProximaVisita { get; set; }
            public string Estado { get; set; }
            public string Usuario { get; set; }
            public string Dispositivo { get; set; }

            public DateTime FechaCreacion { get; set; }
            public string FechaCreacionUtc { get; set; }

            public DateTime FechaModificacion { get; set; }
            public string FechaModificacionUtc { get; set; }
            public string Transaccion { get; set; }
            #endregion
        }
    }
}
