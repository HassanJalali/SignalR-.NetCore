var connectionBasicChat = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/basicchathub").build();

document.getElementById("sendMessage").disabled = true;


connectionBasicChat.on("MessageRecieved", function (sender, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${sender} - ${message}`;
    toastr.success(`You have recieved a message from ${sender}`);
});

document.getElementById("sendMessage").addEventListener("click", function (event) {
    var sender = document.getElementById("senderEmail").value;
    var receiver = document.getElementById("receiverEmail").value;
    var message = document.getElementById("chatMessage").value;
    if (receiver.length > 0) {
        connectionBasicChat.send("SendMessageToPrivate", sender, receiver, message).then(function () {
            document.getElementById("chatMessage").value = "";
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        connectionBasicChat.send("SendMessageToAll", sender, message).then(function () {
            document.getElementById("chatMessage").value = "";
            
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }

    event.preventDefault();
});



function fulfilled() {
    console.log("Connection to User Hub Successful");
    connectionBasicChat.send("");
    document.getElementById("sendMessage").disabled = false;
}

function rejected() {
}

connectionBasicChat.start().then(fulfilled, rejected);