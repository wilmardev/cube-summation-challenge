using Challenge.CodeSummationNS.WebApi.Controllers;
using Challenge.CubeSummationNS.Model.Models;
using Challenge.CubeSummationNS.Model.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Tests
{
    [TestClass]
    public class UnitTest
    {
        private CubeSummationController controller;
        private const string CORRECT_DATA = "2\n4 5\nUPDATE 2 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3\n2 4\nUPDATE 2 2 2 1\nQUERY 1 1 1 1 1 1\nQUERY 1 1 1 2 2 2\nQUERY 2 2 2 2 2 2";
        private const string TAMANO_MATRIZ_DATA = "2\n101 5\nUPDATE 2 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";
        private const string TAMANO_CASOS_PRUEBA_DATA = "51\n1 5\nUPDATE 2 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";
        private const string TAMANO_OPERACIONES_PRUEBA_DATA = "1\n1 1001\nUPDATE 2 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";
        private const string COORDENADAS_DATA = "1\n2 4\nUPDATE 5 2 2 4\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";
        private const string W_DATA = "1\n2 4\nUPDATE 2 2 2 100000000000\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";
        private const string UPDATE_DATA = "1\n2 4\nUPDATE 2 2 2 2 1\nQUERY 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";
        private const string QUERY_DATA = "1\n4 4\nUPDATE 2 2 2 1\nQUERY 1 1 1 1 3 3 3\nUPDATE 1 1 1 23\nQUERY 2 2 2 4 4 4\nQUERY 1 1 1 3 3 3";

        [TestInitialize]
        public void Setup()
        {
            controller = new CubeSummationController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
        }

        [TestMethod]
        public void Options_Test()
        {
            HttpResponseMessage responseMessage = controller.Options();
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
        }

        [TestMethod]
        public void CubeSummation_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(CORRECT_DATA);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            Assert.IsNotNull(respuestaGeneral);
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.IsTrue(respuestaGeneral.Estado);
        }

        [TestMethod]
        public void TamanoMatrizMayorCien_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(TAMANO_MATRIZ_DATA);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            ValidacionesGenerales(respuestaGeneral, responseMessage);
            Assert.AreEqual(CubeSummationResources.Error_Tamano_Matriz, respuestaGeneral.Mensaje);
        }

        [TestMethod]
        public void TamanoCasosPruebaMayorCincuenta_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(TAMANO_CASOS_PRUEBA_DATA);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            ValidacionesGenerales(respuestaGeneral, responseMessage);
            Assert.AreEqual(CubeSummationResources.Error_Cantidad_CasosPruebas, respuestaGeneral.Mensaje);
        }

        [TestMethod]
        public void TamanoOperacionesaMayorMil_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(TAMANO_OPERACIONES_PRUEBA_DATA);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            ValidacionesGenerales(respuestaGeneral, responseMessage);
            Assert.AreEqual(CubeSummationResources.Error_Tamano_Operaciones, respuestaGeneral.Mensaje);
        }

        [TestMethod]
        public void CoordenadaMayorTamanoMatriz_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(COORDENADAS_DATA);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            ValidacionesGenerales(respuestaGeneral, responseMessage);
            Assert.AreEqual(CubeSummationResources.Error_Coordenadas, respuestaGeneral.Mensaje);
        }

        [TestMethod]
        public void WMayor_Diez_n_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(W_DATA);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            ValidacionesGenerales(respuestaGeneral, responseMessage);
            Assert.AreEqual(CubeSummationResources.Error_Valores_Máximos_OperacionUpdate, respuestaGeneral.Mensaje);
        }

        [TestMethod]
        public void DatosVacios_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(string.Empty);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            ValidacionesGenerales(respuestaGeneral, responseMessage);
            Assert.AreEqual(CubeSummationResources.Error_Datos_Entrada_Vacio, respuestaGeneral.Mensaje);
        }

        [TestMethod]
        public void CantidadCoordenatasOperacionUpdate_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(UPDATE_DATA);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            ValidacionesGenerales(respuestaGeneral, responseMessage);
            Assert.AreEqual(CubeSummationResources.Error_Cantidad_Datos_OperacionUpdate, respuestaGeneral.Mensaje);
        }

        [TestMethod]
        public void CantidadCoordenatasOperacionQuery_Test()
        {
            HttpResponseMessage responseMessage = controller.ProcesarInformacion(QUERY_DATA);
            RespuestaGeneral respuestaGeneral = responseMessage.Content.ReadAsAsync<RespuestaGeneral>().Result;
            ValidacionesGenerales(respuestaGeneral, responseMessage);
            Assert.AreEqual(CubeSummationResources.Error_Cantidad_Datos_OperacionQuery, respuestaGeneral.Mensaje);
        }

        private void ValidacionesGenerales(RespuestaGeneral respuestaGeneral, HttpResponseMessage responseMessage)
        {
            Assert.IsNotNull(respuestaGeneral);
            Assert.AreEqual(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.IsFalse(respuestaGeneral.Estado);
            respuestaGeneral.Mensaje = respuestaGeneral.Mensaje.Replace("\n", string.Empty);
        }
    }
}