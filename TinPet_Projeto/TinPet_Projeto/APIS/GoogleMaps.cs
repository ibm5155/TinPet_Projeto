using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinPet_Projeto.APIS.Secret;
using System.Net.Http;
using TinPet_Projeto.Models;
namespace TinPet_Projeto.APIS
{

    /// <summary>
    /// O código é relativamente simples, logo pode ser portatil e não depender de um parser json já que as informações que queriamos
    /// eram simples de se obter com o processamento de strings
    /// </summary>
    public class GoogleMaps
    {
        /**/
        string URLReq = "https://maps.googleapis.com/maps/api/geocode/json?address=";
        private string FiltroLocalizacao;

        public GoogleMaps()
        {
            FiltroLocalizacao = "location" + '"' + " : {";
        }


        /// <summary>
        /// Favor tratar string vazia antes da chamada.
        /// Em caso de falha, vai ser retornado um valor 0.00000000... para lat e long
        /// </summary>
        /// <param name="Endereco"></param>
        /// <returns></returns>
        public async Task<GeoCode> GetGeocodeAsync(string Endereco)
        {
            GeoCode GC = new GeoCode();
            GC.Latitude = 0;
            GC.Longitude = 0;
            string EnderecoFiltrado;
            #region Filtra Endereço
            EnderecoFiltrado = Endereco.Replace(' ', '+');
            for (int i=0;i<EnderecoFiltrado.Length;i++)
            {
                if(EnderecoFiltrado[i] == ',' || EnderecoFiltrado[i] == '.')
                {
                    EnderecoFiltrado = EnderecoFiltrado.Remove(i, 1);
                    i--;
                }
            }
            #endregion
            #region     ENVIA A MENSAGEM PARA O SERVIDOR DO GOOGLE
            HttpClient client = new HttpClient();
            HttpResponseMessage Requisicao;

            Requisicao = await client.GetAsync(new Uri(URLReq + EnderecoFiltrado + "&key=" + APIS.Secret.GoogleMapID.Tinpet_GmapID));
            #endregion
            #region FILTRA A LATITUDE E LONGITUDE
            if (!Requisicao.IsSuccessStatusCode)
            {
                throw new Exception("We could not send the message: " + Requisicao.StatusCode.ToString());
            }
            else
            {
                if (Requisicao.Content != null)
                {
                    //converte a requisição para string
                    var responseString = await Requisicao.Content.ReadAsStringAsync();
                    if (responseString.Length == 52) /*52 == "{\n   \"results\" : [],\n   \"status\" : \"ZERO_RESULTS\"\n}\n"*/
                    {
                        //Obteve retorno da API mas
                        //Endereço não encontrado
                    }
                    else
                    {
                        //procura onde está a localização
                        var Offset = responseString.IndexOf(FiltroLocalizacao) + FiltroLocalizacao.Length;
                        while (responseString[Offset] != '"') Offset++; //pula os espaços até chegar em "Lat"
                        while (responseString[Offset] != ':') Offset++; //pula a string até chegar em ':'
                        Offset += 2; //aponta para o inicio do número da latitude
                        int StartOffset = Offset;
                        while (responseString[Offset] != ',') Offset++; //acha o fim da latitude
                        Offset--;

                        var Lat = double.Parse(responseString.Substring(StartOffset, Offset - StartOffset).Replace('.',','));//faz uma copia do pedaço da string e depois converte para double
                        while (responseString[Offset] != '"') Offset++; //pula os espaços até chegar em "Lng"
                        while (responseString[Offset] != ':') Offset++; //pula a string até chegar em ':'
                        Offset += 2; //aponta para o inicio do número da Longitude
                        StartOffset = Offset;
                        while (responseString[Offset] != ' ' && responseString[Offset] != '\n') Offset++; //acha o fim da longitude
                        Offset--;
                        var Lon = double.Parse(responseString.Substring(StartOffset, Offset - StartOffset).Replace('.', ','));//faz uma copia do pedaço da string e depois converte para double
                        GC.Latitude = Lat;
                        GC.Longitude = Lon;
                    }
                }
            }


            #endregion
            return GC;
        }
    }
}
