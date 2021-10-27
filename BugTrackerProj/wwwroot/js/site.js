import { signalR } from "../lib/aspnet/signalr/dist/browser/signalr";

console.log("Hello From Site")
var connection = new signalR.HubConnectionBuilder("Application").Build();
connection.start().then(() => console.log("Connected")).catch((err) => console.log(err));

//connection.on("NewBugReceived", newBug=>{
//LoadBugData();

//});
