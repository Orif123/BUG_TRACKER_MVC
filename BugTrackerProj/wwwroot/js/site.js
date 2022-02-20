"use strict";

class BugCommentDetailsViewModel {
    constructor(text) {
        this.text = text;
    }
}
console.log("Hello from Site");
var connection = new signalR.HubConnectionBuilder().withUrl("/Application").build();
connection.on("NewBugReceived", addMessageToChat);
connection.start();

function addMessageToChat(user, massage) {

    let container = document.createElement('div');
    container.className += " media"
    let sender = document.createElement('p');
    sender.className += " media-body";
    sender.innerHTML = user;
    let text = document.createElement('p');
    text.className += " media-body"
    text.innerHTML = massage;

    chat.appendChild(container);
    container.appendChild(sender);
    container.appendChild(text);
}
 function joinGroup(event) {
    document.getElementById("JoinButton").addEventListener("click", function (event) {
        var groupElement = document.getElementById('groupj').innerText;
        connection.invoke('JoinGroup', groupElement).catch(function (err) {
            return console.error(err.toString())
        });
        event.preventDefault();
    })

}
function addMessageToGroup(event) {
    document.getElementById("sendButton").addEventListener("click", function (event) {
        var groupElement = document.getElementById('group').value;
        var user = document.getElementById('user').innerText;
        var message = document.getElementById('message').value;
        connection.invoke('SendMessageToGroup', groupElement, user, message).catch(function (err) {
            return console.log(err.toString());
        })
        event.preventDefault();
    });
}
