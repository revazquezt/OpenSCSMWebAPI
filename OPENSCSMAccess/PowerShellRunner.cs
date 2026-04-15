using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using Newtonsoft.Json.Linq;
using OPENSCSMAccessM.Config;
using OPENSCSMAccessM.PowerShell;
using OPENSCSMModel.Exchange;

namespace OPENSCSMAccessM
{
    public static class PowerShellRunner
    {
        private static readonly RunspacePool _runspacePool;
        private static readonly InitialSessionState initialState;

        static PowerShellRunner()
        {
            initialState = InitialSessionState.CreateDefault2();

            initialState.Variables.Add(new SessionStateVariableEntry(
        "SCSM_SERVER", WorkerSettings.ScsmServer, "SCSM Config"));

            initialState.Variables.Add(new SessionStateVariableEntry(
                "SCSM_SDK_PATH", WorkerSettings.ScsmSdkPath , "SCSM Config"));
                                                             

            string modulePaths = string.Join(";", new[]
            {
                @"C:\Program Files\WindowsPowerShell\Modules",
                @"C:\Windows\System32\WindowsPowerShell\v1.0\Modules",
                @"C:\Program Files\Microsoft System Center\Service Manager",
                Environment.GetEnvironmentVariable("PSModulePath", EnvironmentVariableTarget.Machine),
                Environment.GetEnvironmentVariable("PSModulePath", EnvironmentVariableTarget.User)
            }.Where(p => !string.IsNullOrEmpty(p)));


            initialState.Variables.Add(new SessionStateVariableEntry("env:PSModulePath", modulePaths, "Module paths"));

            _runspacePool = RunspaceFactory.CreateRunspacePool(initialState);
            _runspacePool.SetMinRunspaces(1);
            _runspacePool.SetMaxRunspaces(5);

            _runspacePool.Open();
        }
        
