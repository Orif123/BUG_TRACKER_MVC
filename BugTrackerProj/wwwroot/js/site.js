"use strict";

class BugCommentDetailsViewModel {
    constructor(username, text) {
        this.username = username;
        this.text = text;
    }
}
console.log("Hello From Site")
var connection = new signalR.HubConnectionBuilder().withUrl("/Application").build();

connection.on("NewBugReceived", addMessageToChat);
connection.start()
    .catch(error => {
        console.error(error.message);
    });
function sendMessageToHub(details) {
    connection.invoke('sendMessage', details);
}
const textInput = document.getElementById('messageText');
const chat = document.getElementById('chat');
const messagesQueue = [];



function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = textInput.value;

    if (text.trim() === "") return;

    let details = new BugCommentDetailsViewModel(username, text);
    sendMessageToHub(details);
}
function addMessageToChat(details) {
    let isCurrentUserMessage = details.username === userName;

    let container = document.createElement('ul');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('li');
    sender.className = "sender";
    sender.innerHTML = details.username;
    let text = document.createElement('li');
    text.innerHTML = details.text;

    container.appendChild(sender);
    container.appendChild(text);
    chat.appendChild(container);
}


    


