
function addFriend(userId) {  
    connection.invoke("FriendReqSend", userId);
}


connection.on("ReceiveFriendReqRes", (result) => {
    if (result == "Ok") {
        document.getElementById("friendStatus").innerText = "Arkadaşlık İsteği Göderildi";
    }
    else if (result == "AlreadySend") {
        document.getElementById("friendStatus").innerText = "Arkadaş Ekle";
    }
})