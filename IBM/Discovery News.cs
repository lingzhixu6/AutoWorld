using System.Collections;
using System.Collections.Generic;
using IBM.Cloud.SDK;
using IBM.Cloud.SDK.Authentication;
using IBM.Cloud.SDK.Authentication.Iam;
using IBM.Watson.Assistant.V1.Model;
using IBM.Watson.Assistant.V2;
using UnityEngine;

public class DiscoveryNews
{
    Authenticator authenticator;
    AssistantService assistant;
    string versionDate = "2020-02-05";

    IEnumerator TokenExample()
    {
        //  Create authenticator using the IAM token options
        authenticator = new IamAuthenticator(apikey: "hF_mLVrdrTsSePRgAq4lfnFQUjeOhCdj1Wn3G-S2yWxA");
        while (!authenticator.CanAuthenticate())
            yield return null;

        assistant = new AssistantService(versionDate, authenticator);
        assistant.SetServiceUrl("https://api.eu-gb.discovery.watson.cloud.ibm.com/instances/0d3d5a9f-cde8-4f3d-89ea-4f1ba026d930");
        assistant.ListWorkspaces(callback: OnListWorkspaces);
    }
    
    private void OnListWorkspaces(DetailedResponse<WorkspaceCollection> response, IBMError error)
    {
        Log.Debug("OnListWorkspaces()", "Response: {0}", response.Response);
    }
}
