﻿using DesafioAutomacaoApiRest.Bases;
using DesafioAutomacaoApiRest.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DesafioAutomacaoApiRest.Pages;
using DesafioAutomacaoApiRest.Requests.Projects;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoApiRest.Tests.Projects
{
    class ProjectsTests : TestBase
    {
        [Test]
        public void Test_CadastrarUmProjetoComSucesso()
        {
            #region Parameters
            CreateAProjectRequest createAProjectRequest = new CreateAProjectRequest();
            Project project = new Project();
            Status status = new Status();
            ViewState viewState = new ViewState();

            string statusEsperado = "Created";//201

            int projectId = 1;
            string projectName = "Projeto test api 01";
            string projectDescription = "Mantis.  Report problems with the actual bug tracker here. (Do not remove this account)";
            bool projectEnabled = true;
            string projectFilePath = "/tmp/";

            int statusId = 10;
            string statusName = "development";
            string statusLabel = "development";

            int viewStateId = 10;
            string viewStateName = "public";
            string viewStateLabel = "public";

            #region json format
            /*
             {
               "id": 1,
               "name": "_new2",
               "status": {
                  "id": 10,
                  "name": "development",
                  "label": "development"
                },
                "description": "Mantis.  Report problems with the actual bug tracker here. (Do not remove this account)",
                "enabled": true,
                "file_path": "/tmp/",
                "view_state": {
                  "id": 10,
                  "name": "public",
                  "label": "public"
                }
            }
             */
            #endregion

            #endregion

            #region Acoes

            status.id = statusId;
            status.name = statusName;
            status.label = statusLabel;

            viewState.id = viewStateId;
            viewState.name = viewStateName;
            viewState.label = viewStateLabel;

            //montando body
            project.id = projectId;
            project.name = projectName;
            project.description = projectDescription;
            project.enabled = projectEnabled;
            project.file_path = projectFilePath;
            project.status = status;
            project.view_state = viewState;

            createAProjectRequest.SetJsonBody(project);

            IRestResponse<dynamic> response = createAProjectRequest.ExecuteRequest();
            #endregion

            #region Asserts

            #region json result
            /*
             {
                "project": {
                    "id": 2,
                    "name": "_new2",
                    "status": {
                        "id": 10,
                        "name": "development",
                        "label": "development"
                    },
                    "description": "Mantis.  Report problems with the actual bug tracker here. (Do not remove this account)",
                    "enabled": true,
                    "view_state": {
                        "id": 10,
                        "name": "public",
                        "label": "public"
                    },
                    "access_level": {
                        "id": 90,
                        "name": "administrator",
                        "label": "administrator"
                    },
                    "custom_fields": [],
                    "versions": [],
                    "categories": [
                        {
                            "id": 1,
                            "name": "General",
                            "project": {
                                "id": 0,
                                "name": null
                            }
                        }
                    ]
                }
            }
             */
            #endregion


            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusEsperado, response.StatusCode.ToString());
                Assert.AreEqual(projectName, response.Data.project.name.ToString());
                Assert.AreEqual(projectDescription, response.Data.project.description.ToString());
                //Etc
            });

            #endregion
        }

        [Test]
        public void Test_ObterUmProjetoComSucesso()
        {
            #region Parameters
            string statusEsperado = "OK";
            int idIssue = 1;
            string projectName = "Projeto 01";
            string description = "Descriptions";

            #endregion

            #region Acoes
            GetAProjectRequest getAProjectRequest = new GetAProjectRequest(idIssue);
            IRestResponse<dynamic> response = getAProjectRequest.ExecuteRequest();
            #endregion

            #region Asserts
            int idResposta = response.Data["projects"][0]["id"];
            string nomeResposta = response.Data["projects"][0]["name"];
            string descriptionResposta = response.Data["projects"][0]["description"];

            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idIssue, idResposta);
                Assert.AreEqual(projectName, nomeResposta);
                Assert.AreEqual(description, descriptionResposta);
            });

            #endregion
        }

        [Test]
        public void Test_ObterTodosOsProjetosComSucesso()
        {
            #region Parameters
            string statusEsperado = "OK";

            int idIssue = 1;
            string projectName = "Projeto 01";
            string description = "Descriptions";

            int idIssue2 = 3;
            string projectName2 = "Projeto test api 01";
            string description2 = "Mantis.  Report problems with the actual bug tracker here. (Do not remove this account)";

            #endregion

            #region Acoes
            GetAllProjectsRequest getAllProjectsRequest = new GetAllProjectsRequest();
            IRestResponse<dynamic> response = getAllProjectsRequest.ExecuteRequest();
            #endregion

            #region Asserts
            int idResposta = response.Data["projects"][0]["id"];
            string nomeResposta = response.Data["projects"][0]["name"];
            string descriptionResposta = response.Data["projects"][0]["description"];

            int idResposta2 = response.Data["projects"][1]["id"];
            string nomeResposta2 = response.Data["projects"][1]["name"];
            string descriptionResposta2 = response.Data["projects"][1]["description"];


            Assert.Multiple(() =>
            {
                Assert.AreEqual(statusEsperado, response.StatusCode.ToString());
                Assert.AreEqual(idIssue, idResposta);
                Assert.AreEqual(projectName, nomeResposta);
                Assert.AreEqual(description, descriptionResposta);

                Assert.AreEqual(idIssue2, idResposta2);
                Assert.AreEqual(projectName2, nomeResposta2);
                Assert.AreEqual(description2, descriptionResposta2);
            });

            #endregion
        }
    }
}
