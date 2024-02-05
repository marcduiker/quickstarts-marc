using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Dapr.Client;

string DAPR_STORE_NAME = "statestore";
var client = new DaprClientBuilder().Build();
for (int i = 1; i <= 100; i++) {
    var orderId = i;
    var order = new Order(orderId);

    // Save state into the state store
    await client.SaveStateAsync(DAPR_STORE_NAME, orderId.ToString(), order.ToString());
    Console.WriteLine("Saving Order: " + order);

    // Get state from the state store
    var result = await client.GetStateAsync<string>(DAPR_STORE_NAME, orderId.ToString());
    Console.WriteLine("Getting Order: " + result);
    
    // Delete state from the state store
    // await client.DeleteStateAsync(DAPR_STORE_NAME, orderId.ToString());
    // Console.WriteLine("Deleting Order: " + order);
    
    await Task.Delay(TimeSpan.FromSeconds(3));
}

public record Order([property: JsonPropertyName("orderId")] int orderId);
