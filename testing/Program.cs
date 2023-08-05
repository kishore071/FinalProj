using Newtonsoft.Json.Linq;
using System.Text;
using System.Xml.Serialization;
namespace MyApp
{
    class Program
    {
        static async Task Main()
        {
            HttpClient client = new HttpClient();
            //String final = "";
            //Console.WriteLine("Gst Linked Email");
            //String? email = Console.ReadLine();
            //Console.WriteLine("Gst UserName ");
            //String? username = Console.ReadLine();
            //Console.WriteLine("Password:");
            //String? pwd = Console.ReadLine();
            //Console.WriteLine("IP-Address Of this System:");
            //String? ipaddress = Console.ReadLine();
            //Console.WriteLine("Client ID:");
            //var client_id = Convert.ToString(Console.ReadLine());
            //Console.WriteLine("Client_Secret ID:");
            //var client_secret = Convert.ToString(Console.ReadLine());
            //Console.WriteLine("gstin Number:");
            //String? gst = Console.ReadLine();
            String email = $"{ur email id}";
            String final = "";
            String client_id = $"{ur client id}";
            String pwd = $"{ur pwd}";
            String client_secret = $"{ur clien secret id}";
            String ipaddress =$"{ur ip address}";
            String username = $"{ur credential username}";
            String gst = $"{ur gst number}";
            foreach (char i in email!)
            {
                if (i == '@')
                {
                    final = final + i;
                }
                else
                {
                    final = final + i;
                }
            }
            string url = $"https://api.mastergst.com/einvoice/authenticate?email={final}";
            client.DefaultRequestHeaders.Add("accept", "*/*");
            client.DefaultRequestHeaders.Add("username", $"{username}");
            client.DefaultRequestHeaders.Add("password", $"{pwd}");
            client.DefaultRequestHeaders.Add("ip_address", $"{ipaddress}");
            client.DefaultRequestHeaders.Add("client_id", $"{client_id}");
            client.DefaultRequestHeaders.Add("client_secret", $"{client_secret}");
            client.DefaultRequestHeaders.Add("gstin", $"{gst}");

            HttpResponseMessage response = await client.GetAsync(url);
            if (Convert.ToInt64(response.StatusCode) == 200)
            {
                String responseBody = await response.Content.ReadAsStringAsync();
                JObject result = JObject.Parse(responseBody);
                String AuthToken = Convert.ToString(result["data"]["AuthToken"]);
                Console.WriteLine("Generate Auth Token :" + AuthToken);
                client.Dispose();
                await AuthTokens(
                    auth: AuthToken,
                    ip: ipaddress,
                    client_id: client_id,
                    gst: gst,
                    sec: client_secret
                    , name: username);
                await Generate(
                    mail: final,
                    auth: AuthToken,
                    ip: ipaddress,
                    id: client_id,
                    gst: gst,
                    secret: client_secret
                    , name: username);
                await Delete(
                    mail: final,
                    auth: AuthToken,
                    ip: ipaddress,
                    id: client_id,
                    gst: gst,
                    secret: client_secret
                    , name: username);
            }
            else if (Convert.ToInt64(response.StatusCode) == 400)
            {
            }
            else
            {
                Console.WriteLine("Internal Server Errors,Try Again!");
            }
        }
        static async Task AuthTokens(String auth, String ip, String client_id, String name, String gst, String sec)
        {
            Console.WriteLine("Generate Invoice Reference Number");
            string url = "https://api.mastergst.com/einvoice/type/GENERATE/version/V1_03?email={final mail id above}";
            HttpClient httpClient = new HttpClient();
            string requester = "{\"Version\":\"1.1\",\"TranDtls\":{\"TaxSch\":\"GST\",\"SupTyp\":\"B2B\",\"RegRev\":\"N\",\"EcmGstin\":null,\"IgstOnIntra\":\"N\"},\"DocDtls\":{\"Typ\":\"INV\",\"No\":\"MAHI160\",\"Dt\":\"01/08/2023\"},\"SellerDtls\":{\"Gstin\":\"29AABCT1332L000\",\"LglNm\":\"ABC company pvt ltd\",\"TrdNm\":\"NIC Industries\",\"Addr1\":\"5th block, kuvempu layout\",\"Addr2\":\"kuvempu layout\",\"Loc\":\"GANDHINAGAR\",\"Pin\":560001,\"Stcd\":\"29\",\"Ph\":\"9000000000\",\"Em\":\"abc@gmail.com\"},\"BuyerDtls\":{\"Gstin\":\"29AWGPV7107B1Z1\",\"LglNm\":\"XYZ company pvt ltd\",\"TrdNm\":\"XYZ Industries\",\"Pos\":\"37\",\"Addr1\":\"7th block, kuvempu layout\",\"Addr2\":\"kuvempu layout\",\"Loc\":\"GANDHINAGAR\",\"Pin\":560004,\"Stcd\":\"29\",\"Ph\":\"9000000000\",\"Em\":\"abc@gmail.com\"},\"DispDtls\":{\"Nm\":\"ABC company pvt ltd\",\"Addr1\":\"7th block, kuvempu layout\",\"Addr2\":\"kuvempu layout\",\"Loc\":\"Banagalore\",\"Pin\":518360,\"Stcd\":\"37\"},\"ShipDtls\":{\"Gstin\":\"29AWGPV7107B1Z1\",\"LglNm\":\"CBE company pvt ltd\",\"TrdNm\":\"kuvempu layout\",\"Addr1\":\"7th block, kuvempu layout\",\"Addr2\":\"kuvempu layout\",\"Loc\":\"Banagalore\",\"Pin\":518360,\"Stcd\":\"37\"},\"ItemList\":[{\"SlNo\":\"1\",\"IsServc\":\"N\",\"PrdDesc\":\"Rice\",\"HsnCd\":\"1001\",\"Barcde\":\"123456\",\"BchDtls\":{\"Nm\":\"123456\",\"Expdt\":\"01/08/2020\",\"wrDt\":\"01/09/2020\"},\"Qty\":100.345,\"FreeQty\":10,\"Unit\":\"NOS\",\"UnitPrice\":99.545,\"TotAmt\":9988.84,\"Discount\":10,\"PreTaxVal\":1,\"AssAmt\":9978.84,\"GstRt\":12,\"SgstAmt\":0,\"IgstAmt\":1197.46,\"CgstAmt\":0,\"CesRt\":5,\"CesAmt\":498.94,\"CesNonAdvlAmt\":10,\"StateCesRt\":12,\"StateCesAmt\":1197.46,\"StateCesNonAdvlAmt\":5,\"OthChrg\":10,\"TotItemVal\":12897.7,\"OrdLineRef\":\"3256\",\"OrgCntry\":\"AG\",\"PrdSlNo\":\"12345\",\"AttribDtls\":[{\"Nm\":\"Rice\",\"Val\":\"10000\"}]}],\"ValDtls\":{\"AssVal\":9978.84,\"CgstVal\":0,\"SgstVal\":0,\"IgstVal\":1197.46,\"CesVal\":508.94,\"StCesVal\":1202.46,\"Discount\":10,\"OthChrg\":20,\"RndOffAmt\":0.3,\"TotInvVal\":12908,\"TotInvValFc\":12897.7},\"PayDtls\":{\"Nm\":\"ABCDE\",\"Accdet\":\"5697389713210\",\"Mode\":\"Cash\",\"Fininsbr\":\"SBIN11000\",\"Payterm\":\"100\",\"Payinstr\":\"Gift\",\"Crtrn\":\"test\",\"Dirdr\":\"test\",\"Crday\":100,\"Paidamt\":10000,\"Paymtdue\":5000},\"RefDtls\":{\"InvRm\":\"TEST\",\"DocPerdDtls\":{\"InvStDt\":\"01/08/2020\",\"InvEndDt\":\"01/09/2020\"},\"PrecDocDtls\":[{\"InvNo\":\"DOC/002\",\"InvDt\":\"01/08/2020\",\"OthRefNo\":\"123456\"}],\"ContrDtls\":[{\"RecAdvRefr\":\"DOC/002\",\"RecAdvDt\":\"01/08/2020\",\"Tendrefr\":\"Abc001\",\"Contrrefr\":\"Co123\",\"Extrefr\":\"Yo456\",\"Projrefr\":\"Doc-456\",\"Porefr\":\"Doc-789\",\"PoRefDt\":\"01/08/2020\"}]},\"AddlDocDtls\":[{\"Url\":\"https://einv-apisandbox.nic.in\",\"Docs\":\"Test Doc\",\"Info\":\"Document Test\"}],\"ExpDtls\":{\"ShipBNo\":\"A-248\",\"ShipBDt\":\"01/08/2020\",\"Port\":\"INABG1\",\"RefClm\":\"N\",\"ForCur\":\"AED\",\"CntCode\":\"AE\"},\"EwbDtls\":{\"Transid\":\"12AWGPV7107B1Z1\",\"Transname\":\"XYZ EXPORTS\",\"Distance\":100,\"Transdocno\":\"DOC01\",\"TransdocDt\":\"01/08/2020\",\"Vehno\":\"ka123456\",\"Vehtype\":\"R\",\"TransMode\":\"1\"}}";
   
            //Console.WriteLine(requester);
        httpClient.DefaultRequestHeaders.Add("accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("ip_address", $"{ip}");
            httpClient.DefaultRequestHeaders.Add("client_id", $"{client_id}");
            httpClient.DefaultRequestHeaders.Add("client_secret", $"{sec}");
            httpClient.DefaultRequestHeaders.Add("username", $"{name}");
            httpClient.DefaultRequestHeaders.Add("auth-token", $"{auth}");
            httpClient.DefaultRequestHeaders.Add("gstin", $"{gst}");
            HttpResponseMessage response = await httpClient.PostAsync(url, new StringContent(requester, Encoding.UTF8, "application/json"));
            string responseContent = await response.Content.ReadAsStringAsync();
                // Output the response
                Console.WriteLine(responseContent);

         
            // Dispose the HttpClient
            httpClient.Dispose();
        }
        static async Task Generate(String name, String ip, String gst, String auth, String id, String secret, String mail)
        {
            Console.WriteLine("Inside Generate: " + auth);
            Console.WriteLine("Generate E-Way Bill:");
            HttpClient client = new HttpClient();
            String request = @"{
  ""Irn"": ""b7dbcc735d0360c7af3b333247259ca2c944eb3b2e11f7430b7a7411ff3098c1"",
  ""Distance"": 100,
  ""TransMode"": ""1"",
  ""TransId"": ""12AWGPV7107B1Z1"",
  ""TransName"": ""transname"",
  ""TransDocDt"": ""01/08/2020"",
  ""TransDocNo"": ""TRAN/DOC/11"",
  ""VehNo"": ""KA12ER1234"",
  ""VehType"": ""R"",
  ""ExpShipDtls"": {
                ""Addr1"": ""7th block, kuvempu layout"",
    ""Addr2"": ""kuvempu layout"",
    ""Loc"": ""Banagalore"",
    ""Pin"": 562160,
    ""Stcd"": ""29""
  },
  ""DispDtls"": {
                ""Nm"": ""ABC company pvt ltd"",
    ""Addr1"": ""7th block, kuvempu layout"",
    ""Addr2"": ""kuvempu layout"",
    ""Loc"": ""Banagalore"",
    ""Pin"": 562160,
    ""Stcd"": ""29""
  }
        }";
            client.DefaultRequestHeaders.Add("accept", "*/*");
            client.DefaultRequestHeaders.Add("ip_address", $"{ip}");
            client.DefaultRequestHeaders.Add("client_id", $"{id}");
            client.DefaultRequestHeaders.Add("client_secret", $"{secret}");
            client.DefaultRequestHeaders.Add("username", $"{name}");
            client.DefaultRequestHeaders.Add("auth-token", $"{auth}");
            client.DefaultRequestHeaders.Add("gstin", $"{gst}");
            HttpResponseMessage response = await client.PostAsync("https://api.mastergst.com/einvoice/type/GENERATE_EWAYBILL/version/V1_03?email={final mail id above}", new StringContent(request, Encoding.UTF8, "application/json"));
            String responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        static async Task Delete(String name, String ip, String gst, String auth, String id, String secret, String mail)
        {
            Console.WriteLine("Cancel Invoice Refernce Number,");
            HttpClient del_client = new HttpClient();
            del_client.DefaultRequestHeaders.Add("accept", "*/*");
            del_client.DefaultRequestHeaders.Add("ip_address", $"{ip}");
            del_client.DefaultRequestHeaders.Add("client_id", $"{id}");
            del_client.DefaultRequestHeaders.Add("client_secret", $"{secret}");
            del_client.DefaultRequestHeaders.Add("username", $"{name}");
            del_client.DefaultRequestHeaders.Add("auth-token", $"{auth}");
            del_client.DefaultRequestHeaders.Add("gstin", $"{gst}");
            String irn = "b7dbcc735d0360c7af3b333247259ca2c944eb3b2e11f7430b7a7411ff3098c1";
            String cnlrsn = "1";
            String entertType = "Wrong entry";
            JObject requests = new JObject(
            new JProperty("Irn", $"{irn}"),
            new JProperty("CnlRsn", $"{cnlrsn}"),
            new JProperty("CnlRem", $"{entertType}"));
            String requestion = requests.ToString();
            HttpResponseMessage responseMessage = await del_client.PostAsync("https://api.mastergst.com/einvoice/type/CANCEL/version/V1_03?email={final mail id above}", new StringContent(requestion, Encoding.UTF8, "application/json"));
            if (responseMessage.IsSuccessStatusCode)
            {
                String respcontent = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine("Delete Res:");
                Console.WriteLine(respcontent);
            }
        }
    }
}
