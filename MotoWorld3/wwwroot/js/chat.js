var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;
document.getElementById("messageInput").disabled = true;

let currentUser;
let currentUserEmail;
let roomId = '';
let advertisingID;

connection.start()
    .then(() => {
        if (roomId !== '') {
            connection.invoke("LeaveRoom", roomId);
        }

        currentUser = document.getElementById('sender').value;
        currentUserEmail = document.getElementById('senderEmail').value;
        roomId = document.getElementById('room').value;
        advertisingID = document.getElementById('advertising').value;

        if (currentUser !== '' && currentUserEmail !== '' && roomId !== '' && advertisingID !== '') {
            connection.invoke("JoinRoom", roomId);
            document.getElementById("sendButton").disabled = false;
            document.getElementById("messageInput").disabled = false;
        } else {
            console.error("Váratlan hiba lépett fel a szoba csatlakozása közben...");
        }
    })
    .catch(err => console.error("Kapcsolat hiba: ", err));

// Recieving messages
connection.on("ReceiveMessage", function (user, userEmail, message) {
    const isSender = user === currentUser;
    const messageDiv = document.createElement("div");
    messageDiv.className = `message ${isSender ? "sent" : "received"}`;

    const senderDiv = document.createElement("div");
    if (isSender) {
        senderDiv.className = "sender text-light";
    } else {
        senderDiv.className = "sender text-warning";
    }
    senderDiv.textContent = userEmail;

    const bubbleDiv = document.createElement("div");
    bubbleDiv.className = "bubble";
    bubbleDiv.textContent = message;

    messageDiv.appendChild(senderDiv);
    messageDiv.appendChild(bubbleDiv);

    document.getElementById("chat-container").appendChild(messageDiv);
});

// Sending messages
document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", parseInt(advertisingID), roomId, currentUser, currentUserEmail, message).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("messageInput").value = '';
    event.preventDefault();
});
