"use strict";

class BugCommentDetailsViewModel {
    constructor(text) {
        this.text = text;
    }
}
console.log("Hello from Site");
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
const groupName = document.getElementById('groupName');
const chat = document.getElementById('chat');
const messagesQueue = [];



function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = textInput.value;

    if (text.trim() === "") return;

    let details = new BugCommentDetailsViewModel(text);
    sendMessageToHub(details);
}
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

    


