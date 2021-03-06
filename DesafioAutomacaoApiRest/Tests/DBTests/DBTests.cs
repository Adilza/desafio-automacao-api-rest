﻿using DesafioAutomacaoApiRest.Bases;
using DesafioAutomacaoApiRest.DBSteps;
using DesafioAutomacaoApiRest.Helpers;
using DesafioAutomacaoApiRest.Objects;
using DesafioAutomacaoApiRest.Requests.Projects;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoApiRest.Tests.DBTests
{
    //[Parallelizable(ParallelScope.All)]
    public class DBTests : TestBase
    {
        #region Objects
        Project project = new Project();
        Status status = new Status();
        ViewState viewState = new ViewState();
        SubProject subProject = new SubProject();

        #endregion

        [SetUp]
        public void BeforeTest()
        {
            ProjetosDBSteps.InsertNewProject(10, "Projeto de teste DB");
        }


        [Test]
        public void Test_ObterUmProjetoAposResetECargaDoBDComSucesso()
        {
            #region Parameters
            string statusEsperado = "OK";
            int idIssue = 10;
            string projectName = "Projeto de teste DB";
            string description = "Projeto de teste DB description";

            #endregion

            #region Acoes
            

            GetAProjectRequest getAProjectRequest = new GetAProjectRequest(idIssue);
            RestSharp.IRestResponse<dynamic> response = getAProjectRequest.ExecuteRequest();
            #endregion

            #region Asserts
            string nomeResposta = response.Data["projects"][0]["name"];
            string descriptionResposta = response.Data["projects"][0]["description"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusEsperado, response.StatusCode.ToString());
                Assert.AreEqual(projectName, nomeResposta);
                Assert.AreEqual(description, descriptionResposta);
            });

            #endregion
        }

        
    }
}