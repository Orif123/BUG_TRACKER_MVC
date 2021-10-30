"use strict";

class BugCommentDetailsViewModel {
    constructor(text) {
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

    let details = new BugCommentDetailsViewModel (text);
    sendMessageToHub(details);
}
function addMessageToChat(user, massage) {
    let isCurrentUserMessage = user === userName;

    let container = document.createElement('ul');

    let sender = document.createElement('li');
    sender.className = "sender";
    sender.innerHTML = user;
    let text = document.createElement('li');
    text.innerHTML = massage;

    container.appendChild(sender);
    container.appendChild(text);
    chat.appendChild(container);
}


    


