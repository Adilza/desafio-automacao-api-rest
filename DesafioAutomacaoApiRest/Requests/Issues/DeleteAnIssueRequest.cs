﻿using DesafioAutomacaoApiRest.Bases;
using DesafioAutomacaoApiRest.Helpers;
using DesafioAutomacaoApiRest.Objects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioAutomacaoApiRest.Requests.Issues
{
    class DeleteAnIssueRequest : RequestBase
    {
        public DeleteAnIssueRequest(int idIssue)
        {
            requestService = "/api/rest/issues/{issue_id}";
            method = Method.DELETE;

            parameters.Add("issue_id", idIssue.ToString());
        }
    }
}