        public static string ExecuteEmbeddedScriptTools(EPackage request) { 
            string strScriptName = string.Empty;
            string moduleContent = string.Empty;
            string tempModulePath = string.Empty;

            JObject jo = JObject.Parse(request.Request);
            
            switch (request.ActionName)
            {
                case "NewServiceRequest":
                    strScriptName = "OPENSCSMAccessM.PowerShell.ServiceRequest.RegServiceRequest.ps1";
                    break;
                case "GetUserStatus":
                    strScriptName = "UserVerification.ps1";
                    break;
                case "Addfile":
                    strScriptName = "AddAttachmentSCSM.ps1";
                    break;
                default:
                    break;
            }

            string script = EmbeddedScriptReader.GetScript(strScriptName);

            using (Runspace runspace = RunspaceFactory.CreateRunspace(initialState))
            {
                runspace.Open();

                using (System.Management.Automation.PowerShell ps =
                       System.Management.Automation.PowerShell.Create())
                {
                    ps.Runspace = runspace;

                    switch (request.ActionName)
                    {
                        case "NewServiceRequest":
                            ps.AddScript($@"
                                            
                                            $title = '{jo.Value<string>("Title") ?? string.Empty}'
                                            $description = '{jo.Value<string>("Description") ?? string.Empty}'
                                            $urgency = '{jo.Value<string>("Urgency") ?? string.Empty}'
                                            $priority = '{jo.Value<string>("Priority") ?? string.Empty}'
                                            $area = '{jo.Value<string>("Area") ?? string.Empty}'
                                            $supportGroup = '{jo.Value<string>("SupportGroup") ?? string.Empty}'
                                            $affectedUser = '{jo.Value<string>("AffectedUser") ?? string.Empty}'
                                            $status = '{jo.Value<string>("Status") ?? string.Empty}'
                                        ");
                            break;
                        case "GetUserStatus": 
                            ps.AddScript($@"
                                            $PrincipalUserName = '{jo.Value<string>("PrincipalUserName") ?? string.Empty}'
                                            $DomainUserName = '{WorkerSettings.ControlerDomain}'
                                        ");
                            break;
                        case "Addfile":
                            ps.AddScript($@"
                                            $FilePath = '{jo.Value<string>("FilePath") ?? string.Empty}'
                                            $IRid = '{jo.Value<string>("IRid") ?? string.Empty}'   
                                            $Database = '{WorkerSettings.Database}'
                                            $scsmEnviroment = '{WorkerSettings.Enviroment}'
                                        ");
                            break;
                        default:
                            break;
                    }

                    ps.AddScript($@"
                                    $scsmEnviroment = '{WorkerSettings.Enviroment ?? string.Empty}'
                                ");

                    ps.AddScript(script);
                    Collection<PSObject> results = ps.Invoke();

                    
                    if (ps.Streams.Error.Count > 0)
                    {
                        var errors = string.Join(Environment.NewLine, ps.Streams.Error);
                        return $"⚠️ Error en PowerShell:\n{errors}";
                    }

                    ps.Commands.Clear();

                    return string.Join(Environment.NewLine, results);
                }
            }
        }

        public static string ExecuteEmbeddedScriptIncidents(EPackage request)
        {
            string strScriptName = string.Empty;
            string moduleContent = string.Empty;
            string tempModulePath = string.Empty;

            JObject jo = JObject.Parse(request.Request);

            switch (request.ActionName)
            {
                case "ConsultByUserAfected" or "ConsultByUserAssigned":
                    moduleContent = EmbeddedScriptReader.GetScript("OPENSCSMAccessM.PowerShell.Utils.psm1");
                    tempModulePath = Path.Combine(Path.GetTempPath(), "OPENSCSMAccessM_Utils.psm1");
                    File.WriteAllText(tempModulePath, moduleContent);
                    break;
                default:
                    break;
            }

            
            switch (request.ActionName)
            {
                case "ConsultByUserAfected":
                    strScriptName = "IncidentsByAffectedUser.ps1";
                    break;
                case "ConsultByUserAssigned":
                    strScriptName = "IncidentsByAssignedUser.ps1";
                    break;
                case "GetIncidentById":
                    strScriptName = "IncidetById.ps1";
                    break;
                case "NewIncident":
                    strScriptName = "RegIncidentV01.ps1";
                    break;
                case "ResolveIncident":
                    strScriptName = "ResolveIncident.ps1";
                    break;
                case "AddAnalisysComment2Incident":
                    strScriptName = "RegAnalisysCommentIncident.ps1";
                    break;
                default:
                    break;
            }

            string script = EmbeddedScriptReader.GetScript(strScriptName);

            using (Runspace runspace = RunspaceFactory.CreateRunspace(initialState))
            {
                runspace.Open();

                using (System.Management.Automation.PowerShell ps =
                       System.Management.Automation.PowerShell.Create())
                {
                    ps.Runspace = runspace;

                    switch (request.ActionName)
                    {
                        case "ConsultByUserAfected" or "ConsultByUserAssigned":

                            ps.AddScript($@"
                                            $user           = '{jo.Value<string>("PrincipalUserName") ?? string.Empty}'
                                            $modulePath     = '{tempModulePath.Replace(@"\", @"\\")}'
                                        ");


                            break;
                        case "GetIncidentById":
                            ps.AddScript($@"
                                            $incidentID = '{jo.Value<string>("IdIncident") ?? string.Empty}'
                                        ");
                            break;
                        case "NewIncident":
                            ps.AddScript($@"
                                            
                                            $title = '{jo.Value<string>("Title") ?? string.Empty}'
                                            $description = '{jo.Value<string>("Description") ?? string.Empty}'
                                            $urgency = '{jo.Value<string>("Urgency") ?? string.Empty}'
                                            $impact = '{jo.Value<string>("Impact") ?? string.Empty}'
                                            $source = '{jo.Value<string>("Source") ?? string.Empty}'
                                            $status = '{jo.Value<string>("Status") ?? string.Empty}'
                                            $classification = '{jo.Value<string>("Classification") ?? string.Empty}'
                                            $tierQueue = '{jo.Value<string>("TierQueue") ?? string.Empty}'
                                            $affectedUser = '{jo.Value<string>("affectedUser") ?? string.Empty}'
                                        ");
                            break;
                        case "ResolveIncident":
                            ps.AddScript($@"
                                            
                                            $incidentID = '{jo.Value<string>("incidentID") ?? string.Empty}'
                                            $ResolvedUserPrincipalName = '{jo.Value<string>("ResolvedUserPrincipalName") ?? string.Empty}'
                                            $IncidentResolutionDescription = '{jo.Value<string>("IncidentResolutionDescription") ?? string.Empty}'
                                            $IncidentresolutionCategory = '{jo.Value<string>("IncidentresolutionCategory") ?? string.Empty}'
                                        ");
                            break;
                        case "AddAnalisysComment2Incident":
                            ps.AddScript($@"
                                            
                                            $incidentID = '{jo.Value<string>("incidentID") ?? string.Empty}'
                                            $incidentCommentText = '{jo.Value<string>("IncidentResolutionDescription") ?? string.Empty}'
                                            $incidentCommentAddedBy = '{jo.Value<string>("ResolvedUserPrincipalName") ?? string.Empty}'
                                            $incidentIsPrivateComment = '{jo.Value<string>("IncidentisPrivateComment") ?? string.Empty}'
                                        ");
                            break;
                        default:
                            break;
                    }

                    ps.AddScript($@"
                                    $scsmEnviroment = '{WorkerSettings.Enviroment ?? string.Empty}'
                                ");
                    ps.AddScript(script);
                    Collection<PSObject> results = ps.Invoke();

                    if (!string.IsNullOrEmpty(tempModulePath) && File.Exists(tempModulePath))
                        File.Delete(tempModulePath);

                    if (ps.Streams.Error.Count > 0)
                    {
                        var errors = string.Join(Environment.NewLine, ps.Streams.Error);
                        return $"⚠️ Error en PowerShell:\n{errors}";
                    }

                    ps.Commands.Clear();

                    return string.Join(Environment.NewLine, results);
                }
            }
        }

        public static string ExecuteEmbeddedScriptCat(string associatedScriptName)
        {
            string script = EmbeddedScriptReader.GetScript("CatalogsItems.ps1");


            using (System.Management.Automation.PowerShell ps = System.Management.Automation.PowerShell.Create())
            {
                ps.RunspacePool = _runspacePool;
                ps.AddScript(script);

                ps.AddParameter("catalogName", associatedScriptName);
                ps.AddParameter("scsmEnviroment", WorkerSettings.Enviroment);

                Collection<PSObject> results = ps.Invoke();

                if (ps.Streams.Error.Count > 0)
                {
                    var errors = string.Join(Environment.NewLine, ps.Streams.Error);
                    return $"⚠️ Error en PowerShell:\n{errors}";
                }

                return string.Join(Environment.NewLine, results);
            }
        }

        public static string ExecuteEmbeddedScriptSrCat(string associatedScriptName)
        {
            string script = EmbeddedScriptReader.GetScript(
                "OPENSCSMAccessM.PowerShell.ServiceRequest.CatalogsSRItems.ps1");

            using (Runspace runspace = RunspaceFactory.CreateRunspace(initialState))
            {
                runspace.Open();

                using (System.Management.Automation.PowerShell ps =
                       System.Management.Automation.PowerShell.Create())
                {
                    ps.Runspace = runspace;

                    ps.AddScript($@"
                $catalogName    = '{associatedScriptName.Replace("$", "")}'
                $scsmEnviroment = '{WorkerSettings.Enviroment}'
            ");

                    ps.AddScript(script);
                    Collection<PSObject> results = ps.Invoke();

                    if (ps.Streams.Error.Count > 0)
                    {
                        var errors = string.Join(Environment.NewLine, ps.Streams.Error);
                        return $"⚠️ Error:\n{errors}";
                    }

                    return string.Join(Environment.NewLine, results);
                }
            }
        }

    }
}
