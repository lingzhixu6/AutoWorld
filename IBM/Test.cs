// using System;
// using System.Collections.Generic;
// using System.Text;
// using IBM.Cloud.SDK;
// using IBM.Cloud.SDK.Authentication;
// using IBM.Cloud.SDK.Connection;
// using IBM.Watson;
// using IBM.Watson.Discovery.V1.Model;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Linq;
// using UnityEngine.Networking;
//
//
// namespace IBM
// {
//     public class Test
//     {
//         public bool Query(Callback<QueryResponse> callback, string environmentId, string collectionId, string filter = null, string query = null, string naturalLanguageQuery = null, bool? passages = null, string aggregation = null, long? count = null, string _return = null, long? offset = null, string sort = null, bool? highlight = null, string passagesFields = null, long? passagesCount = null, long? passagesCharacters = null, bool? deduplicate = null, string deduplicateField = null, bool? similar = null, string similarDocumentIds = null, string similarFields = null, string bias = null, bool? spellingSuggestions = null, bool? xWatsonLoggingOptOut = null)
//         {
//             if (callback == null)
//                 throw new ArgumentNullException("`callback` is required for `Query`");
//             if (string.IsNullOrEmpty(environmentId))
//                 throw new ArgumentNullException("`environmentId` is required for `Query`");
//             if (string.IsNullOrEmpty(collectionId))
//                 throw new ArgumentNullException("`collectionId` is required for `Query`");
//
//             RequestObject<QueryResponse> req = new RequestObject<QueryResponse>
//             {
//                 Callback = callback,
//                 HttpMethod = UnityWebRequest.kHttpVerbPOST,
//                 DisableSslVerification = DisableSslVerification
//             };
//
//             foreach (KeyValuePair<string, string> kvp in customRequestHeaders)
//             {
//                 req.Headers.Add(kvp.Key, kvp.Value);
//             }
//
//             ClearCustomRequestHeaders();
//
//             foreach (KeyValuePair<string, string> kvp in Common.GetSdkHeaders("discovery", "V1", "Query"))
//             {
//                 req.Headers.Add(kvp.Key, kvp.Value);
//             }
//
//             req.Parameters["version"] = VersionDate;
//             req.Headers["Content-Type"] = "application/json";
//             req.Headers["Accept"] = "application/json";
//
//             if (xWatsonLoggingOptOut != null)
//             {
//                 req.Headers["X-Watson-Logging-Opt-Out"] = (bool)xWatsonLoggingOptOut ? "true" : "false";
//             }
//
//             JObject bodyObject = new JObject();
//             if (!string.IsNullOrEmpty(filter))
//                 bodyObject["filter"] = filter;
//             if (!string.IsNullOrEmpty(query))
//                 bodyObject["query"] = query;
//             if (!string.IsNullOrEmpty(naturalLanguageQuery))
//                 bodyObject["natural_language_query"] = naturalLanguageQuery;
//             if (passages != null)
//                 bodyObject["passages"] = JToken.FromObject(passages);
//             if (!string.IsNullOrEmpty(aggregation))
//                 bodyObject["aggregation"] = aggregation;
//             if (count != null)
//                 bodyObject["count"] = JToken.FromObject(count);
//             if (!string.IsNullOrEmpty(_return))
//                 bodyObject["return"] = _return;
//             if (offset != null)
//                 bodyObject["offset"] = JToken.FromObject(offset);
//             if (!string.IsNullOrEmpty(sort))
//                 bodyObject["sort"] = sort;
//             if (highlight != null)
//                 bodyObject["highlight"] = JToken.FromObject(highlight);
//             if (!string.IsNullOrEmpty(passagesFields))
//                 bodyObject["passages.fields"] = passagesFields;
//             if (passagesCount != null)
//                 bodyObject["passages.count"] = JToken.FromObject(passagesCount);
//             if (passagesCharacters != null)
//                 bodyObject["passages.characters"] = JToken.FromObject(passagesCharacters);
//             if (deduplicate != null)
//                 bodyObject["deduplicate"] = JToken.FromObject(deduplicate);
//             if (!string.IsNullOrEmpty(deduplicateField))
//                 bodyObject["deduplicate.field"] = deduplicateField;
//             if (similar != null)
//                 bodyObject["similar"] = JToken.FromObject(similar);
//             if (!string.IsNullOrEmpty(similarDocumentIds))
//                 bodyObject["similar.document_ids"] = similarDocumentIds;
//             if (!string.IsNullOrEmpty(similarFields))
//                 bodyObject["similar.fields"] = similarFields;
//             if (!string.IsNullOrEmpty(bias))
//                 bodyObject["bias"] = bias;
//             if (spellingSuggestions != null)
//                 bodyObject["spelling_suggestions"] = JToken.FromObject(spellingSuggestions);
//             req.Send = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bodyObject));
//
//             req.OnResponse = OnQueryResponse;
//
//             Connector.URL = GetServiceUrl() + string.Format("/v1/environments/{0}/collections/{1}/query", environmentId, collectionId);
//             Authenticator.Authenticate(Connector);
//
//             return Connector.Send(req);
//         }
//
//         private void OnQueryResponse(RESTConnector.Request req, RESTConnector.Response resp)
//         {
//             DetailedResponse<QueryResponse> response = new DetailedResponse<QueryResponse>();
//             foreach (KeyValuePair<string, string> kvp in resp.Headers)
//             {
//                 response.Headers.Add(kvp.Key, kvp.Value);
//             }
//             response.StatusCode = resp.HttpResponseCode;
//
//             try
//             {
//                 string json = Encoding.UTF8.GetString(resp.Data);
//                 response.Result = JsonConvert.DeserializeObject<QueryResponse>(json);
//                 response.Response = json;
//             }
//             catch (Exception e)
//             {
//                 Log.Error("DiscoveryService.OnQueryResponse()", "Exception: {0}", e.ToString());
//                 resp.Success = false;
//             }
//
//             if (((RequestObject<QueryResponse>)req).Callback != null)
//                 ((RequestObject<QueryResponse>)req).Callback(response, resp.Error);
//         }
// }
