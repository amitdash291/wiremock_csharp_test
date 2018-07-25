using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WiremockTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World to the Wiremock showdown!");
            var stub = FluentMockServer.Start(port: 12334);
            SetupTestMethod(stub);
            SetupAzureMethod(stub);
            Console.ReadLine();
            stub.Stop();
        }

        private static void SetupTestMethod(FluentMockServer stub)
        {
            stub
                .Given(
                    Request.Create()
                        .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                        .WithParam("param", "test")
                        .WithBody("key=value%3Ah")
                        .UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBodyAsJson(new { status = "Success" }));
        }

        private static void SetupAzureMethod(FluentMockServer stub)
        {
            var formUrlEncodedRequestBody = "client_id=0e618855-a494-4c3d-9a75-609f304d545c" +
                                            "&client_secret=sample_client_secret" +
                                            "&grant_type=authorization_code" +
                                            "&redirect_uri=https%3A%2F%2Flocalhost%3A44316" +
                                            "&code=sample_authorization_code";

            stub
                .Given(
                    Request.Create()
                        .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                        .WithParam("p", "b2c_1_sign_up_or_sign_in")
                        .WithBody(formUrlEncodedRequestBody)
                        .UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithBodyAsJson(new { status = "authorized" }));
        }
    }
}
